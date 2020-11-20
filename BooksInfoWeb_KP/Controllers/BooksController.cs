using BooksInfo.Data;
using BooksInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksInfoWeb_KP.Controllers
{
    public class BooksController : Controller
    {
        public IEnumerable<BookGiving> Objects { get; set; }

        public int ItemsPerPage { get; set; }

        public BooksController()
        {
            Objects = StaticDataContext.BookGivings;
            ItemsPerPage = 2;
        }

        // GET: Books
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult ObjectsInfo()
        {
            return View(Objects);
        }

        public PartialViewResult _DescriptiveInfo(int id)
        {
            var obj = Objects.First(e => e.Id == id);
            string[] model = null;
            if (!string.IsNullOrWhiteSpace(obj.Description))
            {
                string s = "Примітка\n" +obj.Description;
                model = s.Split(new[] { '\n' },
                StringSplitOptions.RemoveEmptyEntries);
            }
            return PartialView(model);
        }

        public ViewResult InfoWithPaging(string pageKey = "..", int pageNumber = 0)
        {
            IEnumerable<BookGiving> model = Objects; //.OrderBy(e => e.Name)
            if (!string.IsNullOrEmpty(pageKey) && pageKey != "..")
            {
                model = model.Where(e => e.BookName[0].ToString() == pageKey);
            }
            if (pageNumber != 0)
            {
                model = model.Skip((pageNumber - 1) * ItemsPerPage).Take(ItemsPerPage);
            }
            return View(model);
        }

        public ViewResult BooksByAuthorsInfo(
                string categoryName = NavigationController.ALL_CATEGORIES)
        {
            IEnumerable<BookGiving> models = Objects.OrderBy(e => e.BookName);
            if (!string.IsNullOrEmpty(categoryName) &&
                categoryName != NavigationController.ALL_CATEGORIES)
            {
                models = models.Where(e => e.BookName == categoryName);
            }
            ViewBag.SelectedCategoryName = categoryName;
            return View(models);
        }
    }
}