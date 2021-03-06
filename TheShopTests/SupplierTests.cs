using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheShop;

namespace TheShopTests
{
    [TestClass]
    public class SupplierTests
    {
        [TestMethod]
        public void ArticleOrder_NotFound_ID()
        {
            SupplierService sup = new SupplierService();

            var article = sup.OrderArticle(20, 1000);

            Assert.IsNull(article);
        }

        [TestMethod]
        public void ArticleOrder_NotFound_Price()
        {
            SupplierService sup = new SupplierService();

            var article = sup.OrderArticle(1, 300);

            Assert.IsNull(article);
        }

        [TestMethod]
        public void ArticleOrder_NotFound_PriceBoundary()
        {
            SupplierService sup = new SupplierService();

            var article = sup.OrderArticle(1, 458);

            Assert.IsNotNull(article);
            Assert.AreEqual(458, article.Price);

        }


        [TestMethod]
        public void ArticleOrder_Found()
        {
            List<Supplier> suppliers = new List<Supplier>();

            Article article1 = new Article()
            {
                ID = 1,
                Price = 500
            };

            Article article2 = new Article()
            {
                ID = 1,
                Price = 458
            };

            Article article3 = new Article()
            {
                ID = 1,
                Price = 300
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

            SupplierService sup = new SupplierService(suppliers);

            var article = sup.OrderArticle(1, 460);

            Assert.IsNotNull(article);
            Assert.AreEqual(article2, article);
        }
    }
}
