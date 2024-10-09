using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public enum EarningType
    {
        hourly,
        salary
    }

    public enum EarningCode
    {
        overtime,
        regular,
        bonus
    }

    public enum DeductionType
    {
        percentage,
        flat
    }

    public enum TaxType
    {
        percentage,
        cappedPercentage
    }


    public enum TaxCode
    {
        federalIncome,
        fica
    }
}
