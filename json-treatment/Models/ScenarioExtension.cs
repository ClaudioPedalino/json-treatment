using System;
using System.Collections.Generic;
using System.Linq;

namespace json_treatment.Models
{
    public static class ScenarioExtension
    {
        public static bool HasData<T>(this List<T> data)
            => data.Any();

        public static bool Match<T>(this List<T> data, string input)
            where T : IPropertyId
            => data.Any(x => x.Id == input);


        //public static void Evaluate<T>(this List<T> data, string current, List<bool> matchScenario, ScenarioConfiguration scenario)
        //    where T : IPropertyId
        //{
        //    if (data.HasData())
        //    {
        //        bool matchCustomer = data.Match(current);
        //        matchScenario.Add(matchCustomer);
        //    }
        //}

        //public static bool Evaluate<T>(this List<T> source, Func<T, bool> predicate, string current)
        //    where T : IPropertyId
        //{
        //    bool matchCustomer = source.Match(current);

        //    foreach (var item in source)
        //    {
        //        if (!predicate(item))
        //        {
        //            result.Add(item);
        //        }
        //    }

        //    return matchCustomer;
        //}

    }
}
