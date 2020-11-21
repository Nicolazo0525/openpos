using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using openpos.Server.Context;
using openpos.Shared.Models;

namespace openpos.Server.ModelData
{
    public partial class Product //: Document
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id {get;set;} 

        [BsonElement("Name")]
        [JsonProperty("Name")]
        public string Name { get; set; }
        public double Price { get; set; }
        public String Marca { get; set; }
        
        public string Description { get; set; }
        //[BsonRepresentation(BsonType.ObjectId)]
        //public List<string> Categories {get; set;}
       // [BsonIgnore]
       [JsonProperty("Category")]
       public List<Category> CategoriesList {get; set;}
        //public DateTime? UpdatedOn { get; set; }
       // public DateTime? CreatedOn { get; set; }
        
    }
}