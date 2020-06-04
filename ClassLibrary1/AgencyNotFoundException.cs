using System;

namespace TourLibrary
{
    public class AgencyNotFoundException : Exception
    {
        public AgencyNotFoundException(string message)
         : base(message)
        { }
    }
}
