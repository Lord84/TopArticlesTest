using System;

namespace TopArticlesTest
{
    class Program
    {        
        public static void Main(string[] args)
        {
            Console.WriteLine("Program started");

            Console.Write("Enter limit (press x to exit): ");
            var inputValue = Console.ReadLine();

            while (!inputValue.Equals("x", StringComparison.OrdinalIgnoreCase))
            {
                var successful = int.TryParse(inputValue, out int limit);
                if (successful)
                {
                    var articles = new Articles();
                    var topArticles = articles.TopArticles(limit).Result;

                    Console.WriteLine(Environment.NewLine + "[Top Articles]" + Environment.NewLine + string.Join(Environment.NewLine, topArticles));
                }
                else
                {
                    Console.WriteLine("Error: Invalid limit value");
                }

                Console.Write(Environment.NewLine + "Enter limit (press x to exit): ");
                inputValue = Console.ReadLine();
            }
        }
    }
}
