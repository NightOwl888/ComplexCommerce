﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Threading;
using System.Globalization;
using ComplexCommerce.Business;
using ComplexCommerce.Business.Context;

namespace ComplexCommerce.Web.Mvc.Routing
{
    public class PageRoute
        : RouteBase
    {
        public PageRoute(
            IApplicationContext appContext,
            IRouteUrlPageListFactory routeUrlPageListFactory,
            IRouteUtilities routeUtilities
            )
        {
            if (appContext == null)
                throw new ArgumentNullException("appContext");
            if (routeUrlPageListFactory == null)
                throw new ArgumentNullException("routeUrlPageListFactory");
            if (routeUtilities == null)
                throw new ArgumentNullException("routeUtilities");
            this.appContext = appContext;
            this.routeUrlPageListFactory = routeUrlPageListFactory;
            this.routeUtilities = routeUtilities;
        }

        private readonly IApplicationContext appContext;
        private readonly IRouteUrlPageListFactory routeUrlPageListFactory;
        private readonly IRouteUtilities routeUtilities;

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            RouteData result = null;
            var tenant = appContext.CurrentTenant;
            var localeId = appContext.CurrentLocaleId;

            // Get all of the pages
            var path = httpContext.Request.Path;
            var pathLength = path.Length;

            var page = routeUrlPageListFactory
                .GetRouteUrlPageList(tenant.Id, localeId, tenant.DefaultLocale.LCID)
                .Where(x => x.UrlPath.Length.Equals(pathLength))
                .Where(x => x.UrlPath.Equals(path))
                .FirstOrDefault();
            
            //var page = pages
            //    .Where(x => x.UrlPath.Equals(path))
            //    .FirstOrDefault();
            if (page != null)
            {
                //result = new RouteData(this, new MvcRouteHandler());
                result = routeUtilities.CreateRouteData(this);
                
                routeUtilities.AddQueryStringParametersToRouteData(result, httpContext);

                // TODO: Add area for different tenant types
                result.Values["controller"] = page.ContentType.ToString();
                if (!page.ContentId.Equals(Guid.Empty))
                {
                    result.Values["action"] = "Details";
                    result.Values["id"] = page.ContentId;
                }
                else
                {
                    result.Values["action"] = "Index";
                }
            }
            return result;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            VirtualPathData result = null;
            IRouteUrlPageInfo page = null;
            var tenant = appContext.CurrentTenant;
            var localeId = appContext.CurrentLocaleId;

            // Get all of the pages
            var pages = routeUrlPageListFactory.GetRouteUrlPageList(tenant.Id, localeId, tenant.DefaultLocale.LCID);

            if (TryFindMatch(pages, values, out page))
            {
                if (!string.IsNullOrEmpty(page.VirtualPath))
                {
                    result = new VirtualPathData(this, page.VirtualPath);
                }
            }

            return result;
        }

        private bool TryFindMatch(IEnumerable<IRouteUrlPageInfo> pages, RouteValueDictionary values, out IRouteUrlPageInfo page)
        {
            page = null;
            Guid contentId = Guid.Empty;

            var action = Convert.ToString(values["action"]);
            var controller = Convert.ToString(values["controller"]);

            if (!Guid.TryParse(Convert.ToString(values["id"]), out contentId))
            {
                // TODO: Make home page based on tenant/area

                // Special case for homepage
                if (action == "Index" && controller == "Home")
                {
                    page = pages.Where(x => x.ContentType == ContentTypeEnum.Home).FirstOrDefault();
                    if (page != null)
                    {
                        return true;
                    }
                }
            }

            if (action == "Details")
            {
                page = pages
                    .Where(x => x.ContentId.Equals(contentId) && 
                        x.ContentType.ToString().Equals(controller, StringComparison.InvariantCultureIgnoreCase))
                    .FirstOrDefault();
                if (page != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
