using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using openpos.Server.Context;
using openpos.Server.ModelData;
using openpos.Server.Repositories;

namespace openpos.Server.Services
{
    public class ProductMongoService:IProductRepository
    {

        private readonly IMongoCollection<Product> _product;
        private readonly IMongoCollection<Category> _category;
      //  private readonly MongoRepository _repository = null;

        // contructor de la clase, se usa us para inicializar parametros 
        public ProductMongoService(IMongoDbSettings settings){

            var client= new MongoClient(settings.ConnectionString);

            var database = client.GetDatabase(settings.DatabaseName);

            _product = database.GetCollection<Product>(settings.ProductCollection);
            _category =database.GetCollection<Category>(settings.CategoryCollection);

        }
        public List<Product> GetProduct() =>
            _product.Find(book => true).ToList();
       
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _product.Find(x => true).ToListAsync();
        }
  
        public async Task<Product> GetProduct(string id)
        {
            var filter = Builders<Product>.Filter.Eq("Id", id);
            return await _product.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<Product> AddProduct(Product book)
        {
          await  _product.InsertOneAsync(book);
            return book;
        }

       
        
        public async Task<bool> UpdatePrice(Product model)
        {
  
            var filter = Builders<Product>.Filter.Eq("Name", model.Name);
            var product = _product.Find(filter).FirstOrDefaultAsync();
            if (product.Result == null)
                return false;
            var update = Builders<Product>.Update
                .Set(x => x.Price, model.Price);
               // .Set(x => x.UpdatedOn, model.UpdatedOn);
  
            await _product.UpdateOneAsync(filter, update);
            return true;
        }
        
  
        public async Task<DeleteResult> RemoveProduct(string Id)
        {
            var filter = Builders<Product>.Filter.Eq("_id", Id);
            return await _product.DeleteOneAsync(filter);
        }
        public async Task<DeleteResult> RemoveAllProducts()
        {
            return await _product.DeleteManyAsync(new BsonDocument());
        }

        public async Task<Product> Update(string id, Product productIn)
        {
            await _product.ReplaceOneAsync(book => book.Id == id, productIn);
            return productIn;
        }
          
        public async Task  RemoveData(string id) => 
           await _product.DeleteOneAsync(product => product.Id == id);


        public void Remove(Product bookIn) =>
            _product.DeleteOne(product => product.Id == bookIn.Id);
        
        //categories
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _category.Find(x => true).ToListAsync();
        }
    }
}