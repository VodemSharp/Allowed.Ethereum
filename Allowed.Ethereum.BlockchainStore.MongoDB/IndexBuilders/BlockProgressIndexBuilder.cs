using Allowed.Ethereum.BlockchainStore.MongoDB.Entities;
using Allowed.Ethereum.BlockchainStore.MongoDB.Repositories;
using MongoDB.Driver;

namespace Allowed.Ethereum.BlockchainStore.MongoDB.IndexBuilders
{
    public class BlockProgressIndexBuilder : BaseIndexBuilder<MongoDbBlockProgress>
    {
        public BlockProgressIndexBuilder(IMongoDatabase db) : base(db, MongoDbCollectionName.BlockProgress)
        {
        }

        public override void EnsureIndexes()
        {
            Compound(true, f => f.LastBlockProcessed);
        }
    }
}