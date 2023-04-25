
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CleanArchitecture.Domain
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        //public Guid Id { get; set; }
        public string? Id { get; set; }
        //public Guid Id { get; set; }
        public DateTime lastupdatedon { get; set; } = DateTime.UtcNow;
        public int lastupdatedby { get; set; }

    }
}
