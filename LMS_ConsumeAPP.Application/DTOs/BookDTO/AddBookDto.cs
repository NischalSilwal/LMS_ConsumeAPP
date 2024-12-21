using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_ConsumeAPP.Application.DTOs.BookDTO
{
    public class AddBookDto
    {
       // public int BookId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
        public int Quantity { get; set; }
        public List<string> Authors { get; set; }
    }
}
