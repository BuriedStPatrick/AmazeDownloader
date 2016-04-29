using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace AmazeDownloader
{
    public class Program
    {
        static void Main(string[] args)
        {
            var doc = new HtmlDocument();
            doc.Load("file.html");
            var references = Parse(doc);
            var downloader = new Downloader(references);
            downloader.Start();
        }

        private static Dictionary<string, Data> Parse(HtmlDocument doc)
        {
            var res = new Dictionary<string, Data>();
            foreach (var node in doc.DocumentNode.Descendants().Where(IsValid))
            {
                res.Add(GetUrl(node), new Data(GetName(node), GetDate(node)));
            }
            return res;
        }

        private static string GetDate(HtmlNode resultNode)
        {
            return resultNode.Descendants()
                .First(IsDate)
                .InnerText;
        }

        private static string GetName(HtmlNode resultNode)
        {
            var dataColumn = resultNode
                .Descendants()
                .First(IsDataColumn);
            return dataColumn.ChildNodes["a"].InnerText;
        }

        private static string GetUrl(HtmlNode node)
        {
            var imageColumn = node
                .Descendants()
                .First(IsImageColumn);
            var a = imageColumn.ChildNodes["a"];
            return a.Attributes["href"].Value;
        }

        private static bool IsDate(HtmlNode node)
        {
            return node.HasAttributes && node.Attributes["class"] != null && node.Attributes["class"].Value == "date";
        }

        private static bool IsImageColumn(HtmlNode node)
        {
            return node.HasAttributes && node.Attributes["class"] != null && node.Attributes["class"].Value == "imageColumn";
        }

        private static bool IsDataColumn(HtmlNode node)
        {
            return node.HasAttributes && node.Attributes["class"] != null && node.Attributes["class"].Value == "dataColumn";
        }

        private static bool IsValid(HtmlNode node)
        {
            return node.HasAttributes && node.Attributes["class"] != null &&
                   node.Attributes["class"].Value == "searchResultItem";
        }
    }
}
