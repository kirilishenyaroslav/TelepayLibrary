using TelepayLib.Base;

namespace TelepayLib.Betfor
{
    public class BETFOR02 : BetforBase
    {
        [TelepayField(59, 11, FieldType.Numeric)]
        public long AccountNum { get; set; }

        [TelepayField(70, 4, FieldType.Numeric)]
        public long SeqControl { get; set; }

        [TelepayField(74, 6, FieldType.Alpha)]
        public string ReferenceNum { get; set; }

        [TelepayField(80, 11, FieldType.Alpha)]
        public string SwiftAddress { get; set; }

        [TelepayField(91, 35, FieldType.Alpha)]
        public string BankName { get; set; }

        [TelepayField(126, 35, FieldType.Alpha)]
        public string BankAddress1 { get; set; }

        [TelepayField(161, 35, FieldType.Alpha)]
        public string BankAddress2 { get; set; }

        [TelepayField(196, 35, FieldType.Alpha)]
        public string BankAddress3 { get; set; }

        [TelepayField(231, 11, FieldType.Alpha)]
        public string SwiftAddrCorpBank { get; set; }

        [TelepayField(242, 2, FieldType.Alpha)]
        public string CountryCodeReceivBank { get; set; }

        [TelepayField(244, 15, FieldType.Alpha)]
        public string BankCode { get; set; }

        [TelepayField(259, 35, FieldType.Alpha)]
        public string AccountNoTBIO { get; set; }

        [TelepayField(294, 26, FieldType.Alpha)]
        public string Reserved295_320 { get; set; }

        public BETFOR02()
        {
            TransactionCode = RecordType.BETFOR02;
        }
    }
}