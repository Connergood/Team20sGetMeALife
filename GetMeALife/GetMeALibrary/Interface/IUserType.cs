namespace GetMeALibrary.Interface
{
    public interface IUserType
    {
        int EventTypeID { get; set; }
        int Occurences { get; set; }
        int UserID { get; set; }
    }
}