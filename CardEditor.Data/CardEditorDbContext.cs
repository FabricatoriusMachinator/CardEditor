using CardEditor.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace CardEditor.Data
{
    public class CardEditorDbContext
    {
        private const string cardCollectionName = "cards";
        private const string typeCollectionName = "types";


        private readonly IMongoDatabase database;

        private readonly Dictionary<Type, string> collectionNames = new()
        {
            { typeof(Card), cardCollectionName },
            { typeof(Types), typeCollectionName }
        };

        public IMongoCollection<Card> Cards => GetCollection<Card>();
        public IMongoCollection<Types> Types => GetCollection<Types>();



        public CardEditorDbContext(IMongoDatabase database)
        {
            this.database = database;
        }

        private IMongoCollection<T> GetCollection<T>()
        {
            var typeOfInstance = typeof(T);
            var collectionName = collectionNames[typeOfInstance];
            return database.GetCollection<T>(collectionName);
        }

        public void ClearDataBase()
        {
            foreach (var collectionName in collectionNames)
            {
                database.DropCollection(collectionName.Value);
            }
        }
    }
}

