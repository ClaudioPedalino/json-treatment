using System.Diagnostics;

namespace json_treatment.Models
{
    [DebuggerDisplay("{" + nameof(Id) + "}")]
    public abstract class CommonCircleLimitModel : IPropertyId
    {
        public string Id { get; set; }
        private string GetDebuggerDisplay() => ToString();
    }

    public interface IPropertyId { string Id { get; set; } }

    public class Brand : CommonCircleLimitModel { }
    public class Customer : CommonCircleLimitModel { }
    public class WagerType : CommonCircleLimitModel { }
    public class Sport : CommonCircleLimitModel { }
    public class League : CommonCircleLimitModel { }
}



#region
//matchAnyScenario =
//    (
//        (matchCustomer && matchWager && matchSport && matchLeague && scenario.WithoutBrand()) /// #01 CUSTOMER & WAGER & SPORT & LEAGUE
//        ||
//        (matchCustomer && matchWager && matchSport && scenario.WithoutBrand() && scenario.WithoutLeague()) /// #02 CUSTOMER & WAGER & SPORT
//        ||
//        (matchCustomer && matchWager && scenario.WithoutBrand() && scenario.WithoutLeague() && scenario.WithoutSport()) /// #03 CUSTOMER & WAGER
//        ||
//        (matchCustomer && scenario.WithoutBrand() && scenario.WithoutSport() && scenario.WithoutLeague() && scenario.WithoutWagerType()) /// #04 CUSTOMER 
//        ||
//        (matchBrand && scenario.WithoutSport() && scenario.WithoutLeague() && scenario.WithoutWagerType() && scenario.WithoutCustomer()) /// #05 BRAND 
//        ||
//        (matchBrand && matchWager && matchSport && matchLeague && scenario.WithoutCustomer()) /// #06 BRAND & WAGER & SPORT & LEAGUE
//        ||
//        (matchBrand && matchWager && matchSport && scenario.WithoutLeague() && scenario.WithoutCustomer()) /// #07 BRAND & WAGER & SPORT
//        ||
//        (matchBrand && matchWager && scenario.WithoutSport() && scenario.WithoutLeague() && scenario.WithoutCustomer()) /// #08 BRAND & WAGER
//        ||
//        (matchWager && scenario.WithoutSport() && scenario.WithoutLeague() && scenario.WithoutBrand() && scenario.WithoutCustomer()) /// #09 WAGER 
//        ||
//        (matchWager && matchSport && scenario.WithoutLeague() && scenario.WithoutCustomer() && scenario.WithoutBrand()) /// #10 WAGER & SPORT 
//        ||
//        (matchWager && matchSport && matchLeague && scenario.WithoutBrand() && scenario.WithoutCustomer()) /// #11 WAGER & SPORT & LEAGUE
//    )
//    ;
#endregion