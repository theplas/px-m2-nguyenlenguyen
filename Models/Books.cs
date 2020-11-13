
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quanlysach.Models
{
    public class Books
    {
        public int Id { get; set; }
        [DisplayName(" Book Name ")]
        [Required(ErrorMessage = " Book title cannot be left blank ")]
        [MinLength(3, ErrorMessage = " Book title must have at least 3 characters ")]
        public string Name { get; set; }
        [DisplayName(" Author's Name ")]
        public string NameAuthor { get; set; }
        [DisplayName(" Short Description ")]
        public string Short_content { get; set; }
        [DisplayName(" Year of publication ")]
        public int Year { get; set; }
        [DisplayName(" Quantity: ")]
        public int Count { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
