using Business.Exceptions;
using Business.Extensions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrates
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IWebHostEnvironment _env;

        public BlogService(IBlogRepository blogRepository, IWebHostEnvironment env )
        {
            _blogRepository = blogRepository;
            _env = env;
        }

        public void AddBlog(Blog blog)
        {
            if (blog == null) throw new EntityNotFoundException("Blog tapilmadi!");
            blog.ImageUrl=Helper.SaveFile(_env.WebRootPath,@"uploads\blogs",blog.ImageFile);
            _blogRepository.Add(blog);
            _blogRepository.Commit();
        }

        public Blog GetBlog(Func<Blog, bool>? func = null)
        {
            return _blogRepository.Get(func);
        }

        public List<Blog> GetAllBlog(Func<Blog, bool>? func = null)
        {
            return _blogRepository.GetAll(func);
        }

        public void DeleteBlog(int id)
        {
            var existBlog= _blogRepository.Get(x=>x.Id==id);
            if(existBlog == null) throw new EntityNotFoundException("Blog tapilmadi!");
            Helper.DeleteFile(_env.WebRootPath, @"uploads\blogs", existBlog.ImageUrl);
            _blogRepository.Delete(existBlog);
            _blogRepository.Commit();
        }

        public void UpdateBlog(int id, Blog blog)
        {
            var oldBlog = _blogRepository.Get(x => x.Id == id);
            if (oldBlog == null) throw new EntityNotFoundException("Blog tapilmadi!");
            if(blog.ImageFile!=null)
            {
                Helper.DeleteFile(_env.WebRootPath, @"uploads\blogs", oldBlog.ImageUrl);
                oldBlog.ImageUrl= Helper.SaveFile(_env.WebRootPath, @"uploads\blogs", blog.ImageFile);
            }
            oldBlog.Title=blog.Title;
            oldBlog.Description=blog.Description;
            oldBlog.RedirectUrl=blog.RedirectUrl;
            _blogRepository.Commit();
        }
    }
}
