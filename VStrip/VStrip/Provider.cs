using System.Collections.Generic;
using AngleSharp.Html.Parser;

namespace VStrip
{
    public class Provider
    {
        public Provider(string title, string url, string content_mask, string pages_mask, string cover) {
            Title = title;
            Url = url;
            Content_Mask = content_mask;
            Pages_Mask = pages_mask;
            Cover = cover;
        }

        public string Title { get; }
        public string Url { get; }
        public string Content_Mask { get; }
        public string Pages_Mask { get; }
        public string Cover { get; }

        public List<(string, string)> GetListOfStrips() {
            List<(string, string)> strips = new List<(string, string)>();
            var parser = new HtmlParser();
            var doc = parser.ParseDocument(Url + "/archive/");
            var all_strips = doc.QuerySelectorAll(Pages_Mask);

            foreach (var strip in all_strips) {
                strips.Add((strip.GetAttribute("href"), strip.TextContent));
            }

            return strips;
        }
    }
}