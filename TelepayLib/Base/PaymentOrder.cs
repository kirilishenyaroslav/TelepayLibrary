using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelepayLib.Base
{
    public class PaymentOrder : TelepayObject
    {
        [TelepayField(1, BatchBase.RecordLength, FieldType.Object)]
        public BetforBase TransferRecord { get; set; }

        [TelepayField(FieldType.Object)]
        public virtual IEnumerable<BetforBase> Records { get; set; }

        public PaymentOrder()
        {
            Records = new List<BetforBase>();
        }

        public virtual void AddRecord(BetforBase rec)
        {
            var lst = Records.ToList();
            lst.Add(rec);
            Records = lst;
        }
    }
}