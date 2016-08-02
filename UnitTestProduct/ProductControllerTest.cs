using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using refactor_me.Controllers;
using refactor_me.Models;
using refactor_me.ModelBiz;


namespace UnitTestProduct
{
    [TestClass]
    public class ProductsControllerTest
    {
        public static Guid newGuid = Guid.NewGuid();

        IProductRepository obj = new ProductRepository();

        [TestMethod]
        public void GetAllProducts()
        {
           
            var prod1 = new Product
            {
                Id = newGuid,
                Name =  "UnitTest Name 1",
                Description = "UnitTest Description 1",
                Price = 123.45M,
                DeliveryPrice = 67.89M
            };

            ProductsController controller = new ProductsController();
            controller.Create(prod1);
            var result = controller.GetAll();
            Assert.IsFalse(result.Items.Contains(prod1));

        }

  
        //[TestMethod]
        //public void UpdateProduct()
        //{
        //     var prod1 = new Product
        //    {
        //        Id = newGuid,
        //        Name = "UnitTestUpdate Name 1",
        //        Description = "UnitTestUpdate Description 1",
        //        Price = 122223.45M,
        //        DeliveryPrice = 6227.89M
        //    };

        //    ProductsController controller = new ProductsController();
        //    controller.Update(newGuid, prod1);
        //     var result2 =  obj.GetProductByID(newGuid);
        //     Assert.AreEqual(result2.Name , prod1.Name);
        //}


    }
    }
