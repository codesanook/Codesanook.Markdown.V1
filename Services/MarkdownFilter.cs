using System;
using Orchard.Services;

namespace Codesanook.Markdown.Services {

    public class MarkdownFilter : IHtmlFilter {

        public string ProcessContent(string text, string flavor) =>
            string.Equals(flavor, "markdown", StringComparison.OrdinalIgnoreCase)
                ? TransformMarkdownContent(text)
                : text;

        private static string TransformMarkdownContent(string text) {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            var markdown = new MarkdownSharp.Markdown();
            return markdown.Transform(text);
        }
    }
}