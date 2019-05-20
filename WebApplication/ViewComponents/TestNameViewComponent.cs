using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.ViewComponents
{
    public class TestNameViewComponent : ViewComponent
    {
        private readonly IRepository<Student> repository;

        public TestNameViewComponent(IRepository<Student> repository
            )
        {
            this.repository = repository;
        }
         
        public IViewComponentResult Invoke()
        {

            var count = repository.GetList().Count().ToString();
            return View("default",count);

        }

    }
}
