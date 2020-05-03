using Orchard.UI.Resources;

namespace Markdown {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();
            manifest.DefineScript("OrchardMarkdown-MediaPicker").SetUrl("orchard-markdown-media-picker.min.js", "orchard-markdown-media-picker.js");
            manifest.DefineScript("OrchardMarkdown-MediaLibrary").SetUrl("orchard-markdown-media-library.min.js", "orchard-markdown-media-library.js");
            manifest.DefineStyle("FontAwesome4.7").SetUrl("font-awesome.min.css", "font-awesome.css").SetVersion("4.7.0").SetCdn("//maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css", "//maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.css");
        }
    }
}
