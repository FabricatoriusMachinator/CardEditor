using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;



namespace CardEditor.Domain
{
    public class Card : MongoBase
    {

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("typing")]
        public  string Typing { get; set; }

        [BsonElement("attack")]
        public int Attack { get; set; }

        [BsonElement("defence")]
        public int Defence { get; set; }

        [BsonElement("cost")]
        public int Cost { get; set; }

        [BsonElement("image")]
        public string filePath { get; set; }
    }
}
