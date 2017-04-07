using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelepayLib.Base;

namespace TelepayLib.Betfor
{
    //domestic
    public class BETFOR99 : BetforBase
    {
        [TelepayField(59, 11, FieldType.Alpha)]
        public string Reserved60_70 { get; set; }

        [TelepayField(70, 4, FieldType.Numeric)]
        public long SeqControl { get; set; }

        [TelepayField(74, 6, FieldType.Alpha)]
        public string Reserved75_80 { get; set; }

        [TelepayField(80, 4, FieldType.Alphanumeric)]
        public string ProdDate { get; set; }

        [TelepayField(84, 4, FieldType.Alpha)]
        public string NumberOfPayments { get; set; }

        [TelepayField(88, 15, FieldType.Alpha)]
        public string TotalAmountBatch { get; set; }

        [TelepayField(103, 5, FieldType.Numeric)]
        public long NumberOfRecords { get; set; }

        [TelepayField(108, 163, FieldType.Alpha)]
        public string Reserved109_271 { get; set; }

        [TelepayField(271, 4, FieldType.Alpha)]
        public string SigillSecurity { get; set; }

        [TelepayField(275, 1, FieldType.Alpha)]
        public string SigillLanguage { get; set; }

        [TelepayField(276, 1, FieldType.Alpha)]
        public string SigillVersion { get; set; }

        [TelepayField(277, 1, FieldType.Alpha)]
        public string SigillInterface { get; set; }

        [TelepayField(278, 18, FieldType.Alpha)]
        public string SigillControl { get; set; }

        [TelepayField(296, 16, FieldType.Alpha)]
        public string VersionSoftware { get; set; }

        [TelepayField(312, 8, FieldType.Alpha)]
        public string VersionBank { get; set; }

        public BETFOR99()
        {
            TransactionCode = RecordType.BETFOR99;
        }
    }
}