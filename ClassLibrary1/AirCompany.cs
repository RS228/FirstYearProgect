using System;
using System.Collections.Generic;
using System.Text;

namespace TourLibrary
{
    public class AirCompany
    {
        public string _Name { get; private set; }
        public string[] _Path { get; private set; }
        public int[] _DaysOfWork { get; private set; }
        public AirCompany(string Name, string[] Path, int[] DaysOfWork)
        {
            _Name = Name;
            _Path = Path;
            _DaysOfWork = DaysOfWork;
        }
        public AirCompany() : this(null, null, null)
        {
        }

    }
}
