namespace GetMeALibrary.Interface
{
    public interface IUser
    {
        string Firstname { get; set; }
        string Lastname { get; set; }
        string Password { get; set; }
        string Phone { get; set; }
        string Username { get; set; }
    }
}