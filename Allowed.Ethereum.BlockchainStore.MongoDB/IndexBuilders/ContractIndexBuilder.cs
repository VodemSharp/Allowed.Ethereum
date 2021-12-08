using Allowed.Ethereum.BlockchainStore.MongoDB.Entities;
using Allowed.Ethereum.BlockchainStore.MongoDB.Repositories;
using MongoDB.Driver;

namespace Allowed.Ethereum.BlockchainStore.MongoDB.IndexBuilders
{
    public class ContractIndexBuilder : BaseIndexBuilder<MongoDbContract>
    {
        public ContractIndexBuilder(IMongoDatabase db) : base(db, MongoDbCollectionName.Contracts)
        {
        }

        public override void EnsureIndexes()
        {
            Index(f => f.Name);
            Index(f => f.Address, true);
        }
    }
}