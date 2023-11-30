using System;
using System.ComponentModel.DataAnnotations;

namespace BaiCuoiKy.Models
{
	public class Product
	{
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string? ImageURL { get; set; }

        public string Descride { get; set; }
    }
}

