using System;

namespace TheShop
{
    public class ShopService
	{
		private DatabaseDriver DatabaseDriver;
		private Logger logger;
		private SuppliersService suppliers;


		public ShopService()
		{
			DatabaseDriver = new DatabaseDriver();
			logger = new Logger();
			suppliers = new SuppliersService();
		}

		public void OrderAndSellArticle(int id, int maxExpectedPrice, int buyerId)
		{
						
			Article article = suppliers.OrderArticle(id, maxExpectedPrice);
		
					
			if (article == null)
			{
				throw new Exception("Could not order article");
			}

			logger.Debug("Trying to sell article with id=" + id);

			article.IsSold = true;
			article.SoldDate = DateTime.Now;
			article.BuyerUserId = buyerId;
			
			try
			{
				DatabaseDriver.Save(article);
				logger.Info("Article with id=" + id + " is sold.");
				
			}
			catch (ArgumentNullException ex)
			{
				logger.Error("Could not save article with id=" + id);
				throw new Exception("Could not save article with id");
			}
			catch (Exception)
			{
			}			
		}

		public Article GetById(int id)
		{
			return DatabaseDriver.GetById(id);
		}
	}


}
