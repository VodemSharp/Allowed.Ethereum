using Allowed.Ethereum.BlockchainStore.MongoDB.Entities;
using Allowed.Ethereum.BlockchainStore.MongoDB.Repositories;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Allowed.Ethereum.BlockchainStore.MongoDB.IndexBuilders
{
    public abstract class BaseIndexBuilder<TEntity> : IIndexBuilder where TEntity : IMongoDbEntity
    {
        protected readonly IMongoCollection<TEntity> Collection;

        protected BaseIndexBuilder(IMongoDatabase db, MongoDbCollectionName collectionName)
        {
            Collection = db.GetCollection<TEntity>(collectionName.ToString());
        }

        public abstract void EnsureIndexes();

        protected void Index(Expression<Func<TEntity, object>> field, bool unique = false)
        {
            Collection.Indexes.CreateOneAsync(new CreateIndexModel<TEntity>(BuildIndexDefinition(field),
                new CreateIndexOptions() { Unique = unique })).Wait();
        }

        protected void Compound(bool unique, params Expression<Func<TEntity, object>>[] fields)
        {
            Collection.Indexes.CreateOneAsync(new CreateIndexModel<TEntity>(
                Builders<TEntity>.IndexKeys.Combine(fields.Select(BuildIndexDefinition)),
                new CreateIndexOptions() { Unique = unique })).Wait();
        }

        protected IndexKeysDefinition<TEntity> BuildIndexDefinition(Expression<Func<TEntity, object>> field)
        {
            return Builders<TEntity>.IndexKeys.Ascending(field);
        }
    }
}