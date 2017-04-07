using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelepayLib.Base;

namespace TelepayLib.Betfor
{
    public class BETFOR23 : BetforBase
    {
        public string AccountNumber { get; set; }
        public string SeqControl { get; set; }
        public string RefNumber { get; set; }

        public string PayeeRefInvoice1 { get; set; }
        public string PayeeRefInvoice2 { get; set; }
        public string PayeeRefInvoice3 { get; set; }

        public string KID { get; set; }
        public string OwnReferenceInvoice { get; set; }
        public string InvoiceAmount { get; set; }
        public string DebitOrCreditCode { get; set; }
        //public string CancellationCode { get; set; }

        public string InvoiceNumber { get; set; }
        public string SerialNumber { get; set; }
        public string CancellationCause { get; set; }
        public string CustomerNumber { get; set; }
        public string InvoiceDate { get; set; }
    }
}