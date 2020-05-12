//https://google.github.io/styleguide/javaguide.html#s3-source-file-structure

using System.Linq;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Common.Models;
using Orchard.Data.Migration;

namespace Markdown {

    public class Migrations : DataMigrationImpl {
        private readonly IContentDefinitionManager contentDefinitionManager;
        private const string BlogPostTypeName = "BlogPost";

        public Migrations(IContentDefinitionManager contentDefinitionManager) =>
            this.contentDefinitionManager = contentDefinitionManager;

        public int Create() {
            if (DoesBodyPartOfBlogPostTypeExist()) {
                SetEditorToMarkdown();
            }

            return 1;
        }

        private bool DoesBodyPartOfBlogPostTypeExist() {
            var blogPost = contentDefinitionManager.GetTypeDefinition(BlogPostTypeName);
            if (blogPost == null) return false;

            var bodyPart = blogPost.Parts.SingleOrDefault(x => x.PartDefinition.Name == nameof(BodyPart));
            return bodyPart != null;
        }

        private void SetEditorToMarkdown() {
            contentDefinitionManager.AlterTypeDefinition(
                BlogPostTypeName,
                build => build.WithPart(
                    nameof(BodyPart),
                    // Change setting of content type
                    // https://docs.orchardproject.net/en/latest/Documentation/Adding-custom-settings/#defining-settings-for-content-types
                    cfg => cfg.WithSetting("BodyTypePartSettings.Flavor", "markdown")
                )
            );
        }
    }
}