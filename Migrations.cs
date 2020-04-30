//https://google.github.io/styleguide/javaguide.html#s3-source-file-structure

using System.Linq;
using Orchard.ContentManagement.MetaData;
using Orchard.Data.Migration;

namespace Markdown {

    public class Migrations : DataMigrationImpl {
        private readonly IContentDefinitionManager contentDefinitionManager;

        public Migrations(IContentDefinitionManager contentDefinitionManager) =>
            this.contentDefinitionManager = contentDefinitionManager;

        public int Create() {
            if (DoesBodyPartOfBlogPostTypeExist()) {
                SetEditorToMarkdown();
            }

            return 1;
        }

        private bool DoesBodyPartOfBlogPostTypeExist() {
            var blogPost = contentDefinitionManager.GetTypeDefinition("BlogPost");
            if (blogPost == null) return false;

            var bodyPart = blogPost.Parts.SingleOrDefault(x => x.PartDefinition.Name == "BodyPart");
            return bodyPart != null;
        }

        private void SetEditorToMarkdown() {
            contentDefinitionManager.AlterTypeDefinition(
                "BlogPost",
                build => build.WithPart(
                    "BodyPart",
                    // Change setting of content type
                    // https://docs.orchardproject.net/en/latest/Documentation/Adding-custom-settings/#defining-settings-for-content-types
                    cfg => cfg.WithSetting("BodyTypePartSettings.Flavor", "markdown")
                )
            );
        }
    }
}