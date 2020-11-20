using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksInfo.Entities
{
   public class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Note { get; set; }
        public string Description { get; set; }

    }
}
