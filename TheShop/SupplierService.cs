using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TheShop
{

    public class SupplierService : ISupplierService
    {
		private List<Supplier> _suppliers = new List<Supplier>();
        public SupplierService()
        {
			 
			_suppliers.Add(new Supplier());
			_suppliers.Add(new Supplier());
			_suppliers.Add(new Supplier());
			_suppliers[0].AddArticle(new Article()
			{
                ID = 1,
                Name = "Article from supplier1",
				Price = 458
			});
			_suppliers[1].AddArticle(new Article()
			{
				ID = 1,
				Name = "Article from supplier2",
				Price = 459
			});
			_suppliers[2].AddArticle(new Article()
			{
				ID = 1,
				Name = "Article from supplier3",
				Price = 460
			});
		}

		public SupplierService(List<Supplier> supliers)
		{
			_suppliers = supliers;
		}


		public Article OrderArticle(int id, int maxExpectedPrice)
        {
			foreach(Supplier s in _suppliers)
            {
				if(s.IsArticleInInventory(id))
                {
					Article article = s.GetArticle(id);
					if(article.Price <= maxExpectedPrice)
                    {
						return article;
                    }

				}
            }
			return null;
        }		

	}




}
