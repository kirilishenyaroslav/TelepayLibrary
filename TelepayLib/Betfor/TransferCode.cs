using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelepayLib
{
    public enum TransferCode
    {
        /// <summary>
        /// Salary
        /// </summary>
        L,

        /// <summary>
        /// Other mass payment
        /// </summary>
        M,

        /// <summary>
        ///  To own account
        /// </summary>
        E,

        /// <summary>
        /// Invoice payment
        /// </summary>
        F
    }
}