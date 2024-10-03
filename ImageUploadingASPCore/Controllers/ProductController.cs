using ImageUploadingASPCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ImageUploadingASPCore.Controllers
{
    public class ProductController : Controller
    {
        ProductsContext context;
        IWebHostEnvironment env;
        public ProductController(ProductsContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }
        public IActionResult Index()
        {
            return View(context.Products.ToList());
        }
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            var ext = Path.GetExtension(product.ProductImageFile.FileName);
            var size = product.ProductImageFile.Length;
            if (ext.Equals(".png") || ext.Equals(".jpg") || ext.Equals(".jpeg"))
            {
                if (size <= 1000000)
                {
                    string fileName = "";
                    if (product.ProductImageFile != null)
                    {
                        string folder = Path.Combine(env.WebRootPath, "images");
                        fileName = Guid.NewGuid().ToString() + "_" + product.ProductImageFile.FileName;
                        string filepath = Path.Combine(folder, fileName);
                        product.ProductImageFile.CopyTo(new FileStream(filepath, FileMode.Create));

                        Product p = new Product()
                        {
                            ProductName = product.ProductName,
                            Price = product.Price,
                            ProductImage = fileName
                        };
                        context.Products.Add(p);
                        context.SaveChanges();
                        TempData["SuccessMessage"] = "The product has been added successfully.";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["Size_ErrorMessage"] = "File size exceeds the 1 MB limit.";
                }
            }
            else
            {
                TempData["Extension_ErrorMessage"] = "Invalid file type. Please upload an image with one of the following extensions: jpg, jpeg, png.";
            }
            return View();
        }
    }
}


