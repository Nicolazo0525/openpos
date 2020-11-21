using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using openpos.Server.ModelData;
using openpos.Server.Services;
using Microsoft.AspNetCore.Mvc;
using openpos.Server.Repositories;

namespace openpos.Server.Controllers
{
    //http REST   localhost:5000/api/Books
    [Route("[controller]")] //anotaciones -ejecutar condigo interno que reduce instrucciones y hace varias operaciones
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _productService;
        //el contructor, metodo interno , que se carga primero, cuando se llama a la clase, o cuando se instancia la clase

        public ProductController(IProductRepository productService)
        {
            _productService = productService;

        }

        //los metodos CRUD. 
        //cnsultar lista o un colleccion determinado
        [HttpGet]
        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = await _productService.GetAllProducts();

            return products;
        }


        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Product> GetProductId(string Id)
        {

            var product = _productService.GetProduct(Id).Result;

            if (product == null)
                return NotFound();

            return product;

        }

        [HttpPost]
        public async Task<IActionResult> Post(Product model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name))
                    return BadRequest("Please enter product name");
                else if (string.IsNullOrWhiteSpace(model.Marca))
                    return BadRequest("Please enter category");
                else if (model.Price <= 0)
                    return BadRequest("Please enter price");

                //   model.CreatedOn = DateTime.UtcNow;
                _productService.AddProduct(model);
                return Ok("Your product has been added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult update(string id, Product productIn)
        {

            var product = _productService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            _productService.Update(id, productIn);

            return NoContent();


        }
        /*   [HttpDelete("{id:length(24)}")]
           public IActionResult Delete(string id)
           {
               var product = _productService.GetProduct(id).Result;
   
               if (product == null)
               {
                   return NotFound();
               }
   
               _productService.Remove(product);
   
               return NoContent();
           }*/

        [HttpDelete]
        [Route("{id:length(24)}")]
        public async Task<IActionResult> DeleteAsync(string Id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Id))
                    return BadRequest("Product name missing");
                await _productService.RemoveProduct(Id);
                return Ok("Your product has been deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        

        [HttpPut]
        [Route("api/product/updatePrice")]
        public async Task<IActionResult> UpdatePrice(Product model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
                return BadRequest("Product name missing");
            //  model.UpdatedOn = DateTime.UtcNow;
            var result = await _productService.UpdatePrice(model);
            if (result)
            {
                return Ok("Your product's price has been updated successfully");
            }

            return BadRequest("No product found to update");

        }


    }
}