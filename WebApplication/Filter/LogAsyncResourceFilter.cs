using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Filter
{
    public class LogAsyncResourceFilter : Attribute, IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {

            Console.WriteLine("---------OnResourceExecutionAsync---start--------");
            var excutedContext = await next();


            Console.WriteLine("---------OnResourceExecutionAsync----end-------");

        }
    }
}
