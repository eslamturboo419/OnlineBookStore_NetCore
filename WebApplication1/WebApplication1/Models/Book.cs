using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public int? Bookquantity { get; set; }
        public int? Price { get; set; }
        public string Imgfile { get; set; }
        public int? Cataid { get; set; }
        public string Author { get; set; }
    }
}
