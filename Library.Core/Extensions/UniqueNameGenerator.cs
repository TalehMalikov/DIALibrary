using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Extensions
{
    public static class UniqueNameGenerator
    {
        public static string UniqueFileNameGenerator(string name)
        {
            string hashCode = String.Format("{0:X}", name.GetHashCode());
            string fileName = Guid.NewGuid().ToString();
            return fileName + "-" + hashCode;
        }
        /*public static string UniqueFilePathGenerator(IFormFile file)
        {
            FileInfo fileInfo = new FileInfo(file.FileName);
            string fileExtension = fileInfo.Extension;
            string fileName = Guid.NewGuid().ToString();
            return fileName + fileExtension;
        }*/
    }
}
