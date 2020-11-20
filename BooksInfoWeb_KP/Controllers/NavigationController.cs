using BooksInfo.Data;
using BooksInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksInfoWeb_KP.Controllers
{
    public class NavigationController : Controller
    {
        public IEnumerable<BookGiving> Books { get; set; }

        public NavigationController()
        {
            Books = StaticDataContext.BookGivings;
        }

        // GET: Navigation
        public ActionResult Index()
        {
            return View();
        }

        public const string ALL_CATEGORIES = "...";

        [ChildActionOnly]
        public PartialViewResult BooksByAuthorsMenu(
                string categoryName = ALL_CATEGORIES)
        {
            ViewBag.SelectedCategoryName = categoryName;
            List<string> categoryNames = new List<string>();
            categoryNames.Add(ALL_CATEGORIES);
            categoryNames.AddRange(Books.Select(e => e.BookName).Distinct().OrderBy(e => e));
            return PartialView(categoryNames);
        }
    }
}