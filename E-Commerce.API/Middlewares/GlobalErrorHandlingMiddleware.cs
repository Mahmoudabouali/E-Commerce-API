﻿using Domain.Exceptions;
using Shared.ErrorModels;
using System.Net;

namespace E_Commerce.API.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);

                if(httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                    await HandleNotFoundEndPointAsync(httpContext);
            }
            catch (Exception exeption)
            {
                _logger.LogError($"somthing went wrong {exeption}");
                
                await HandleExeptionAsync(httpContext, exeption);
            }
        }

        private async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                ErrorMessage = $"the end point {httpContext.Request.Path} not found"
            }.ToString();
            await httpContext.Response.WriteAsync(response);
        }

        private async Task HandleExeptionAsync(HttpContext httpContext, Exception exception)
        {
            // set deafult status code to 500
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            

            // set content type => Apllication/json
            httpContext.Response.ContentType = "application/json";
            
//pattern matching 
            httpContext.Response.StatusCode = exception switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };
            //return standerd response
            var response = new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = exception.Message
            }.ToString();

            await httpContext.Response.WriteAsync(response);
        }
    }
}