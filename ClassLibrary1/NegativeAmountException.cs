using System;
using System.Collections.Generic;
using System.Text;

namespace TourLibrary
{
    public class NegativeAmountException:Exception
    {
            public NegativeAmountException(string message)
             : base(message)
            { }
    }
}

