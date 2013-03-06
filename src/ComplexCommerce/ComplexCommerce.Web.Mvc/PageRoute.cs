﻿using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Threading;
using System.Globalization;
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
            var cultureName = contextUtilities.GetCultureNameFromUrl(url).ToLowerInvariant();

            

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


            // TODO: The 301 redirect shouldn't happen when the culture is not configured in the tenant. Instead it should
            // return null so we get a 404.
            if (this.IsDefaultUICulture(cultureName, tenant))
            {
                var urlWithoutCulture = url.ToString().ToLowerInvariant().Replace("/" + cultureName.ToLowerInvariant(), "");

                // If the current culture matches the URL, we need to 301 redirect to the default URL
                return this.RedirectPermanent(urlWithoutCulture, httpContext);
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
            VirtualPathData result = null;
            RouteUrlPageInfo page = null;
            string virtualPath = string.Empty;
            var tenant = appContext.CurrentTenant;

            // Get all of the pages
            var pages = routeUrlListFactory.GetRouteUrlPageList(tenant.Id, tenant.DefaultLocale.LCID);

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

        //private bool IsCurrentUICulture(string cultureName)
        //{
        //    return Thread.CurrentThread.CurrentUICulture.Name.Equals(cultureName, StringComparison.InvariantCultureIgnoreCase);
        //}

        private bool IsDefaultUICulture(string cultureName, ITenant tenant)
        {
            return tenant.DefaultLocale.Name.Equals(cultureName, StringComparison.InvariantCultureIgnoreCase);
        }

        private RouteData RedirectPermanent(string destinationUrl, HttpContextBase httpContext)
        {
            var response = httpContext.Response;

            response.CacheControl = "no-cache";
            response.StatusCode = 301;
            response.RedirectLocation = destinationUrl;

            var routeData = new RouteData(this, new MvcRouteHandler());
            routeData.Values["controller"] = "Home";
            routeData.Values["action"] = "Redirect301";
            routeData.Values["url"] = destinationUrl;

            return routeData;
        }


        //private void RedirectPermanent(string destinationUrl, HttpContextBase httpContext)
        //{
        //    var response = httpContext.Response;

        //    response.StatusCode = 301;
        //    response.RedirectLocation = destinationUrl;

        //    // Output Custom View Here
        //    // response.Write(The View)
        //    var routeData = new RouteData();
        //    routeData.Values["controller"] = "Home";
        //    routeData.Values["action"] = "RedirectPermanent";
        //    routeData.Values["url"] = destinationUrl;


        //    //var newContext = new HttpContextWrapper(new HttpContext(HttpContext.Current.Request,

        //    var requestContext = new RequestContext(httpContext, routeData);


        //    // Get controller factory
        //    var controllerFactory = ControllerBuilder.Current.GetControllerFactory();
        //    IController homeController = controllerFactory.CreateController(requestContext, "Home");


        //    //IController homeController = new HomeController();
            
        //    homeController.Execute(requestContext);

        //    response.End();
        //}



        //public string RenderRazorViewToString(string viewName, object model)
        //{
        //    ViewData.Model = model;
        //    using (var sw = new StringWriter())
        //    {
        //        var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
        //        var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
        //        viewResult.View.Render(viewContext, sw);
        //        viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
        //        return sw.GetStringBuilder().ToString();
        //    }
        //}



    }
}
