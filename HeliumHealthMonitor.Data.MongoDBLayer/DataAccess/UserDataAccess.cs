namespace HeliumHealthMonitor.Data.MongoDBLayer.DataAccess
{
    public class UserDataAccess : IUserDataAccess
    {
        private readonly IDBConnection _db;
        private readonly IMongoCollection<UserModel> _collection;
        public UserDataAccess(IDBConnection db)
        {
            _db = db;
            _collection = db.UserCollection;
        }
        public Task Create(UserModel user)
        {
            return _collection.InsertOneAsync(user);
        }
        public async Task<List<UserModel>> GetAll()
        {
            var results = await _collection.FindAsync(_ => true);
            return results.ToList();
        }
        public async Task<UserModel> Get(string id)
        {
            var result = (await _collection.FindAsync(c => c.Id == id)).FirstOrDefault();
            return result;
        }
        public Task Update(UserModel user)
        {
            var filter = Builders<UserModel>.Filter.Eq("Id", user.Id);
            return _collection.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert = true });
        }
        public async Task Delete(UserModel user)
        {
            await _collection.DeleteOneAsync(d => d.Id == user.Id);

        }
        private async Task DropCollection(string collection)
        {
            var db = _db.Client.GetDatabase(_db.DbName);
            await db.DropCollectionAsync(collection);
        }
        public async Task DropAll()
        {
            await DropCollection(_db.DeviceCollectionName);
        }
    }
}
