using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class PayrollCalculator
    {
        private IDeductionsCalculator _deductionsCalculator;
        private IEarningsCalculator _earningsCalculator;
        private ITaxCalculator _taxCalculator;

        public PayrollCalculator(IDeductionsCalculator deductionsCalculator,
            IEarningsCalculator earningsCalculator, ITaxCalculator taxCalculator) {
            _deductionsCalculator = deductionsCalculator;
            _earningsCalculator = earningsCalculator;
            _taxCalculator = taxCalculator;
        }

        private Output CalculateNetPay(List<Earning> earnings, List<Deduction> deductions, List<Tax> taxes)
        {

            var taxableEarnings = _earningsCalculator.GetEarnings(earnings.Where(e => e.isTaxable == true).ToList());
            var nonTaxableEarnings = _earningsCalculator.GetEarnings(earnings.Where(e => e.isTaxable = false).ToList());
            var preTaxDeductions = _deductionsCalculator.GetDeductions(deductions.Where(d=>d.isPreTax == true).ToList(), taxableEarnings);

            var taxableAmount = taxableEarnings - preTaxDeductions.DeductionAmount;

            var taxesToPay = _taxCalculator.GetTax(taxes, taxableAmount);

            var amountForPostTaxDeduction = taxableAmount - preTaxDeductions.DeductionAmount - taxesToPay.TaxAmount;
            var postTaxDeductions = _deductionsCalculator.GetDeductions(deductions.Where(d => d.isPreTax == false).ToList(), amountForPostTaxDeduction);

            var netPay = taxableEarnings - preTaxDeductions.DeductionAmount - taxesToPay.TaxAmount - postTaxDeductions.DeductionAmount  + nonTaxableEarnings;

            var output = new Output();
            output.GrossPay = taxableEarnings + nonTaxableEarnings;
            output.GrossTaxableEarnings = taxableAmount;
            output.NetPay = netPay;
            var withholdings = new List<Withholding>();
            withholdings.AddRange(preTaxDeductions.Withholdings);
            withholdings.AddRange(postTaxDeductions.Withholdings);
            withholdings.AddRange(taxesToPay.Withholdings);
            output.Withholdings = withholdings;

            return output;
        }

        public Output CalculatePayroll(Input input)
        {
            var output = CalculateNetPay(input.earnings, input.deductions, input.taxes);
            return output;

            //TODO Deduction and tax priority 
        }
    }
}
