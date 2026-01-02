// using MongoDB.Bson;
// using MongoDB.Bson;
// using MongoDB.Bson.Serialization.Attributes;

// namespace P10_WebApi.Models.AbstractClasses;

// public abstract class BaseEntity
// {
//     [BsonId]
//     [BsonRepresentation(BsonType.ObjectId)]
//     public Guid Id { get; set; }

//     public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
//     public DateTime? UpdatedAt { get; set; }
// }


using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace P10_WebApi.Models.AbstractClasses
{
    public abstract class BaseEntity
    {
        [BsonId] // MongoDB primary key
        [BsonRepresentation(BsonType.ObjectId)] // Store as ObjectId in DB but expose as string in C#
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString(); // Auto-generate new Id

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("updatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }
}
