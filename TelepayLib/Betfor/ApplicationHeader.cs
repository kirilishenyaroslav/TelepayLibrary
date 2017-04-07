using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelepayLib.Base;

namespace TelepayLib.Betfor
{
    public enum AHReturnCode
    {
        BatchRejectedOrInput = 00,
        BatchReceived = 01,
        BatchOk = 02,
        ErroneousEnterprise = 10,
        CannotChangeOrCancel = 11,
        WrongAmountOfInvoices = 12,
        WrongSerialNumber = 13,
        TransactionTypeCannotBeChangedBETFOR21 = 14,
        MissingDebitOrCreditCode = 15,
        MixingMessageInformation = 16,
        InvalidKID = 17,
        KIDIsMandatory = 18,
        InvalidCreditAccountNumber = 19,
        InvalidDebitAccountNumber = 20,
        PaymentDateInvalid = 21,
        InvalidRefNo = 22,
        PasswordExpired = 25,
        OperatorLocked = 26,
        InvalidPassword = 27,
        OperatorNotAuthorised = 28,
        InvalidOperatorID = 29,
        InvalidVersionNumberBETFOR00 = 30,
        WrongNameOrAddress = 34,
        IncorrectCurrencyCode = 35,
        ErrorInExchangeRate = 36,
        InvalidChequeCode = 37,
        InvalidChargesCodes = 38,
        InvalidNotificationIndicator = 39,
        InvalidPriorityCode = 40,
        InvalidAuthorityReportCode = 41,
        InvalidAmountField = 42,
        MissingMandatoryAuthorityReportText = 43,
        IncorrectPayeeCountryCode = 44,
        ErrorInSWIFTCode = 45,
        ErrorInBankCode = 46,
        InvalidProductionDate = 47,
        ErrorInSequenceControl = 80,
        IncorrectlyConstructedBatch = 81,
        InvalidTransactionCode = 82,
        SealInvalid = 83,
        MissingNewSealKey = 84,
        IncorrectlyConstructedPaymentOrder = 85,
        BETFORNotFollowedBySufficientNumberOfRecords = 86,
        ErrorsInBETFOR99 = 87,
        ErrorsInBETFOR00 = 88,
        WringTransactionsNubber = 89,
        SequenceErrorInAH = 90,
        UnknownAhProcedureID = 91,
        InvalidAhTransactionDate = 92,
        FreetextInformationIsTooLong = 93,
        InvalidDivision = 95
    }
        
    public enum AHProcedure
    {
        #region Input procedures

        TBII,
        TBIU,
        TBIO,

        #endregion

        #region Output procedures

        TBRI,
        TBRU,
        TBRO

        #endregion
    }

    public class ApplicationHeader : TelepayObject
    {        
        [TelepayField(0, 2, FieldType.Alpha)]
        public string ID { get; set; }

        [TelepayField(2, 1, FieldType.Numeric)]
        public long Version { get; set; }

        [TelepayField(3, 2, FieldType.Numeric)]
        public AHReturnCode ReturnCode { get; set; }

        [TelepayField(5, 4, FieldType.Alpha)]
        public AHProcedure Procedure { get; set; }

        [TelepayField(9, 4, FieldType.Alphanumeric)]
        public string TransactionDate { get; set; }

        [TelepayField(13, 6, FieldType.Numeric)]
        public long SeqNum { get; set; }
        
        [TelepayField(19, 8, FieldType.Alpha)]
        public string TransCode { get; set; }
        
        [TelepayField(27, 11, FieldType.Alpha)]
        public string UserId { get; set; }

        [TelepayField(38, 2, FieldType.Alpha)]
        public string NoOf80Char { get; set; }
                                                
        public ApplicationHeader()
        {
            ID         = "AH";
            Version    = 2;
            NoOf80Char = "04";
            ReturnCode = AHReturnCode.BatchRejectedOrInput;
        }
    }




}