using System;
using System.Collections.Generic;
using System.Text;

namespace TourLibrary
{
    public class AirCompanyNotFoundException : Exception
    {
        public AirCompanyNotFoundException(string message)
         : base(message)
        { }
    }
}
