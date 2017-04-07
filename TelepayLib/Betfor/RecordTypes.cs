namespace TelepayLib
{
    public enum RecordType
    {
        //start of batch
        BETFOR00 = 00,

        #region domestic

        BETFOR01 = 01,
        BETFOR02 = 02,
        BETFOR03 = 03,
        BETFOR04 = 04,

        #endregion

        #region international

        BETFOR21 = 21,
        BETFOR22 = 22,
        BETFOR23 = 23,

        #endregion

        //end of batch
        BETFOR99 = 99
    }

    //Transaction code. BETFOR00/01/02/03/04 and BETFOR21/22/23 (Position 41-48)
    //BETFOR00 = Start-of-batch record.
    //BETFOR01 = Transfer record, International
    //BETFOR02 = Bank details record, International.
    //BETFOR03 = Payee record, International.
    //BETFOR04 = Invoice record, International.
    //BETFOR21 = Transfer record, Domestic.
    //BETFOR22 = Mass payment record, Domestic.
    //BETFOR23 = Invoice record, Domestic.
    //BETFOR99 = End-of-batch record.
}