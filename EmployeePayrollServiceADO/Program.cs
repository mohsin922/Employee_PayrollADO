using System;

namespace EmployeePayrollServiceADO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Payroll Services using ADO!");
            //Create object for Employee Repository
            EmployeeRepository employeeRepository = new EmployeeRepository();
            EmployeePayroll employeePayroll = new EmployeePayroll();

            employeePayroll.Name = "Zuhaib";
            employeePayroll.PhoneNumber = 324674932213;
            employeePayroll.Address = "MandirBagh";
            employeePayroll.Department = "Civil";
            employeePayroll.Gender = 'M';
            employeePayroll.BasicPay = 22000.00;
            employeePayroll.Deduction = 1500.00;
            employeePayroll.TaxablePay = 200.00;
            employeePayroll.IncomeTax = 300;
            employeePayroll.NetPay = 25000.00;


            //employeeRepository.AddEmployeeDetails(employeePayroll);

           // employeeRepository.GetSqlData();
            employeeRepository.UpdateSalary();
        }

    }
    
}
