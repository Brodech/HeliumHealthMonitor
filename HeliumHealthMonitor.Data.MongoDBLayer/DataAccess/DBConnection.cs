using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using HeliumHealthMonitor.Data.Shared.Models;

namespace HeliumHealthMonitor.Data.MongoDBLayer.DataAccess;

public class DBConnection : IDBConnection
{
    private readonly IConfiguration _config;
    private string _connectionId = "DevMongoDBConnectionString";
    private readonly IMongoDatabase _db;

    public string DbName { get; private set; }
    public string DeviceCollectionName { get; private set; } = "devices";
    public string EnergyStatusCollectionName { get; private set; } = "energyStatus";
    public string UserCollectionName { get; private set; } = "users";

    public MongoClient Client { get; private set; }
    public IMongoCollection<DeviceModel> DeviceCollection { get; private set; }
    public IMongoCollection<EnergyStatusModel> EnergyStatusCollection { get; private set; }
    public IMongoCollection<UserModel> UserCollection { get; private set; }
    //public DbConnection(IConfiguration config)
    //{
    //    _config = config;
    //    Client = new MongoClient(_config.GetConnectionString(_connectionId));
    //    DbName = _config["DatabaseName"];
    //    _db = Client.GetDatabase(DbName);

    //    TaskCollection = _db.GetCollection<TaskEntity>(TaskCollectionName);
    //    AppointmentCollection = _db.GetCollection<AppointmentEntity>(AppointmentCollectionName);
    //    CustomerCollection = _db.GetCollection<CustomerEntity>(CustomerCollectionName);
    //    CustomerArchiveCollection = _db.GetCollection<CustomerArchiveEntity>(CustomerArchiveCollectionName);
    //    ContactCollection = _db.GetCollection<ContactEntity>(ContactCollectionName);
    //    ContactArchiveCollection = _db.GetCollection<ContactArchiveEntity>(ContactArchiveCollectionName);
    //    OpportunityCollection = _db.GetCollection<OpportunityEntity>(OpportunityCollectionName);
    //    OpportunityArchiveCollection = _db.GetCollection<OpportunityArchiveEntity>(OpportunityArchiveCollectionName);
    //    CaseCollection = _db.GetCollection<CaseEntity>(CaseCollectionName);
    //    CaseArchiveCollection = _db.GetCollection<CaseArchiveEntity>(CaseArchiveCollectionName);
    //    OfferCollection = _db.GetCollection<OfferEntity>(OfferCollectionName);
    //    OfferArchiveCollection = _db.GetCollection<OfferArchiveEntity>(OfferArchiveCollectionName);
    //    OrderCollection = _db.GetCollection<OrderEntity>(OrderCollectionName);
    //    OrderArchiveCollection = _db.GetCollection<OrderArchiveEntity>(OrderArchiveCollectionName);
    //    InvoiceCollection = _db.GetCollection<InvoiceEntity>(InvoiceCollectionName);
    //    InvoiceArchiveCollection = _db.GetCollection<InvoiceArchiveEntity>(InvoiceArchiveCollectionName);
    //    OfferConfirmationCollection = _db.GetCollection<OfferConfirmationEntity>(OfferConfirmationCollectionName);
    //    OfferConfirmationArchiveCollection = _db.GetCollection<OfferConfirmationArchiveEntity>(OfferConfirmationArchiveCollectionName);
    //    UserCollection = _db.GetCollection<UserEntity>(UserCollectionName);
    //    UserArchiveCollection = _db.GetCollection<UserArchiveEntity>(UserArchiveCollectionName);
    //}
}
