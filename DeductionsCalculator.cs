using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public interface IDeductionsCalculator
    {
        (double DeductionAmount, List<Withholding> Withholdings) GetDeductions(List<Deduction> deductions, double totalEarnings);
    }

    internal class DeductionsCalculator : IDeductionsCalculator
    {
        const string DeductionWithholdingType = "Deduction";

        public (double DeductionAmount, List<Withholding> Withholdings) GetDeductions(List<Deduction> deductions, double totalEarnings)
        {
            List<Withholding> withholdings = new List<Withholding>();
            double totalDeductions = 0;

            foreach (var deduction in deductions)
            {
                var withholding = new Withholding();
                withholding.Code = deduction.code;
                withholding.Type = DeductionWithholdingType;
                double amountWithHeld = 0;

                if (deduction.type == DeductionType.percentage)
                    amountWithHeld =  deduction.value * totalEarnings / 100;

                if ((deduction.type == DeductionType.flat) && (deduction.value > 0))
                    amountWithHeld =  deduction.value;

                totalDeductions += amountWithHeld;
                withholding.AmountWithheld = amountWithHeld;
                withholdings.Add(withholding);
            }

            return (totalDeductions, withholdings) ;

        }
    }
}
