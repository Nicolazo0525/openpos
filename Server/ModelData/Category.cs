using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using openpos.Server.Context;
using openpos.Shared.Models;
namespace openpos.Server.ModelData
{
    public partial class Category//:Document
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id {get;set;}
        [BsonElement("Name")]
        [JsonProperty("Name")]
        public string Name { get; set; }
        [BsonElement("Description")]
        [JsonProperty("Description")]
        public string Description { get; set; }
        
        
    }
}