
using DotNetNuke.Web.Services;

namespace Christoc.Com.Modules.SlidePresentation.services
{
    public class SlidePresentationRouteMapper : IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapRoute("SlidePresentation", "{controller}.ashx/{action}",
                                     new[] {"Christoc.Com.Modules.SlidePresentation.services"});
        }
    }
}