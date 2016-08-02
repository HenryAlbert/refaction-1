using System;
using System.Net;
using System.Web.Http;
using refactor_me.Models;
using System.Collections.Generic;
using System.Linq;
using refactor_me.ModelBiz;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Text;
using System.Web.Http.Description;
using refactor_me.ModelBinder;
using System.Web.Http.ModelBinding;
using refactor_me.CustomFilters;

namespace refactor_me.Controllers
{
   
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private IProductRepository ProductRepositoryobj;

        public ProductsController()
        {
            this.ProductRepositoryobj = new ProductRepository();
        }

        [Route]
        [HttpGet]
       [ResponseType(typeof(Products))]
        public Products GetAll()
        {
            return ProductRepositoryobj.GetProductsAll();
        }
   
        [Route]
        [HttpGet]
        [ResponseType(typeof(Products))]
        public Products SearchByName([Required]string name)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
            }

            var rec = ProductRepositoryobj.GetProductsByName(name);
            if (rec == null)
            {
                return new Products();
               // var json = new JavaScriptSerializer().Serialize(rec);
               // return json;
               //// Request.CreateErrorResponse(HttpStatusCode.OK, ModelState);
               // return Request.CreateErrorResponse(HttpStatusCode.OK, ModelState);
               // return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(json, Encoding.UTF8, "application/json"), RequestMessage = Request };

            }
            else
                return rec;

        }

        [Route("{id}")]
        [HttpGet]
        [ResponseType(typeof(Product))]
      //  [ValidateGuidAttribute]
        public Product GetProduct(Guid id)
        {

            var rec = ProductRepositoryobj.GetProductByID(id);
            if(rec == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            else
                return rec;
        }

        [Route]
        [HttpPost]
       public void Create([ModelBinder(typeof(ProductModelBinder))] Models.Product prod)
        //  public void Create( Product product)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
            }

            if (!ProductRepositoryobj.CreateNewProduct(prod))
                throw new HttpResponseException(HttpStatusCode.NotFound);
              
                }

        [Route("{id}")]
        [HttpPut]
        public void Update(Guid id, [ModelBinder(typeof(ProductModelBinder))] Models.Product prod )
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
            }
            if (!ProductRepositoryobj.UpdateProductByID(id, prod))
                throw new HttpResponseException(HttpStatusCode.NotFound);
            
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {

            if (!ProductRepositoryobj.DeleteProductById(id))
                throw new HttpResponseException(HttpStatusCode.NotFound);
            
        }
        

        [Route("{productId}/options")]
        [HttpGet]
        public ProductOptions GetOptions(Guid productId)
        {
            var rec = ProductRepositoryobj.GetProductOptionsAll(productId);
            if (rec == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            else
                return rec;
        }


        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ProductOption GetOption(Guid productId, Guid id)
        {
            var rec = ProductRepositoryobj.GetProductOptionsByID(productId, id);
            if (rec == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            else
                return rec;

        }

        [Route("{productId}/options")]
        [HttpPost]
        public void CreateOption(Guid productId, ProductOption option)
        {
             if (!ProductRepositoryobj.CreateNewProductOptions(productId, option))
                throw new HttpResponseException(HttpStatusCode.NotFound);

        }
                
       [Route("{productId}/options/{id}")]
       [HttpPut]
       public void UpdateOption(Guid id, ProductOption option)
       {
                    if (!ProductRepositoryobj.UpdateProductOptionByID(id, option))
                        throw new HttpResponseException(HttpStatusCode.NotFound);

       }    

       [Route("{productId}/options/{id}")]
       [HttpDelete]
       public void DeleteOption(Guid id)
       {
               
            if (!ProductRepositoryobj.DeleteProductOptionByID(id))
                throw new HttpResponseException(HttpStatusCode.NotFound);


        }
    }
}
