using Core.Application.Common;
using Core.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Core.Application.CrossCuttings
{
    public class ExceptionHandlingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
            where TResponse : BaseResult
    {
        private readonly ILogger _logger;

        public ExceptionHandlingBehaviour(ILogger<ExceptionHandlingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response;

            try
            {
                response = await next();
            }
            catch (BusinessException businessException)
            {
                response = (TResponse)BaseResult.Failure(businessException.ErrorCode, businessException.Message);
            }
            catch (Exception ex)
            {
                return (TResponse)BaseResult.Failure("9999", "Technical Exception");
            }

            _logger.LogError(JsonConvert.SerializeObject(response));

            return response;
        }
    }
}
