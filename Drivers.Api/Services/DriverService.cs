using MongoDB.Driver;
using Drivers.Api.Models;
using Microsoft.Extensions.Options;
using Drivers.Api.Configurations;

namespace Drivers.Api.Services
{
    public class DriverService
    {
        private readonly IMongoCollection<Driver> _driversCollection;
        public DriverService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _driversCollection = mongoDb.GetCollection<Driver>(databaseSettings.Value.CollectionName);
        }

        public async Task<List<Driver>> GetAsync() =>
            await _driversCollection.Find(_ => true).ToListAsync();
        public async Task CreateAsync(Driver driver)
        {
            await _driversCollection.InsertOneAsync(driver);
            return;
        }
        public async Task DeleteAsync(string id)
        {
            FilterDefinition<Driver> filter = Builders<Driver>.Filter.Eq("Id", id);
            await _driversCollection.DeleteOneAsync(filter);
            return;
        }
        public async Task UpdateAsync(string id, int number)
        {
            FilterDefinition<Driver> filter = Builders<Driver>.Filter.Eq("Id", id);
            UpdateDefinition<Driver> update = Builders<Driver>.Update.Set<int>("Number", number);
            await _driversCollection.UpdateOneAsync(filter, update);
            return;

        }
    }
}
