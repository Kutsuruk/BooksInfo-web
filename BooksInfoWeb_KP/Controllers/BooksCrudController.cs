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
    public class BooksCrudController : Controller
    {
        private ICollection<BookGiving> objects;
        private IEnumerable<BookSelectionModel> selectionModelObjects { get; set; }
        private IEnumerable<BookTableModel> editingModelObjects { get; set; }

        public ICollection<BookGiving> Objects
        {
            get { return objects; }
            set
            {
                objects = value;
                selectionModelObjects = objects
                    .Select(e => (BookSelectionModel)e).OrderBy(e => e.BookName);
                editingModelObjects = objects
                    .Select(e => (BookTableModel)e).OrderBy(e => e.BookName);
            }
        }

        public IEnumerable<Book> Books { get; set; }

        public BooksCrudController()
        {
            Objects = StaticDataContext.BookGivings;
            Books = StaticDataContext.Books;
        }

        // GET: CountriesCrud
        public ActionResult Index()
        {
            return View(selectionModelObjects);
        }

        public ActionResult Create()
        {
            ViewBag.BookName = CreateBookNamesSelectList();
            return View(new BookTableModel());
        }

        [HttpPost]
        public ActionResult Create(BookTableModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.BookName = CreateBookNamesSelectList();
                return View(model);
            }
            Objects.Add((BookGiving)model);
            StaticDataContext.Save();
            TempData["message"] = string.Format(
                "Дані \"{0}\" збережено", model.BookName);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var model = editingModelObjects.First(e => e.Id == id);
            ViewBag.BookName = CreateBookNamesSelectList(model.BookName);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BookTableModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.BookName = CreateBookNamesSelectList();
                return View(model);
            }
            var entityObject = Objects.First(e => e.Id == model.Id);
            UpdateEntityObject(entityObject, model);
            StaticDataContext.Save();
            TempData["message"] = string.Format(
                "Зміни даних \"{0}\" збережено", model.BookName);
            return RedirectToAction("Index");
        }

        private void UpdateEntityObject(BookGiving entityObject,
                BookTableModel model)
        {
            entityObject.ClassName = model.ClassName;
            entityObject.BookName = model.BookName;
            entityObject.Author = model.Author;
            entityObject.DateGiving = model.DateGiving;
            entityObject.Note = model.Note;
            entityObject.Description = model.Description;
        }

        public ActionResult Delete(int id)
        {
            var model = editingModelObjects.First(e => e.Id == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(BookGiving model)
        {
            BookGiving entityObject = Objects.First(e => e.Id == model.Id);
            Objects.Remove(entityObject);
            StaticDataContext.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var model = editingModelObjects.First(e => e.Id == id);
            return View(model);
        }

        List<SelectListItem> CreateBookNamesSelectList(string selectedValue = "")
        {
            List<string> values = new List<string>();
            values.AddRange(Books.Select(e => e.BookName));
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (string e in values)
            {
                list.Add(new SelectListItem
                {
                    Text = e,
                    Value = e,
                    Selected = e == selectedValue
                });
            }
            return list;
        }

        [HttpPost]
        public JsonResult JsonInfo(int id)
        {
            var info = GetInfo(id);
            System.Threading.Thread.Sleep(1000);
            return Json(info);
        }

        string[] GetInfo(int id)
        {
            var obj = Objects.First(e => e.Id == id);
            string s = null;
            if (!string.IsNullOrWhiteSpace(obj.Note))
            {
                s += "Примітка: " + obj.Note + "\n";
            }
            if (!string.IsNullOrWhiteSpace(obj.Description))
            {
                s += "Опис\n" + obj.Description;
            }
            string[] info = null;
            if (s != null)
            {
                info = s.Split(new[] { '\n' },
                    StringSplitOptions.RemoveEmptyEntries);
            }
            return info;
        }

        [HttpPost]
        public JsonResult JsonIdInfo(int id)
        {
            var info = GetInfo(id);
            return Json(new { Id = id, Info = info });
        }
    }
}
