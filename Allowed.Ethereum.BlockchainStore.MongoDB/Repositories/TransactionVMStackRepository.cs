using Allowed.Ethereum.BlockchainStore.MongoDB.Entities;
using MongoDB.Driver;
using Nethereum.BlockchainProcessing.BlockStorage.Entities;
using Nethereum.BlockchainProcessing.BlockStorage.Entities.Mapping;
using Nethereum.BlockchainProcessing.BlockStorage.Repositories;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Allowed.Ethereum.BlockchainStore.MongoDB.Repositories
{
    public class TransactionVMStackRepository : MongoDbRepositoryBase<MongoDbTransactionVmStack>, ITransactionVMStackRepository
    {
        public TransactionVMStackRepository(IMongoClient client, string databaseName) : base(client, databaseName, MongoDbCollectionName.TransactionVMStacks)
        {
        }

        public async Task<ITransactionVmStackView> FindByAddressAndTransactionHashAsync(string address, string hash)
        {
            FilterDefinition<MongoDbTransactionVmStack> filter = CreateDocumentFilter(new MongoDbTransactionVmStack()
            { Address = address, TransactionHash = hash });

            MongoDbTransactionVmStack response = await Collection.Find(filter).SingleOrDefaultAsync();
            return response;
        }

        public async Task<ITransactionVmStackView> FindByTransactionHashAsync(string hash)
        {
            FilterDefinition<MongoDbTransactionVmStack> filter = CreateDocumentFilter(new MongoDbTransactionVmStack() { TransactionHash = hash });

            MongoDbTransactionVmStack response = await Collection.Find(filter).SingleOrDefaultAsync();
            return response;
        }

        public async Task UpsertAsync(string transactionHash, string address, JObject stackTrace)
        {
            MongoDbTransactionVmStack transactionVmStack = stackTrace.MapToStorageEntityForUpsert<MongoDbTransactionVmStack>(transactionHash, address);
            await UpsertDocumentAsync(transactionVmStack);
        }
    }
}