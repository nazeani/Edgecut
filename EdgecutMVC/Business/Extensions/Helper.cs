using Business.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Extensions
{
    public static class Helper
    {
        public static string SaveFile(string rootPath,string folder, IFormFile file) 
        {
            if (file.ContentType != "image/jpeg" && file.ContentType != "image/jpeg") throw new ImageContextException("Sekilin uzantisi sehvdir!");
            if (file.Length > 2000000000) throw new ImageLengthException("Sekil 2mbdan cox ola bilmez!");
            string fileName=Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string path = rootPath + $@"\{folder}\" + fileName;
            using(FileStream fileStream=new FileStream(path,FileMode.Create)) 
            {
                file.CopyTo(fileStream);
            }
            return fileName;
        }
        public static void DeleteFile(string rootPath, string folder, string fileName) 
        {
            string path = rootPath + $@"\{folder}\" + fileName;
            if (!File.Exists(path)) throw new Exceptions.FileNotFoundException("File tapilmadi!");
            File.Delete(path);
        }
    }
}
