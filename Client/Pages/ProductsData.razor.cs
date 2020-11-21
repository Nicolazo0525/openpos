using Grpc.Net.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components;
using openpos.Shared;
using System.Net.Http;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Components.Web;

namespace openpos.Client.Pages
{
   // [Inject]
  
    public class ProductsData_razor:ComponentBase
    {
        
      //  [Inject]
       // protected ProductServiceGrpc.ProductServiceGrpcClient _productService { get; set; }
        [Inject]
        public HttpClient Http { get; set; }
        protected IList<Product>? products;
        protected string SearchString { get; set; }
        public Product product = new Product();
       // GrpcChannel Channel;
       [Inject]
       private GrpcChannel Channel { get; set; }
       
       
      //variables front
      public bool ShowModeletePopup = false;
      public string DeleteItemId { get; set; }
      public bool ShowModel = false;
      public bool ShowAlert = false;
      public string ActionText = "";
      
      public string OperationStatusText = "";
      
      public string PopupTitle = "";
      //for categories
      [Parameter] public EventCallback<KeyboardEventArgs> OnKeyPress { get; set; }
      [Parameter]  public EventCallback<FocusEventArgs> OnBlur { get; set; }
      [Parameter] public bool IsDisabled { get; set; }
      [Parameter] public string CategorySelecId { get; set; }
      public IEnumerable<Category> CategoryList {
          get;
          set;
      }


      protected override async Task OnInitializedAsync()
        {
            try
            {

               var client = new  ProductServiceGrpc.ProductServiceGrpcClient(Channel);
               products = (await client.GetProductsAsync(new Empty())).Products;
               await GetCategories();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }
        
        protected async Task GetProductListAsync()
        {
              
            var client = new  ProductServiceGrpc.ProductServiceGrpcClient(Channel);
            products = (await client.GetProductsAsync(new Empty())).Products;
         
        }

        protected async Task GetCategories()
        {
            var client = new  ProductServiceGrpc.ProductServiceGrpcClient(Channel);
            CategoryList = (await client.GetCategoriesAsync(new Empty())).Categories;

        }

        protected async Task<Product> GetProduct(string id)
        {
            var client= new ProductServiceGrpc.ProductServiceGrpcClient(Channel);
            product =  (await client.GetProductAsync(new GetProductRequest() {Id = id})).Product;
            return product;
        }
        
        protected async Task PostDataAsync()
        {
            
            try
            {
                var client = new  ProductServiceGrpc.ProductServiceGrpcClient(Channel);
               // products = (await client.GetProductsAsync(new Empty())).Products;
                bool status = false;
                if (product.Id != null)
                {
                    var product = await client.CreateOrUpdateProductAsync(new CreateOrUpdateProductRequest()
                    {
                        Product = this.product,
                       
                    });
                    if (product != null)
                    {
                        var response = new CreateOrUpdateProductResponse()
                        {
                            Product = this.product,
                            Success = true
                        };
                        
                            status = true;
                        
                        

                    }
                  
                   
                    
                }
              //  else
               // {
                //    status = await ToDoService.AddToDoData(this.ToDoDataItem);
              //  }
                await Reload(status);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }

        }
        protected async Task Reload(bool status)
        {
            ShowModeletePopup = false;
            ShowModel = false;
            await GetProductListAsync();
            ShowAlert = true;
            if (status)
            {
                OperationStatusText = "1";
            }
            else
            {
                OperationStatusText = "0";
            }

        }
        protected async Task SearchProduct()
        {
           // await GetProductListAsync();
            if (!string.IsNullOrEmpty(SearchString))
            {
                products = products.Where(x => x.Name.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) != -1).ToList();
            }
        }

      

        protected async Task DeleteDataAsync()
        {
            var client = new  ProductServiceGrpc.ProductServiceGrpcClient(Channel);
            var productdelete = await client.DeleteProductAsync(new GetProductRequest(){Id = DeleteItemId});
            //  product = products.FirstOrDefault(x => x.Id == ID);
            if (productdelete.Status)
            {
             await GetProductListAsync();
            }
            else
            {
                Console.Write("Error de borrado");
            }
        }
        
        //operationes con el front
        protected void ShowDeletePopup(string Id)
        {
            Console.Write(Id);
            DeleteItemId = Id;
            ShowModeletePopup = true;
        }
        
        protected  void DismissPopup()
        {
            ShowModel = false;
            ShowAlert = false;
            ShowModeletePopup = false;
          
        }
        
        protected async Task ShowEditForm(string Id)
        {
            Console.Write(Id);
            PopupTitle = "Editar";
            ActionText = "Update";
            product = await GetProduct(Id);
            ShowModel = true;
        }
        
        protected void ShowAddpopup()
        {
            product = new Product() { Name = "", Description = "", Price = 0 };
            PopupTitle = "Add new Product";
            ActionText = "Add";
            ShowModel = true;

        }

    }
   
    


}