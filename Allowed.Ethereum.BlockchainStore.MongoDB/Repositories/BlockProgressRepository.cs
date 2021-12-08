﻿using Allowed.Ethereum.BlockchainStore.MongoDB.Entities;
using MongoDB.Driver;
using Nethereum.BlockchainProcessing.BlockStorage.Entities.Mapping;
using Nethereum.BlockchainProcessing.ProgressRepositories;
using System.Numerics;

namespace Allowed.Ethereum.BlockchainStore.MongoDB.Repositories
{
    public class BlockProgressRepository : MongoDbRepositoryBase<MongoDbBlockProgress>, IBlockProgressRepository
    {
        public BlockProgressRepository(IMongoClient client, string databaseName) : base(client, databaseName, MongoDbCollectionName.BlockProgress)
        {
        }

        public async Task<BigInteger?> GetLastBlockNumberProcessedAsync()
        {
            var count = await Collection.CountDocumentsAsync(FilterDefinition<MongoDbBlockProgress>.Empty);

            if (count == 0)
            {
                return null;
            }

            var max = await Collection.Find(FilterDefinition<MongoDbBlockProgress>.Empty).Limit(1)
                .Sort(new SortDefinitionBuilder<MongoDbBlockProgress>().Descending(block => block.LastBlockProcessed))
                .Project(block => block.LastBlockProcessed).SingleOrDefaultAsync().ConfigureAwait(false);

            return BigInteger.Parse(max);
        }

        public async Task UpsertProgressAsync(BigInteger blockNumber)
        {
            var block = blockNumber.MapToStorageEntityForUpsert<MongoDbBlockProgress>();
            await UpsertDocumentAsync(block).ConfigureAwait(false);
        }

    }
}