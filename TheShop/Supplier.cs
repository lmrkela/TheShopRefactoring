using System.Collections.Generic;
using System.Linq;

namespace TheShop
{
    public class Supplier
	{
		private List<Article> _inventory = new List<Article>();

		public bool IsArticleInInventory(int id)
		{
			return _inventory.Exists(x => x.ID == id);
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
