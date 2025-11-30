using Ecommerce.Domain.Models.Contracts.UOW;
using Ecommerce.Domain.Models.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashBoardV1._0._0.Controllers
{
    [Authorize]
    public class BrandController(IUnitOfWork _unitOfWork): Controller
    {
        public async Task<IActionResult> Index()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();
            return View(brands);
        }
        public async Task<IActionResult> Create(ProductBrand brand)
        {
            try
            {
                _unitOfWork.GetRepository<ProductBrand, int>().add(brand);
                _unitOfWork.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("Name", "Please enter new name");
                return View("Index", await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync());
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _unitOfWork.GetRepository<ProductBrand, int>().GetByIdAsync(id);
            _unitOfWork.GetRepository<ProductBrand, int>().delete(brand);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
