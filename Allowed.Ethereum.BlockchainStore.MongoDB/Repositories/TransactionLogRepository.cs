using Allowed.Ethereum.BlockchainStore.MongoDB.Entities;
using MongoDB.Driver;
using Nethereum.BlockchainProcessing.BlockStorage.Entities;
using Nethereum.BlockchainProcessing.BlockStorage.Entities.Mapping;
using Nethereum.BlockchainProcessing.BlockStorage.Repositories;
using Nethereum.RPC.Eth.DTOs;
using System.Numerics;
using System.Threading.Tasks;

namespace Allowed.Ethereum.BlockchainStore.MongoDB.Repositories
{
    public class TransactionLogRepository : MongoDbRepositoryBase<MongoDbTransactionLog>, ITransactionLogRepository
    {
        public TransactionLogRepository(IMongoClient client, string databaseName) : base(client, databaseName, MongoDbCollectionName.TransactionLogs)
        {
        }

        public async Task<ITransactionLogView> FindByTransactionHashAndLogIndexAsync(string hash, BigInteger idx)
        {
            FilterDefinition<MongoDbTransactionLog> filter = CreateDocumentFilter(new MongoDbTransactionLog { TransactionHash = hash, LogIndex = idx.ToString() });

            MongoDbTransactionLog response = await Collection.Find(filter).SingleOrDefaultAsync().ConfigureAwait(false);
            return response;
        }

        public async Task UpsertAsync(FilterLogVO log)
        {
            MongoDbTransactionLog transactionLog = log.MapToStorageEntityForUpsert<MongoDbTransactionLog>();
            await UpsertDocumentAsync(transactionLog).ConfigureAwait(false);
        }
    }
}