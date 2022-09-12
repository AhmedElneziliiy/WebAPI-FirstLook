using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPICRUD.Models;

namespace WebAPICRUD.Controllers
{
    public class EmployeeController : ApiController
    {
        CompanyContext Db;
        public EmployeeController()
        {
            Db = new CompanyContext();
        }

        public IHttpActionResult GetEmployees()
        {
            return Ok(Db.Employees.ToList());
        }
        public IHttpActionResult GetEmployee(int id)
        {
            var emp1 = Db.Employees.Find(id);
            if (emp1 is null)
            {
                return NotFound();
            }
            return Ok(emp1);
        }

        public IHttpActionResult GetEmployeeByName(string name)
        {
            var emp1 = Db.Employees.FirstOrDefault(w=>w.Name.ToLower()==name.ToLower());
            if (emp1 is null)
            {
                return NotFound();
            }
            return Ok(emp1);
        }

        public IHttpActionResult PostEmployee(Employee emp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Employee Object is not valid !!!");
            }
            Db.Employees.Add(emp);

            try
            {
                Db.SaveChanges();
            }
            catch (Exception)
            {
                if (EmployeeExist(emp.ID))
                {
                    return Conflict();
                }
                else
                {
                    return StatusCode(HttpStatusCode.InternalServerError);
                }
            }

            //success

            //return Ok();
            //return Ok(emp);  // -->200
            //return Created("",emp); //-->201
            return CreatedAtRoute("DefaultApi", new { id = emp.ID }, emp);
        }
        //edit
        public IHttpActionResult PutEmployee( [FromUri] int id, [FromBody] Employee emp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id!=emp.ID)
            {
                return BadRequest("primary key is wrong");
            }
           
            //update set where based on priary key
            Db.Entry(emp).State = System.Data.Entity.EntityState.Modified;

            try
            {
                Db.SaveChanges();
            }
            catch (Exception)
            {
                if (!EmployeeExist(id))
                {
                    return NotFound();
                }
                else
                {
                    return InternalServerError();
                }
              
            }
            //success
            return StatusCode(HttpStatusCode.NoContent);//return 201 and no content with it

        }
        public IHttpActionResult DeletEmp(int id)
        {
            var emp1 = Db.Employees.Find(id);
            if (emp1 is null)
            {
                return NotFound();
            }
            Db.Employees.Remove(emp1);
            Db.SaveChanges();
            //return Ok();
            return Ok(emp1);
        }

        //function to ensure is exist in database
        public bool EmployeeExist(int id)
        {
            return Db.Employees.Count(w => w.ID == id) > 0; //emp exist
        }
    }
}
