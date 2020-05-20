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
                resourceManager.Value.Require("stylesheet", "HighlightJS").AtHead();
                resourceManager.Value.Require("script", "HighlightJS").AtHead();
                resourceManager.Value.Require("script", "HighlightJSInitialization").AtHead();
            }

            base.BuildDisplayShape(context);
        }
    }
}

