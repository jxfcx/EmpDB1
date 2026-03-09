/////////////////////////////////////////////////////////////////////////////////////
// TINFO 200 B, Winter 2026
// UWTacoma SET, Jorge Francisco-Chavez, Michael Caroll
// 2026-03-02  - DbApp.cs
//
// Description - 
//
/////////////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer -- Description
// 2026-03-02 -  Jorge  -    Created DBApp class as the main application class for
//                           the EmpDB project.                         
//
// 2026-03-08 -  Jorge  -    Added List<Employee> to store employee objects in memory
//                           and serve as the database for the application.
//
// 2026-03-08 -  Jorge  -    Added constructor to read and load employee data from
//                           an input file at startup.
//                           
// 2026-03-08 -  Jorge  -    Implemented ReadEmployeeDataFromInputFile method to
//                           create Employee objects from the file data and add them
//                           to the employee list.      
//
// 2026-03-02 - Michael -    Test 3
//                           
//
/////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;

namespace EmpDB
{
    internal class DbApp
    {
        // Raw storage of the employee records. 
        // 
        private List<Employee> employees = new List<Employee>(); // List to store employee records

        public DbApp() 
        {
            ReadEmployeeDataFromInputFile();
        }

        private const string EMPLOYEE_INPUTFILE = "__EMPLOYEE_INPUTFILE__.txt";
        private void ReadEmployeeDataFromInputFile()
        {
            StreamReader inFile = new StreamReader(EMPLOYEE_INPUTFILE);

            // First line of each record will specify employee type
            string employeeType = string.Empty;

            //
            while ((employeeType = inFile.ReadLine()) != null)
            {
                string first = inFile.ReadLine();
                string last = inFile.ReadLine();
                string ssn = inFile.ReadLine();
                string email = inFile.ReadLine();

                if (employeeType == "HourlyEmployee")
                {
                    decimal wage = decimal.Parse(inFile.ReadLine());
                    decimal hours = decimal.Parse(inFile.ReadLine());

                    Employee hourly = new HourlyEmployee(first, last, ssn, email, wage, hours);
                    employees.Add(hourly);
                }
                else if (employeeType == "CommissionEmployee")
                {
                    decimal grossSales = decimal.Parse(inFile.ReadLine());
                    decimal commissionRate = decimal.Parse(inFile.ReadLine());

                    Employee commision = new CommissionEmployee(first, last, ssn, email, grossSales, commissionRate);
                    employees.Add(commision);
                }
                else if (employeeType == "SalariedEmployee")
                {
                    decimal weeklySalary = decimal.Parse(inFile.ReadLine());

                    Employee salaried = new SalariedEmployee(first, last, ssn, email, weeklySalary);
                    employees.Add(salaried);
                }
                else if (employeeType == "BasePlusCommissionEmployee")
                {
                    decimal grossSales = decimal.Parse(inFile.ReadLine());
                    decimal commissionRate = decimal.Parse(inFile.ReadLine());
                    decimal baseSalary = decimal.Parse(inFile.ReadLine());

                    Employee basePlus = new BasePlusCommissionEmployee(first, last, ssn, email, grossSales, 
                        commissionRate, baseSalary);
                    employees.Add(basePlus);
                }
                else
                {
                    Console.WriteLine($"ERROR: {employeeType} is not a valid employee type.");
                }
            }
            inFile.Close();
        }
    }
}