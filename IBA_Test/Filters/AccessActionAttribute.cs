using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace IBA_Test.Filters
{
    public class AccessActionAttribute : ActionFilterAttribute
    {
        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            DateTime start = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["StartTime"]);
            DateTime end = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["EndTime"]);
            if (DateTime.Now.TimeOfDay < start.TimeOfDay || DateTime.Now.TimeOfDay > end.TimeOfDay)
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);

            return Task.CompletedTask;
        }
    }
}