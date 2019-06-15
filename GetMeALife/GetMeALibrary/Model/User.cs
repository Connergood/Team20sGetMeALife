using GetMeALibrary.Interface;

namespace GetMeALibrary.Model
{
    /// <summary>
    /// Class representation of a User
    /// </summary>
    public class User : DatabaseObject, IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }

    }
}
