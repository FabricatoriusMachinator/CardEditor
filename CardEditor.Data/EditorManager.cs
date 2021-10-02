using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardEditor.Domain;

namespace CardEditor.Data
{
    public class EditorManager
    {
        MongoClient client;
        IMongoDatabase database;
        CardEditorDbContext context;
        IMongoCollection<Card> cardCollection;

        private const string MongoDbUrl = "mongodb://localhost:27017";
        private const string SuperHeroDbName = "MongoCards";

        public void Init()
        {
            client = new MongoClient(MongoDbUrl);
            database = client.GetDatabase(SuperHeroDbName);
            context = new CardEditorDbContext(database);
            cardCollection = context.Cards;
        }
    }
}

