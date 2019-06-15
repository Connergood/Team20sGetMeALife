using MySql.Data.MySqlClient;

namespace GetMeALibrary.Model
{
    public abstract class DatabaseObject
    {
        public int ID { get; set; }
        public abstract void Parse(MySqlDataReader reader);
    }
}
