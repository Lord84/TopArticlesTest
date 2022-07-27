using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopArticlesTest.Constants;
using TopArticlesTest.Models;
using TopArticlesTest.ServiceProviders;

namespace TopArticlesTest
{
    public class Articles
    {
        private readonly WebRequestProvider WebRequestProvider;

        public Articles()
        {
            WebRequestProvider = new WebRequestProvider(Constant.ArticlesURL);
        }

        public async Task<string[]> TopArticles(int limit)
        {
            string[] topArticles;            

            if (limit > 0)
            {
                try
                {
                    var articleData = new List<ArticleModel>();
                    var pageNumber = 1;
                    var totalPages = 1;

                    while (pageNumber <= totalPages)
                    {
                        Console.WriteLine($"Fetching articles on page {pageNumber}...");
                        var (success, message, articlePageModel) = await WebRequestProvider.GetArticles(pageNumber);
                        if (success)
                        {
                            totalPages = articlePageModel.total_pages;

                            if (articlePageModel.data != null)
                            {
                                foreach (var article in articlePageModel.data)
                                {
                                    var articleName = article.title ?? article.story_title;

                                    if (!string.IsNullOrEmpty(articleName))
                                    {
                                        article.article_name = articleName;
                                        articleData.Add(article);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: " + message);
                            break;
                        }

                        pageNumber++;
                    }
                    
                    //var a = articleData.OrderByDescending(i => i.num_comments)
                    //    .ThenByDescending(i => i.article_name.ToLower())
                    //    .Select(i => $"{i.num_comments.ToString().PadRight(6)} {i.article_name}")
                    //    .ToArray();

                    //Console.WriteLine(string.Join(Environment.NewLine, a));

                    topArticles = articleData
                        .OrderByDescending(i => i.num_comments)
                        .ThenByDescending(i => i.article_name.ToLower())
                        .Take(limit)
                        .Select(i => i.article_name)
                        .ToArray();
                }
                catch (Exception ex)
                {
                    topArticles = Array.Empty<string>();
                    Console.WriteLine("Error: " + ex.Message + Environment.NewLine + "StackTrace:" + Environment.NewLine + ex.StackTrace);
                }
            }
            else
            {
                topArticles = Array.Empty<string>();
            }

            return topArticles;
        }
    }
}
