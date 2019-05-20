using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class MemoryRepository : IRepository<Student>
    {
        public List<Student> students = new List<Student>()
        {
            new Student() { ID = 1, Address = "shanghai", FirstName = "sun", LastName = "xuefei"},
            new Student() { ID = 2, Address = "beijing", FirstName = "han",LastName = "liping"},
            new Student() { ID = 3, Address = "wuan", FirstName = "wan",LastName = "cheng"},
        };
        public IEnumerable<Student> GetList()
        {
            return students;
        }

        public Student Get(int id)
        {
            return students.FirstOrDefault(t => t.ID == id);
        }
     

        public int Create(Student stu)
        {
            //  int id = students.OrderByDescending(t => t.ID).First().ID + 1;

            int id = students.Max(t => t.ID) + 1;
            stu.ID = id;
            students.Add(stu);
            return id;
        }
    }
}
