using Orchard.UI.Resources;

namespace Markdown {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();
            manifest.DefineStyle("HighlightJS").SetUrl("darcula.min.css");
            manifest.DefineScript("HighlightJS").SetUrl("highlight.min.js");
            manifest.DefineScript("HighlightJSInitialization").SetUrl("highlight-initialization.js");
        }
    }
}
