using System;
using MongoDB.Bson;

namespace openpos.Server.Context
{
    public class Document:IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}