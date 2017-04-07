using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelepayLib
{
    public enum TextCode
    {
        TransferWithoutAdvice = 600,
        KidTransfer = 601,
        TransferWithAdvice = 602,
        MoneyOrder = 603,
        Salary = 604,
        SeamansPay = 605,
        AgriculturalSettlement = 606,
        PensionSocialSecurity = 607,
        AdviceSentFromInstitutionOtherThanBBS = 608,
        Tax = 609,

        /// <summary>
        /// Own reference in BETFOR21 is forwarded to payee's account statement
        /// </summary>
        FreeTextMassPayment = 621,

        /// <summary>
        /// Own reference in BETFOR22 (pos. 138-172) is forwarded to payee's account statement
        /// </summary>
        FreeText = 622,
        SelfproducedMoneyOrder = 630
    }
}