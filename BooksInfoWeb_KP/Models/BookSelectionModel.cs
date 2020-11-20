using BooksInfo.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BooksInfoWeb_KP.Models
{
    public class BookSelectionModel
    {
        public int Id { get; set; }

        [Display(Name = "Назва класу")]
        public string ClassName { get; set; }

        [Display(Name = "Назва підручника")]
        public string BookName { get; set; }

        [Display(Name = "Видано")]
        public string Author { get; set; }

        [Display(Name = "Дата видачі")]
        public string DateGiving { get; set; }

        [ScaffoldColumn(false)]
        public bool HasInfo { get; set; }


        public static explicit operator BookSelectionModel(BookGiving obj)
        {
            return new BookSelectionModel()
            {
                Id = obj.Id,
                ClassName = obj.ClassName,
                BookName = obj.BookName,
                Author = obj.Author,
                DateGiving = obj.DateGiving,
                HasInfo = !string.IsNullOrWhiteSpace(obj.Note)
                    || !string.IsNullOrWhiteSpace(obj.Description)
            };
        }
    }
}