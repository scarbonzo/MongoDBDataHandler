using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;

namespace MongoDBDataHandler
{
    public class MongoDBDataHandler
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        private string connectionString;
        private MongoClient client;

        public MongoDBDataHandler()
        { }

        /// <summary>
        /// Connect to MongoDB during the constructor
        /// </summary>
        /// <param name="Server">Required: The MongoDB hostname or IP</param>
        /// <param name="Database">Required: The Database that houses the collections</param>
        /// <param name="Port">Optional: Non-default port if necessary</param>
        /// <param name="Username">Optional: This is MongoDB Username</param>
        /// <param name="Password">Optional: This is MongoDB Password</param>
        public MongoDBDataHandler(string Server, string Database, int Port = 27017, string Username = null, string Password = null)
        {
            this.Server = Server;
            this.Database = Database;
            this.Port = Port;
            this.Username = Username;
            this.Password = Password;

            connectionString = Server + ":" + Port.ToString() + "/" + Database;

            if (Username != null)
            {
                if (Password != null)
                {
                    connectionString = "mongodb://" + Username + ":" + Password + "@" + connectionString;
                }
                else
                {
                    connectionString = "mongodb://" + Username + "@" + connectionString;
                }
            }
            else
            {
                connectionString = "mongodb://" + connectionString;
            }

            try
            {
                // Try and connect to the server using the constructed connection string
                client = new MongoClient(connectionString);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        /// <summary>
        /// Connect to MongoDB outside of the constructor
        /// </summary>
        /// <param name="Server">Required: The MongoDB hostname or IP</param>
        /// <param name="Database">Required: The Database that houses the collections</param>
        /// <param name="Port">Optional: Non-default port if necessary</param>
        /// <param name="Username">Optional: This is MongoDB Username</param>
        /// <param name="Password">Optional: This is MongoDB Password</param>
        // Manually connect to the database if not already done via the constructor, or if the database information changes
        private bool Connect(string Server, string Database, int Port = 27017, string Username = null, string Password = null)
        {
            this.Server = Server;
            this.Database = Database;
            this.Port = Port;
            this.Username = Username;
            this.Password = Password;

            connectionString = Server + ":" + Port.ToString() + "/" + Database;

            if (Username != null)
            {
                if (Password != null)
                {
                    connectionString = "mongodb://" + Username + ":" + Password + "@" + connectionString;
                }
                else
                {
                    connectionString = "mongodb://" + Username + "@" + connectionString;
                }
            }
            else
            {
                connectionString = "mongodb://" + connectionString;
            }

            try
            {
                // Try and connect to the server using the constructed connection string
                client = new MongoClient(connectionString);
                if (client != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        // This function will attempt to create a document in the MongoDB Database (and passed collection) and return the created ObjectId if successful
        public ObjectId? WriteObject(string Collection, object o)
        {

            try
            {
                if (client != null)
                {
                    IMongoCollection<BsonDocument> collection =
                        client
                        .GetDatabase(Database)
                        .GetCollection<BsonDocument>(Collection);

                    var doc = o.ToBsonDocument();

                    collection.InsertOne(doc);

                    var id = (ObjectId)doc.Elements.ElementAt(1).Value;

                    return id;
                }
                else
                {
                    return null;
                }
            }
            catch { return null; }
        }

        // This function returns an IQueryable of objects from a given collection
        public IQueryable<T> GetObjects<T>(string Collection)
        {
            try
            {
                return client
                    .GetDatabase(Database)
                    .GetCollection<T>(Collection)
                    .AsQueryable();
            }
            catch { return null; }
        }
    }
}
