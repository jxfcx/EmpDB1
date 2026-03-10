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
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

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
        // Main loop running the CRUD operations
        public void GoDatabase()
        {
            while (true)
            {
                DisplayMenu();

                char selection = GetUserSelection();

                switch (selection)
                {
                    case 'C':
                    case 'c':
                        //[C]reate a new employee record
                        Console.WriteLine("\nYou chose C for Create a new employee");
                        CreateNewEmployeeRecord();
                        break;

                    case 'F':
                    case 'f':
                        //[F] ind a single existing employee record
                        {
                            Console.WriteLine("\nYou chose F for Find a record. ");

                            string email;
                            Employee found = FindEmployeeRecord(out email);

                            if (found != null)
                            {
                                Console.WriteLine(found);
                            }
                        }
                        break;

                    case 'P':
                    case 'p':
                        //[P]rint all employee records
                        Console.WriteLine("\nYou chose P for Print all employee records");
                        PrintAllRecords();
                        break;

                    case 'U':
                    case 'u':
                        //[U]pdate an existing employee record
                        Console.WriteLine("\nYou chose U for Update an existing employee record");
                        //UpdateEmployeeRecord();
                        break;

                    case 'D':
                    case 'd': 
                        //[D]elete an existing employee record
                        Console.WriteLine("\nYou chose D for Delete an existing employee record");
                        //DeleteEmployeeRecord();
                        break;

                    case 'E':
                    case 'e':
                        //[E]xit Exit the app -Saving all changes
                        Console.WriteLine("\nYou chose E for Exit the app - saving all changes");
                        //SaveStudentDataToOutputFile();
                        Console.WriteLine("\nExiting application. Changes saved.");
                        Environment.Exit(0);
                        break;

                    case 'Q':
                    case 'q':
                        //[Q]uit Quit the app -Discard changes
                        Console.WriteLine("\nYou chose Q for Quit the app - discarding all changes");
                        Console.WriteLine("\nQuitting application. Changes discarded.");
                        Environment.Exit(0);
                        break;

                    case 'S':
                    case 's':
                        //[S]ave all changes and continue the app
                        Console.WriteLine("\nYou chose S for Save all changes and continue the app");
                        //SaveStudentDataToOutputFile();
                        break;
                }
            }
        }

        private void PrintAllRecords()
        {
            foreach (Employee emp in employees)
            {
                Console.WriteLine(emp);
                Console.WriteLine();
            }
        }

        private Employee FindEmployeeRecord(out string email)
        {
            Console.Write("\nENTER the email address (primary key) to search for: ");
            email = Console.ReadLine();

            foreach (Employee emp in employees)
            {
                if (email == emp.EmailAddress)
                {
                    Console.WriteLine($"\nFOUND email address: {emp.EmailAddress}\n");
                    Console.WriteLine();
                    return emp;
                }
            }
            Console.WriteLine($"\n{email} NOT FOUND.");
            return null;
        }

        private void CreateNewEmployeeRecord()
        {

        }

        private char GetUserSelection()
        {
            ConsoleKeyInfo key = Console.ReadKey();
            return key.KeyChar;
        }

        // Main menu with CRUD options displayed
        private void DisplayMenu()
        {
            Console.Write(@"
***********************************************
********* Employee Database Main Menu *********
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
[C]reate a new employee record
[F]ind a single existing employee record
[P]rint all employee records 
[U]pdate an existing employee record
[D]elete an existing employee record
[E]xit      Exit the app - Saving all changes
[Q]uit      Quit the app - Discard changes
[S]ave all changes and continue the app
***********************************************
User Selection: ");
        }
    }
}