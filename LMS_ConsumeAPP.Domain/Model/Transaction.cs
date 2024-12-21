using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_ConsumeAPP.Domain.Model
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string TransactionType { get; set; } // Borrow or Return
        public DateTime Date { get; set; }
    }
}
