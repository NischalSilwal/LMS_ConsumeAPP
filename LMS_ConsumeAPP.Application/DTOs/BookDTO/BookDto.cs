using LMS_ConsumeAPP.Application.DTOs.AuthorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_ConsumeAPP.Application.DTOs.BookDTO
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
        public int Quantity { get; set; }
        public List<int> AuthorIds { get; set; }

        // New property to handle AuthorIds as a comma-separated string
        public string AuthorIdsAsString
        {
            get => AuthorIds != null ? string.Join(",", AuthorIds) : string.Empty;
            set => AuthorIds = string.IsNullOrWhiteSpace(value)
                ? new List<int>()
                : value.Split(',').Select(id => int.Parse(id.Trim())).ToList();
        }

      //  public List<AuthorDtoForBook> Authors { get; set; } = new List<AuthorDtoForBook>();

    }


}
