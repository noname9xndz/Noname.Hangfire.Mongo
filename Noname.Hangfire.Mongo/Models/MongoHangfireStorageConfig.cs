namespace Noname.Hangfire.Mongo.Models
{
    public class MongoHangfireStorageConfig
    {
        /// <summary>
        /// </summary>
        public MongoConnectionObject ConnectionObject { set; get; }

        /// <summary>
        /// </summary>
        public string ConnectionString
        {
            get
            {
                if (ConnectionObject != null)
                {
                    return ConnectionObject.ConnectionString;
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// </summary>
        public string DatabaseName { set; get; } = "db_hangfire";
    }
}