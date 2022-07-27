using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopArticlesTest.Models
{
    public class ArticleModel
    {
        public string article_name { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string author { get; set; }
        public int? num_comments { get; set; }
        public int? story_id { get; set; }
        public string story_title { get; set; }
        public string story_url { get; set; }
        public int? parent_id { get; set; }
        public long created_at { get; set; }
    }
}
