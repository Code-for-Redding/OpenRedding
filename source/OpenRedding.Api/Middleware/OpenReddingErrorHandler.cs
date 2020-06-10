namespace OpenRedding.Api.Middleware
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;
    using FluentValidation;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Core.Exception;
    using OpenRedding.Domain.Common.Dto;
    using OpenRedding.Domain.Common.ViewModels;

    public class OpenReddingErrorHandler
    {
        private readonly RequestDelegate _pipeline;
        private readonly ILogger<OpenReddingErrorHandler> _logger;

        public OpenReddingErrorHandler(RequestDelegate pipeline, ILogger<OpenReddingErrorHandler> logger) =>
            (_pipeline, _logger) = (pipeline, logger);

        /// <summary>
        /// Kicks off he request pipeline while catching any exceptions thrown in the application layer.
        /// </summary>
        /// <param name="context">HTTP context from the request pipeline.</param>
        /// <returns>Hand off to next request delegate in the pipeline.</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _pipeline(context);
            }
            catch (Exception e)
            {
                _logger.LogError($"An exception has occurred processing the request: {e.Message}");
                await HandleExceptionAsync(context, e);
            }
        }

        /// <summary>
        /// Handles any exception thrown during the pipeline process and in the application layer. Note that model state
        /// validation failures made in the web layer are handled by the ASP.NET Core model state validation failure filter.
        /// </summary>
        /// <param name="context">HTTP context from the request pipeline.</param>
        /// <param name="exception">Exceptions thrown during pipeline processing.</param>
        /// <returns>Writes the API response to the context to be returned in the web layer.</returns>
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            IList<OpenReddingErrorDto> errorList = new List<OpenReddingErrorDto>();

            /*
             * Handle exceptions based on type, while defaulting to generic internal server error for unexpected exceptions.
             * Each case handles binding the API response message, API response status code, the HTTP response status code,
             * and any errors incurred in the application layer. Validation failures returned from Fluent Validation will
             * be added to the API response if there are any instances.
             */
            switch (exception)
            {
                case OpenReddingApiException apiException:
                    errorList.Add(new OpenReddingErrorDto(apiException.StatusCode.ToString(), apiException.Message));
                    context.Response.StatusCode = (int)apiException.StatusCode;

                    // Add any appropriate API errors thrown in the core layer
                    if (apiException.ApiErrors.Count > 0)
                    {
                        foreach (var error in apiException.ApiErrors)
                        {
                            errorList.Add(new OpenReddingErrorDto(error.PropertyName, error.ErrorMessage));
                        }
                    }

                    break;

                case ValidationException validationException:
                    context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;

                    foreach (var validationFailure in validationException.Errors)
                    {
                        var validationError = new OpenReddingErrorDto(validationFailure.PropertyName, validationFailure.ErrorMessage);
                        errorList.Add(validationError);
                    }

                    break;

                default:
                    errorList.Add(new OpenReddingErrorDto("Internal server error", "An error has occurred, please attempt the request again at a later time."));
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            // Instantiate the response
            context.Response.ContentType = "application/json";
            var errorResponse = new OpenReddingErrorViewModel("An error(s) has occurred while processing the request.", errorList);

            // Serialize the response and write out to the context buffer to return
            var result = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(result);
        }
    }
}
