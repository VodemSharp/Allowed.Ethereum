using Allowed.Ethereum.BlockchainStore.MongoDB.Entities;
using Allowed.Ethereum.BlockchainStore.MongoDB.Repositories;
using MongoDB.Driver;

namespace Allowed.Ethereum.BlockchainStore.MongoDB.IndexBuilders
{
    public class TransactionIndexBuilder : BaseIndexBuilder<MongoDbTransaction>
    {
        public TransactionIndexBuilder(IMongoDatabase db) : base(db, MongoDbCollectionName.Transactions)
        {
        }

        public override void EnsureIndexes()
        {
            Index(f => f.Hash);
            Index(f => f.AddressFrom);
            Index(f => f.AddressTo);
            Index(f => f.NewContractAddress);
        }
    }
}