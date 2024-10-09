using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public interface ITaxCalculator
    {
        double GetTax(List<Tax> taxes, double taxableAmount);
    }

    internal class TaxCalculator : ITaxCalculator
    {

        public double GetTax(List<Tax> taxes, double taxableAmount)
        {
            double totalTaxes = 0;

            foreach (var tax in taxes)
            {
                if (tax.type == TaxType.percentage)
                    totalTaxes += tax.value * taxableAmount / 100;

                if (tax.type == TaxType.cappedPercentage)
                {
                    var cappedTaxamount = tax.value * taxableAmount / 100;
                    if (cappedTaxamount > tax.cap)
                        cappedTaxamount = tax.cap.Value;
                    totalTaxes += cappedTaxamount;
                }
            }

            return totalTaxes;
        }


    }
}
