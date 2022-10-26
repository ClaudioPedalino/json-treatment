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


            /// Nested search
            var scenarioConfig01 = GetScenarioInfoFrom(mockdata01);
            var scenarioConfig02 = GetScenarioInfoFrom(mockdata02);
            var scenarioConfig03 = GetScenarioInfoFrom(mockdata03);
            var scenarioConfig04 = GetScenarioInfoFrom(mockdata04);
            var scenarioConfig05 = GetScenarioInfoFrom(mockdata05);
            var scenarioConfig06 = GetScenarioInfoFrom(mockdata06);
            var scenarioConfig07 = GetScenarioInfoFrom(mockdata07);
            var scenarioConfig08 = GetScenarioInfoFrom(mockdata08);
            var scenarioConfig09 = GetScenarioInfoFrom(mockdata09);
            var scenarioConfig10 = GetScenarioInfoFrom(mockdata10);
            var scenarioConfig11 = GetScenarioInfoFrom(mockdata11);

            var scenarios = new List<ScenarioConfiguration>() { scenarioConfig01 , scenarioConfig02, scenarioConfig03, scenarioConfig04, scenarioConfig05
            , scenarioConfig06, scenarioConfig07, scenarioConfig08, scenarioConfig09, scenarioConfig10, scenarioConfig11};

            var currentCustomer = "654321";
            var currentBrand = "BetOnline"; /// BetOnline | Sportsbetting | Lowvig | TigerGaming
            var currentWagerType = "Straight"; /// Straigth | Parlay | Teaser | IfBet | ActionReverse
            var currentSport = "Soccer";
            var currentLeague = "Eng League";

            bool matchAnyScenario = false;
            var index = 1;

            foreach (var scenario in scenarios)
            {
                //bool matchCustomer = false;
                //bool matchWager = false;
                //bool matchSport = false;
                //bool matchLeague = false;
                //bool matchBrand = false;

                bool matchCustomer = scenario.MatchCustomer(currentCustomer);
                bool matchBrand = scenario.MatchBrand(currentBrand);
                bool matchWager = scenario.MatchWagerType(currentWagerType);
                bool matchSport = scenario.MatchSport(currentSport);
                bool matchLeague = scenario.MatchLeague(currentLeague);

                //if (scenario.WagerTypes != null)
                //{
                //    matchWager = scenario.WagerTypes.Any(x => x.Id == currentWagerType);
                //}

                //if (scenario.Sports != null)
                //{
                //    matchSport = scenario.Sports.Any(x => x.Id == currentSport);
                //}

                //if (scenario.Leagues != null)
                //{
                //    matchLeague = scenario.Leagues.Any(x => x.Id == currentLeague);
                //}

                //if (scenario.Brands != null)
                //{
                //    matchBrand = scenario.Brands.Any(x => x.Id == currentBrand);
                //}


                #region match inclusive
                //matchAnyScenario =
                //    (
                //        (matchCustomer && matchWager && matchSport && matchLeague) /// #01 CUSTOMER & WAGER & SPORT & LEAGUE
                //        ||
                //        (matchCustomer && matchWager && matchSport) /// #02 CUSTOMER & WAGER & SPORT
                //        ||
                //        (matchCustomer && matchWager) /// #03 CUSTOMER & WAGER
                //        ||
                //        (matchCustomer) /// #04 CUSTOMER 
                //        ||
                //        (matchBrand) /// #05 BRAND 
                //        ||
                //        (matchBrand && matchWager && matchSport && matchLeague) /// #06 BRAND & WAGER & SPORT & LEAGUE
                //        ||
                //        (matchBrand && matchWager && matchSport) /// #07 BRAND & WAGER & SPORT
                //        ||
                //        (matchBrand && matchWager) /// #08 BRAND & WAGER
                //        ||
                //        (matchWager) /// #09 WAGER 
                //        ||
                //        (matchWager && matchSport) /// #10 WAGER & SPORT 
                //        ||
                //        (matchWager && matchSport && matchLeague) /// #11 WAGER & SPORT & LEAGUE
                //    )
                //    ;
                #endregion

                matchAnyScenario =
                    (
                        (matchCustomer && matchWager && matchSport && matchLeague && scenario.WithoutBrand()) /// #01 CUSTOMER & WAGER & SPORT & LEAGUE
                        ||
                        (matchCustomer && matchWager && matchSport && scenario.WithoutBrand() && scenario.WithoutLeague()) /// #02 CUSTOMER & WAGER & SPORT
                        ||
                        (matchCustomer && matchWager && scenario.WithoutBrand() && scenario.WithoutLeague() && scenario.WithoutSport()) /// #03 CUSTOMER & WAGER
                        ||
                        (matchCustomer && scenario.WithoutBrand() && scenario.WithoutSport() && scenario.WithoutLeague() && scenario.WithoutWagerType()) /// #04 CUSTOMER 
                        ||
                        (matchBrand && scenario.WithoutSport() && scenario.WithoutLeague() && scenario.WithoutWagerType() && scenario.WithoutCustomer()) /// #05 BRAND 
                        ||
                        (matchBrand && matchWager && matchSport && matchLeague && scenario.WithoutCustomer()) /// #06 BRAND & WAGER & SPORT & LEAGUE
                        ||
                        (matchBrand && matchWager && matchSport && scenario.WithoutLeague() && scenario.WithoutCustomer()) /// #07 BRAND & WAGER & SPORT
                        ||
                        (matchBrand && matchWager && scenario.WithoutSport() && scenario.WithoutLeague() && scenario.WithoutCustomer()) /// #08 BRAND & WAGER
                        ||
                        (matchWager && scenario.WithoutSport() && scenario.WithoutLeague() && scenario.WithoutBrand() && scenario.WithoutCustomer()) /// #09 WAGER 
                        ||
                        (matchWager && matchSport && scenario.WithoutLeague() && scenario.WithoutCustomer() && scenario.WithoutBrand()) /// #10 WAGER & SPORT 
                        ||
                        (matchWager && matchSport && matchLeague && scenario.WithoutBrand() && scenario.WithoutCustomer()) /// #11 WAGER & SPORT & LEAGUE
                    )
                    ;
                Console.WriteLine($"Scenario {index} match: {matchAnyScenario}");
                index++;

                //if (matchAnyScenario) // Testear
                //{
                //    break;
                //}
            }

            Console.ReadLine();




            //var dynamicObject = JsonConvert.DeserializeObject<dynamic>(mockdata01);

            //var customerObj = JsonConvert.DeserializeObject<List<Customer>>(dynamicObject.Customers.Customer);


            //var jsonElement = JsonSerializer.Deserialize<JsonElement>(mockdata01);



            //var jsonDom = JsonConvert.DeserializeObject<JObject>(mockdata01);

            //var cusomter01 = jsonDom["Customers"];
            //var cusomter02 = jsonDom["Customer"];



            /// TO dictionary
            //var serializer = new JavaScriptSerializer();
            //serializer.RegisterConverters(new[] { new DynamicJsonConverter() });

            //dynamic obj = serializer.Deserialize(mockdata01, typeof(object));

            //var pepe = obj["Customers"];






            //Console.WriteLine(dynamicObject?.customerObj);
            //Console.WriteLine(dynamicObject?.Customer);


            Console.ReadLine();
        }

        //public static bool MatchCustomer(string currentCustomer, ScenarioConfiguration scenario, bool matchCustomer)
        //{
        //    if (scenario.Customers != null)
        //    {
        //        matchCustomer = scenario.Customers.Any(x => x.Id == currentCustomer);
        //    }

        //    return matchCustomer;
        //}

        private static ScenarioConfiguration GetScenarioInfoFrom(string scenarioConfigurationData)
        {
            var customers = GetDataFrom<List<Customer>>(scenarioConfigurationData, nameof(Customer));
            var brands = GetDataFrom<List<Brand>>(scenarioConfigurationData, nameof(Brand));
            var wagerTypes = GetDataFrom<List<WagerType>>(scenarioConfigurationData, nameof(WagerType));
            var sports = GetDataFrom<List<Sport>>(scenarioConfigurationData, nameof(Sport));
            var leagues = GetDataFrom<List<League>>(scenarioConfigurationData, nameof(League));

            var test = GetDataFrom<List<League>>(scenarioConfigurationData, "asdasdasdcawecasec");

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
