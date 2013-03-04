﻿using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ComplexCommerce.Business;
using ComplexCommerce.Business.Context;

namespace ComplexCommerce.Web.Mvc
{
    public class PageRoute
        : RouteBase
    {
        public PageRoute(
            IApplicationContext appContext,
            IRouteUrlListFactory routeUrlListFactory,
            IContextUtilities contextUtilities
            )
        {
            if (appContext == null)
                throw new ArgumentNullException("appContext");
            if (routeUrlListFactory == null)
                throw new ArgumentNullException("routeUrlListFactory");
            if (contextUtilities == null)
                throw new ArgumentNullException("contextUtilities");
            this.appContext = appContext;
            this.routeUrlListFactory = routeUrlListFactory;
            this.contextUtilities = contextUtilities;
        }

        private readonly IApplicationContext appContext;
        private readonly IRouteUrlListFactory routeUrlListFactory;
        private readonly IContextUtilities contextUtilities;

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            RouteData result = null;
            var tenant = appContext.CurrentTenant;

            // Get all of the pages
            var pages = routeUrlListFactory.GetRouteUrlPageList(tenant.Id, tenant.DefaultLocale.LCID);

            // Get the culture name
            var url = httpContext.Request.Url;
            var cultureName = contextUtilities.GetCultureNameFromUrl(url);

            string localizedUrl;

            // Get the default Route URL
            string defaultUrl = url.AbsolutePath.Substring(1).ToLowerInvariant();

            // Get the localized Route URL
            if (!string.IsNullOrEmpty(cultureName))
            {
                localizedUrl = cultureName + url.AbsolutePath.ToLowerInvariant();
            }
            else
            {
                localizedUrl = defaultUrl;
            }
            

            var page = pages.Where(x => x.RouteUrl.Equals(localizedUrl, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (page == null && !localizedUrl.Equals(defaultUrl))
            {
                page = pages.Where(x => x.RouteUrl.Equals(defaultUrl, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            }

            if (page != null)
            {
                result = new RouteData(this, new MvcRouteHandler());
                // TODO: Add area for different tenant types

                result.Values["controller"] = page.ContentType.ToString();
                result.Values["action"] = "Index";
                result.Values["id"] = page.ContentId;
            }

            return result;

        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            var tenant = appContext.CurrentTenant;

            // Get all of the pages
            var pages = routeUrlListFactory.GetRouteUrlPageList(tenant.Id, tenant.DefaultLocale.LCID);

            VirtualPathData result = null;
            RouteUrlPageInfo page = null;
            string virtualPath = string.Empty;

            if (TryFindMatch(pages, values, out page))
            {
                virtualPath = page.RouteUrl;
            }

            if (!string.IsNullOrEmpty(virtualPath))
            {
                result = new VirtualPathData(this, virtualPath);
            }

            return result;
        }

        private bool TryFindMatch(RouteUrlPageList pages, RouteValueDictionary values, out RouteUrlPageInfo page)
        {
            page = null;
            Guid contentId = Guid.Empty;

            if (!Guid.TryParse(Convert.ToString(values["id"]), out contentId))
            {
                return false;
            }

            var controller = Convert.ToString(values["controller"]);
            var action = Convert.ToString(values["action"]);

            if (action == "Index")
            {
                foreach (var item in pages)
                {
                    if (item.ContentId.Equals(contentId) && item.ContentType.ToString().Equals(controller))
                    {
                        page = item;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
