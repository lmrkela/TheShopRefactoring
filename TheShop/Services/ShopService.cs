using System;
using log4net;

namespace TheShop
{
    public class ShopService
	{
		private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private IDatabaseDriver _databaseDriver;		 
		private ISupplierService _supplierService;


		public ShopService()
		{
			configureLogger();
			_databaseDriver = new DatabaseDriver();			 
			_supplierService = new SupplierService();
		}

		public ShopService(IDatabaseDriver databaseDriver, ISupplierService supplierService)
		{
			configureLogger();
			_databaseDriver = databaseDriver;
			_supplierService = supplierService;
		}

		private void configureLogger()
        {
			log4net.Config.XmlConfigurator.Configure();
			//Could be extended to include log files, levels, etc...
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
						
			Article article = _supplierService.OrderArticle(id, maxExpectedPrice);
		
					
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

		/// <summary>
		/// Sells article and sets sold date and buyer user id 
		/// </summary>
		/// <param name="article"></param>
		/// <param name="buyerId"></param>
		public void SellArticle(Article article,int buyerId)
        {
			Logger.Debug("Trying to sell article with id=" + article.ID);
			_databaseDriver.Save(article);
			article.IsSold = true;
			article.SoldDate = DateTime.Now;
			article.BuyerUserId = buyerId;
			Logger.Info("Article with id=" + article.ID + " is sold.");
		}

		public Article GetById(int id)
		{
			try
			{
				return _databaseDriver.GetById(id);
			} catch (Exception ex)
            {
				Logger.Error("Coudn't find article with id=" + id + " ", ex);
				throw ;
            }
		}
	}


}
