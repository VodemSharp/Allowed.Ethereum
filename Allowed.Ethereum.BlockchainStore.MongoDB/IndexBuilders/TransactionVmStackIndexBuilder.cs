using Allowed.Ethereum.BlockchainStore.MongoDB.Entities;
using Allowed.Ethereum.BlockchainStore.MongoDB.Repositories;
using MongoDB.Driver;

namespace Allowed.Ethereum.BlockchainStore.MongoDB.IndexBuilders
{
    public class TransactionVmStackIndexBuilder : BaseIndexBuilder<MongoDbTransactionVmStack>
    {
        public TransactionVmStackIndexBuilder(IMongoDatabase db) : base(db, MongoDbCollectionName.TransactionVMStacks)
        {
        }

        public override void EnsureIndexes()
        {
            Index(f => f.Address);
            Index(f => f.TransactionHash);
        }
    }
}