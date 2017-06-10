using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace FashionAde.Web.Common
{
    /// <summary>
    /// Converts a JSON or XML input into a system object.
    /// </summary>
    /// <remarks>
    /// Check documentation at:     
    /// http://omaralzabir.com/create_rest_api_using_asp_net_mvc_that_speaks_both_json_and_plain_xml/
    /// </remarks>
    public class ObjectFilter : ActionFilterAttribute
    {
        public string Param { get; set; }
        public Type RootType { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if ((filterContext.HttpContext.Request.ContentType ?? string.Empty).Contains("application/json"))
            {
                object o = new DataContractJsonSerializer(RootType).ReadObject(filterContext.HttpContext.Request.InputStream);
                filterContext.ActionParameters[Param] = o;
            }
            else
            {
                var xmlRoot = XElement.Load(new StreamReader(filterContext.HttpContext.Request.InputStream, filterContext.HttpContext.Request.ContentEncoding));
                object o = new XmlSerializer(RootType).Deserialize(xmlRoot.CreateReader());
                filterContext.ActionParameters[Param] = o;
            }
        }
    }
}
