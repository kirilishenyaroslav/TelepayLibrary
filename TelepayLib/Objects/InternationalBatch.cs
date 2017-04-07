using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelepayLib.Base;
using TelepayLib.Betfor;

namespace TelepayLib
{
    public class InternationalBatch : BatchBase
    {
        public override void Init(int startSeqNum, int seqControl, int enterpriseNum, DateTime productionDate, AHProcedure defProcedure = AHProcedure.TBIU)
        {            
            base.Init(startSeqNum, seqControl, enterpriseNum, productionDate, defProcedure);            
        }

        public void AddPayment(long accountNumber, 
                               DateTime paymentDate, 
                               string ownRef, 
                               TextCode textCode,
                               TransferCode transferCode, 
                               int amount, 
                               string payeeAccNum, 
                               string payeeName,
                               string swiftBankAddr,
                               string bankAddress, 
                               string bankCode, 
                               string bankName, 
                               string receivingCountryCode,
                               string payeeAddress,
                               string payeeInvoiceRef,
                               ISOCurrencySymbols currencyInfo,
                               Charges chargesAbroad = Charges.BEN,
                               Charges chargesInNorway = Charges.OUR)
        {
            InternationalPaymentOrder order = new InternationalPaymentOrder();
            this.PaymentOrders.Add(order);
                        
            AddTransferRecord(order, accountNumber, paymentDate, ownRef, currencyInfo, chargesAbroad, chargesInNorway);
            
            AddPaymentRecord(order, accountNumber, amount, payeeAccNum, payeeName, swiftBankAddr, bankAddress, bankCode,
                             bankName, receivingCountryCode, payeeAddress, payeeInvoiceRef);
        }

        private void AddTransferRecord(InternationalPaymentOrder order, 
                                       long accountNumber, 
                                       DateTime paymentDate,
                                       string ownRef,
                                       ISOCurrencySymbols currencyInfo,
                                       Charges chargesAbroad,
                                       Charges chargesInNorway)
        {
            var transferRecord = new BETFOR01
            {
                Header            = CreateDefaultHeader(),
                EnterpriseNum     = EnterpriseNumber,
                AccountNumber     = accountNumber,
                PaymentDate       = paymentDate.ToString("yyMMdd"),
                PaymentCurrency   = currencyInfo.ToString(),
                InvoiceCurrency   = currencyInfo.ToString(),
                ChargesAbroad     = chargesAbroad,
                ChargesInNorway   = chargesInNorway,
                OwnReferenceOrder = ownRef
            };

            order.TransferRecord = transferRecord;
        }


        private void AddPaymentRecord(InternationalPaymentOrder order, 
                                      long accountNumber, 
                                      int amount,
                                      string payeeAccNum, 
                                      string payeeName, 
                                      string swiftBankAddr, 
                                      string bankAddress,
                                      string bankCode, 
                                      string bankName, 
                                      string receivingCountryCode, 
                                      string payeeAddress,
                                      string payeeInvoiceRef)
        {
            order.BankDetails = new BETFOR02
            {
                Header = CreateDefaultHeader(),
                EnterpriseNum = this.EnterpriseNumber,
                AccountNum = accountNumber,
                SwiftAddress = swiftBankAddr,
                SwiftAddrCorpBank = swiftBankAddr
            };

            if (bankAddress.Length > 35)
            {
                order.BankDetails.BankAddress1 = bankAddress.Substring(0, 35);

                if (bankAddress.Length > 70)
                {
                    order.BankDetails.BankAddress2 = bankAddress.Substring(35, 35);

                    order.BankDetails.BankAddress3 = bankAddress.Length > 105 ? bankAddress.Substring(70, 35) : bankAddress.Substring(71, bankAddress.Length - 70);
                }
                else
                    order.BankDetails.BankAddress2 = bankAddress.Substring(35, bankAddress.Length - 35);
            }
            else
                order.BankDetails.BankAddress1 = bankAddress;

            order.BankDetails.BankCode = bankCode;
            order.BankDetails.BankName = bankName;
            order.BankDetails.CountryCodeReceivBank = receivingCountryCode;

            order.Invoice = new BETFOR04
            {
                Header = CreateDefaultHeader(),
                EnterpriseNum = this.EnterpriseNumber,
                AccountNumber = accountNumber,
                InvoiceAmount = amount,
                DebitCreditCancelCode = "D",
                PayeeRefInvoice = payeeInvoiceRef
            };


            order.Payee = new BETFOR03
            {
                Header = CreateDefaultHeader(),
                EnterpriseNum = this.EnterpriseNumber,
                PayeeAccount = payeeAccNum,
                PayeeName = payeeName,
                AccountNum = accountNumber,
                PayeeCountry = receivingCountryCode
            };


            if (payeeAddress.Length > 35)
            {
                order.Payee.PayeeAddress1 = payeeAddress.Substring(0, 35);

                if (bankAddress.Length > 70)
                {
                    order.Payee.PayeeAddress2 = payeeAddress.Substring(35, 35);
                    order.Payee.PayeeAddress3 = bankAddress.Length > 105 ? payeeAddress.Substring(70, 35) : payeeAddress.Substring(70, payeeAddress.Length - 70);
                }
                else
                    order.Payee.PayeeAddress2 = payeeAddress.Substring(35, payeeAddress.Length - 35);
            }
            else
                order.Payee.PayeeAddress1 = payeeAddress;
        }
    }
}