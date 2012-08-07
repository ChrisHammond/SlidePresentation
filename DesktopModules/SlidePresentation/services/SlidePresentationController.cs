using System;
using Christoc.Com.Modules.SlidePresentation.Components;
using DotNetNuke.Security;
using DotNetNuke.Web.Services;
using System.Web.Mvc;
using DotNetNuke.Instrumentation;

namespace Christoc.Com.Modules.SlidePresentation.services
{
    public class SlidePresentationController:DnnController
    {
        [DnnAuthorize(AllowAnonymous = true)]
        public ActionResult GetSlides(int tabId, int moduleId)
        {
            try
            {
                var slides = Components.Slide.GetSlides(tabId, moduleId);
                return Json(slides, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                DnnLog.Error(exc);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [SupportedModules("Christoc.Com.Modules.SlidePresentation")]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        public ActionResult SaveSlide(int tabId, int moduleId, string slideContent, int slideOrder, int slideId)
        {
            try
            {
                //TODO: create a new slide

                //save the slide
                var newSlide = new Slide();
                newSlide.Body = slideContent;
                newSlide.CreatedByUserId = newSlide.LastModifiedByUserId = UserInfo.UserID;
                newSlide.CreatedOnDate = DateTime.Now;
                newSlide.ModuleId = moduleId;

                if (slideId>0)
                {
                    newSlide.ContentItemId = Convert.ToInt32(slideId);
                }
                newSlide.Save(tabId);
                //TODO: return something if the slide is created
                return null;
            }
            catch (Exception exc)
            {
                DnnLog.Error(exc);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}