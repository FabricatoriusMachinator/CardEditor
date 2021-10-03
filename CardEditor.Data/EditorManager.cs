using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardEditor.Domain;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using Microsoft.Win32;

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

        public void StoreData(Card card)
        {
            var newCard = card;
            cardCollection.InsertOne(newCard);
        }

        public void storeData(Types newType)
        {
            typeCollection.InsertOne(newType);
            Debug.WriteLine("Pressed");
        }

        public List<Types> GetTypeList()
        {
            List<Types> list = typeCollection.AsQueryable().ToList();
            return list;
        }

        public void Export(object obj, string path)
        {
            JsonSerializer serializer = new JsonSerializer();

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            StreamWriter streamWriter = new StreamWriter(path);
            JsonWriter jsonWriter = new JsonTextWriter(streamWriter);

            serializer.Serialize(jsonWriter, obj);

            jsonWriter.Close();
            streamWriter.Close();
        }

        public void Import(string path)
        {
            JObject obj;
            JsonSerializer serializer = new JsonSerializer();
            if (File.Exists(path))
            {
                StreamReader streamReader = new StreamReader(path);
                JsonReader jsonReader = new JsonTextReader(streamReader);
                obj = serializer.Deserialize(jsonReader) as JObject;
                Debug.WriteLine(obj);
                jsonReader.Close();
                streamReader.Close();
                cardCollection.InsertOne(obj.ToObject<Card>());
            }
            
        }
    }
}

