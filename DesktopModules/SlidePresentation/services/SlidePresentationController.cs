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
                var slides = Slide.GetSlides(tabId, moduleId);
                return Json(slides, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                DnnLog.Error(exc);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        //below code is not currently in use and doesn't yet work
        [SupportedModules("SlidePresentation")]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit)]
        public ActionResult SaveSlide(int tabId, int moduleId, string slideContent, int slideOrder, int slideId)
        {
            //not currently using slide order
            try
            {
                //TODO: create a new slide
                //if slideid is passed in we should get the original slide
                var saveSlide = new Slide();
                if (slideId > 0)
                {
                    saveSlide = Slide.SlideLoad(slideId);
                }
                else
                {
                    saveSlide.CreatedByUserId = UserInfo.UserID;
                    saveSlide.CreatedOnDate = DateTime.UtcNow;
                    saveSlide.ModuleId = moduleId;
                }
                saveSlide.LastModifiedByUserId = UserInfo.UserID;
                saveSlide.LastModifiedOnDate = DateTime.UtcNow;
                saveSlide.Body = slideContent;
                saveSlide.Save(tabId);
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