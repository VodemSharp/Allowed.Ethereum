﻿using Allowed.Ethereum.BlockchainStore.MongoDB.Entities;
using MongoDB.Driver;
using Nethereum.BlockchainProcessing.BlockStorage.Entities;
using Nethereum.BlockchainProcessing.BlockStorage.Entities.Mapping;
using Nethereum.BlockchainProcessing.BlockStorage.Repositories;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using System.Threading.Tasks;

namespace Allowed.Ethereum.BlockchainStore.MongoDB.Repositories
{
    public class TransactionRepository : MongoDbRepositoryBase<MongoDbTransaction>, ITransactionRepository
    {
        public TransactionRepository(IMongoClient client, string databaseName) : base(client, databaseName, MongoDbCollectionName.Transactions)
        {
        }

        public async Task<ITransactionView> FindByBlockNumberAndHashAsync(HexBigInteger blockNumber, string hash)
        {
            var filter = CreateDocumentFilter(new MongoDbTransaction()
            { Hash = hash, BlockNumber = blockNumber.Value.ToString() });

            var response = await Collection.Find(filter).SingleOrDefaultAsync().ConfigureAwait(false);
            return response;
        }

        public async Task UpsertAsync(TransactionReceiptVO transactionReceiptVO, string code, bool failedCreatingContract)
        {
            var tx = transactionReceiptVO.MapToStorageEntityForUpsert<MongoDbTransaction>(code, failedCreatingContract);
            await UpsertDocumentAsync(tx).ConfigureAwait(false);
        }

        public async Task UpsertAsync(TransactionReceiptVO transactionReceiptVO)
        {
            var tx = transactionReceiptVO.MapToStorageEntityForUpsert<MongoDbTransaction>();
            await UpsertDocumentAsync(tx).ConfigureAwait(false);
        }
    }
}