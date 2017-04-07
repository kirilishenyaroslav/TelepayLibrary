using TelepayLib.Base;

namespace TelepayLib.Betfor
{
    public class BETFOR03 : BetforBase
    {
        [TelepayField(59, 11, FieldType.Numeric)]
        public long AccountNum { get; set; }

        [TelepayField(70, 4, FieldType.Numeric)]
        public long SeqControl { get; set; }

        [TelepayField(74, 6, FieldType.Alpha)]
        public string ReferenceNum { get; set; }

        [TelepayField(80, 35, FieldType.Alpha)]
        public string PayeeAccount { get; set; }

        [TelepayField(115, 35, FieldType.Alpha)]
        public string PayeeName { get; set; }

        [TelepayField(150, 35, FieldType.Alpha)]
        public string PayeeAddress1 { get; set; }

        [TelepayField(185, 35, FieldType.Alpha)]
        public string PayeeAddress2 { get; set; }

        [TelepayField(220, 35, FieldType.Alpha)]
        public string PayeeAddress3 { get; set; }

        [TelepayField(255, 2, FieldType.Alpha)]
        public string PayeeCountry { get; set; }

        [TelepayField(257, 1, FieldType.Alpha)]
        public string FaxCode { get; set; }

        [TelepayField(258, 2, FieldType.Alpha)]
        public string TelexCountryCode { get; set; }

        [TelepayField(260, 18, FieldType.Alpha)]
        public string FaxNo { get; set; }

        [TelepayField(278, 20, FieldType.Alpha)]
        public string Attention { get; set; }

        [TelepayField(298, 22, FieldType.Alpha)]
        public string Reserved299_320 { get; set; }

        public BETFOR03()
        {
            TransactionCode = RecordType.BETFOR03;            
        }
    }
}