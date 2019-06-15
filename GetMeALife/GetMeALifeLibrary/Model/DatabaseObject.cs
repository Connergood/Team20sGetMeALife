using MySql.Data.MySqlClient;

namespace GetMeALibrary.Model
{
    public abstract class DatabaseObject
    {
        public int ID { get; set; }
        public abstract void Parse(MySqlDataReader reader);
        public abstract string GetInsertColumns();
        public abstract string GetInsertValues();
        public abstract string GetSetValues(int ID);
        public abstract string GetTableName();
    }
}
