using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelepayLib.Base;
using TelepayLib.Betfor;

namespace TelepayLib
{
    public class InvoicesPaymentOrder : PaymentOrder
    {
        [TelepayField(FieldType.Object)]
        public override IEnumerable<BetforBase> Records { get; set; }

        public override void AddRecord(BetforBase rec)
        {
            (Records as List<BETFOR21>).Add(rec as BETFOR21);
        }

        public InvoicesPaymentOrder()
        {
            TransferRecord = new BETFOR21();
            Records = new List<BETFOR23>();
        }
    }

    public class MassPaymentOrder : PaymentOrder
    {
        [TelepayField(FieldType.Object)]
        public override IEnumerable<BetforBase> Records { get; set; }

        public override void AddRecord(BetforBase rec)
        {
            (Records as List<BETFOR22>).Add(rec as BETFOR22);
        }

        public MassPaymentOrder()
        {
            TransferRecord = new BETFOR21();
            Records = new List<BETFOR22>();
        }
    }

    public class InternationalPaymentOrder : PaymentOrder
    {
        [TelepayField(FieldType.Object)]
        public BETFOR02 BankDetails
        {
            get { return ((List<BetforBase>) Records)[0] as BETFOR02; }
            set { ((List<BetforBase>) Records)[0] = value; }
        }

        [TelepayField(FieldType.Object)]
        public BETFOR03 Payee
        {
            get { return ((List<BetforBase>) Records)[1] as BETFOR03; }
            set { ((List<BetforBase>) Records)[1] = value; }
        }

        [TelepayField(FieldType.Object)]
        public BETFOR04 Invoice
        {
            get { return ((List<BetforBase>) Records)[2] as BETFOR04; }
            set { ((List<BetforBase>) Records)[2] = value; }
        }


        public InternationalPaymentOrder()
        {
            TransferRecord = new BETFOR01();
            Records = new List<BetforBase>() { new BETFOR02(), new BETFOR03(), new BETFOR04() };
        }
    }
}