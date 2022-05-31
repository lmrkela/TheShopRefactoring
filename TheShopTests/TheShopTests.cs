using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TheShop;

namespace TheShopTests
{

    [TestClass]
    public class TheShopTests
    {
        [TestMethod]
        public void ArticleOrder_NotFound_ID()
        {
            SuppliersService sup = new SuppliersService();

            var article = sup.OrderArticle(20, 1000);

            Assert.IsNull(article);
        }

        [TestMethod]
        public void ArticleOrder_NotFound_Price()
        {
            SuppliersService sup = new SuppliersService();

            var article = sup.OrderArticle(1, 300);

            Assert.IsNull(article);
        }

        [TestMethod]
        public void ArticleOrder_Found()
        {
            List<Supplier> suppliers = new List<Supplier>();
           
            Article article1 = new Article()
            {
                ID = 1,               
                ArticlePrice = 500
            };

            Article article2 = new Article()
            {
                ID = 1,              
                ArticlePrice = 458
            };

            Article article3 = new Article()
            {
                ID = 1,              
                ArticlePrice = 300
            };

            Supplier supplier1 = new Supplier();
            supplier1.AddArticle(article1);
            Supplier supplier2 = new Supplier();
            supplier2.AddArticle(article2);
            Supplier supplier3 = new Supplier();
            supplier3.AddArticle(article3);

            suppliers.Add(supplier1);
            suppliers.Add(supplier2);
            suppliers.Add(supplier3);

            SuppliersService sup = new SuppliersService(suppliers);

            var article = sup.OrderArticle(1, 460);
                        
            Assert.IsNotNull(article);
            Assert.AreEqual(article2, article);
        }

        [TestMethod]
        public void ArticleOrderAndSold_Success()
        {
            ShopService sup = new ShopService();

            sup.OrderAndSellArticle(1, 500, 5);
            Article article = sup.GetById(1);

            Assert.AreEqual(1, article.ID);
            Assert.AreEqual(5, article.BuyerUserId);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ArticleOrderAndSold_OrderProblem()
        {
            ShopService sup = new ShopService();

            sup.OrderAndSellArticle(3, 500, 5);
            sup.GetById(3);            
        }

    }
}