using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class PayrollCalculator
    {
        public double GetEarnings(List<Earning> earnings)
        {

            double totalEarnings = 0;
            foreach (var earning in earnings)
            {
                if(earning.type== EarningType.hourly)
                    totalEarnings += earning.rate * earning.hours;

                if ((earning.type == EarningType.salary) &&(earning.amount >  0))
                    totalEarnings += earning.amount.Value;

            }
            return totalEarnings;

        }

        public double GetPreTaxDeductions(List<Deduction> deductions, double totalEarnings)
        {

            double totalDeductions = 0;
            foreach (var deduction in deductions)
            {
                if (deduction.type == DeductionType.percentage)
                    totalDeductions += deduction.value  * totalEarnings / 100;

                if ((deduction.type == DeductionType.flat) && (deduction.value > 0))
                    totalDeductions += deduction.value;

            }
            return totalDeductions;

        }


        public double GetTax(List<Tax> taxes, double taxableAmount)
        {
            double totalTaxes = 0;

            foreach (var tax in taxes)
            {
                if (tax.type  == TaxType.percentage)
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


        public double CalculateNetPay(List<Earning> earnings, List<Deduction> deductions, List<Tax> taxes)
        {

            var taxableEarnings = GetEarnings(earnings.Where(e => e.isTaxable == true).ToList());
            var nonTaxableEarnings = GetEarnings(earnings.Where(e => e.isTaxable = false).ToList());
            var preTaxDeductions = GetPreTaxDeductions(deductions.Where(d=>d.isPreTax == true).ToList(), taxableEarnings);

            var taxableAmount = taxableEarnings - preTaxDeductions;

            var taxToPay = GetTax(taxes, taxableAmount);

            var amountForPostTaxDeduction = taxableAmount - preTaxDeductions - taxToPay;
            var postTaxDeductions = GetPreTaxDeductions(deductions.Where(d => d.isPreTax == false).ToList(), amountForPostTaxDeduction);

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
