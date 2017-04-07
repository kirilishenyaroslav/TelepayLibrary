using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelepayLib.Betfor;

namespace TelepayLib.Base
{
    public class BatchBase : TelepayObject
    {
        #region Properties

        /// <summary>
        /// Length of the sub-records 
        /// </summary>
        public const int SubRecordLength = 80;

        public const int RecordLength = 320;
        public const string SoftVersion = "Telepay Lib v0.1";
        private int currentSeqCtrl;
        protected int EnterpriseNumber { get; set; }

        private int BatchCurrentSequenceControl
        {
            get
            {
                currentSeqCtrl++;
                return currentSeqCtrl;
            }
        }

        private int startSeqCtrl;

        public int BatchStartSequenceControl
        {
            get { return startSeqCtrl; }
            set
            {
                currentSeqCtrl = value - 1;
                startSeqCtrl = value;
            }
        }

        public AHProcedure DefaultProcedure { get; set; }
        public AHReturnCode DefaultReturnCode { get; set; }

        protected int _startSeqNum;
        protected int _currentSeqNum;

        private int seqNum
        {
            get
            {
                _currentSeqNum++;
                return _currentSeqNum;
            }
            set
            {
                _startSeqNum = value;
                _currentSeqNum = value - 1;
            }
        }

        public string ProductionDate { get; set; }

        public string TransactionDate
        {
            get { return DateTime.Now.ToString("MMdd"); // string.Concat(DateTime.Now.Month, DateTime.Now.Day); 
            }
        }

        [TelepayField(1, BatchBase.RecordLength, FieldType.Object)]
        public BETFOR00 StartOfBatch { get; set; }

        [TelepayField(FieldType.Object)]
        public List<PaymentOrder> PaymentOrders { get; set; }

        [TelepayField(BatchBase.RecordLength, FieldType.Object)]
        public BETFOR99 EndOfBatch { get; set; }

        #endregion

        #region Initializers

        public virtual void Init(int startSeqNum, int seqControl, int enterpriseNum, DateTime productionDate, AHProcedure DefProcedure)
        {
            EnterpriseNumber          = enterpriseNum;
            BatchStartSequenceControl = seqControl;
            seqNum                    = startSeqNum;
            ProductionDate            = string.Concat(productionDate.Month.ToString("d2"), productionDate.Day.ToString("d2"));
            DefaultReturnCode         = AHReturnCode.BatchRejectedOrInput;
            DefaultProcedure          = DefProcedure;
            InitStartOfBatch(enterpriseNum);
            InitEndOfBatch(enterpriseNum);
        }

        protected virtual void InitEndOfBatch(int enterpriseNum)
        {
            this.EndOfBatch.Header           = CreateDefaultHeader();
            this.EndOfBatch.TransactionCode  = RecordType.BETFOR99;
            this.EndOfBatch.EnterpriseNum    = enterpriseNum;            
            this.EndOfBatch.VersionSoftware  = SoftVersion;
            this.EndOfBatch.ProdDate         = ProductionDate;
        }

        protected virtual void InitStartOfBatch(int enterpriseNum)
        {            
            this.StartOfBatch.ProdDate      = ProductionDate;
            this.StartOfBatch.EnterpriseNum = enterpriseNum;
            this.StartOfBatch.Header        = CreateDefaultHeader();
        }

        public ApplicationHeader CreateDefaultHeader()
        {
            ApplicationHeader DefaultHeader = new ApplicationHeader();
            DefaultHeader.Procedure         = DefaultProcedure;            
            DefaultHeader.ReturnCode        = DefaultReturnCode;
            DefaultHeader.TransactionDate   = TransactionDate;
            return DefaultHeader;
        }

        #endregion

        #region Constructors

        public BatchBase()
        {
            StartOfBatch = new BETFOR00();
            EndOfBatch = new BETFOR99();
            PaymentOrders = new List<PaymentOrder>();
        }

        #endregion

        #region Helpers

        protected RecordType GetTranCode(string data)
        {
            var tranCode = data.Substring(40, 8);
            RecordType TransactionCode;
            if (!Enum.TryParse(tranCode, out TransactionCode))
                throw new ArgumentException("Wrong transaction code");
            return TransactionCode;
        }

        #endregion

        public override string ToString()
        {
            SetSeqNumbers();

            var start = StartOfBatch.ToString();
            var str = string.Concat(
                start,
                string.Join("", PaymentOrders.Select(p =>
                                                     string.Concat(p.TransferRecord.ToString(),
                                                                   string.Join("", p.Records.Select(r => r.ToString()))))),
                EndOfBatch.ToString()
                );

            var len = str.Length/SubRecordLength;

            var parts = new string[len];
            for (int i = 0; i < len; i++)
                parts[i] = str.Substring(i*SubRecordLength, SubRecordLength);

            return string.Join("\r\n", parts);
        }

        private void SetSeqNumbers()
        {
            StartOfBatch.Header.SeqNum = seqNum;

            StartOfBatch.SeqControl = StartOfBatch.SeqControl == 0
                                          ? BatchCurrentSequenceControl
                                          : StartOfBatch.SeqControl;

            foreach (var paymentOrder in PaymentOrders)
            {
                bool isInternational = false;

                if (paymentOrder.TransferRecord != null)
                {
                    if (paymentOrder.TransferRecord is BETFOR21)
                    {
                        paymentOrder.TransferRecord.Header.SeqNum = seqNum;
                        var ctrl = ((BETFOR21) paymentOrder.TransferRecord);
                        ctrl.SeqControl = ctrl.SeqControl == 0 ? BatchCurrentSequenceControl : ctrl.SeqControl;
                    }
                    if (paymentOrder.TransferRecord is BETFOR01)
                    {
                        isInternational = true;
                        paymentOrder.TransferRecord.Header.SeqNum = seqNum;
                        var ctrl = ((BETFOR01) paymentOrder.TransferRecord);
                        ctrl.SeqControl = ctrl.SeqControl == 0 ? BatchCurrentSequenceControl : ctrl.SeqControl;
                    }
                }

                if (isInternational)
                {
                    var po = (InternationalPaymentOrder) paymentOrder;

                    po.BankDetails.Header.SeqNum = seqNum;
                    po.BankDetails.SeqControl = po.BankDetails.SeqControl == 0
                                                    ? BatchCurrentSequenceControl
                                                    : po.BankDetails.SeqControl;

                    po.Payee.Header.SeqNum = seqNum;
                    po.Payee.SeqControl = po.Payee.SeqControl == 0 ? BatchCurrentSequenceControl : po.Payee.SeqControl;

                    po.Invoice.Header.SeqNum = seqNum;
                    po.Invoice.SeqControl = po.Invoice.SeqControl == 0
                                                ? BatchCurrentSequenceControl
                                                : po.Invoice.SeqControl;
                                        
                }
                else
                {
                    foreach (var record in paymentOrder.Records)
                    {
                        record.Header.SeqNum = seqNum;
                        var ctrl = ((BETFOR22) record);
                        ctrl.SeqControl = ctrl.SeqControl == 0 ? BatchCurrentSequenceControl : ctrl.SeqControl;
                    }
                }
            }

            EndOfBatch.Header.SeqNum = seqNum;
            EndOfBatch.SeqControl = EndOfBatch.SeqControl == 0 ? BatchCurrentSequenceControl : EndOfBatch.SeqControl;
            EndOfBatch.NumberOfRecords = EndOfBatch.Header.SeqNum;
        }
    }
}