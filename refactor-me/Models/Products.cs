using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using refactor_me.ModelBinder;
using System.Web.Http.ModelBinding;


namespace refactor_me.Models
{
   
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [RegularExpression(@"^\d+(\.\d{1,2})?$ ", ErrorMessage = "Must be decimal")]
        public decimal? Price { get; set; }
        [RegularExpression(@"^\d+(\.\d{1,2})?$ ", ErrorMessage = "Must be decimal")]
        public decimal DeliveryPrice { get; set; }
    }

    public class Products
    {
        public List<Product> Items { get; private set; }
        public Products()
        {
            Items = new List<Product>();
        }
     }

    public class ProductOption
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }

    public class ProductOptions
    {
        public List<ProductOption> Items { get; private set; }
        public ProductOptions()
        {
            Items = new List<ProductOption>();
        }
     }

   
}