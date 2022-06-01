using System;
using log4net;

namespace TheShop
{
    public class ShopService
	{
		private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private DatabaseDriver DatabaseDriver;		 
		private SuppliersService suppliers;


		public ShopService()
		{
			log4net.Config.XmlConfigurator.Configure();
			DatabaseDriver = new DatabaseDriver();			 
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
            else
            {
				Logger.Debug("Article with id=" + id + " is found.");
			}
			
									
			try
			{
				SellArticle(article, buyerId);

			}
			catch (Exception ex)
			{
				Logger.Error("Could not save article with id=" + id, ex);
				throw;
			}

			return true;
				
		}

		public void SellArticle(Article article,int buyerId)
        {
			Logger.Debug("Trying to sell article with id=" + article.ID);
			DatabaseDriver.Save(article);
			article.IsSold = true;
			article.SoldDate = DateTime.Now;
			article.BuyerUserId = buyerId;
			Logger.Info("Article with id=" + article.ID + " is sold.");
		}

		public Article GetById(int id)
		{
			try
			{
				return DatabaseDriver.GetById(id);
			} catch (Exception ex)
            {
				Logger.Error("Coudn't find article with id=" + id + " ", ex);
				throw ;
            }
		}
	}


}
