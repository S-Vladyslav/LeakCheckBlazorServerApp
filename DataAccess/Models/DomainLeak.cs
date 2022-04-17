using DataAccess.Interfaces;

namespace DataAccess.Models
{
    public class DomainLeak : IDBModel
    {
        public int Id { get; set; }
        public string Domain { get; set; } = "Unknown";
        public string EmailAddress { get; set; } = "Unknown";
        public string Password { get; set; } = "Unknown";
        public string LastBreach { get; set; } = "Unknown";
        public string Source { get; set; } = "Unknown";
    }
}
