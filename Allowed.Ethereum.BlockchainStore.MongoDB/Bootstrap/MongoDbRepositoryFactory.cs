using Allowed.Ethereum.BlockchainStore.MongoDB.IndexBuilders;
using Allowed.Ethereum.BlockchainStore.MongoDB.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using Nethereum.BlockchainProcessing.BlockStorage.Repositories;
using Nethereum.BlockchainProcessing.ProgressRepositories;
using System;
using System.Threading.Tasks;

namespace Allowed.Ethereum.BlockchainStore.MongoDB.Bootstrap
{
    public class MongoDbRepositoryFactory : IBlockchainStoreRepositoryFactory, IBlockProgressRepositoryFactory
    {
        public static MongoDbRepositoryFactory Create(string connectionString,
            string databaseName, string locale, bool deleteAllExistingCollections = false)
        {
            MongoDbRepositoryFactory factory = new(connectionString, databaseName);

            IMongoDatabase db = factory.CreateDbIfNotExists();

            if (deleteAllExistingCollections)
                DeleteAllCollections(db).Wait();

            CreateCollectionsIfNotExist(db, locale).Wait();

            return factory;
        }

        private readonly IMongoClient _client;
        private readonly string _databaseName;

        public MongoDbRepositoryFactory(string connectionString, string databaseName)
        {
            _databaseName = databaseName ?? "BlockchainStorage";
            _client = new MongoClient(connectionString);
        }

        public IMongoDatabase CreateDbIfNotExists()
        {
            return _client.GetDatabase(_databaseName);
        }

        public async Task DeleteDatabase()
        {
            await _client.DropDatabaseAsync(_databaseName);
        }

        public static async Task CreateCollectionsIfNotExist(IMongoDatabase db, string locale)
        {
            foreach (MongoDbCollectionName collectionName in (MongoDbCollectionName[])Enum.GetValues(typeof(MongoDbCollectionName)))
            {
                IAsyncCursor<BsonDocument> collections = await db.ListCollectionsAsync(new ListCollectionsOptions
                { Filter = new BsonDocument("name", collectionName.ToString()) });

                if (!await collections.AnyAsync())
                    await db.CreateCollectionAsync(collectionName.ToString(),
                        new CreateCollectionOptions() { Collation = new Collation(locale, numericOrdering: true) });

                IIndexBuilder builder;
                switch (collectionName)
                {
                    case MongoDbCollectionName.AddressTransactions:
                        builder = new AddressTransactionIndexBuilder(db);
                        break;
                    case MongoDbCollectionName.BlockProgress:
                        builder = new BlockProgressIndexBuilder(db);
                        break;
                    case MongoDbCollectionName.Blocks:
                        builder = new BlockIndexBuilder(db);
                        break;
                    case MongoDbCollectionName.Contracts:
                        builder = new ContractIndexBuilder(db);
                        break;
                    case MongoDbCollectionName.Transactions:
                        builder = new TransactionIndexBuilder(db);
                        break;
                    case MongoDbCollectionName.TransactionLogs:
                        builder = new TransactionLogIndexBuilder(db);
                        break;
                    case MongoDbCollectionName.TransactionVMStacks:
                        builder = new TransactionVmStackIndexBuilder(db);
                        break;
                    default:
                        return;
                }

                builder.EnsureIndexes();
            }
        }

        public static async Task DeleteAllCollections(IMongoDatabase db)
        {
            foreach (MongoDbCollectionName collectionName in (MongoDbCollectionName[])Enum.GetValues(typeof(MongoDbCollectionName)))
            {
                await db.DropCollectionAsync(collectionName.ToString());
            }
        }

        public IAddressTransactionRepository CreateAddressTransactionRepository() => new AddressTransactionRepository(_client, _databaseName);
        public IBlockRepository CreateBlockRepository() => new BlockRepository(_client, _databaseName);
        public IContractRepository CreateContractRepository() => new ContractRepository(_client, _databaseName);
        public ITransactionLogRepository CreateTransactionLogRepository() => new TransactionLogRepository(_client, _databaseName);
        public ITransactionRepository CreateTransactionRepository() => new TransactionRepository(_client, _databaseName);
        public ITransactionVMStackRepository CreateTransactionVmStackRepository() => new TransactionVMStackRepository(_client, _databaseName);
        public IBlockProgressRepository CreateBlockProgressRepository() => new BlockProgressRepository(_client, _databaseName);
    }
}