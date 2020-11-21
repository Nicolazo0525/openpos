using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using openpos.Server.ModelData;

namespace openpos.Server.Repositories
{
    //las interfaces me sirven para realizar plantillas de metodos, CONTRATOS
    //separar las clases de la logica, acomplamiento debil
    public interface IProductRepository
    {
        //que hacer, NO como.
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Product> GetProduct(string Id);
        Task<Product> AddProduct(Product model);
        Task<bool> UpdatePrice(Product model);
        Task<DeleteResult> RemoveProduct(string Id);
        Task RemoveData(string id);
        Task<DeleteResult> RemoveAllProducts();
        Task<Product> Update(string id, Product productIn);


    }
}