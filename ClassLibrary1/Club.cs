using System;
using System.Collections.Generic;
using System.Text;

namespace TourLibrary
{
    public class Club
    {
        public string _Name { get; private set; }
        public List<Account> RealAccounts { get; set; }
        public List<InterestingCountry> InterestingCountryTours { get; set; }
        public List<TematicTours> TematicToursTours { get; set; }
        public List<SightseeingTours> SightseeingToursTours { get; set; }
        public List<Agency> PartnerAgencies { get; set; }

        public Club(string Name)
        {
            _Name = Name;
            RealAccounts = new List<Account>();
            InterestingCountryTours = new List<InterestingCountry>();
            TematicToursTours = new List<TematicTours>();
            SightseeingToursTours = new List<SightseeingTours>();
            PartnerAgencies = new List<Agency>();
        }
        public Agency [] FindAgency(PersonalTour LikeTour)
        {
            Agency[] AvailableAgencies = new Agency[0];
            for (int m = 0; m < PartnerAgencies.Count; m++)
            {
                for (int j = 0; j < PartnerAgencies[m]._ProvidedTours.Length; j++)
                {
                    if (PartnerAgencies[m]._ProvidedTours[j].Path == LikeTour.Path)
                    {
                        Array.Resize(ref AvailableAgencies, AvailableAgencies.Length + 1);
                        AvailableAgencies[AvailableAgencies.Length - 1] = PartnerAgencies[m];
                        break;
                    }
                }
            }
            return AvailableAgencies;
        }
    }
}
