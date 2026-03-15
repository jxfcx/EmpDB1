/////////////////////////////////////////////////////////////////////////////////////
// TINFO 200 B, Winter 2026
// UWTacoma SET, Jorge Francisco-Chavez, Michael Caroll
// 2026-03-02  - HourlyEmployee.cs
//
// Description - Contains HourlyEmployee class which extends Employee by adding
//               hourly wage and hours worked, data used to calculate earnings
//               for hourly employees.
/////////////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer -- Description
// 2026-03-02 -  Jorge  -    Imported HourlyEmployee class structure from Deitel
//                           book example                           
//                           
// 2026-03-02 -  Jorge  -    Updated constructor and ToString to include email address
//                           property from Employee base
//                                                     
// 2026-03-02 - Michael -    Updated ToString() to display hourly wage and hours
//                           worked in a user-friendly format
//
// 2026-03-02 - Michael -    Added ToStringForOutPutFile() to write raw data to the
//                           output file for saving and reloading employee records.
// 
// 2026-03-05 - Michael -    Rewrote ToString() to display salary in a user-friendly
//                           format and deleted old ToString() method.                    
//
/////////////////////////////////////////////////////////////////////////////////////

// Fig. 12.6: HourlyEmployee.cs
// HourlyEmployee class that extends Employee.
using System;

namespace EmpDB
{
    public class HourlyEmployee : Employee
    {
        private decimal wage { get; set; } // wage per hour
        private decimal hours { get; set; } // hours worked for the week

        // six-parameter constructor
        public HourlyEmployee(string firstName, string lastName,
           string socialSecurityNumber, string emailAddress, decimal hourlyWage,
           decimal hoursWorked)
           : base(firstName, lastName, socialSecurityNumber, emailAddress)
        {
            Wage = hourlyWage; // validate hourly wage 
            Hours = hoursWorked; // validate hours worked 
        }

        // property that gets and sets hourly employee's wage
        public decimal Wage
        {
            get
            {
                return wage;
            }
            set
            {
                if (value < 0) // validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(Wage)} must be >= 0");
                }

                wage = value;
            }
        }

        // property that gets and sets hourly employee's hours
        public decimal Hours
        {
            get
            {
                return hours;
            }
            set
            {
                if (value < 0 || value > 168) // validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(Hours)} must be >= 0 and <= 168");
                }

                hours = value;
            }
        }

        // calculate earnings; override Employee’s abstract method Earnings
        public override decimal Earnings()
        {
            if (Hours <= 40) // no overtime                          
            {
                return Wage * Hours;
            }
            else
            {
                return (40 * Wage) + ((Hours - 40) * Wage * 1.5M);
            }
        }

        // Friendly/pretty output statement for the object data - for display to the user
        public override string ToString()
        {
            // Declare a string to "build" using the data from the emp obj
            string str = base.ToString();
            // Formats HourlyWage as currency for readability
            str += $" Hourly Wage:{Wage:C}\n";
            str += $"Hours Worked:{Hours:F2}\n";

            // Now return the string
            return str;
        }

        // Prints the raw data info to the file for persistent storing
        public override string ToStringForOutputFile()
        {
            // Declare a string to "build" using the data from the emp obj
            string str = this.GetType().Name + "\n";
            str += base.ToStringForOutputFile() + "\n";
            // Saves HourlyWage as a fixed point for output file.
            str += $"{Wage:F2}\n";
            str += $"{Hours:F2}\n";

            // Now return the string
            return str;
        }

    }
}

/**************************************************************************
 * (C) Copyright 1992-2017 by Deitel & Associates, Inc. and               *
 * Pearson Education, Inc. All Rights Reserved.                           *
 *                                                                        *
 * DISCLAIMER: The authors and publisher of this book have used their     *
 * best efforts in preparing the book. These efforts include the          *
 * development, research, and testing of the theories and programs        *
 * to determine their effectiveness. The authors and publisher make       *
 * no warranty of any kind, expressed or implied, with regard to these    *
 * programs or to the documentation contained in these books. The authors *
 * and publisher shall not be liable in any event for incidental or       *
 * consequential damages in connection with, or arising out of, the       *
 * furnishing, performance, or use of these programs.                     *
 **************************************************************************/
