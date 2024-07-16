using NetCore.Base.Enum;
using Domain.Validation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.MongoDB.Entities;

public sealed class Grupo 
{
    public string Id { get; set; }

    [BsonId]
    public ObjectId _id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public Domain.Enums.EGenero Gender { get; private set; }
    public string? Email { get; private set; }    
     
}
