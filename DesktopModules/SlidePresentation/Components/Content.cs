using System.Linq;
using System.Web;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Content;
using DotNetNuke.Entities.Content.Common;

namespace Christoc.Com.Modules.SlidePresentation.Components
{
    public class Content
    {

        private const string ContentTypeName = "Christoc.Com.Modules.SlidePresentation.Slide";

        /// <summary>
        /// This should only run after the Article exists in the data store. 
        /// </summary>
        /// <returns>The newly created ContentItemID from the data store.</returns>
        public ContentItem CreateContentItem(Slide objPress, int tabId)
        {
            var typeController = new ContentTypeController();
            var colContentTypes = (from t in typeController.GetContentTypes() where t.ContentType == ContentTypeName select t);
            int contentTypeId;

            if (colContentTypes.Any())
            {
                var contentType = colContentTypes.Single();
                contentTypeId = contentType == null ? CreateContentType() : contentType.ContentTypeId;
            }
            else
            {
                contentTypeId = CreateContentType();
            }

            var objContent = new ContentItem
            {
                Content = HttpUtility.HtmlDecode(objPress.Body),
                ContentTypeId = contentTypeId,
                Indexed = false,
                ContentKey = "slideid=" + objPress.SlideId,
                ModuleID = objPress.ModuleId,
                TabID = tabId
            };

            objContent.ContentItemId = Util.GetContentController().AddContentItem(objContent);

            return objContent;
        }

        /// <summary>
        /// This is used to update the content in the ContentItems table. Should be called when an Slide is updated.
        /// </summary>
        public void UpdateContentItem(Slide objSlide, int tabId)
        {
            
            var objContent = Util.GetContentController().GetContentItem(objSlide.ContentItemId);

            if (objContent == null) return;
            objContent.Content = HttpUtility.HtmlDecode(objSlide.Body);
            objContent.TabID = tabId;

            Util.GetContentController().UpdateContentItem(objContent);
        }

        /// <summary>
        /// This removes a content item associated with an Slide from the data store. Should run every time an Slide is deleted.
        /// </summary>
        /// <param name="objSlide">The Content Item we wish to remove from the data store.</param>
        public void DeleteContentItem(Slide objSlide)
        {
            if (objSlide.ContentItemId <= Null.NullInteger) return;
            var objContent = Util.GetContentController().GetContentItem(objSlide.ContentItemId);
            if (objContent == null) return;

            Util.GetContentController().DeleteContentItem(objContent);
        }

        public IQueryable<ContentItem> GetContentItems(int tabId, int moduleId)
        {
            var typeController = new ContentTypeController();
            var colContentTypes = (from t in typeController.GetContentTypes() where t.ContentType == ContentTypeName select t);
            int contentTypeId;

            if (colContentTypes.Any())
            {
                var contentType = colContentTypes.Single();
                contentTypeId = contentType == null ? CreateContentType() : contentType.ContentTypeId;
            }
            else
            {
                contentTypeId = CreateContentType();
            }
            var cc = new ContentController();
            var slides = cc.GetContentItems(contentTypeId, tabId, moduleId);
            return slides;

        }

        public ContentItem GetContentItem(int contentItemId)
        {
            var typeController = new ContentTypeController();
            var colContentTypes = (from t in typeController.GetContentTypes() where t.ContentType == ContentTypeName select t);

            var cc = new ContentController();
            var slide = cc.GetContentItem(contentItemId);
            return slide;

        }


        #region Private Methods

        /// <summary>
        /// Creates a Content Type (for taxonomy) in the data store.
        /// </summary>
        /// <returns>The primary key value of the new ContentType.</returns>
        private static int CreateContentType()
        {
            var typeController = new ContentTypeController();
            var objContentType = new ContentType { ContentType = ContentTypeName };
            return typeController.AddContentType(objContentType);
        }

        #endregion

    }
}