namespace GetMeALibrary.Interface
{
    public interface IUser : IDatabaseObject
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Password { get; set; }
        string Phone { get; set; }
        string Username { get; set; }
    }
}