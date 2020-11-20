using System;
using BooksInfo.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BooksInfoWeb_KP.Models
{
    public class BookTableModel
    {
        public int Id { get; set; }

        [Display(Name = "Назва класу")]
        public string ClassName { get; set; }

        [Required(ErrorMessage =
             "Потрібно заповнити поле \'Назва підручника\'")]
        [StringLength(50, MinimumLength = 3,
             ErrorMessage = "Назва підручника "
             + "повинна містити від 3 до 50 символів")]
        [Display(Name = "Назва підручника")]
        public string BookName { get; set; }

        [Display(Name = "Видано")]
        [StringLength(50, MinimumLength = 3,
             ErrorMessage = "Назва автора "
             + "повинна містити від 3 до 50 символів")]
        public string Author { get; set; }

        [Display(Name = "Дата видачі")]
        [RegularExpression(@"^\s*(3[01]|[12][0-9]|0?[1-9])\.(1[012]|0?[1-9])\.((?:19|20)\d{2})\s*$",
            ErrorMessage =
            "Дата не відповідає формату ДД.ММ.РРРР")]
        public string DateGiving { get; set; }

        [Display(Name = "Примітка")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        [Display(Name = "Опис")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        //public string[] Info { get; private set; }

        //private static string[] CreateInfo(BookGiving obj)
        //{
        //    string s = null;
        //    if (!string.IsNullOrWhiteSpace(obj.Note))
        //    {
        //        s += "Примітка: " + obj.Note + "\n";
        //    }
        //    if (!string.IsNullOrWhiteSpace(obj.Description))
        //    {
        //        s += "Опис\n" + obj.Description;
        //    }
        //    string[] info = null;
        //    if (s != null)
        //    {
        //        info = s.Split(new[] { '\n' },
        //            StringSplitOptions.RemoveEmptyEntries).ToArray();
        //    }
        //    return info;
        //}

        public static explicit operator BookTableModel(BookGiving obj)
        {
            return new BookTableModel()
            {
                Id = obj.Id,
                ClassName = obj.ClassName,
                BookName = obj.BookName,
                Author = obj.Author,
                DateGiving = obj.DateGiving,
                Note = obj.Note,
                Description = obj.Description
                //Info = CreateInfo(obj)
            };
        }

        public static explicit operator BookGiving(BookTableModel obj)
        {
            return new BookGiving()
            {
                Id = obj.Id,
                ClassName = obj.ClassName,
                BookName = obj.BookName,
                Author = obj.Author,
                DateGiving = obj.DateGiving,
                Note = obj.Note,
                Description = obj.Description
            };
        }



    }
}