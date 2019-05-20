using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Dbcontext;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class StudentRepositroy : IRepository<Student>
    {
        private readonly CoreDBContext context;

        public StudentRepositroy(CoreDBContext context)
        {
            this.context = context;
        }
        public int Create(Student t)
        {

            context.Students.Add(t);
            context.SaveChanges();
            return t.ID;
        }


        public Student Get(int id)
        {
            return context.Students.FirstOrDefault(t => t.ID == id);
        }

        public IEnumerable<Student> GetList()
        {
            return context.Students.ToList();
        }
    }
}
