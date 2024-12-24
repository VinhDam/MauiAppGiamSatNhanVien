using API.Models;
using API.Models.DTO;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IEmployeeRepository _dbEmployee;
        private readonly IMapper _mapper;
        public EmployeeAPIController(IEmployeeRepository dbEmployee, IMapper mapper)
        {
            this._response = new APIResponse();
            _dbEmployee=dbEmployee;
            _mapper=mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllEmployees()
        {
            try
            {
                IEnumerable<Employee> locationList;
                locationList = await _dbEmployee.GetAllAsync(includeProperties: "Department,Shift");
                _response.Result = locationList;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _response;
        }
        [HttpGet("{id:int}", Name = "GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetEmployee(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var location = await _dbEmployee.GetAsync(u => u.Id==id);
                if (location == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }
                _response.Result = location;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _response;
        }
        [HttpPost]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateEmployee([FromBody] EmployeeDTO locationDTO)
        {
            try
            {
                if (await _dbEmployee.GetAsync(u => u.Name.ToLower() == locationDTO.Name.ToLower())!=null)
                {
                    ModelState.AddModelError("ErrorMessages", "Employee already exists!");
                    return BadRequest(ModelState);
                }

                if (locationDTO == null)
                {
                    return BadRequest(locationDTO);
                }
                var location = await _dbEmployee.CreateAsync(locationDTO);
                _response.Result = location;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetEmployee", new { id = location.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteEmployee")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteEmployee(int id)
        {
            try
            {
                if (id==0)
                {
                    return BadRequest();
                }
                var Employee = await _dbEmployee.GetAsync(v => v.Id == id);
                if (Employee == null)
                {
                    return NotFound();
                }
                await _dbEmployee.RemoveAsync(Employee);
                _response.StatusCode=HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _response;
        }
        [HttpPut("{id:int}", Name = "UpdateEmployee")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateEmployee(int id, [FromBody] EmployeeDTO locationDTO)
        {
            try
            {
                if (locationDTO==null)
                {
                    return BadRequest();
                }

                _response.Result = await _dbEmployee.UpdateAsync(id, locationDTO);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _response;
        }
    }
}
