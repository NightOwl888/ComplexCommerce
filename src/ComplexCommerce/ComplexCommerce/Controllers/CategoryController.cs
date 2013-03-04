﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComplexCommerce.Business;

namespace ComplexCommerce.Controllers
{
    public class CategoryController : Controller
    {
        public CategoryController(
            ICategoryFactory categoryFactory
            )
        {
            if (categoryFactory == null)
                throw new ArgumentNullException("categoryFactory");
            this.categoryFactory = categoryFactory;
        }

        private readonly ICategoryFactory categoryFactory;

        //
        // GET: /Category/

        public ActionResult Index(Guid id)
        {
            ViewData.Model = categoryFactory.GetCategory(id);
            return View();
        }

        ////
        //// GET: /Category/Details/5

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        ////
        //// GET: /Category/Create

        //public ActionResult Create()
        //{
        //    return View();
        //}

        ////
        //// POST: /Category/Create

        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /Category/Edit/5

        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Category/Edit/5

        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /Category/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Category/Delete/5

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}