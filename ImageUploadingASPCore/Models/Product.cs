using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageUploadingASPCore.Models;

public partial class Product
{
    public int ProductId { get; set; }
    [Required]
    public string ProductName { get; set; } = null!;
    [Required]
    public decimal Price { get; set; }
    public string ProductImage { get; set; } // This stores the filename or path
    [NotMapped]
    public IFormFile ProductImageFile { get; set; } // This handles the file upload
}
