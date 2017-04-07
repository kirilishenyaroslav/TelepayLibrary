using TelepayLib.Base;

namespace TelepayLib.Betfor
{
    public class BETFOR04 : BetforBase
    {
        [TelepayField(59, 11, FieldType.Numeric)]
        public long AccountNumber { get; set; }

        [TelepayField(70, 4, FieldType.Numeric)]
        public long SeqControl { get; set; }

        [TelepayField(74, 6, FieldType.Alpha)]
        public string ReferenceNum { get; set; }

        [TelepayField(80, 35, FieldType.Alpha)]
        public string PayeeRefInvoice { get; set; }

        [TelepayField(115, 35, FieldType.Alpha)]
        public string OwnRefInvoice { get; set; }

        [TelepayField(150, 15, FieldType.Numeric)]
        public long InvoiceAmount { get; set; }

        [TelepayField(165, 1, FieldType.Alpha)]
        public string DebitCreditCancelCode { get; set; }

        [TelepayField(166, 6, FieldType.Alpha)]
        public string AuthorityReportCode { get; set; }

        [TelepayField(172, 60, FieldType.Alpha)]
        public string AuthorityReportText { get; set; }

        [TelepayField(232, 1, FieldType.Alpha)]
        public string ToOwnAccount { get; set; }

        [TelepayField(233, 1, FieldType.Alpha)]
        public string CancelCause { get; set; }

        [TelepayField(234, 6, FieldType.Numeric)]
        public long Reserved235_240 { get; set; }

        [TelepayField(240, 1, FieldType.Alpha)]
        public string Reserved241_242 { get; set; }

        [TelepayField(241, 6, FieldType.Numeric)]
        public long Reserved242_247_ { get; set; }

        [TelepayField(247, 45, FieldType.Alpha)]
        public string Reserved248_292 { get; set; }

        [TelepayField(292, 1, FieldType.Alpha)]
        public string KID { get; set; }

        [TelepayField(293, 3, FieldType.Numeric)]
        public long SerialNumber { get; set; }

        [TelepayField(296, 24, FieldType.Alpha)]
        public string Reserved297_320 { get; set; }

        public BETFOR04()
        {
            TransactionCode = RecordType.BETFOR04;
        }
    }
}