using System;
using System.Collections.Generic;
using System.Text;

namespace TourLibrary
{
    public class Agency
    {
        public string _Name { get; private set; }
        public AirCompany[] _PartnerСompanies { get; private set; }
        public Tour[] _ProvidedTours { get; private set; }
        public Agency(string Name, AirCompany[] PartnerСompanies, Tour[] ProvidedTours)
        {
            _Name = Name;
            _PartnerСompanies = PartnerСompanies;
            _ProvidedTours = ProvidedTours;
        }
        public AirCompany[]  FindAirCompany(Tour LikeTour)
        {
            AirCompany[] AvailableCompanies = new AirCompany[0];
            for (int i = 0; i < _PartnerСompanies.Length; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (_PartnerСompanies[i]._DaysOfWork[j] == LikeTour.DateOfTour.Day)
                    {
                        for (int k = 0; k < _PartnerСompanies[i]._Path.Length; k++)
                        {
                            if (_PartnerСompanies[i]._Path[k] == LikeTour.Path)
                            {

                                Array.Resize(ref AvailableCompanies, AvailableCompanies.Length + 1);
                                AvailableCompanies[AvailableCompanies.Length - 1] = _PartnerСompanies[i];
                                
                            }
                        }

                    }

                }

            }
            return AvailableCompanies;
        }

    }
}
