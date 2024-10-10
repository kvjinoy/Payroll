using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public interface ITaxCalculator
    {
        (double TaxAmount, List<Withholding> Withholdings) GetTax(List<Tax> taxes, double taxableAmount);
    }

    internal class TaxCalculator : ITaxCalculator
    {
        const string TaxWithholdingType = "tax";

        public (double TaxAmount, List<Withholding> Withholdings) GetTax(List<Tax> taxes, double taxableAmount)
        {
            double totalTaxes = 0;
            List<Withholding> withholdings = new List<Withholding>();

            foreach (var tax in taxes)
            {

                var withholding = new Withholding();
                withholding.Code = tax.code.ToString();
                withholding.Type = TaxWithholdingType;
                double amountWithHeld = 0;


                if (tax.type == TaxType.percentage)
                    amountWithHeld = tax.value * taxableAmount / 100;

                if (tax.type == TaxType.cappedPercentage)
                {
                    var cappedTaxamount = tax.value * taxableAmount / 100;
                    if (cappedTaxamount > tax.cap)
                        cappedTaxamount = tax.cap.Value;
                    amountWithHeld = cappedTaxamount;
                }

                totalTaxes += amountWithHeld;

            }

            return (totalTaxes, withholdings);
        }


    }
}
