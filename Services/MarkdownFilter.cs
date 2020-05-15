using System;
using Orchard.Services;
using Markdig;

namespace Codesanook.Markdown.Services {

    public class MarkdownFilter : IHtmlFilter {

        public string ProcessContent(string text, string flavor) =>
            string.Equals(flavor, "markdown", StringComparison.OrdinalIgnoreCase)
                ? TransformMarkdownContent(text)
                : text;

        private static string TransformMarkdownContent(string text) {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            return Markdig.Markdown.ToHtml(text);
        }
    }
}
