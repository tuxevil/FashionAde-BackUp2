using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using FashionAde.Core;
using FashionAde.Core.DataInterfaces;

namespace FashionAde.Web.Controllers.BuildYourCloser
{
    [HandleError]
    public class EventTypeSelectorController : BuildYourClosetController
    {
        private IEventTypeRepository eventTypeRepository;

        public EventTypeSelectorController(IEventTypeRepository eventTypeRepository)
        {
            this.eventTypeRepository = eventTypeRepository;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Index()
        {
            if (ClosetState.Flavors == null)
                Response.Redirect(Url.RouteUrl(new { controller = "FlavorSelect", action = "Index" }));

            if (ClosetState.EventTypes != null)
                ViewData["EventTypeSelected"] = ClosetState.EventTypes;

            ViewData["previousUrl"] = Session["previousUrl"];
            return View(eventTypeRepository.GetAll());
        }

        public RedirectToRouteResult SelectEventType(FormCollection values)
        {
            List<EventType> selectedET = new List<EventType>();

            foreach (var value in values)
            {
                object o = values[value.ToString()];
                if (o.ToString().Contains("true"))
                    selectedET.Add(eventTypeRepository.Get(Convert.ToInt32(value.ToString().Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries)[2])));
            }

            ClosetState.SetEventTypes(selectedET);

            bool previous = ((NameValueCollection)(values)).AllKeys[6].ToLower().Contains("previous");
            if (previous)
                return RedirectToAction("Index", Session["previousUrl"].ToString());

            return RedirectToAction("Index", "GarmentSelector"); ;
        }
    }
}
