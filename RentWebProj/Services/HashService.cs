using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace RentWebProj.Services
{
    public class HashService
    {
        public static string MD5Hash(string rawString)
        {
            if (string.IsNullOrEmpty(rawString))
            {
                return "";
            }
            StringBuilder sb;
            using (MD5 md5 = MD5.Create())
            {
                byte[] btyeArray = Encoding.UTF8.GetBytes(rawString);
                byte[] encryption = md5.ComputeHash(btyeArray);

                sb = new StringBuilder();

                for (int i = 0; i < encryption.Length; i++)
                {
                    sb.Append(encryption[i].ToString("x2"));
                }
            }
           
                return sb.ToString() ;
        }
    }
}