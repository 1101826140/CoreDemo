using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Enums;

namespace WebApplication.ViewModels
{
    public class StudentViewArgs
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 性别
        /// </summary>

        public GenderEnumType Gender { get; set; }
    }
}
