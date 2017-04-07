using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TelepayLib.Betfor;

namespace TelepayLib.Base
{
    public class BetforBase : TelepayObject
    {
        #region Properties

        /// <summary>
        /// Length of the whole payment record
        /// </summary>
        public const int RecordLength = 320;

        /// <summary>
        /// Length of the application header part 
        /// </summary>
        public const int HeaderLength = 40;

        /// <summary>
        /// Length of the payment data part
        /// </summary>
        public const int DetailsLength = 280;

        /// <summary>
        /// Application header part
        /// </summary>
        public string applicationHeaderString { get; private set; }

        /// <summary>
        /// Payment data part
        /// </summary>
        public string paymentDataString { get; private set; }

        [TelepayField(40, 8, FieldType.Alpha)]
        public RecordType TransactionCode { get; set; }

        [TelepayField(48, 11, FieldType.Numeric)]
        public long EnterpriseNum { get; set; }

        [TelepayField(0, 40, FieldType.Object)]
        public ApplicationHeader Header { get; set; }

        public string Data { get; private set; }

        #endregion

        public BetforBase()
        {
        }
    }
}