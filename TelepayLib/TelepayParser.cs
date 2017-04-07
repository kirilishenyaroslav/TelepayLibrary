using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TelepayLib.Base;
using TelepayLib.Betfor;

namespace TelepayLib
{
    public class TelepayParser
    {
        public static void ParseObject(string data, object obj)
        {
            var t = obj.GetType();

            var props = t.GetProperties().Where(prop => prop.IsDefined(typeof (TelepayFieldAttribute), true))
                         .Select(p =>
                             {
                                 var attr =
                                     p.GetCustomAttributes(typeof (TelepayFieldAttribute), true).FirstOrDefault() as
                                     TelepayFieldAttribute;
                                 return new {prop = p, attr = attr};
                             }).ToList();

            data = data.Replace("\r\n", "").Replace("\n\r", "");
            foreach (var p in props)
            {
                var dataStr = data;
                if ((typeof (BETFOR00)).IsAssignableFrom(p.prop.PropertyType))
                    dataStr = data.Substring(0, BatchBase.RecordLength);
                if ((typeof (BETFOR99)).IsAssignableFrom(p.prop.PropertyType))
                    dataStr = data.Substring(data.Length - BatchBase.RecordLength, BatchBase.RecordLength);
                if ((typeof (List<PaymentOrder>).IsAssignableFrom(p.prop.PropertyType)))
                    dataStr = data.Substring(BatchBase.RecordLength, data.Length - (BatchBase.RecordLength*2));

                SetValue(dataStr, p.prop, p.attr, obj);
            }
        }


        public static void SetValue(string data, PropertyInfo prop, TelepayFieldAttribute attr, object obj)
        {
            object value = null;
            switch (attr.Type)
            {
                case FieldType.Object:
                    {
                        var t = prop.PropertyType;
                        if ((typeof (List<PaymentOrder>).IsAssignableFrom(t)))
                        {
                            var orders = new List<PaymentOrder>();
                            // PaymentOrder val = new PaymentOrder();

                            PaymentOrder order = null;

                            for (int i = 0; i < data.Length; i = i + BatchBase.RecordLength)
                            {
                                BetforBase rec = new BetforBase();
                                ParseObject(data.Substring(i, BatchBase.RecordLength), rec);

                                switch (rec.TransactionCode)
                                {
                                    case RecordType.BETFOR21:
                                        var trRec = new BETFOR21();
                                        order = new PaymentOrder();
                                        order.Records = new List<BetforBase>();
                                        ParseObject(data.Substring(i, BatchBase.RecordLength), trRec);
                                        order.TransferRecord = trRec;
                                        break;
                                    case RecordType.BETFOR01:
                                        var trRecInt = new BETFOR01();
                                        var ordTmp = new InternationalPaymentOrder();
                                        ordTmp.Invoice = new BETFOR04();
                                        ordTmp.BankDetails = new BETFOR02();
                                        ordTmp.Payee = new BETFOR03();
                                        order = ordTmp;
                                        ParseObject(data.Substring(i, BatchBase.RecordLength), trRecInt);
                                        order.TransferRecord = trRecInt;
                                        orders.Add(order);
                                        break;
                                    case RecordType.BETFOR02:
                                        if (order != null)
                                        {
                                            var payment = new BETFOR02();
                                            ParseObject(data.Substring(i, BatchBase.RecordLength), payment);
                                            ((InternationalPaymentOrder)order).BankDetails = payment;
                                        }
                                        break;
                                    case RecordType.BETFOR03:
                                        if (order != null)
                                        {
                                            var payment = new BETFOR03();
                                            ParseObject(data.Substring(i, BatchBase.RecordLength), payment);
                                            ((InternationalPaymentOrder)order).Payee = payment;
                                        }
                                        break;
                                    case RecordType.BETFOR04:
                                        if (order != null)
                                        {
                                            var payment = new BETFOR04();
                                            ParseObject(data.Substring(i, BatchBase.RecordLength), payment);
                                            ((InternationalPaymentOrder)order).Invoice = payment;
                                        }
                                        break;
                                    case RecordType.BETFOR22:
                                        if (order != null)
                                        {
                                            if (!(typeof (MassPaymentOrder).Equals(order.GetType())))
                                            {
                                                var ord = new MassPaymentOrder();
                                                ord.TransferRecord = order.TransferRecord ?? null;
                                                order = ord;
                                                order.Records = new List<BETFOR22>();
                                                orders.Add(order);
                                            }
                                            var payment = new BETFOR22();
                                            ParseObject(data.Substring(i, BatchBase.RecordLength), payment);
                                            order.AddRecord(payment);
                                        }
                                        break;
                                    case RecordType.BETFOR23:
                                        if (order != null)
                                        {
                                            if (!(typeof (InvoicesPaymentOrder).Equals(order.GetType())))
                                            {
                                                var ord = new InvoicesPaymentOrder();
                                                ord.TransferRecord = order.TransferRecord ?? null;
                                                order = ord;
                                                order.Records = new List<BETFOR21>();
                                                orders.Add(order);
                                            }
                                            var payment = new BETFOR21();
                                            ParseObject(data.Substring(i, BatchBase.RecordLength), payment);
                                            order.AddRecord(payment);
                                        }
                                        break;
                                }

                            }
                            value = orders;
                        }
                        else if ((typeof (IEnumerable<BetforBase>)).IsAssignableFrom(prop.PropertyType))
                        {
                            var records = new List<BetforBase>();
                            var record = new BetforBase();
                            ParseObject(data, record);
                            switch (record.TransactionCode)
                            {
                                case RecordType.BETFOR21:
                                    record = new BETFOR21();
                                    break;
                                case RecordType.BETFOR22:
                                    record = new BETFOR22();
                                    break;
                                    
                            }
                            ParseObject(data, record);
                            records.Add(record);
                            value = records;
                        }
                        else if ((typeof (TelepayObject).IsAssignableFrom(prop.PropertyType)))
                        {
                            value = Activator.CreateInstance(prop.PropertyType);
                            ParseObject(data, value);
                        }
                    }
                    break;
                default:
                    value = ParseData(data, attr);
                    break;
            }

            if (prop.PropertyType.IsEnum)
            {
                value = Enum.Parse(prop.PropertyType, value.ToString());
            }

            prop.SetValue(obj, value, null);
        }

        private static object ParseData(string data, TelepayFieldAttribute attr)
        {
            object value = null;
            try
            {
                var str = data.Substring(attr.Position.Value, attr.Length.Value);
                switch (attr.Type)
                {
                    case FieldType.Alpha:
                        value = str;
                        break;
                    case FieldType.Alphanumeric:
                        value = str;
                        break;
                    case FieldType.Numeric:
                        long val = 0;
                        long.TryParse(str, out val);
                        value = val;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                throw new Exception("Field parser exception: start position - " + attr.Position + "; length - " +
                                    attr.Length);
            }

            return value;
        }
    }
}