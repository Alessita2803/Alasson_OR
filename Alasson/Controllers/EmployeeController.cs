using Alasson.Interfaces;
using Alasson.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Alasson.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeesService _employeeService;
        //recibiendo la clase ES y le decimos que la estructura es IES
        //Desde el controlador no podemos acceder a la clase, por lo que si queremos usar la dependencia, debemos saber su estructura => interfaz
        public EmployeeController(IEmployeesService employee)
        {
            _employeeService = employee;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task< IActionResult> Get() //La devolución será uná respuesta http
        {
           
            var employees = await _employeeService.ListAsync();
            var res = new { employees }; //Json atributo employee, y dentro tendrá la lista convertida en un array
            return Ok(res); //Código de respuesta 200
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        //Definiendo la estructura del cuerpo de la petición
        public class PostBody //El cuerpo de la petición puede ser una clase 
        {
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Charge { get; set; }
            public float Salary { get; set; }
        }

        //usando [FromBody] le decimos que queremos obtener los datos del body de la petición y le ponemos que la estructura será del postbody y los datos se guardarán en la variable req
        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostBody req)
        {
            var fullname = req.FullName;
            var email = req.Email;
            var charge = req.Charge;
            var salary = req.Salary;

            var enployee = new Employee(fullname, email, charge, salary);   
            await _employeeService.AddAsync(enployee);
            var message = "Employee Registered Successfully";
            var res = new { message};

            return Ok(res);

        }

        public class PutBody
        {
            public int key { get; set; }

            public string value { get; set; }
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{fullname}")]
        public async Task<IActionResult> Put(string fullname, [FromBody] PutBody body)
        {
            try
            {
                var employee = await _employeeService.UpdateAsync(fullname, body.key, body.value);
                return Ok(employee); 
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{fullname}")]
        public  IActionResult Delete( string fullname) //Task- await
        {
            try
            {
                var message = _employeeService.DeleteAsync(fullname);
                return Ok(message);
            }catch (Exception ex)
            {
                return  BadRequest(ex.Message);
            }
        }
    }
}
