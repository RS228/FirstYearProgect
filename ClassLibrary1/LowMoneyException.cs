using System;
using System.Collections.Generic;
using System.Text;

namespace TourLibrary
{
    public class LowMoneyException : Exception
    {
        public LowMoneyException(string message)
         : base(message)
        { }
    }
}
