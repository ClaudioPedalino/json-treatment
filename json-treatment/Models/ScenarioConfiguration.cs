using System.Collections.Generic;

namespace json_treatment.Models
{
    public class ScenarioConfiguration
    {
        public ScenarioConfiguration(List<Customer> customers,
                                     List<Brand> brands,
                                     List<WagerType> wagerTypes,
                                     List<Sport> sports,
                                     List<League> leagues)
        {
            Customers = customers;
            Brands = brands;
            WagerTypes = wagerTypes;
            Sports = sports;
            Leagues = leagues;

            if (Customers.HasData()) Items++;
            if (Brands.HasData()) Items++;
            if (WagerTypes.HasData()) Items++;
            if (Sports.HasData()) Items++;
            if (Leagues.HasData()) Items++;
        }

        public List<Customer> Customers { get; private set; }
        public List<Brand> Brands { get; private set; }
        public List<WagerType> WagerTypes { get; private set; }
        public List<Sport> Sports { get; private set; }
        public List<League> Leagues { get; private set; }
        public int Items { get; private set; }
    }
}
