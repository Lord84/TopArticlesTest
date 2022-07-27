using Microsoft.VisualStudio.TestTools.UnitTesting;
using TopArticlesTest;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var articles = new Articles();
            var topArticles = articles.TopArticles(2).Result;

            Assert.IsTrue(topArticles.Length == 2);
            Assert.IsTrue(topArticles[0] == "UK votes to leave EU");
            Assert.IsTrue(topArticles[1] == "F.C.C. Repeals Net Neutrality Rules");
        }
    }
}
