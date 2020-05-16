using System;
using Markdig;
using Orchard.Services;

namespace Codesanook.Markdown.Services {

    public class MarkdownFilter : IHtmlFilter {

        public string ProcessContent(string text, string flavor) =>
            string.Equals(flavor, "markdown", StringComparison.OrdinalIgnoreCase)
                ? TransformMarkdownContent(text)
                : text;

        private static string TransformMarkdownContent(string markdownContent) {
            if (string.IsNullOrEmpty(markdownContent)) return string.Empty;
            var pipeline = new MarkdownPipelineBuilder()
                .UseTaskLists()
                .UsePipeTables()
                .Build();
            return Markdig.Markdown.ToHtml(markdownContent, pipeline);

        }
    }
}
