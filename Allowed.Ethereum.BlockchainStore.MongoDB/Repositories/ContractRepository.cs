using Allowed.Ethereum.BlockchainStore.MongoDB.Entities;
using MongoDB.Driver;
using Nethereum.BlockchainProcessing.BlockStorage.Entities;
using Nethereum.BlockchainProcessing.BlockStorage.Entities.Mapping;
using Nethereum.BlockchainProcessing.BlockStorage.Repositories;
using Nethereum.RPC.Eth.DTOs;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Allowed.Ethereum.BlockchainStore.MongoDB.Repositories
{
    public class ContractRepository : MongoDbRepositoryBase<MongoDbContract>, IContractRepository
    {
        private readonly ConcurrentDictionary<string, MongoDbContract> _cachedContracts = new();

        public ContractRepository(IMongoClient client, string databaseName) : base(client, databaseName, MongoDbCollectionName.Contracts)
        {
        }

        public async Task<bool> ExistsAsync(string contractAddress)
        {
            FilterDefinition<MongoDbContract> filter = CreateDocumentFilter(contractAddress);

            MongoDbContract response = await Collection.Find(filter).SingleOrDefaultAsync().ConfigureAwait(false);
            return response != null;
        }

        public async Task FillCacheAsync()
        {
            using IAsyncCursor<MongoDbContract> cursor = await Collection.FindAsync(FilterDefinition<MongoDbContract>.Empty);
            while (await cursor.MoveNextAsync().ConfigureAwait(false))
            {
                IEnumerable<MongoDbContract> batch = cursor.Current;
                foreach (MongoDbContract contract in batch)
                    _cachedContracts.AddOrUpdate(contract.Address, contract, (s, existingContract) => contract);
            }
        }

        public async Task<IContractView> FindByAddressAsync(string contractAddress)
        {
            FilterDefinition<MongoDbContract> filter = CreateDocumentFilter(contractAddress);

            MongoDbContract response = await Collection.Find(filter).SingleOrDefaultAsync().ConfigureAwait(false);
            return response;
        }

        public bool IsCached(string contractAddress)
        {
            return _cachedContracts.ContainsKey(contractAddress);
        }

        public async Task UpsertAsync(ContractCreationVO contractCreation)
        {
            MongoDbContract contract = contractCreation.MapToStorageEntityForUpsert<MongoDbContract>();
            await UpsertDocumentAsync(contract);

            _cachedContracts.AddOrUpdate(contract.Address, contract,
                (s, existingContract) => contract);
        }
    }
}