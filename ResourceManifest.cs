using Orchard.UI.Resources;

namespace Markdown {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();

            manifest.DefineStyle("highlight").SetUrl("darcula.min.css");
            manifest.DefineScript("highlight").SetUrl("highlight.min.js");
            manifest.DefineScript("highlight-init").SetUrl("highlight-init.js");

            manifest.DefineStyle("codesanook-markdown").SetUrl("codesanook-markdown.css");
        }
    }
}
