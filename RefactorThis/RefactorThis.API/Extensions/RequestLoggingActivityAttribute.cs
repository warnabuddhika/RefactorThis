using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using TimeSheet.Core.Common.Interfaces;
using TimeSheet.Domain.Models;

namespace TimeSheet.Api.Extensions
{
    public class RequestLoggingActivityAttribute : ActionFilterAttribute
    {
        public readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IConfiguration _configuration;

        public RequestLoggingActivityAttribute(IApplicationDbContext context,
            ICurrentUserService currentUserService,
            IConfiguration configuration)
        {
            _context = context;
            _currentUserService = currentUserService;
            _configuration = configuration;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var enableActivityLog = _configuration.GetValue<bool>("EnableActivityLog");

            if (!enableActivityLog)
                return;

            var sb = new StringBuilder();
            foreach (var key in filterContext.RouteData.Values.Keys)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                _ = sb.AppendFormat("{0}: {1}", key, filterContext.RouteData.Values[key].ToString());
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var audit = new UserActivityLog
            {
                UserName = _currentUserService.IdentifierNumber,
                IPAddress = filterContext.HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString(),
                ActivityJson = sb.ToString(),
                TimeStamp = DateTime.UtcNow
            };
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            _context.UserActivityLogs.Add(audit);
            _context.SaveChangesAsync().Wait();

            base.OnActionExecuting(filterContext);
        }


    }
}
