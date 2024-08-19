using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace ProyectoHotel.App_Start
{
    public class SessionEnabledHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var httpContext = HttpContext.Current;
            if (httpContext != null && httpContext.Session != null)
            {
                httpContext.SetSessionStateBehavior(SessionStateBehavior.Required);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }

}