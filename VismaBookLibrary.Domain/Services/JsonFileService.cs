using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Domain.Interfaces;
using VismaBookLibrary.Domain.Models;

namespace VismaBookLibrary.Domain.Services
{
    public class JsonFileService : IFileService
    {
        private readonly string dataUrl = AppDomain.CurrentDomain.BaseDirectory + "BookData.json";
        
        public IEnumerable<Book> GetAll()
        {
            if (!File.Exists(dataUrl))
            {
                File.Create(dataUrl).Close();
            }

            List<Book> books = new();

            var lines = File.ReadAllLines(dataUrl);

            foreach (var line in lines)
            {
                var book = JsonConvert.DeserializeObject<Book>(line);
                books.Add(book);
            }
            return books;
        }

        public void Overwrite(IEnumerable<Book> books)
        {
            File.WriteAllText(dataUrl, "");

            foreach (var book in books)
            {
                SaveNew(book);
            }
        }

        public void SaveNew(Book book)
        {
            var jsonString = JsonConvert.SerializeObject(book);           

            File.AppendAllText(dataUrl, jsonString + Environment.NewLine);
        }
    }
}
