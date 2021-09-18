using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RentWebProj
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "memberapi",
                routeTemplate: "api/members/ChangeProfile/${MemberName}${ MemberYear}/${ MemberMonth}/${ MemberDay}/${ MemberPhone}",
                defaults: new { controller= "MemberProfileAPI", action= "ChangeProfile", MemberName = RouteParameter.Optional, MemberYear = RouteParameter.Optional, MemberMonth = RouteParameter.Optional, MemberDay = RouteParameter.Optional, id = RouteParameter.Optional, MemberPhone = RouteParameter.Optional }
            );
            ///
        }
    }
}
