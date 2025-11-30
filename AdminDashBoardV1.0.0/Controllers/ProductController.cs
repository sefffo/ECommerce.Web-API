
using AdminDashBoardV1._0._0.Models;
using AutoMapper;
using Ecommerce.Domain.Models.Contracts.UOW;
using Ecommerce.Domain.Models.Products;
using Microsoft.AspNetCore.Mvc;
using AdminDashBoardV1._0._0.Helper;
using Microsoft.AspNetCore.Authorization;
namespace AdminDashBoardV1._0._0.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ProductController(IUnitOfWork unitOfWork, IMapper mapper) : Controller
    {

        public async Task<IActionResult> Index()
        {
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync();

            // Manually load Brand and Type for each product
            foreach (var product in products)
            {
                if (product.BrandId > 0)
                {
                    product.ProductBrand = await unitOfWork.GetRepository<ProductBrand, int>()
                        .GetByIdAsync(product.BrandId);
                }

                if (product.TypeId > 0)
                {
                    product.ProductType = await unitOfWork.GetRepository<ProductType, int>()
                        .GetByIdAsync(product.TypeId);
                }
            }

            var mappedProducts = mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);

            return View(mappedProducts);
        }


        public async Task<IActionResult> Create(int id)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    //logic of upload image to server

                    model.PictureUrl = PictureResolver.Resolve(model.Image, "products");
                }
                else
                {
                    model.PictureUrl = "images/products/CheesyVegetableLasagna.png";
                }
                var mappedProduct = mapper.Map<ProductViewModel, Product>(model);
                unitOfWork.GetRepository<Product, int>().add(mappedProduct);
                await unitOfWork.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);

        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            var mappedProduct = mapper.Map<ProductViewModel>(product);
            return View(mappedProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(model.Id);
                if (model.Image != null)
                {
                    //delete the old image
                    PictureResolver.Delete(product.PictureUrl);
                    //upload the new image
                    product.PictureUrl = PictureResolver.Resolve(model.Image, "products");
                }
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.BrandId = model.BrandId;
                product.TypeId = model.TypeId;

                //var mappedProduct = mapper.Map<Product>(model);
                unitOfWork.GetRepository<Product, int>().update(product);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            if (product != null)
            {
                //delete the image from the server
                PictureResolver.Delete(product.PictureUrl);
                unitOfWork.GetRepository<Product, int>().delete(product);
                await unitOfWork.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
