using BooksInfo.Data;
using BooksInfo.Entities;
using BooksInfoWeb_KP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksInfoWeb_KP.Controllers
{
    public class FormsController : Controller
    {
        const string ALL_VALUES = "...";
        public const string ALL_PAGE_LINK_NAME = "..";

        private IEnumerable<BookGiving> objects;
        private IEnumerable<BookTableModel> tableModelObjects;

        public IEnumerable<BookGiving> Objects
        {
            get { return objects; }
            set
            {
                objects = value;
                tableModelObjects = objects.Select(e => (BookTableModel)e)
                    .OrderBy(e => e.BookName);
            }
        }

        public FormsController()
        {
            Objects = StaticDataContext.BookGivings;
        }

        // GET: Countries
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Selection()
        {
            ViewBag.selAuthor = CreateAuthorSelectList();
            return View(tableModelObjects);
        }

        List<SelectListItem> CreateAuthorSelectList()
        {
            List<string> values = new List<string>();
            values.Add(ALL_VALUES);
            values.AddRange(Objects
                .Select(e => e.Author).Distinct());
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (string e in values)
            {
                list.Add(new SelectListItem
                {
                    Text = e,
                    Value = e
                });
            }
            return list;
        }

        public PartialViewResult _SelectData(string selName, string selAuthor)
        {
            var model = tableModelObjects;
            if (!string.IsNullOrWhiteSpace(selName))
                model = model.Where(e => e.BookName.ToLower()
                    .StartsWith(selName.ToLower()));
            if (selAuthor != null && selAuthor != ALL_VALUES)
                model = model.Where(e => e.Author == selAuthor);
            System.Threading.Thread.Sleep(2000);
            return PartialView("_TableBody", model);
        }

        public ActionResult BrowseByLetters()
        {
            ViewBag.Letters = new[] { ALL_PAGE_LINK_NAME }
                .Concat(Objects
                    .Select(e => e.BookName[0].ToString())
                    .Distinct().OrderBy(e => e));
            return View(tableModelObjects);
        }

        public PartialViewResult _GetDataByLetter(string selLetter)
        {
            var model = tableModelObjects;
            if (selLetter != null && selLetter != ALL_PAGE_LINK_NAME)
                model = model.Where(e => e.BookName[0] == selLetter[0]);
            System.Threading.Thread.Sleep(2000);
            return PartialView("_BrowseData", model);
        }
    }
}