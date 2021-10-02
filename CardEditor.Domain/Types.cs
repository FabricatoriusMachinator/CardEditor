using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace CardEditor.Domain
{
    public class Types : MongoBase
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("attack")]
        public int Attack { get; set; }

        [BsonElement("defence")]
        public int Defence { get; set; }
        
        [BsonElement("cost")]
        public int Cost { get; set; }
    }
}
