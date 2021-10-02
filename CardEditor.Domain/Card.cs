using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;



namespace CardEditor.Domain
{
    public class Card : MongoBase
    {

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("type")]
        [BsonRepresentation(BsonType.String)]
        public  string Type { get; set; }

        [BsonElement("attack")]
        public int Attack { get; set; }


        [BsonElement("health")]
        public int Health { get; set; }

        [BsonElement("cost")]
        public int Cost { get; set; }

        [BsonElement("image")]
        public string filePath { get; set; }
    }
}
