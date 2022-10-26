using System.Collections.Generic;
using System.Linq;

namespace json_treatment.Models
{
    public class ScenarioConfiguration
    {
        public ScenarioConfiguration(List<Customer> customers, List<Brand> brands, List<WagerType> wagerTypes, List<Sport> sports, List<League> leagues)
        {
            Customers = customers;
            Brands = brands;
            WagerTypes = wagerTypes;
            Sports = sports;
            Leagues = leagues;
        }

        public List<Customer> Customers { get; set; }
        public List<Brand> Brands { get; set; }
        public List<WagerType> WagerTypes { get; set; }
        public List<Sport> Sports { get; set; }
        public List<League> Leagues { get; set; }


        public bool WithoutCustomer() => Customers == null;
        public bool WithoutBrand() => Brands == null;
        public bool WithoutSport() => Sports == null;
        public bool WithoutLeague() => Leagues == null;
        public bool WithoutWagerType() => WagerTypes == null;


        public bool MatchCustomer(string currentCustomer) => Customers != null && Customers.Any(x => x.Id == currentCustomer);
        public bool MatchWagerType(string currentWagerType) => WagerTypes != null && WagerTypes.Any(x => x.Id == currentWagerType);
        public bool MatchSport(string currentSport) => Sports != null && Sports.Any(x => x.Id == currentSport);
        public bool MatchLeague(string currentLeague) => Leagues != null && Leagues.Any(x => x.Id == currentLeague);
        public bool MatchBrand(string currentBrand) => Brands != null && Brands.Any(x => x.Id == currentBrand);
    }
}
