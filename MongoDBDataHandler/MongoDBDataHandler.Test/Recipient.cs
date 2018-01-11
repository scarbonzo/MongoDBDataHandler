using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDBDataHandler.Test
{
    public class Recipient
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }
}
