﻿using Allowed.Ethereum.BlockchainStore.MongoDB.Entities;
using MongoDB.Driver;
using Nethereum.BlockchainProcessing.BlockStorage.Entities;
using Nethereum.BlockchainProcessing.BlockStorage.Entities.Mapping;
using Nethereum.BlockchainProcessing.BlockStorage.Repositories;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;

namespace Allowed.Ethereum.BlockchainStore.MongoDB.Repositories
{
    public class AddressTransactionRepository : MongoDbRepositoryBase<MongoDbAddressTransaction>, IAddressTransactionRepository
    {
        public AddressTransactionRepository(IMongoClient client, string databaseName) : base(client, databaseName, MongoDbCollectionName.AddressTransactions)
        {
        }

        public async Task<IAddressTransactionView> FindAsync(string address, HexBigInteger blockNumber, string transactionHash)
        {
            var filter = CreateDocumentFilter(
                new MongoDbAddressTransaction()
                {
                    Address = address,
                    Hash = transactionHash,
                    BlockNumber = blockNumber.Value.ToString()
                });

            var response = await Collection.Find(filter).SingleOrDefaultAsync().ConfigureAwait(false);
            return response;
        }

        public async Task UpsertAsync(TransactionReceiptVO transactionReceiptVO, string address, string error = null, string newContractAddress = null)
        {
            var tx = transactionReceiptVO.MapToStorageEntityForUpsert<MongoDbAddressTransaction>(address);
            tx.UpdateRowDates();
            await UpsertDocumentAsync(tx).ConfigureAwait(false);
        }
    }
}