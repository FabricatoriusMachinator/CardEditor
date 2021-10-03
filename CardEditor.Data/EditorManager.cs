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
        private static EditorManager instance = null;

        MongoClient client;
        IMongoDatabase database;
        CardEditorDbContext context;
        public IMongoCollection<Card> cardCollection;
        public IMongoCollection<Types> typeCollection;

        private const string MongoDbUrl = "mongodb://localhost:27017";
        private const string CardDbName = "MongoCards";

        public static EditorManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EditorManager();
                }
                return instance;
            }
        }

        public void Init()
        {
            client = new MongoClient(MongoDbUrl);
            database = client.GetDatabase(CardDbName);
            context = new CardEditorDbContext(database);
            cardCollection = context.Cards;
            typeCollection = context.Types;
        }

        public void storeData(Card card)
        {
            var newCard = card;
            cardCollection.InsertOne(newCard);
        }

        public void storeData(Types type)
        {
            var newType = type;
            typeCollection.InsertOne(type);
        }

        public List<Types> getTypeList()
        {
            List<Types> list = typeCollection.AsQueryable().ToList<Types>();
            return list;
        }


    }
}

