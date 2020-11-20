using BooksInfo.Entities;
using System.IO;
using System.Text;
using System.Xml;

namespace BooksInfo.Data
{
	partial class StaticDataContext
    {
        public static void Save()
        {
            XmlTextWriter writer = null;
            writer = new XmlTextWriter(filePath, Encoding.Unicode);
            writer.WriteStartDocument();
            writer.WriteStartElement("BooksInfo");
            WriteBooks(writer);
            WriteBookGivings(writer);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        private static void WriteBooks(XmlTextWriter writer)
        {
            writer.WriteStartElement("Books");
            foreach (var obj in Books)
            {
                writer.WriteStartElement("Book");
                writer.WriteAttributeString("Id", obj.Id.ToString());
                writer.WriteAttributeString("BookName", obj.BookName);
                writer.WriteAttributeString("Author", obj.Author);
                writer.WriteAttributeString("Note", obj.Note);
                writer.WriteAttributeString("Description", obj.Description);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        private static void WriteBookGivings(XmlTextWriter writer)
        {
            writer.WriteStartElement("BookGivings");
            foreach (var obj in BookGivings)
            {
                writer.WriteStartElement("BookGiving");
                writer.WriteAttributeString("Id", obj.Id.ToString());
                writer.WriteAttributeString("ClassName", obj.ClassName);
                writer.WriteAttributeString("BookName", obj.BookName);
                writer.WriteAttributeString("Author", obj.Author);
                writer.WriteAttributeString("DateGiving", obj.DateGiving);
                writer.WriteAttributeString("Note", obj.Note);
                writer.WriteAttributeString("Description", obj.Description);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        public static void Load()
        {
            if (!File.Exists(filePath)) return;
            XmlTextReader reader = null;
            reader = new XmlTextReader(filePath)
            {
                WhitespaceHandling = WhitespaceHandling.None
            };
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "Book")
                    {
                        ReadBook(reader);
                    }
                    else if (reader.Name == "BookGiving")
                    {
                        ReadBookGiving(reader);
                    }
                }
            }
            reader.Close();
        }

        private static void ReadBook(XmlTextReader reader)
        {
            Book obj = new Book();
            string s = reader.GetAttribute("Id");
            if (!string.IsNullOrEmpty(s))
                obj.Id = int.Parse(s);
            obj.BookName = reader.GetAttribute("BookName");
            obj.Author = reader.GetAttribute("Author");
            obj.Note = reader.GetAttribute("Note");
            obj.Description = reader.GetAttribute("Description");
            Books.Add(obj);
        }

        private static void ReadBookGiving(XmlTextReader reader)
        {
            BookGiving obj = new BookGiving();
            string s = reader.GetAttribute("Id");
            if (!string.IsNullOrEmpty(s))
                obj.Id = int.Parse(s);
            obj.ClassName = reader.GetAttribute("ClassName");
            obj.BookName = reader.GetAttribute("BookName");
            obj.Author = reader.GetAttribute("Author");
            obj.DateGiving = reader.GetAttribute("DateGiving");
            obj.Note = reader.GetAttribute("Note");
            obj.Description = reader.GetAttribute("Description");
            BookGivings.Add(obj);
        }
    }
}
