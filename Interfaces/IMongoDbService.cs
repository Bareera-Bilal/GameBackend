// using MongoDB.Driver;
// using P10_WebApi.Models;

// namespace P10_WebApi.Services
// {
//     public interface IMongoDbService
//     {
//         IMongoCollection<User> Users { get; }
//     }
// }


using MongoDB.Driver;
using P10_WebApi.Models;

namespace P10_WebApi.Services
{
    public interface IMongoDbService
    {
        IMongoCollection<User> Users { get; }
    }
}
