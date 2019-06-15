namespace GetMeALibrary.Interface
{
    public interface IUser
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Password { get; set; }
        string Phone { get; set; }
        string Username { get; set; }
    }
}