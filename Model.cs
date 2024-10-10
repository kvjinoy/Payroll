namespace Payroll
{

    public class Deduction
    {
        public string code { get; set; }
        public DeductionType type { get; set; }
        public int priority { get; set; }
        public int value { get; set; }
        public bool isPreTax { get; set; }
    }

    public class Earning
    {
        public string code { get; set; }
        public EarningType type { get; set; }
        public int hours { get; set; }
        public int rate { get; set; }
        public bool isTaxable { get; set; }
        public int? amount { get; set; }
    }

    public class Input
    {
        public List<Earning> earnings { get; set; }
        public List<Deduction> deductions { get; set; }
        public List<Tax> taxes { get; set; }
    }

    public class Output
    {
        public double GrossPay { get; set; }
        public double GrossTaxableEarnings { get; set; }
        public List<Withholding> Withholdings { get; set; }
        public double NetPay { get; set; }
    }

    public class Root
    {
        public Input input { get; set; }
        public Output output { get; set; }
    }

    public class Tax
    {
        public TaxCode code { get; set; }
        public TaxType type { get; set; }
        public int priority { get; set; }
        public double value { get; set; }
        public int? cap { get; set; }
    }

    public class Withholding
    {
        public string Type { get; set; }
        public string Code { get; set; }
        public double AmountWithheld { get; set; }
        public double Deficit { get; set; }
    }


}
