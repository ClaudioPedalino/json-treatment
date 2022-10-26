using System.Diagnostics;

namespace json_treatment.Models
{
    [DebuggerDisplay("{" + nameof(Id) + "}")]
    public abstract class WithId
    {
        public string Id { get; set; }
        private string GetDebuggerDisplay() => ToString();
    }


    public class Brand : WithId { }
    public class Customer : WithId { }
    public class WagerType : WithId { }
    public class Sport : WithId { }
    public class League : WithId { }
}
