using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPIFirstLook.Models;

namespace WebAPIFirstLook.Controllers
{
    public class EmployeeController : ApiController
    {
        public static List<Employee> Employees { get; set; } = new List<Employee>
        {

            new Employee { Id = 1, Name = "sara" },
            new Employee { Id = 2, Name = "sara" },
            new Employee { Id = 3, Name = "sara" },
            new Employee { Id = 4, Name = "sara" },
            new Employee { Id = 5, Name = "sara" },
            new Employee { Id = 6, Name = "sara" },
            new Employee { Id = 7, Name = "sara" }

        };

        [HttpGet]
        [Route("api/emps")]
        public List<Employee> AllEmployees()
        {
            return Employees;
        }

        [HttpGet]
        [Route("api/emp/{id:int}")]
        public Employee EmpById(int Id)
        {
           
            return Employees.FirstOrDefault(w=>w.Id==Id);
        }

        [HttpGet]
        [Route("api/emp/{name:alpha}")]
        public Employee EmpByName(string name)
        {

            return Employees.FirstOrDefault(w => w.Name.ToLower() == name.ToLower());
        }
    }
}
