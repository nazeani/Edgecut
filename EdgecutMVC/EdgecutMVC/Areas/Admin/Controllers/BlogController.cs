using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Reflection.Metadata;

namespace EdgecutMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Index()
        {
           var blogs= _blogService.GetAllBlog();
            return View(blogs);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Blog blog)
        {
            if (!ModelState.IsValid) { return View(); }
            try
            {
                _blogService.AddBlog(blog);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ImageContextException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();

            }
            catch (ImageLengthException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

                return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {

            var blog = _blogService.GetBlog(x => x.Id == id);

            if (blog == null)
                return NotFound("blog taplmadi");
            return View(blog);
        }
        [HttpPost]
        public IActionResult Update(Blog blog)
        {
            if (!ModelState.IsValid) { return View(); }
            try
            {
                _blogService.UpdateBlog(blog.Id, blog);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Business.Exceptions.FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ImageContextException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();

            }
            catch (ImageLengthException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var blog = _blogService.GetBlog(x => x.Id == id);

            if (blog == null) return NotFound("blog taplmadi");

            return View(blog);
        }
        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            if(!ModelState.IsValid) { return View(); }
            try
            {
                _blogService.DeleteBlog(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Business.Exceptions.FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            { return BadRequest(ex.Message); }
            return RedirectToAction("Index");
        }
    }
}
