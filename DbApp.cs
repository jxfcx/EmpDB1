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
// 2026-03-08 -  Jorge  -    Implemented ReadEmployeeDataFromInputFile() method to
//                           create Employee subclass objects from the file data and
//                           add them to the employee list.      
//
// 2026-03-09 -  Jorge  -    Implemented GoDatabase() main loop to run the console
//                           menu and handle user selections.
//                           
// 2026-03-09 -  Jorge  -    Added DisplayMenu() and GetUserSelection() for the
//                           console menu and user selections.
//                           
// 2026-03-09 -  Jorge  -    Created PrintAllRecords() method to display all employee
//                           records currently stored in the list
//                           
// 2026-03-09 -  Jorge  -    Implemented CreateNewEmployeeRecord() to create new
//                           employees of different subtypes
//                           
// 2026-03-09 - Michael -    Test 3
//                           
//
/////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Configuration;
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
                        UpdateEmployeeRecord();
                        break;

                    case 'D':
                    case 'd':
                        //[D]elete an existing employee record
                        Console.WriteLine("\nYou chose D for Delete an existing employee record");
                        DeleteEmployeeRecord();
                        break;

                    case 'R':
                    case 'r':
                        //[R]un payroll
                        Console.WriteLine("\nYou chose R for Run Payroll");
                        //RunPayroll();
                        break;

                    case 'E':
                    case 'e':
                        //[E]xit Exit the app -Saving all changes
                        Console.WriteLine("\nYou chose E for Exit the app - saving all changes");
                        SaveEmployeeDataToOutputFile();
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
                        SaveEmployeeDataToOutputFile();
                        break;
                    default:
                        Console.WriteLine($"\nERROR: {selection} is not a valid choice. Select again: ");
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
            string email = string.Empty;
            Employee emp = FindEmployeeRecord(out email);

            if (emp == null)
            {
                // Employee is NOT in the database - we can add them
                Console.WriteLine($"Creating new employee record for email: {email}");
                Console.Write("Enter first name: ");
                string firstName = Console.ReadLine();
                Console.Write("ENTER last name: ");
                string lastName = Console.ReadLine();
                Console.Write("ENTER social security number: ");
                string ssn = Console.ReadLine();

                // Asks for employee type
                Console.Write("[S]alaried, [H]ourly, [C]ommission, or [B]ase-Plus-Commission");
                char employeeType = GetUserSelection();

                // Salaried
                if (employeeType == 'S' || employeeType == 's')
                {
                    Console.WriteLine("\nEnter weekly salary: ");
                    decimal weeklySalary = decimal.Parse(Console.ReadLine());

                    // Create new SalariedEmployee object
                    emp = new SalariedEmployee(firstName, lastName, ssn, email, weeklySalary);
                    employees.Add(emp);
                    Console.WriteLine("New employee record created successfully!");
                }
                // Hourly
                else if (employeeType == 'H' || employeeType == 'h')
                {
                    Console.WriteLine("\nEnter hourly wage: ");
                    decimal hourlyWage = decimal.Parse(Console.ReadLine());

                    Console.WriteLine("\nEnter hours worked: ");
                    decimal hoursWorked = decimal.Parse(Console.ReadLine());

                    // Create new HourlyEmployee object
                    emp = new HourlyEmployee(firstName, lastName, ssn, email, hourlyWage, hoursWorked);
                    employees.Add(emp);
                    Console.WriteLine("New employee record created successfully!");
                }
                // Commission
                else if (employeeType == 'C' || employeeType == 'c')
                {
                    Console.WriteLine("\nEnter gross sales: ");
                    decimal grossSales = decimal.Parse(Console.ReadLine());

                    Console.WriteLine("\nEnter commission rate: ");
                    decimal commissionRate = decimal.Parse(Console.ReadLine());

                    // Create new CommissionEmployee object
                    emp = new CommissionEmployee(firstName, lastName, ssn, email, grossSales, commissionRate);
                    employees.Add(emp);
                    Console.WriteLine("New employee record created successfully!");
                }
                // Base Plus Commission
                else if (employeeType == 'B' || employeeType == 'b')
                {
                    Console.WriteLine("\nEnter gross sales: ");
                    decimal grossSales = decimal.Parse(Console.ReadLine());

                    Console.WriteLine("\nEnter commission rate: ");
                    decimal commissionRate = decimal.Parse(Console.ReadLine());

                    Console.WriteLine("\nEnter base salary: ");
                    decimal baseSalary = decimal.Parse(Console.ReadLine());

                    // Create new SalariedEmployee object
                    emp = new BasePlusCommissionEmployee(firstName, lastName, ssn, email, grossSales, commissionRate, baseSalary);
                    employees.Add(emp);
                    Console.WriteLine("New employee record created successfully!");
                }
                else
                {
                    // Employee type entered is not valid
                    Console.WriteLine($"ERROR: {employeeType} is not a valid entry");
                }
            }
        }

        private void UpdateEmployeeRecord()
        {
            string email = string.Empty;
            Employee emp = FindEmployeeRecord(out email);

            if (emp == null)
            {
                Console.WriteLine("Employee not found.");
                return;
            }

            Console.Write($"Current first name: [{emp.FirstName}] - Update first name (ENTER to keep current first name): ");
            // Input will replace current value as long as it is not null or white space
            // Otherwise, hitting enter will keep the value as is
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                emp.FirstName = input;

            // Update last name
            Console.Write($"Current last name: [{emp.LastName}] - Update last name (Enter to keep current last name): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                emp.LastName = input;

            // Update hourly employee
            if (emp is SalariedEmployee s)
            {
                // Update weekly salary
                Console.Write($"Current weekly salary: [{s.WeeklySalary}] - Update weekly salary (Enter to keep current weekly salary): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal newSalary) && newSalary >= 0)
                    s.WeeklySalary = newSalary;
            }
            else if (emp is HourlyEmployee h)
            {
                // Update hourly wage
                Console.Write($"Current hourly wage: [{h.Wage}] - Update hourly wage (Enter to keep current wage): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal newWage) && newWage >= 0)
                    h.Wage = newWage;

                // Update hours worked
                Console.Write($"Current hours worked: [{h.Hours}] - Update hours worked (Enter to keep current hours worked): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal newHours) && newHours >= 0)
                    h.Hours = newHours;
            }
            else if (emp is CommissionEmployee c)
            {
                // Update gross sales
                Console.Write($"Current gross sales: [{c.GrossSales}] - Update gross sales (Enter to keep current gross sales): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal newGrossSales) && newGrossSales >= 0)
                    c.GrossSales = newGrossSales;
                // Update commission rate
                Console.Write($"Current commission rate: [{c.CommissionRate}] - Update commission rate (Enter to keep current commission rate): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal newCommissionRate) && newCommissionRate >= 0)
                    c.CommissionRate = newCommissionRate;
            }
            else if (emp is BasePlusCommissionEmployee b)
            {
                // Update gross sales
                Console.Write($"Current gross sales: [{b.GrossSales}] - Update gross sales (Enter to keep current gross sales): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal newGrossSales) && newGrossSales >= 0)
                    b.GrossSales = newGrossSales;
                // Update commission rate
                Console.Write($"Current commission rate: [{b.CommissionRate}] - Update commission rate (Enter to keep current commission rate): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal newCommissionRate) && newCommissionRate >= 0)
                    b.CommissionRate = newCommissionRate;
                // Update base salary
                Console.Write($"Current base salary: [{b.BaseSalary}] - Update base salary (Enter to keep current base salary): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal newBaseSalary) && newBaseSalary >= 0)
                    b.BaseSalary = newBaseSalary;
            }

            // Updated record confirmation
            Console.WriteLine("Record updated successfully!");
        }

        private void DeleteEmployeeRecord()
        {
            string email = string.Empty;

            // Search for employee
            Employee emp = FindEmployeeRecord(out email);

            if (emp != null)
            {
                // Display employee record
                Console.WriteLine("\n*** Employee Record to be DELETED ***");
                Console.WriteLine(emp);

                //Ask for confirmation
                Console.Write("\nAre you sure you want to delete this record? (Y/N): ");
                char confirm = Console.ReadKey().KeyChar;
                Console.WriteLine();

                // Y to confirm
                if (confirm == 'Y' || confirm == 'y')
                {
                    // Deletes record
                    employees.Remove(emp);
                    Console.WriteLine($"Employee with email {email} has been deleted.");
                }
                else
                {
                    // Deletion cancelled
                    Console.WriteLine("Deletion cancelled");
                }

            }
            else
            {
                Console.WriteLine($"Employee with emial {email} does not exist.");
            }
        }

        private string EMPLOYEE_OUTPUTFILE = "__EMPLOYEE_OUTPUTFILE__.txt";

        private void SaveEmployeeDataToOutputFile()
        {
            // Make the file and associcated objects
            StreamWriter outFile = new StreamWriter(EMPLOYEE_OUTPUTFILE);

            // Use the file object - same as any other output stream
            foreach (Employee emp in employees)
            {
                //outFile.WriteLine(stu)
                outFile.Write(emp.ToStringForOutputFile()); // leave alone if u want pretty labels :3
                Console.WriteLine(emp); // also echo to shell
            }
            // Close the file reference - release the resource
            outFile.Close();

            File.Copy(EMPLOYEE_OUTPUTFILE, EMPLOYEE_INPUTFILE, true);
            Console.WriteLine($"\nAll records succesffuly saved to {EMPLOYEE_OUTPUTFILE}.");
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
[R]un payroll
[E]xit      Exit the app - Saving all changes
[Q]uit      Quit the app - Discard changes
[S]ave all changes and continue the app
***********************************************
User Selection: ");
        }
    }
}