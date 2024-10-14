**Payroll Calculator Demo**

The primary goal is to calculate the net pay for an employee, considering various earnings, deductions, and taxes. The challenge lies in the priority of deductions and taxes, especially when taxable earnings are insufficient to cover all withholdings.


**1. Data Structure:**

Employee:
ID
Name
Earnings (Salary, Hourly)
Deductions (Pre-tax, Post-tax)
Taxes
**Deduction/Tax:**
Code
Type (Flat, Percentage)
Amount/Rate
Priority
Cap (if applicable)
**2. Calculation Steps:**

**Calculate Gross Pay:**
Sum of salary and hourly earnings.
Determine Taxable Earnings:
Gross Pay minus non-taxable earnings.
Calculate Pre-Tax Deductions:
Apply deductions based on taxable earnings, considering priority and caps.
Calculate Taxes:
Apply taxes based on taxable earnings minus pre-tax deductions, considering priority and caps.
Calculate Post-Tax Deductions:
Apply deductions based on taxable earnings minus pre-tax deductions and taxes.
Calculate Net Pay:
Gross Pay minus pre-tax deductions, taxes, and post-tax deductions, plus non-taxable earnings.
**3. Handling Insufficient Funds:**

**Prioritize Deductions/Taxes:**
If taxable earnings are insufficient to cover all withholdings, apply them in order of priority.
Record Deficits:
For any deduction or tax that cannot be withheld in full, record the deficit.
**4. Output:**

**Gross Pay:** Total earnings before deductions and taxes.
**Net Pay:** Amount paid to the employee after deductions and taxes.
**Withheld Amounts:** Deductions and taxes by code, including any deficits.
