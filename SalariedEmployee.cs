/////////////////////////////////////////////////////////////////////////////////////
// TINFO 200 B, Winter 2026
// UWTacoma SET, Jorge Francisco-Chavez, Michael Caroll
// 2026-03-02  - SalariedEmployee.cs
//
// Description - Contains the SalariedEmployee class which extends Employee by adding
//               weekly salary data used to calculate earnings for salaried employees.
/////////////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer -- Description
// 2026-03-02 -  Jorge  -    Imported SalariedEmployee class structure from Deitel
//                           book example
//                           
// 2026-03-02 -  Jorge  -    Updated constructor and ToString to include email address
//                           property from Employee base
//                              
// 2026-03-02 - Michael -    Added ToStringForOutPutFile() to write raw data to the
//                           output file for saving and reloading employee records.
// 
// 2026-03-05 - Michael -    Rewrote ToString() to display salary in a user-friendly
//                           format and deleted old ToString() method.
//
/////////////////////////////////////////////////////////////////////////////////////


// Fig. 12.5: SalariedEmployee.cs
// SalariedEmployee class that extends Employee.
using System;

namespace EmpDB
{
    public class SalariedEmployee : Employee
    {
        private decimal weeklySalary { get; set; }
        // five-parameter constructor
        public SalariedEmployee(string firstName, string lastName,
           string socialSecurityNumber, string emailAddress, decimal weeklySalary)
           : base(firstName, lastName, socialSecurityNumber, emailAddress)
        {
            WeeklySalary = weeklySalary; // validate salary via property
        }

        // property that gets and sets salaried employee's salary
        public decimal WeeklySalary
        {
            get
            {
                return weeklySalary;
            }
            set
            {
                if (value < 0) // validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(WeeklySalary)} must be >= 0");
                }

                weeklySalary = value;
            }
        }

        // calculate earnings; override abstract method Earnings in Employee
        public override decimal Earnings() => WeeklySalary;

        // Friendly/pretty output statement for the object data - for display to the user
        public override string ToString()
        {
            // Declare a string to "build" using the data from the emp obj
            string str = base.ToString();
            // Formats WeeklySalary as currency for readability
            str += $"weekly salary: {WeeklySalary:C}\n";

            // Now return the string
            return str;
        }

        // Prints the raw data info to the file for persistent storing
        public override string ToStringForOutputFile()
        {
            // Declare a string to "build" using the data from the emp obj
            string str = this.GetType().Name + "\n";
            str += base.ToStringForOutputFile() + "\n";
            // Saves WeeklySalary as a fixed point for output file.
            str += $"{WeeklySalary:F2}\n";

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
