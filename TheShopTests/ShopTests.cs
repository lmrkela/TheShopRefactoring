using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TheShop;

namespace TheShopTests
{

    [TestClass]
    public class ShopTests
    {
       

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
        [ExpectedException(typeof(InvalidOperationException))]
        public void ArticleOrderAndSold_OrderProblem()
        {
            ShopService sup = new ShopService();

            sup.OrderAndSellArticle(3, 500, 5);
            sup.GetById(3);            
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ArticleOrderAndSold_OrderProblem2()
        {
            ShopService sup = new ShopService();

            sup.OrderAndSellArticle(1, 500, 5);
            sup.GetById(3);
        }

    }
}
