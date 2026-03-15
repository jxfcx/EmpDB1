/////////////////////////////////////////////////////////////////////////////////////
// TINFO 200 B, Winter 2026
// UWTacoma SET, Jorge Francisco-Chavez, Michael Caroll
// 2026-03-02  - Program.cs
//
// Description - Program file, entry point for the EmpDB application. Instantiates
//               DBApp and starts the main database loop
/////////////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer -- Description
// 2026-03-02 -  Jorge  -    Created Program entry point for EmpDB application.
//                           
// 2026-03-02 - Jorge -    Instantiated DbApp and connected it to GoDataBases()
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
        // constant flag to turn debugging on and off easily
        public const bool _DEBUG_MODE_ = false;
        static void Main(string[] args)
        {
            // Create the database application instance and start the main menu loop.
            DbApp db = new DbApp(); // There will only ever be one of these
            db.GoDatabase();
        }
    }
}
