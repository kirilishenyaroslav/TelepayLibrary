using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelepayLib.Base;
using TelepayLib.Betfor;

namespace TelepayLib
{
    public class DomesticBatch : BatchBase
    {
        public override void Init(int startSeqNum, int seqControl, int enterpriseNum, DateTime productionDate, AHProcedure defProcedure = AHProcedure.TBII)
        {
            base.Init(startSeqNum, seqControl, enterpriseNum, productionDate, defProcedure);            
        }

        public void AddPayment(long AccountNumber, DateTime PaymentDate, string OwnRef, TextCode textCode,
                               TransferCode transferCode, int Amount, long PayeeAccNum, string PayeeName)
        {
            MassPaymentOrder order =
                PaymentOrders.Where(o => ((BETFOR21) o.TransferRecord).AccountNumber == AccountNumber)
                             .Select(o => (MassPaymentOrder) o)
                             .FirstOrDefault();
            if (order == null)
            {
                order = new MassPaymentOrder();
                PaymentOrders.Add(order);
            }

            if (order.TransferRecord == null || ((BETFOR21) order.TransferRecord).AccountNumber != null)
                AddTransferRecord(order, AccountNumber, PaymentDate, OwnRef, textCode, transferCode);

            AddPaymentRecord(order, AccountNumber, Amount, PayeeAccNum, PayeeName);
        }

        private void AddTransferRecord(MassPaymentOrder order, long AccountNumber, DateTime PaymentDate, string OwnRef,
                                       TextCode textCode, TransferCode transferCode)
        {
            var transferRecord = new BETFOR21();

            transferRecord.Header = CreateDefaultHeader();

            transferRecord.EnterpriseNum = EnterpriseNumber;


            transferRecord.AccountNumber = AccountNumber;
            transferRecord.PaymentDate = PaymentDate.ToString("yyMMdd");
            transferRecord.OwnRefOrder = OwnRef;
            transferRecord.TextCode = textCode;
            transferRecord.TransferCode = transferCode;

            order.TransferRecord = transferRecord;
        }


        private void AddPaymentRecord(MassPaymentOrder order, long AccountNumber, int Amount, long PayeeAccNum,
                                      string payeeName)
        {
            var record = new BETFOR22();
            record.EnterpriseNum = EnterpriseNumber;

            record.Header = CreateDefaultHeader();

            record.AccountNumber = AccountNumber;
            record.Amount = Amount;
            record.PayeeAccNum = PayeeAccNum;
            record.PayeeName = payeeName;

            order.AddRecord(record);
        }
    }
}