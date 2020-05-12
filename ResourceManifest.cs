using Orchard.UI.Resources;

namespace Markdown {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();
        }
    }
}
