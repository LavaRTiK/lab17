using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace lab17
{
    public class Book
    {
        [JsonPropertyName("author_name")]
        public string[]? author_name { get; set; }
        public int first_publish_year { get; set; }
        public string title { get; set; }
    }
    public class ListBooks
    {
        public List<Book> docs { get; set; } = new List<Book>();
        public int num_found { get; set; }
        public ListBooks() { 

        }
    }
}
