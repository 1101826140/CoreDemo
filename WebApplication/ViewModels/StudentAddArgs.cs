using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Enums;

namespace WebApplication.ViewModels
{
    public class StudentAddArgs
    {

        [Display(Name = "姓"),Required,MinLength(1,ErrorMessage ="最小长度为1")]
        public string FirstName { get; set; }
        [Display(Name = "名")]

        public string LastName { get; set; }
        [Display(Name = "地址")]
        public string Address { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [Display(Name = "生日")]
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public GenderEnumType Gender { get; set; }
    }
}
