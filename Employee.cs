/////////////////////////////////////////////////////////////////////////////////////
// TINFO 200 B, Winter 2026
// UWTacoma SET, Jorge Francisco-Chavez, Michael Caroll
// 2026-03-02  - Employee.cs
//
// Description - 
//
/////////////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer -- Description
// 2026-03-02 -  Jorge  -    Imported Employee base class structure from Deitel book
//                           example
//
// 2026-03-08 -  Jorge  -    Added EmailAddress property to Employee class and updated
//                           constructor and ToString                         
//
// 2026-03-02 - Michael -    
//                           
//
/////////////////////////////////////////////////////////////////////////////////////


// Fig. 12.4: Employee.cs
// Employee abstract base class.

namespace EmpDB
{
    public abstract class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SocialSecurityNumber { get; set;  }
        public string EmailAddress { get; set; }

        // three-parameter constructor
        public Employee(string firstName, string lastName,
           string ssn, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            SocialSecurityNumber = ssn;
            EmailAddress = email;
        }

        // return string representation of Employee object, using properties
        //public override string ToString() => $"{FirstName} {LastName}\n" +
        //   $"social security number: {SocialSecurityNumber}\n" +
        //    $"email address: {EmailAddress}";


        // abstract method overridden by derived classes
        public abstract decimal Earnings(); // no implementation here

        // OUTPUT section - ways to get the data from the database
        // back to the user
        public override string ToString()
        {
            // declare a string to "build" using the data from the emp obj
            string str = "\n*********** Employee Record ***********\n";
            str += $" Type:{this.GetType().Name}\n";   // will show employee type
            str += $"First:{FirstName}\n";
            str += $" Last:{LastName}\n";
            str += $"  SSN:{SocialSecurityNumber}\n";
            str += $"Email:{EmailAddress}\n";

            // now return the string
            return str;
        }
        public virtual string ToStringForOutputFile()
        {
            // declare a string to "build" using the data from the emp obj
            string str = "\n*********** Employee Record ***********\n";
            str += $" Type:{this.GetType().Name}\n";   // will show employee type
            str += $"First:{FirstName}\n";
            str += $" Last:{LastName}\n";
            str += $"  SSN:{SocialSecurityNumber}\n";
            str += $"Email:{EmailAddress}\n";

            // now return the string
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
