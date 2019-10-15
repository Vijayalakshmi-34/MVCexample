using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCexample.Models
{
    public class Movie
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Movie Name should not be empty")]
        [Display(Name ="Movie Name")]
        [Column(TypeName ="varchar")]
        [StringLength(100)]
        public string MovieName { get; set; }


        [Required(ErrorMessage = "Release Date should not be empty")]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }


        [Required(ErrorMessage = "Date Added should not be empty")]
        [Display(Name = "Movie Added Date")]
        public DateTime? DateAdded { get; set; }


        [Required(ErrorMessage = "Available Stock should not be empty")]
        public int? AvailableStock { get; set; }


        public Genre Genre { get; set; } //Table Reference

        [Required(ErrorMessage ="Genre should not be empty")]
        [Display(Name ="Genre")]
        public int? GenreID { get; set; }
        



    }
}