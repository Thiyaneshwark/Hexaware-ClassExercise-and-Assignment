using API_CF_Demo1.Models;
using API_CF_Demo1.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_CF_Demo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _service;
        public DepartmentsController(IDepartmentService service)
        {
            _service=service;
        }
        // [Route("AllDepartments")]
        //[Route("GetAll")]
        [EnableCors("AllowSpecificOrigin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Department> departments = _service.GetAllDepartments();
            return Ok(departments);
        }
        //[Route("DepartmentById")]
         [HttpGet("{id:int}")]
        //[HttpGet("{id:int:range(101,200")]
        // [HttpGet("date/{date:dateTime}")]
        // [HttpGet("{id:int:min(1)}")]
        [EnableCors("AllowSpecificOrigin")]
        public IActionResult GetDepartmentById(int id)
        {
            Department department = _service.GetDepartmentById(id);
            return Ok(department);
        }
        //[HttpGet]
       //[HttpGet("{name}")]
        [HttpGet("name/{name:length(3,12)}")]
        public IActionResult GetDepartmentByName(string name)
        {
           var department = _service.SearchByName(name);
            return Ok(department);
        }
        [HttpGet("search")]
        public IActionResult GetDepartmentBySearch(string search)
        {
            throw new Exception();
            var department = _service.SearchByName(search);
            return Ok(department);
        }

        [HttpPost]
        public IActionResult Post(Department department)
        {
            int Result =_service.AddNewDepartment(department);
            return Ok(Result);
        }

        [HttpPut]
        public IActionResult Put(Department department)
        {
            string result=_service.UpdateDepartment(department);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            string result=_service.DeleteDepartment(id);
            return Ok(result);
        }
    }
}
