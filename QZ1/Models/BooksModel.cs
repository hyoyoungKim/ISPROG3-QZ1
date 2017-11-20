using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QZ1.Models
{
    public class BooksModel
    {
        [Key]
        public int ID { get; set; }

        public int PubID { get; set; }

        public int AuthorID { get; set; }

        [Display(Name = "Title Name")]
        [MaxLength(80, ErrorMessage = "Up to 80 characters only")]
        [Required(ErrorMessage = "Required")]
        public string TitleName { get; set; }

        [Display(Name = "Price")]
        [MaxLength(18, ErrorMessage = "Up to 18 characters only")]
        [Required(ErrorMessage = "Required")]
        public string Price { get; set; }

        public DateTime PublicationDate { get; set; }

        [Display(Name = "Notes")]
        [MaxLength(200, ErrorMessage = "Up to 200 characters only")]
        [Required(ErrorMessage = "Required")]
        public string Notes { get; set; }

        [Display(Name = "Author")]
        [Required]
        [MaxLength(80)]
        public string FN { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [MaxLength(80)]
        public string LN { get; set; }

        [Display(Name = "Publisher")]
        [Required]
        [MaxLength(80)]
        public string PN { get; set; }

        public List<SelectListItem> Publishers { get; set; }

        public List<SelectListItem> Authors { get; set; }

    }
}