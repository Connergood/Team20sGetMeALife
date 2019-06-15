namespace GetMeALibrary.Interface
{
    public interface IUserSetting
    {
        int EventTypeID { get; set; }
        int Occurences { get; set; }
        int UserID { get; set; }
    }
}