using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace RefactorThis.Core.Common.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
        private readonly IConfiguration _configuration;

        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            string logLevel = _configuration.GetValue("Logging:LogLevel:RefactorThis.Core.Common.Behaviours.LoggingBehaviour", "Information");
            bool enableLogging = logLevel == "Information";

            //Request
            if (enableLogging)
            {
                _logger.LogInformation($"Handling {typeof(TRequest).Name}");
                Type myType = request.GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
                foreach (PropertyInfo prop in props)
                {
                    object propValue = prop.GetValue(request, null);
                    _logger.LogInformation("{Property} : {@Value}", prop.Name, propValue);
                }
            }

            var response = await next();

            //Response
            if (enableLogging)
            {
                _logger.LogInformation($"Handled {typeof(TResponse).Name}");
            }

            return response;
        }
    }
}
