using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public interface IDeductionsCalculator
    {
        double GetDeductions(List<Deduction> deductions, double totalEarnings);
    }

    internal class DeductionsCalculator : IDeductionsCalculator
    {
        public double GetDeductions(List<Deduction> deductions, double totalEarnings)
        {

            double totalDeductions = 0;
            foreach (var deduction in deductions)
            {
                if (deduction.type == DeductionType.percentage)
                    totalDeductions += deduction.value * totalEarnings / 100;

                if ((deduction.type == DeductionType.flat) && (deduction.value > 0))
                    totalDeductions += deduction.value;

            }
            return totalDeductions;

        }
    }
}
