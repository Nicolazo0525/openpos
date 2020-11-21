using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Components;
using MongoDB.Bson;
using MongoDB.Driver;
using openpos.Server.Context;
using openpos.Server.Repositories;
using openpos.Shared;
//using Category = openpos.Server.ModelData.Category;

namespace openpos.Server.Services
{
    public class ProductService:ProductServiceGrpc.ProductServiceGrpcBase
    {
       // private readonly IMongoRepository<ModelData.Product> _productRepository;
        private readonly IMapper _mapper;
   
        private readonly IProductRepository _productMongoServiceService;

        public ProductService(IProductRepository productMongoServiceService, IMapper mapper)
        {
            _productMongoServiceService = productMongoServiceService;
            _mapper = mapper;
        }
       

        public override async Task<GetProductResponse> GetProduct(GetProductRequest request, ServerCallContext context)
        {
            try
            {
                if (request.Id != null)
                {
                    var product = await _productMongoServiceService.GetProduct(request.Id);
                    return new GetProductResponse
                    {
                        Product = _mapper.Map<Product>(product)
                       
                    };
                }
                else
                {
                    return new GetProductResponse
                    {
                        Error = "ID is null or empty"
                    };
                }
            }
            catch (Exception ex)
            {
                return new GetProductResponse
                {
                    Error = $"{ex.Message}"
                };
            }
        }


      

        public override async Task<GetProductsResponse> GetProducts(Empty request, ServerCallContext context)
        {
            try
            {
                //return base.GetProducts(request, context);
                  
                    var products = await _productMongoServiceService.GetAllProducts();
                    var results= new GetProductsResponse
                    {
                        
                        Products= {_mapper.Map<IList<Product>>(products)},
                       
                    };
                    
                    var response = new GetProductsResponse();
                    response.Products.AddRange(results.Products);
                    return Task.FromResult(response).Result;
                
               
            }
            catch (Exception ex)
            {
                return new GetProductsResponse
                {
                    Error = $"{ex.Message}"
                };
            }
            
        }

     
     
        public override async Task<CreateOrUpdateProductResponse> CreateOrUpdateProduct(CreateOrUpdateProductRequest request, ServerCallContext context)
        {
            ModelData.Product productResult = new ModelData.Product();

            var productnew = new ModelData.Product()
            {
                Id=request.Product.Id,
                Name = request.Product.Name,
                Description = request.Product.Description,
             //   Code = request.Product.Code,
                Price    = request.Product.Price,
             //   CategoriesList = request.Product.Categories

            };
            var productInsert = _mapper.Map<ModelData.Product>(productnew);
          //  var idnew = ObjectId.Parse(request.Product.Id); // parses a 24 hex digit string
         //   var productExist = await _productMongoServiceService.GetProduct(idnew.ToString());
            if (productInsert.Id !=String.Empty  )
            { 
                 productResult= await   _productMongoServiceService.Update( request.Product.Id, productInsert);//aqui toc primero mapear el proto al modelo del servidor

            }
            else
            {
                 productResult=   await  _productMongoServiceService.AddProduct(productInsert);//aqui toc primero mapear el proto al modelo del servidor

            }
           
            if (productResult!=null)
            {
               
                return new CreateOrUpdateProductResponse
                {
                    Product = _mapper.Map<Product>(productResult),
                 
                    Success = true
                   
                };
            }
            else
            {
                return new CreateOrUpdateProductResponse
                {
                    Error = "producto is null or empty"
                };
            }
            
        }

        public override async Task<ProductDeleteResponse> DeleteProduct(GetProductRequest request, ServerCallContext context)
        {
            try
            {
                
            
          //  var product = await _productMongoServiceService.GetProduct(request.Id);
            await _productMongoServiceService.RemoveData(request.Id);
            return new ProductDeleteResponse()
            {
                StatusMessage = "borrado correctamente",
                StatusCode = 1,
                Status = true
            };
            }
            catch (Exception ex)
            {
                return new ProductDeleteResponse
                {
                    StatusMessage = $"{ex.Message}",
                    StatusCode = 3,
                    Status = false,
                };
            }
        }
        
        //categories
        public override async Task<GetCategoriesResponse> GetCategories(Empty request, ServerCallContext context)
        {
            try
            {
                //return base.GetProducts(request, context);
                  
                var categories = await _productMongoServiceService.GetAllCategories();
                var results= new GetCategoriesResponse
                {
                        
                    Categories= {_mapper.Map<IList<Category>>(categories)},
                       
                };
                    
                var response = new GetCategoriesResponse();
                response.Categories.AddRange(results.Categories);
                return Task.FromResult(response).Result;
                
               
            }
            catch (Exception ex)
            {
                return new GetCategoriesResponse
                {
                    Error = $"{ex.Message}"
                };
            }
        }
    }
}