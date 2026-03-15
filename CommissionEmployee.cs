/////////////////////////////////////////////////////////////////////////////////////
// TINFO 200 B, Winter 2026
// UWTacoma SET, Jorge Francisco-Chavez, Michael Caroll
// 2026-03-02  - CommissionEmployee.cs
//
// Description - Contains the CommissionEmployee class which extends Employee by
//               adding gross sales and comission rate data used to calculate
//               earnings for commission-based employees.
/////////////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer -- Description
// 2026-03-02 -  Jorge  -    Imported CommissionEmployee class structure from Dietel
//                           book example
//
// 2026-03-02 -  Jorge  -    Updated constructor and ToString to include email address
//                           property from Employee base
//                              
// 2026-03-02 - Michael -    Added ToStringForOutPutFile() to write raw data to the
//                           output file for saving and reloading employee records.
//
// 2026-03-05 - Michael -    Rewrote ToString() to display gross sales and comission
//                           rate in a user-friendly format and deleted old
//                           ToString() method.
//
/////////////////////////////////////////////////////////////////////////////////////

// Fig. 12.7: CommissionEmployee.cs
// CommissionEmployee class that extends Employee.
using System;

namespace EmpDB
{
    public class CommissionEmployee : Employee
    {
        private decimal grossSales { get; set; }// gross weekly sales
        private decimal commissionRate { get; set; } // commission percentage

        // six-parameter constructor
        public CommissionEmployee(string firstName, string lastName,
           string socialSecurityNumber, string emailAddress, decimal grossSales,
           decimal commissionRate)
           : base(firstName, lastName, socialSecurityNumber, emailAddress)
        {
            GrossSales = grossSales; // validates gross sales
            CommissionRate = commissionRate; // validates commission rate
        }

        // property that gets and sets commission employee's gross sales
        public decimal GrossSales
        {
            get
            {
                return grossSales;
            }
            set
            {
                if (value < 0) // validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(GrossSales)} must be >= 0");
                }

                grossSales = value;
            }
        }

        // property that gets and sets commission employee's commission rate
        public decimal CommissionRate
        {
            get
            {
                return commissionRate;
            }
            set
            {
                if (value <= 0 || value >= 1) // validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(CommissionRate)} must be > 0 and < 1");
                }

                commissionRate = value;
            }
        }

        // calculate earnings; override abstract method Earnings in Employee
        public override decimal Earnings() => CommissionRate * GrossSales;

        public override string ToString()
        {
            // Declare a string to "build" using the data from the emp obj
            string str = base.ToString();
            // Formats GrossSales as currency for readability
            str += $"Gross sales: {GrossSales:C}\n";
            str += $"Commission Rate: {CommissionRate:F2}\n";

            // Now return the string
            return str;
        }

        // Prints the raw data info to the file for persistent storing
        // so records can be saved and loaded again later.
        public override string ToStringForOutputFile()
        {
            // Declare a string to "build" using the data from the emp obj
            string str = this.GetType().Name + "\n";
            str += base.ToStringForOutputFile() + "\n";
            // Saves GrossSales as a fixed point for output file.
            str += $"{GrossSales:F2}\n";
            str += $"{CommissionRate:F2}\n";
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
