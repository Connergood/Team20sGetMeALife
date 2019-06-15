using GetMeALibrary.Interface;

namespace GetMeALibrary.Model
{
    public class UserType : DatabaseObject, IUserType
    {
        public int UserID { get; set; }
        public int EventTypeID { get; set; }
        public int Occurences { get; set; }
    }
}
