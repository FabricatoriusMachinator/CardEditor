using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace CardEditor.Domain
{
    public class Types : MongoBase
    {
        [BsonElement("type")]
        public string Type { get; set; }
            
    }
}
