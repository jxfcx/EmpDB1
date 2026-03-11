/////////////////////////////////////////////////////////////////////////////////////
// TINFO 200 B, Winter 2026
// UWTacoma SET, Jorge Francisco-Chavez, Michael Caroll
// 2026-03-02  - BasePlusCommissionEmployee.cs
//
// Description - 
//
/////////////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer -- Description
// 2026-03-02 -  Jorge  -    Imported BasePlusCommissionEmployee class structure from
//                           Deitel book example.
//                           
// 2026-03-02 -  Jorge  -    Updated constructor and ToString to include email address
//                           property from Employee base
//                              
// 2026-03-02 - Michael -    
//                           
//
/////////////////////////////////////////////////////////////////////////////////////


// Fig. 12.8: BasePlusCommissionEmployee.cs
// BasePlusCommissionEmployee class that extends CommissionEmployee.
using System;

namespace EmpDB
{
    public class BasePlusCommissionEmployee : CommissionEmployee
    {
        private decimal baseSalary { get; set; } // base salary per week

        // six-parameter constructor
        public BasePlusCommissionEmployee(string firstName, string lastName,
           string socialSecurityNumber, string emailAddress, decimal grossSales,
           decimal commissionRate, decimal baseSalary)
           : base(firstName, lastName, socialSecurityNumber, emailAddress,
                grossSales, commissionRate)
        {
            BaseSalary = baseSalary; // validates base salary
        }

        // property that gets and sets 
        // BasePlusCommissionEmployee's base salary
        public decimal BaseSalary
        {
            get
            {
                return baseSalary;
            }
            set
            {
                if (value < 0) // validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(BaseSalary)} must be >= 0");
                }

                baseSalary = value;
            }
        }

        // calculate earnings
        public override decimal Earnings() => BaseSalary + base.Earnings();

        //// return string representation of BasePlusCommissionEmployee
        //public override string ToString() =>
        //   $"base-salaried {base.ToString()}\nbase salary: {BaseSalary:C}";
        public override string ToString()
        {
            // Declare a string to "build" using the data from the emp obj
            string str = base.ToString();
            // Formats Tuitioncredit as currency for readability
            //str += $"Gross sales: {GrossSales:C}\n";
            //str += $"Commission Rate: {CommissionRate:F2}\n";
            str += $"Base Salary: {BaseSalary:C}\n";

            // Now return the string
            return str;
        }

        // Prints the raw data info to the file for persistent storing
        public override string ToStringForOutputFile()
        {
            // Declare a string to "build" using the data from the emp obj
            string str = this.GetType().Name + "\n";
            str += $"{FirstName}\n";
            str += $"{LastName}\n";
            str += $"{SocialSecurityNumber}\n";
            str += $"{EmailAddress}\n";
            str += $"{GrossSales:F2}\n";
            str += $"{CommissionRate:F2}\n";
            str += $"{BaseSalary:F2}\n";
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
