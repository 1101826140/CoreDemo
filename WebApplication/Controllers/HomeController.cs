using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using WebApplication.Filter;
using WebApplication.Log;
using WebApplication.Models;
using WebApplication.Services;
using WebApplication.ViewModels;

/// <summary>
/// 打开项目位置 命令行 dotnet run 运行，默认http://localhost:5000
/// </summary>
namespace WebApplication.Controllers
{
    //[LogResourceFilter] : controller级别
    public class HomeController : Controller
    {
        private readonly IRepository<Student> repository;
        private readonly ILogger<HomeController> logger; //内置logger

        public HomeController(IRepository<Student> repository, ILogger<HomeController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }



        //public string Index()
        //{

        //    //action名字,输出 : Index
        //    //return this.ControllerContext.ActionDescriptor.ActionName;

        //    //输出字符串
        //    //return this.Content("啊啊啊啊我是controller");

        //}

        //public IActionResult Index()
        //{
        //    var list = repository.GetList();

        //    return View(list);
        //}
        /// <summary>
        /// home/index
        /// </summary>
        /// <returns></returns>
       // [LogResourceFilter] //action级别
       
        public IActionResult Index()
        {
            logger.LogInformation(MyLogger.HomePage, "------------------Visit Home Index Page---------------");
            logger.LogTrace("-----------------我等级低就不写入到控制台--------------");  //根据appsettings.{development}.json文件中的设置
            // throw new Exception("我是异常");
            var list = repository.GetList();
            var svm = list.Select(t => new StudentViewArgs()
            {
                ID = t.ID,
                Name = $"{t.FirstName} {t.LastName}",
                Address = t.Address,
                Birthday = DateTime.Now.AddYears(-10),  //DateTime.Now.Subtract(new DateTime(2001,10,12)).Days/365,
                Gender = Enums.GenderEnumType.female,
            }).ToList();

            return View(svm);
        }

        /// <summary>
        /// home/details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            //var stu = repository.Get(id); 
            logger.LogInformation(MyLogger.HomePage, "------------------Visit Home Details Page---------------" + id);

            StudentViewArgs stu = repository.GetList().Where(t => t.ID == id).Select(t => new StudentViewArgs()
            {
                ID = t.ID,
                Name = t.FirstName + " " + t.LastName,
                Address = t.Address,
            }).First();

            if (stu == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //int zhousui = DateTime.Now.Subtract(new DateTime(2019, 10, 12)).Days / 365;  //周岁
            return View(stu);

        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        /// <summary>
        /// 创建学生
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(StudentAddArgs args)
        {
            if (ModelState.IsValid)
            {
                int id = repository.Create(new Student()
                {
                    Address = args.Address,
                    LastName = args.LastName,
                    FirstName = args.FirstName,
                    Birthday = args.Birthday,
                    Gender = args.Gender,
                });
                //return RedirectToAction(nameof(Details), new { id = id });
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "我是error");
            return View();
        }


        public IActionResult MyError()
        {

            return View();
        }
    }
}