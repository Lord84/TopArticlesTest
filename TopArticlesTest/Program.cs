using System;

namespace TopArticlesTest
{
    class Program
    {        
        static async void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var successful = int.TryParse(args[0], out int limit);
                if (successful)
                {
                    var articles = new Articles();
                    var topArticles = await articles.TopArticles(limit);

                    Console.WriteLine(string.Join(Environment.NewLine, topArticles));
                }
                else
                {
                    Console.WriteLine("Invalid limit value");
                }
            }
            else
            {
                Console.WriteLine("Limit not defined");
            }
        }
    }
}
