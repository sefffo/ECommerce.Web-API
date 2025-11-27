using Ecommerce.Domain.Models.Contracts.UOW;
using Ecommerce.Domain.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashBoardV1._0._0.Controllers
{
    public class TypeController(IUnitOfWork _unitOfWork) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return View(types);
        }
        public async Task<IActionResult> Create(ProductType type)
        {
            try
            {
                _unitOfWork.GetRepository<ProductType, int>().add(type);
                _unitOfWork.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("Name", "Please enter new name");
                return View("Index", await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync());
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var type = await _unitOfWork.GetRepository<ProductType, int>().GetByIdAsync(id);
            _unitOfWork.GetRepository<ProductType, int>().delete(type);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
