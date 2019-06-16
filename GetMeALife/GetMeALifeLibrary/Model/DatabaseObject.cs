using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GetMeALibrary.Model
{
    /// <summary>
    /// The class all Database Objects inherit from
    /// </summary>
    public abstract class DatabaseObject
    {
        /// <summary>
        /// The ID of the object in the Database (may be 0 if provided be alternative source).
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Parses a Database reader into our object
        /// </summary>
        /// <param name="reader"></param>
        public abstract void Parse(MySqlDataReader reader);

        /// <summary>
        /// Forms the Graph QL query for a Single object
        /// </summary>
        /// <param name="identifier">The ID of the object you want</param>
        /// <returns>A single database object</returns>
        public string GetGraphSingleQuery(int identifier)
        {
            var query = $"?query={{{this.GetTableName().ToLower()}({this.GetTableName().ToLower()}ID: {identifier}) {{ id, {this.GetInsertColumns().ToLower()} }}}}";
            return query;
        }

        /// <summary>
        /// Forms the Graph QL query for a Single Object
        /// </summary>
        /// <param name="whereClause">The paremeters for the UserID (ex: userID:1)</param>
        /// <returns></returns>
        public string GetGraphSingleQuery(string whereClause)
        {
            var query = $"?query={{{this.GetTableName().ToLower()}({whereClause}) {{ id, {this.GetInsertColumns().ToLower()} }}}}";
            return query;
        }

        /// <summary>
        /// Creates the Graph QL Query to get all objects of this type from the API.
        /// </summary>
        /// <returns>
        /// The Graph QL query to get all objects from the API.
        /// </returns>
        public string GetGraphManyQuery()
        {
            var query = $"?query={{{this.GetTableName().ToLower()}s{{ id, {this.GetInsertColumns().ToLower()} }}}}";
            return query;
        }

        /// <summary>
        /// Updates a single object via a Graph QL mutation
        /// </summary>
        /// <param name="identifier">the ID of the object to mutate</param>
        /// <returns>The value query to update this object</returns>
        public string GetGraphUpdateQuery(int identifier)
        {
            var type = this.GetTableName().ToLower();
            var properties = this.GetInsertColumns().ToLower();
            var query = $"?query=mutation(${type}:{type}Input!,${type}ID:ID!){{update{type}({type}:${type},{type}ID:${type}ID){{{properties}}}}}";
            //"?query=mutation($user:userInput!,$userID:ID!){updateUser(user:$user,userID:$userID){password,phone}}
            var variables = $"&variables={{\"{type}\":{ToJson().Replace($",\"id\":{identifier}", "")},\"{type}ID\":\"{identifier}\"}}";
            //&variables={"user":{"firstName":"Alex","lastName":"Huffman","password":"HUFF","phone":"716 555 4321","username":"AHDefault"},"userID":"7"}"
            return query + variables;
        }

        /// <summary>
        /// Creates a Graph QL query to create this object onto our database
        /// </summary>
        /// <returns>The requested query</returns>
        public string GetGraphCreateQuery()
        {
            var type = this.GetTableName().ToLower();
            var properties = "id, " + this.GetInsertColumns().ToLower();
            var query = $"?query=mutation(${type}:{type}Input!){{create{type}({type}:${type}){{{properties}}}}}";
            //"?query=mutation($user:userInput!){createUser(user:$user){firstName,lastName,username,password,phone}}
            var variables = $"&variables={{\"{type}\":{ToJson().Replace($",\"id\":0","")}}}";
            //&variables={"user":{"firstName":"Alex","lastName":"Huffman","password":"HUFF2","phone":"716 555 4321","username":"AHDefault"}}
            return query + variables;
        }

        /// <summary>
        /// Get all the columns associated with this object
        /// </summary>
        /// <returns>a Comma delimited string of the columns for this object</returns>
        public abstract string GetInsertColumns();

        /// <summary>
        /// Gets the VALUES() portion of an INSERT STATEMENT
        /// </summary>
        /// <returns></returns>
        public abstract string GetInsertValues();

        /// <summary>
        /// Gets the SET portion of a MySql UPDATE
        /// </summary>
        /// <param name="ID">the ID of the object we are udpating</param>
        /// <returns></returns>
        public abstract string GetSetValues(int ID);

        /// <summary>
        /// Gets the name of this object, which is also the name of the SQL table.
        /// </summary>
        /// <returns></returns>
        public abstract string GetTableName();

        /// <summary>
        /// Converts this object to Json format.
        /// </summary>
        /// <returns>a json string representation of this object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Converts a json string to a database object
        /// </summary>
        /// <typeparam name="T">The object we are tying to get out of the string</typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public T ReadFromJson<T>(string jsonString) where T : DatabaseObject, new()
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        /// <summary>
        /// Connverts a json string to a list of database objects
        /// </summary>
        /// <typeparam name="T">The object we are tying to get out of the string</typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public List<T> ReadListFromJson<T>(string jsonString) where T : DatabaseObject, new()
        {
            return JsonConvert.DeserializeObject<List<T>>(jsonString);
        }
    }
}
