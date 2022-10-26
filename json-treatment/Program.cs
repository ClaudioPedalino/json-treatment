using json_treatment.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text.Json;

namespace json_treatment
{
    class Program
    {
        static void Main(string[] args)
        {
            var mockdata01 = "{\"Customers\":{\"Customer\":[{\"Id\":\"654321\"}]},\"WagerTypes\":{\"WagerType\":[{\"Id\":\"Straight\"}]},\"Sports\":{\"Sport\":[{\"Id\":\"Soccer\",\"Leagues\":{\"League\":[{\"Id\":\"Eng. Premier League\"},{\"Id\":\"Eng League\"}]}}]}}";
            var mockdata02 = "{\"Customers\":{\"Customer\":[{\"Id\":\"654321\"}]},\"WagerTypes\":{\"WagerType\":[{\"Id\":\"Straight\"}]},\"Sports\":{\"Sport\":[{\"Id\":\"Soccer\"},{\"Id\":\"Baseball\"}]}}";
            var mockdata03 = "{\"Customers\":{\"Customer\":[{\"Id\":\"654321\"}]},\"WagerTypes\":{\"WagerType\":[{\"Id\":\"Straight\"}]}}";
            var mockdata04 = "{\"Customers\":{\"Customer\":[{\"Id\":\"654321\"}],\"Customers\":[{},{\"Id\":\"222\"}]}}";
            var mockdata05 = "{\"Brands\":{\"Brand\":[{\"Id\":\"BetOnline\"},{\"Id\":\"Lowvig\"}]}}";
            var mockdata06 = "{\"Brands\":{\"Brand\":[{\"Id\":\"BetOnline\"}]},\"WagerTypes\":{\"WagerType\":[{\"Id\":\"Straight\"}]},\"Sports\":{\"Sport\":[{\"Id\":\"Soccer\",\"Leagues\":{\"League\":[{\"Id\":\"EngPremierLeague\"},{\"Id\":\"EngLeague\"}]}}]}}";
            var mockdata07 = "{\"Brands\":{\"Brand\":[{\"Id\":\"BetOnline\"}]},\"WagerTypes\":{\"WagerType\":[{\"Id\":\"Straigth\"}]},\"Sports\":{\"Sport\":[{\"Id\":\"Soccer\"}]}}";
            var mockdata08 = "{\"Brands\":{\"Brand\":[{\"Id\":\"BetOnline\"}]},\"WagerTypes\":{\"WagerType\":[{\"Id\":\"Straigth\"}]}}";
            var mockdata09 = "{\"WagerTypes\":{\"WagerType\":[{\"Id\":\"Straigth\"}]}}";
            var mockdata10 = "{\"WagerTypes\":{\"WagerType\":[{\"Id\":\"Straigth\"}]},\"Sports\":{\"Sport\":[{\"Id\":\"Soccer\"}]}}";
            var mockdata11 = "{\"WagerTypes\":{\"WagerType\":[{\"Id\":\"Straigth\"}]},\"Sports\":{\"Sport\":[{\"Id\":\"Soccer\",\"Leagues\":{\"League\":[{\"Id\":\"EngPremierLeague\"},{\"Id\":\"EngLeague\"}]}}]}}";

            var scenarios = new List<ScenarioConfiguration>() {
                GetScenarioInfoFrom(mockdata01),
                GetScenarioInfoFrom(mockdata02),
                GetScenarioInfoFrom(mockdata03),
                GetScenarioInfoFrom(mockdata04),
                GetScenarioInfoFrom(mockdata05),
                GetScenarioInfoFrom(mockdata06),
                GetScenarioInfoFrom(mockdata07),
                GetScenarioInfoFrom(mockdata08),
                GetScenarioInfoFrom(mockdata09),
                GetScenarioInfoFrom(mockdata10),
                GetScenarioInfoFrom(mockdata11),
                };

            var currentCustomer = "654321";
            var currentBrand = "BetOnline"; /// BetOnline | Sportsbetting | Lowvig | TigerGaming
            var currentWagerType = "Straight Blablabla"; /// Straigth | Parlay | Teaser | IfBet | ActionReverse
            var currentSport = "Soccer Blablabla";
            var currentLeague = "";

            List<bool> matchScenario = new List<bool>();
            var index = 1;

            foreach (var scenario in scenarios.OrderBy(x => x.Items))
            {
                if (scenario.Customers.HasData())
                {
                    bool matchCustomer = scenario.Customers.Match(currentCustomer);
                    matchScenario.Add(matchCustomer);
                }

                if (scenario.Brands.HasData())
                {
                    bool matchBrand = scenario.Brands.Match(currentBrand);
                    matchScenario.Add(matchBrand);
                }

                if (scenario.WagerTypes.HasData())
                {
                    bool matchWagerType = scenario.WagerTypes.Match(currentWagerType);
                    matchScenario.Add(matchWagerType);
                }

                if (scenario.Sports.HasData())
                {
                    bool matchSport = scenario.Sports.Match(currentSport);
                    matchScenario.Add(matchSport);
                }

                if (scenario.Leagues.HasData())
                {
                    bool matchLeague = scenario.Leagues.Match(currentLeague);
                    matchScenario.Add(matchLeague);
                }




                if (matchScenario.All(x => x))
                {
                    //return true;
                    Console.WriteLine($"Scenario {index} - Coincide con escenario configurado. A la Api nueva");
                    break;
                }
                else
                {
                    Console.WriteLine($"Scenario {index} - No coincide con ningún configurado. a la Vieja");
                }
            }

            Console.ReadLine();
        }


        private static ScenarioConfiguration GetScenarioInfoFrom(string scenarioConfigurationData)
        {
            var customers = GetDataFrom<List<Customer>>(scenarioConfigurationData, nameof(Customer));
            var brands = GetDataFrom<List<Brand>>(scenarioConfigurationData, nameof(Brand));
            var wagerTypes = GetDataFrom<List<WagerType>>(scenarioConfigurationData, nameof(WagerType));
            var sports = GetDataFrom<List<Sport>>(scenarioConfigurationData, nameof(Sport));
            var leagues = GetDataFrom<List<League>>(scenarioConfigurationData, nameof(League));

            return new ScenarioConfiguration(customers, brands, wagerTypes, sports, leagues);
        }


        private static T GetDataFrom<T>(string data, string searchBy)
        {
            var jsonData = JObject.Parse(data)
                .DescendantsAndSelf()
                .OfType<JProperty>()
                .SingleOrDefault(x => x.Name.Equals(searchBy));

            if (jsonData != null)
            {
                var dataObj = jsonData.Value.ToObject<T>();

                return dataObj;
            }

            return default;
        }
    }
}
