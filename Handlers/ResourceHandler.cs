using Orchard.ContentManagement.Handlers;
using Orchard.Core.Common.Models;
using Orchard.Environment;
using Orchard.UI.Resources;

namespace Codesanook.Markdown.Handlers {
    public class ResourceHandler : ContentHandler {
        private readonly Work<IResourceManager> resourceManager;

        public ResourceHandler(Work<IResourceManager> resourceManager) =>
            this.resourceManager = resourceManager;

        protected override void BuildDisplayShape(BuildDisplayContext context) {
            if (
                context.ContentItem.ContentType == "BlogPost" &&
                context.ContentItem.Has(typeof(BodyPart)) &&
                context.DisplayType == "Detail"
            ){
                resourceManager.Value.Require("stylesheet", "highlight").AtHead();
                resourceManager.Value.Require("script", "highlight").AtHead();
                resourceManager.Value.Require("script", "highlight-init").AtHead();

                resourceManager.Value.Require("stylesheet", "codesanook-markdown").AtHead();
            }

            base.BuildDisplayShape(context);
        }
    }
}

