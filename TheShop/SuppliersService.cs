using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheShop
{

	public class SuppliersService
    {
		private List<Supplier> _suppliers = new List<Supplier>();
        public SuppliersService()
        {
			 
			_suppliers.Add(new Supplier());
			_suppliers.Add(new Supplier());
			_suppliers.Add(new Supplier());
			_suppliers[0].AddArticle(new Article()
			{
                ID = 1,
                Name_of_article = "Article from supplier1",
				ArticlePrice = 458
			});
			_suppliers[1].AddArticle(new Article()
			{
				ID = 1,
				Name_of_article = "Article from supplier2",
				ArticlePrice = 459
			});
			_suppliers[2].AddArticle(new Article()
			{
				ID = 1,
				Name_of_article = "Article from supplier3",
				ArticlePrice = 460
			});
		}

		public Article OrderArticle(int id, int maxExpectedPrice)
        {
			foreach(Supplier s in _suppliers)
            {
				if(s.ArticleInInventory(id))
                {
					Article article = s.GetArticle(id);
					if(article.ArticlePrice <= maxExpectedPrice)
                    {
						return article;
                    }

				}
            }
			return null;
        }

		private class Supplier
		{
			private List<Article> _inventory = new List<Article>();

			public bool ArticleInInventory(int id)
			{
				return true;
			}
			public Article GetArticle(int id)
			{
				return _inventory.Single(x => x.ID == id);
			}

			public void AddArticle(Article a)
			{
				_inventory.Add(a);
			}
		}

	}

	


}
