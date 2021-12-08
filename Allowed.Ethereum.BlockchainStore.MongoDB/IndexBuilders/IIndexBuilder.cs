namespace Allowed.Ethereum.BlockchainStore.MongoDB.IndexBuilders
{
    public interface IIndexBuilder
    {
        void EnsureIndexes();
    }
}