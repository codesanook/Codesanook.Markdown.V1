using Orchard.UI.Resources;

namespace Markdown {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();
            manifest.DefineScript("Markdown_Converter").SetUrl("Markdown.Converter.min.js", "Markdown.Converter.js");
            manifest.DefineScript("Markdown_Sanitizer").SetUrl("Markdown.Sanitizer.min.js", "Markdown.Sanitizer.js").SetDependencies("Markdown_Converter");
            manifest.DefineScript("Markdown_Editor").SetUrl("Markdown.Editor.min.js", "Markdown.Editor.js").SetDependencies("Markdown_Sanitizer");
            manifest.DefineScript("CodesnookMarkdown").SetUrl("codesnook-markdown.min.js", "codesnook-markdown.js").SetDependencies("Markdown_Editor");
        }
    }
}
