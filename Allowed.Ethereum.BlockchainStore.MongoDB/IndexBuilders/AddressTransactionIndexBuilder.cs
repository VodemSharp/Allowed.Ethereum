using Allowed.Ethereum.BlockchainStore.MongoDB.Entities;
using Allowed.Ethereum.BlockchainStore.MongoDB.Repositories;
using MongoDB.Driver;

namespace Allowed.Ethereum.BlockchainStore.MongoDB.IndexBuilders
{
    public class AddressTransactionIndexBuilder : BaseIndexBuilder<MongoDbAddressTransaction>
    {
        public AddressTransactionIndexBuilder(IMongoDatabase db) : base(db, MongoDbCollectionName.AddressTransactions)
        {
        }

        public override void EnsureIndexes()
        {
            Compound(true, f => f.BlockNumber, f => f.Hash, f => f.Address);
            Index(f => f.Hash);
            Index(f => f.Address);
        }
    }
}