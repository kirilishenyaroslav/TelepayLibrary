using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TelepayLib.Base;

namespace TelepayLib.Betfor
{
    public class BETFOR21 : BetforBase
    {
        [TelepayField(59, 11, FieldType.Numeric)]
        public long AccountNumber { get; set; }

        [TelepayField(70, 4, FieldType.Numeric)]
        public long SeqControl { get; set; }

        [TelepayField(74, 6, FieldType.Alpha)]
        public string RefNumber { get; set; }

        /// <summary>
        /// yyMMdd
        /// </summary>
        [TelepayField(80, 6, FieldType.Alphanumeric)]
        public string PaymentDate { get; set; }

        [TelepayField(86, 30, FieldType.Alpha)]
        public string OwnRefOrder { get; set; }

        [TelepayField(116, 1, FieldType.Alpha)]
        public string Reserved117_1 { get; set; }

        [TelepayField(117, 11, FieldType.Numeric)]
        public long PayeeAccNum { get; set; }

        [TelepayField(128, 30, FieldType.Alpha)]
        public string PayeeName { get; set; }

        [TelepayField(158, 30, FieldType.Alpha)]
        public string Address1 { get; set; }

        [TelepayField(188, 30, FieldType.Alpha)]
        public string Address2 { get; set; }

        [TelepayField(218, 4, FieldType.Numeric)]
        public long Postcode { get; set; }

        [TelepayField(222, 26, FieldType.Alpha)]
        public string City { get; set; }

        [TelepayField(248, 15, FieldType.Numeric)]
        public long AmountToOwnAccount { get; set; }

        [TelepayField(263, 3, FieldType.Numeric)]
        public TextCode TextCode { get; set; }

        [TelepayField(265, 1, FieldType.Alpha)]
        public TransferCode TransferCode { get; set; }

        [TelepayField(267, 1, FieldType.Alpha)]
        public string CancellationCode { get; set; }

        [TelepayField(268, 15, FieldType.Numeric)]
        public long TotalAmount { get; set; }

        [TelepayField(283, 5, FieldType.Alphanumeric)]
        public string ClientReference { get; set; }

        [TelepayField(288, 6, FieldType.Numeric)]
        public long ValueDate { get; set; }

        [TelepayField(294, 6, FieldType.Alphanumeric)]
        public string ValueDateReceivingBank { get; set; }

        [TelepayField(300, 1, FieldType.Alpha)]
        public string CancellationCause { get; set; }

        [TelepayField(301, 9, FieldType.Alpha)]
        public string Reserved302_310 { get; set; }

        [TelepayField(310, 10, FieldType.Numeric)]
        public long FormNo { get; set; }

        public BETFOR21()
        {
            TransactionCode = RecordType.BETFOR21;
        }

        protected override object GetPropertyValue(PropertyInfo prop, TelepayFieldAttribute attr)
        {
            switch (prop.Name)
            {
                case "TextCode":
                    return ((int) TextCode).ToString("d3");
                case "TransferCode":
                    return TransferCode.ToString();
                default:
                    return base.GetPropertyValue(prop, attr);
            }
        }

        public static string GetTextCode(PropertyInfo prop, TelepayFieldAttribute attr, object data)
        {
            TextCode code = (TextCode) prop.GetValue(data, null);
            return ((int) code).ToString("d3");
        }
    }
}