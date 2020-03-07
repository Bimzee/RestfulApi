using LandonApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LandonApi.Filters
{
    public class JsonExceptionFilters : IExceptionFilter
    {
        public JsonExceptionFilters(IHostingEnvironment env)
        {
            this._env = env;
        }

        private readonly IHostingEnvironment _env;

        public void OnException(ExceptionContext context)
        {
            var error = new ApiError();
            if (this._env.IsDevelopment())
            {

                error.Message = context.Exception.Message;
                error.Details = context.Exception.StackTrace;
            }
            else
            {
                error.Message = "A server error occured";
                error.Details = context.Exception.Message;
            }

            context.Result = new ObjectResult(error)
            {
                StatusCode = 500

            };
        }
    }
}
