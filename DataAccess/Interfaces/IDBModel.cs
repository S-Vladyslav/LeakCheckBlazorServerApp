namespace DataAccess.Interfaces
{
    public interface IDBModel
    {
        int Id { get; set; }
        string Domain { get; set; }
        string EmailAddress { get; set; }
        string Password { get; set; }
        string LastBreach { get; set; }
        string Source { get; set; }
    }
}
