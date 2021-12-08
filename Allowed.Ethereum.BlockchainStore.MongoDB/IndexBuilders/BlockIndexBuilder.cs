using Allowed.Ethereum.BlockchainStore.MongoDB.Entities;
using Allowed.Ethereum.BlockchainStore.MongoDB.Repositories;
using MongoDB.Driver;

namespace Allowed.Ethereum.BlockchainStore.MongoDB.IndexBuilders
{
    public class BlockIndexBuilder : BaseIndexBuilder<MongoDbBlock>
    {
        public BlockIndexBuilder(IMongoDatabase db) : base(db, MongoDbCollectionName.Blocks)
        {
        }

        public override void EnsureIndexes()
        {
            Compound(true, f => f.BlockNumber, f => f.Hash);
        }
    }
}