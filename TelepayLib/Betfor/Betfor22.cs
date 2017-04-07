using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelepayLib.Base;

namespace TelepayLib.Betfor
{
    public class BETFOR22 : BetforBase
    {
        [TelepayField(59, 11, FieldType.Numeric)]
        public long AccountNumber { get; set; }

        [TelepayField(70, 4, FieldType.Numeric)]
        public long SeqControl { get; set; }

        [TelepayField(74, 6, FieldType.Alpha)]
        public string RefNumber { get; set; }

        [TelepayField(80, 11, FieldType.Numeric)]
        public long PayeeAccNum { get; set; }

        [TelepayField(91, 30, FieldType.Alpha)]
        public string PayeeName { get; set; }

        [TelepayField(121, 15, FieldType.Numeric)]
        public long Amount { get; set; }

        [TelepayField(136, 1, FieldType.Alpha)]
        public string CancellationCode { get; set; }

        [TelepayField(137, 35, FieldType.Alpha)]
        public string OwnReference1 { get; set; }

        [TelepayField(172, 110, FieldType.Alpha)]
        public string Reserved173_282 { get; set; }

//possible bug in specifications

        [TelepayField(282, 10, FieldType.Alpha)]
        public string OwnReference2 { get; set; }

        [TelepayField(292, 4, FieldType.Numeric)]
        public long SerialNum { get; set; }

        [TelepayField(296, 1, FieldType.Alpha)]
        public string CancellationCause { get; set; }

        [TelepayField(297, 23, FieldType.Alpha)]
        public string Reserved298_320 { get; set; }

        public BETFOR22()
        {
            TransactionCode = RecordType.BETFOR22;
        }
    }
}