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

		/// <summary>
		/// Orders and sells article with given parameters.
		/// </summary>
		/// <param name="id"> </param>
		/// <param name="maxExpectedPrice"></param>
		/// <param name="buyerId"></param>
		/// <returns>True if article is ordered and sold. False if there is no article with given paramteres. Exception if there is a problem with saving article
		/// to the database.</returns>
		public bool OrderAndSellArticle(int id, int maxExpectedPrice, int buyerId)
		{
						
			Article article = suppliers.OrderArticle(id, maxExpectedPrice);
		
					
			if (article == null)
			{
				return false;
			}

			logger.Debug("Trying to sell article with id=" + id);
									
			try
			{
				DatabaseDriver.Save(article);
				article.IsSold = true;
				article.SoldDate = DateTime.Now;
				article.BuyerUserId = buyerId;
				logger.Info("Article with id=" + id + " is sold.");
				
			}
			catch (Exception)
			{
				logger.Error("Could not save article with id=" + id);
				throw;
			}

			return true;
				
		}

		public Article GetById(int id)
		{
			try
			{
				return DatabaseDriver.GetById(id);
			} catch (Exception ex)
            {
				logger.Error("Coudn't find article with id=" + id + " " + ex);
				throw ;
            }
		}
	}


}
