using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using refactor_me.DataAccess;

namespace refactor_me.ModelBiz
{
    public class ProductRepository : IProductRepository
    {
        private ProductsModel db = new ProductsModel();
        #region Get 
        public Models.Products GetProductsAll()
        {
            Models.Products allProducts = new Models.Products();
            var lst = from temp in db.Products select temp;
            if (lst.Count() > 0)
            {
                foreach (DataAccess.Product prod in lst)
                    allProducts.Items.Add(FillProduct(prod));
            }
            return allProducts;
        }



        public Models.Product GetProductByID(Guid productId)
        {
            Models.Product objProduct = new Models.Product();
            var lst = from temp in db.Products where temp.Id == productId select temp;
            objProduct = FillProduct(lst.First());
            return objProduct;
        }

        public Models.Products GetProductsByName(string name)
        {
            Models.Products ProdsByName = new Models.Products();
            var lst = from temp in db.Products where temp.Name == name select temp;
            if (lst.Count() > 0)
            {
                foreach (DataAccess.Product prod in lst)
                    ProdsByName.Items.Add(FillProduct(prod));
            }
            else
                ProdsByName = null;

           return ProdsByName;
        }



        public Models.ProductOptions GetProductOptionsAll(Guid productId)
        {
            Models.ProductOptions ProdOptions = new Models.ProductOptions();
            var lst = from temp in db.ProductOptions where temp.ProductId == productId select temp;

            if (lst.Count() > 0)
            {
                foreach (DataAccess.ProductOption prod in lst)
                    ProdOptions.Items.Add(FillProductOption(prod));
            }
            else
                ProdOptions = null;
            return ProdOptions;

        }

        public Models.ProductOption GetProductOptionsByID(Guid productId, Guid optionid)
        {

            Models.ProductOption ProdOption = new Models.ProductOption();
            var lst = from temp in db.ProductOptions where temp.ProductId == productId && temp.Id == optionid select temp;

            ProdOption = FillProductOption(lst.First());
            return ProdOption;
        }

        private Models.Product FillProduct(DataAccess.Product prod)
        {

            Models.Product locProd = new Models.Product();
            locProd.Id = prod.Id;
            locProd.Name = prod.Name;
            locProd.Description = prod.Description;
            locProd.Price = prod.Price;
            locProd.DeliveryPrice = prod.DeliveryPrice;

            return locProd;
        }

        private Models.ProductOption FillProductOption(DataAccess.ProductOption prod)
        {

            Models.ProductOption locProdOption = new Models.ProductOption();
            locProdOption.Id = prod.Id;
            locProdOption.ProductId = prod.ProductId;
            locProdOption.Name = prod.Name;
            locProdOption.Description = prod.Description;
            return locProdOption;
        }
        #endregion

        #region Create

        public bool CreateNewProduct(Models.Product prod)
        {
             try
            {
                    DataAccess.Product objProd = new DataAccess.Product();
                    prod.Id = Guid.NewGuid();
                    objProd = FillProduct(prod);
                    db.Products.Add(objProd);
                    db.SaveChanges();
                    return true;
            
            }

            catch (Exception e)
            {
                return false;
            }
        }


        public bool CreateNewProductOptions(Guid Prodid, Models.ProductOption prod)
        {
            var lst = from temp in db.Products where temp.Id == Prodid select temp;
            try
            {
                if (lst.Count() > 0)
                {
                    DataAccess.ProductOption objProdOption = new DataAccess.ProductOption();
                    prod.Id = Guid.NewGuid();
                    objProdOption = FillProductOptions(Prodid, prod);
                    db.ProductOptions.Add(objProdOption);
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }

            catch (Exception e)
            {
                return false;
            }
        }


        private DataAccess.Product FillProduct(Models.Product prod)
        {
            DataAccess.Product locProd = new DataAccess.Product();
            locProd.Id = prod.Id;
            locProd.Name = prod.Name;
            locProd.Description = prod.Description;
            locProd.Price = prod.Price ?? 0;  //(prod.Price== null)?
            locProd.DeliveryPrice = prod.DeliveryPrice;

            return locProd;
        }

        private DataAccess.ProductOption FillProductOptions(Guid Prodid, Models.ProductOption prod)
        {
            DataAccess.ProductOption locProd = new DataAccess.ProductOption();
            locProd.Id = prod.Id;
            locProd.Name = prod.Name;
            locProd.ProductId = Prodid;
            locProd.Description = prod.Description;
            return locProd;
        }

        #endregion

        #region Update
        public bool UpdateProductByID(Guid Prodid, Models.Product prod)
        {
            DataAccess.Product lProd;
            bool bStatus = false;
            try
            {
                using (var ctx = new ProductsModel())
                {
                    lProd = ctx.Products.Where(s => s.Id == Prodid).FirstOrDefault<DataAccess.Product>();
                }

                if (lProd != null)
                {
                    lProd.Name = prod.Name;
                    lProd.Price = prod.Price ?? 0; //prod.Price;
                    lProd.Description = prod.Description;
                }
                else
                    bStatus = false;

                using (var dbCtx = new ProductsModel())
                {
                    dbCtx.Entry(lProd).State = System.Data.Entity.EntityState.Modified;
                    dbCtx.SaveChanges();
                    bStatus =  true;
                }
                return bStatus;
            }

            catch (Exception e)
            {
                return false;
            }
        }


        public bool UpdateProductOptionByID(Guid Prodid, Models.ProductOption prodOption)
        {
            DataAccess.ProductOption lProd;
            bool bStatus = false;
            try
            {
                using (var ctx = new ProductsModel())
                {
                    lProd = ctx.ProductOptions.Where(s => s.ProductId == prodOption.ProductId && s.Id == prodOption.Id).FirstOrDefault<DataAccess.ProductOption>();
                }

                if (lProd != null)
                {
                    lProd.Name = prodOption.Name;
                    lProd.Description = prodOption.Description;
                }
                else
                    bStatus = false;

                using (var dbCtx = new ProductsModel())
                {
                    dbCtx.Entry(lProd).State = System.Data.Entity.EntityState.Modified;
                    dbCtx.SaveChanges();
                    bStatus = true;
                }
                return bStatus;
            }

            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region Delete

        public bool DeleteProductById(Guid id)
        {
            DataAccess.Product itemToRemove;
          //  DataAccess.ProductOption itemToRemoveOption;
            bool bStatus = false;
            try
            {
                using (var ctx = new ProductsModel())
                {
                    itemToRemove = ctx.Products.Where(s => s.Id == id).FirstOrDefault<DataAccess.Product>();
                    ctx.ProductOptions.RemoveRange(ctx.ProductOptions.Where(s =>s.ProductId == id));

                    if (itemToRemove != null)
                    {
                        ctx.Products.Remove(itemToRemove);
                        ctx.SaveChanges();
                        bStatus = true;
                    }
                    else
                        bStatus = false;
                }
                return bStatus;
              }
            catch (Exception e)
            {
                return false;
            }
         }
        public bool DeleteProductOptionByID(Guid id)
        {

            DataAccess.ProductOption itemToRemove;
            bool bStatus = false;
            try
            {
                using (var ctx = new ProductsModel())
                {
                    itemToRemove = ctx.ProductOptions.Where(s =>s.Id == id).FirstOrDefault<DataAccess.ProductOption>();
                    if (itemToRemove != null)
                    {
                        ctx.ProductOptions.Remove(itemToRemove);
                        ctx.SaveChanges();
                        bStatus = true;
                    }
                    else
                        bStatus = false;
                }
                return bStatus;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion


    }
}
