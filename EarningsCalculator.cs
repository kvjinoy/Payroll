using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public interface IEarningsCalculator
    {
        double GetEarnings(List<Earning> earnings);
    }

    internal class EarningsCalculator : IEarningsCalculator
    {
        public double GetEarnings(List<Earning> earnings)
        {

            double totalEarnings = 0;
            foreach (var earning in earnings)
            {
                if (earning.type == EarningType.hourly)
                    totalEarnings += earning.rate * earning.hours;

                if ((earning.type == EarningType.salary) && (earning.amount > 0))
                    totalEarnings += earning.amount.Value;

            }
            return totalEarnings;

        }
    }
}
