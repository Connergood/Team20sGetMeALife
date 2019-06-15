namespace GetMeALibrary.Interface
{
    public interface IUserType : IDatabaseObject
    {
        int EventTypeID { get; set; }
        int Occurences { get; set; }
        int UserID { get; set; }
    }
}