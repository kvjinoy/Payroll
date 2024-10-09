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

        public double CalculateNetPay(List<Earning> earnings, List<Deduction> deductions, List<Tax> taxes)
        {

            var taxableEarnings = _earningsCalculator.GetEarnings(earnings.Where(e => e.isTaxable == true).ToList());
            var nonTaxableEarnings = _earningsCalculator.GetEarnings(earnings.Where(e => e.isTaxable = false).ToList());
            var preTaxDeductions = _deductionsCalculator.GetDeductions(deductions.Where(d=>d.isPreTax == true).ToList(), taxableEarnings);

            var taxableAmount = taxableEarnings - preTaxDeductions;

            var taxToPay = _taxCalculator.GetTax(taxes, taxableAmount);

            var amountForPostTaxDeduction = taxableAmount - preTaxDeductions - taxToPay;
            var postTaxDeductions = _deductionsCalculator.GetDeductions(deductions.Where(d => d.isPreTax == false).ToList(), amountForPostTaxDeduction);

            var netPay = taxableEarnings - preTaxDeductions - taxToPay - postTaxDeductions + nonTaxableEarnings;

            return netPay;
        }

        public Output CalculatePayroll(Input input)
        {
            var output = new Output();
            return output;

            //TODO Deduction and tax priority   and assign Withholding to Output 
        }
    }
}
