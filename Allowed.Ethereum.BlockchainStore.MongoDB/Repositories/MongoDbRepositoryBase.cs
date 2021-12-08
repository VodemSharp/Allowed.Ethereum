using Allowed.Ethereum.BlockchainStore.MongoDB.Entities;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Allowed.Ethereum.BlockchainStore.MongoDB.Repositories
{
    public abstract class MongoDbRepositoryBase<TDocument> where TDocument : IMongoDbEntity
    {
        protected readonly IMongoDatabase Database;
        protected readonly IMongoCollection<TDocument> Collection;
        protected readonly IMongoClient Client;

        protected MongoDbRepositoryBase(IMongoClient client, string databaseName, MongoDbCollectionName collectionName)
        {
            Client = client;
            Database = Client.GetDatabase(databaseName);
            Collection = Database.GetCollection<TDocument>(collectionName.ToString());
        }

        protected async Task UpsertDocumentAsync(TDocument updatedDocument)
        {
            await Collection.ReplaceOneAsync(CreateDocumentFilter(updatedDocument), updatedDocument,
                new ReplaceOptions() { IsUpsert = true });
        }

        protected FilterDefinition<TDocument> CreateDocumentFilter(string id)
        {
            return Builders<TDocument>.Filter.Eq(document => document.Id, id);
        }

        protected FilterDefinition<TDocument> CreateDocumentFilter(TDocument entity)
        {
            return Builders<TDocument>.Filter.Eq(document => document.Id, entity.Id);
        }
    }
}