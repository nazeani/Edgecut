using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface IBlogService
    {
        void AddBlog(Blog blog);
        void DeleteBlog(int id);
        Blog GetBlog(Func<Blog,bool>? func=null);
        List<Blog> GetAllBlog(Func<Blog, bool>? func = null);
        void UpdateBlog(int id, Blog blog);
    }
}
