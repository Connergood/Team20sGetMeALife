namespace GetMeALibrary.Interface
{
    public interface IUser
    {
        string firstname { get; set; }
        string lastname { get; set; }
        string password { get; set; }
        string phone { get; set; }
        string username { get; set; }
    }
}