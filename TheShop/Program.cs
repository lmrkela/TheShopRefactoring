using System;

namespace TheShop
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var shopService = new ShopService();

			try
			{
				//order and sell				
                if (shopService.OrderAndSellArticle(1, 500, 10))
                {
					Console.WriteLine("Article successfully ordered and sold.");					
                }
                else
                {
					Console.WriteLine("There is no article with entered ID and maximum price.");
				}
			}
			catch (Exception)
			{
				Console.WriteLine("Coudn't save article.");				
			}

			try
			{
				//print article on console
				var article = shopService.GetById(1);
				Console.WriteLine("Found article with ID: " + article.ID);
			}
			catch (Exception)
			{
				Console.WriteLine("Article is not found.");
			}

			try
			{
				//print article on console				
				var article = shopService.GetById(12);
				Console.WriteLine("Found article with ID: " + article.ID);
			}
			catch (Exception)
			{
				Console.WriteLine("Article is not found.");
			}

			Console.ReadKey();
		}
	}
}