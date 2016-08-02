using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using refactor_me.Models;

namespace refactor_me.ModelBiz
{
    public interface IProductRepository
    {
        Models.Products GetProductsAll();
        Models.Product GetProductByID(Guid productId);
        Models.Products GetProductsByName(string name);
        Models.ProductOptions GetProductOptionsAll(Guid productId);
        Models.ProductOption GetProductOptionsByID(Guid productId, Guid optionid);

        bool CreateNewProduct(Models.Product prod);
        bool CreateNewProductOptions(Guid Prodid, Models.ProductOption prodOption);

        bool UpdateProductByID(Guid Prodid, Models.Product prod);
        bool UpdateProductOptionByID(Guid Prodid, Models.ProductOption prodOption);

        bool DeleteProductById(Guid id);
        bool DeleteProductOptionByID(Guid id);


    }
}
