/////////////////////////////////////////////////////////////////////////////////////
// TINFO 200 B, Winter 2026
// UWTacoma SET, Jorge Francisco-Chavez, Michael Caroll
// 2026-03-02  - Program.cs
//
// Description - 
//
/////////////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer -- Description
// 2026-03-02 -  Jorge  -    Created Program entry point for EmpDB application.
//                           
// 2026-03-02 - Michael -    
//                           
//
/////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create the database application instance and start the main menu loop.
            DbApp db = new DbApp(); // There will only ever be one of these
            //db.GoDatabase();
        }
    }

}
