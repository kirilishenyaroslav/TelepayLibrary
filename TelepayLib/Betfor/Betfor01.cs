using TelepayLib.Base;

namespace TelepayLib.Betfor
{
    public class BETFOR01 : BetforBase
    {
        [TelepayField(59, 11, FieldType.Numeric)]
        public long AccountNumber { get; set; }

        [TelepayField(70, 4, FieldType.Numeric)]
        public long SeqControl { get; set; }

        [TelepayField(74, 6, FieldType.Alpha)]
        public string ReferenceNum { get; set; }
        
        [TelepayField(80, 6, FieldType.Alphanumeric)]
        public string PaymentDate { get; set; }

        [TelepayField(86, 30, FieldType.Alpha)]
        public string OwnReferenceOrder { get; set; }

        [TelepayField(116, 3, FieldType.Alpha)]
        public string PaymentCurrency { get; set; }

        [TelepayField(119, 3, FieldType.Alpha)]
        public string InvoiceCurrency { get; set; }

        [TelepayField(122, 3, FieldType.Alpha)]
        public Charges ChargesAbroad { get; set; }

        [TelepayField(125, 3, FieldType.Alpha)]
        public Charges ChargesInNorway { get; set; }

        [TelepayField(128, 30, FieldType.Alpha)]
        public string NotificationIndicator { get; set; }

        [TelepayField(158, 1, FieldType.Alpha)]
        public string PriorityCode { get; set; }

        [TelepayField(159, 8, FieldType.Numeric)]
        public long AgreedExchangeRate { get; set; }

        [TelepayField(167, 6, FieldType.Alpha)]
        public string ForwardContractNo { get; set; }

        [TelepayField(173, 8, FieldType.Numeric)]
        public long ForwardContractExchRate { get; set; }

        [TelepayField(181, 1, FieldType.Alpha)]
        public string ChequeCode { get; set; }
        
        [TelepayField(182, 6, FieldType.Alphanumeric)]
        public string ValuDateReceivingBank { get; set; }

        [TelepayField(188, 2, FieldType.Alpha)]
        public string Reserved189_190 { get; set; }

        [TelepayField(190, 12, FieldType.Numeric)]
        public long ActualExchangeRate { get; set; }

        [TelepayField(202, 12, FieldType.Alpha)]
        public string ExecutionRef2 { get; set; }

        [TelepayField(214, 16, FieldType.Numeric)]
        public long AmountDebited { get; set; }

        [TelepayField(230, 16, FieldType.Numeric)]
        public long AmountTransferred { get; set; }

        [TelepayField(246, 5, FieldType.Alpha)]
        public string ClientReference { get; set; }

        [TelepayField(251, 6, FieldType.Numeric)]
        public long ExecutionRef1 { get; set; }

        [TelepayField(257, 6, FieldType.Alpha)]
        public string DealMadeWith { get; set; }

        [TelepayField(263, 1, FieldType.Alpha)]
        public string CancellationCode { get; set; }

        [TelepayField(264, 1, FieldType.Alpha)]
        public string ClearingCode { get; set; }
        
        [TelepayField(265, 6, FieldType.Numeric)]
        public long ValueDate { get; set; }

        [TelepayField(271, 9, FieldType.Numeric)]
        public long Fee { get; set; }

        [TelepayField(280, 12, FieldType.Numeric)]
        public long RateAgainstNOK { get; set; }

        [TelepayField(292, 1, FieldType.Alpha)]
        public string CancellationCause { get; set; }

        [TelepayField(293, 16, FieldType.Alphanumeric)]
        public string OrderedTransferredAmount { get; set; }

        [TelepayField(309, 1, FieldType.Alpha)]
        public string PriceInfo { get; set; }

        [TelepayField(310, 10, FieldType.Alpha)]
        public string Reserved311_320 { get; set; }

        public BETFOR01()
        {
            TransactionCode = RecordType.BETFOR01;
            ChargesAbroad = Charges.BEN;
            ChargesInNorway = Charges.OUR;
        }
    }
}