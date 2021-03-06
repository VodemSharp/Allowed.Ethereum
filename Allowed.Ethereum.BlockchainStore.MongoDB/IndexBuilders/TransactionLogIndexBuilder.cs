using Allowed.Ethereum.BlockchainStore.MongoDB.Entities;
using Allowed.Ethereum.BlockchainStore.MongoDB.Repositories;
using MongoDB.Driver;

namespace Allowed.Ethereum.BlockchainStore.MongoDB.IndexBuilders
{
    public class TransactionLogIndexBuilder : BaseIndexBuilder<MongoDbTransactionLog>
    {
        public TransactionLogIndexBuilder(IMongoDatabase db) : base(db, MongoDbCollectionName.TransactionLogs)
        {
        }

        public override void EnsureIndexes()
        {
            Compound(true, f => f.TransactionHash, f => f.LogIndex);
            Index(f => f.Address);
            Index(f => f.EventHash);
            Index(f => f.IndexVal1);
            Index(f => f.IndexVal2);
            Index(f => f.IndexVal3);
        }
    }
}