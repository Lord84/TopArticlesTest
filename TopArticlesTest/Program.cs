using System;

namespace TopArticlesTest
{
    class Program
    {        
        public static void Main(string[] args)
        {
            Console.WriteLine("Program started");

            var userInput = GetUserInput();

            while (!userInput.Equals("x", StringComparison.OrdinalIgnoreCase))
            {
                var successful = int.TryParse(userInput, out int limit);
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

                userInput = GetUserInput();
            }
        }

        public static string GetUserInput()
        {
            Console.Write("Enter limit (press x to exit): ");
            var userInput = Console.ReadLine();

            return userInput;
        }
    }
}
