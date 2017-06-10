using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Security;
using FashionAde.Core;
using FashionAde.Core.Accounts;
using FashionAde.Data.Repository;

using System.Web;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using FashionAde.Web.Controllers.MVCInteraction;

namespace FashionAde.Web.Controllers
{
    public class Common
    {
        public static readonly RegexOptions Options = RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline;

        public static string RemoveExtraSpaces(string text)
        {
            Regex regex = new Regex(@"\s{2,}", Options);
            text = regex.Replace(text.Trim(), " ");
            regex=new Regex(@"\s(\!|\.|\?|\;|\,|\:)");
            text = regex.Replace(text, "$1");
            return text;

        }

        public static Pager Paging(int totalCount, int currentPage, int pageSize, int pageCount)
        {
            Pager pager = new Pager();
            
            if (totalCount == 0)
                return pager;

            if (currentPage == -1)
                if (totalCount % pageSize > 0)
                    currentPage = (totalCount / pageSize) + 1;
                else
                    currentPage = (totalCount / pageSize);

            int totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(totalCount) / pageSize));

            int startPosition = currentPage - (pageCount / 2);
            int endPosition = currentPage + (pageCount / 2);
            if (startPosition <= 0)
            {
                startPosition = 1;
                endPosition = startPosition + pageCount;
                if (endPosition > totalPages)
                    endPosition = totalPages;
            }
            else if (endPosition > totalPages)
            {
                endPosition = totalPages;
                startPosition = endPosition - pageCount;
                if (startPosition <= 0)
                    startPosition = 1;
            }

            List<SelectListItem> pages = new List<SelectListItem>();
            SelectListItem page;

            if (totalPages > pageCount && currentPage - (pageCount / 2) > 1)
            {
                SelectListItem first = new SelectListItem();
                first.Text = "|<";
                first.Value = "1";
                pages.Add(first);
            }

            for (int j = startPosition; j <= endPosition; j++)
            {
                page = new SelectListItem();
                page.Value = j.ToString();
                page.Selected = j == currentPage;
                page.Text = j.ToString();
                pages.Add(page);
            }

            if (totalPages > pageCount && currentPage + (pageCount / 2) < totalPages)
            {
                SelectListItem last = new SelectListItem();
                last.Text = ">|";
                last.Value = "-1";
                pages.Add(last);
            }

            pager.Pages = pages;
            pager.TotalPages = totalPages;

            return pager;
        }
    }

    public static class CookieManager
    {
        public static bool Set(string cookieName, object cookieValue)
        {

            bool retval = true;

            try
            {
                BinaryFormatter bf = new BinaryFormatter();

                MemoryStream ms = new MemoryStream();

                bf.Serialize(ms, cookieValue);

                byte[] inbyt = ms.ToArray();

                System.IO.MemoryStream objStream = new MemoryStream();

                System.IO.Compression.DeflateStream objZS = new System.IO.Compression.DeflateStream(objStream, System.IO.Compression.CompressionMode.Compress);

                objZS.Write(inbyt, 0, inbyt.Length);

                objZS.Flush();

                objZS.Close();

                byte[] b = objStream.ToArray();

                string sCookieVal = Convert.ToBase64String(b);

                HttpCookie cook = new HttpCookie(cookieName);

                cook.Value = sCookieVal;

                cook.Expires = DateTime.Today.AddDays(5);

                HttpContext.Current.Response.Cookies.Add(cook);
            }

            catch
            {

                retval = false;

                throw;

            }

            return retval;

        }

        public static object Get(string cookieName)
        {

            object retval = null;

            try
            {

                byte[] bytCook = Convert.FromBase64String(HttpContext.Current.Request.Cookies[cookieName].Value);

                MemoryStream inMs = new MemoryStream(bytCook);

                inMs.Seek(0, 0);

                DeflateStream zipStream = new DeflateStream(inMs,
                                  CompressionMode.Decompress, true);

                byte[] outByt = ReadFullStream(zipStream);

                zipStream.Flush();

                zipStream.Close();

                MemoryStream outMs = new MemoryStream(outByt);

                outMs.Seek(0, 0);

                BinaryFormatter bf = new BinaryFormatter();

                retval = (object)bf.Deserialize(outMs, null);

            }

            catch (Exception ex)
            {

                throw ex;

            }

            return retval;

        }

        public static bool Has(string cookieName) 
        {
            return (IsEnabled() && HttpContext.Current.Request.Cookies[cookieName] != null);
        }

        public static bool IsEnabled()
        {
            return (HttpContext.Current.Request.Browser.Cookies);
        }

        public static bool Delete(string cookieName)
        {

            bool retval = true;

            try
            {

                HttpContext.Current.Response.Cookies[cookieName].Expires =

                        DateTime.Now.AddDays(-365);

            }

            catch
            {

                retval = false;

            }

            return retval;

        }

        private static byte[] ReadFullStream(Stream stream)
        {

            byte[] buffer = new byte[32768];

            using (MemoryStream ms = new MemoryStream())
            {

                while (true)
                {

                    int read = stream.Read(buffer, 0, buffer.Length);

                    if (read <= 0)

                        return ms.ToArray();

                    ms.Write(buffer, 0, read);

                }

            }

        }
    }

}


