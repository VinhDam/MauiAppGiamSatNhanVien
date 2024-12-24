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
    public class DepartmentAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IDepartmentRepository _dbDepartment;
        private readonly ILocationRepository _dbLocation;
        private readonly IMapper _mapper;
        public DepartmentAPIController(IDepartmentRepository dbDepartment, IMapper mapper, ILocationRepository locationRepository)
        {
            this._response = new APIResponse();
            _dbDepartment=dbDepartment;
            _mapper=mapper;
            _dbLocation=locationRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllDepartments()
        {
            try
            {
                IEnumerable<Department> locationList;
                locationList = await _dbDepartment.GetAllAsync(includeProperties: "Location");
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
        [HttpGet("{id:int}", Name = "GetDepartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetDepartment(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var location = await _dbDepartment.GetAsync(u => u.Id==id);
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
        public async Task<ActionResult<APIResponse>> CreateDepartment([FromBody] DepartmentDTO locationDTO)
        {
            try
            {
                if (await _dbDepartment.GetAsync(u => u.Name.ToLower() == locationDTO.Name.ToLower())!=null)
                {
                    ModelState.AddModelError("ErrorMessages", "Department already exists!");
                    return BadRequest(ModelState);
                }

                if (await _dbLocation.GetAsync(u => u.Id == locationDTO.LocationId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Location ID is invalid!");
                    return BadRequest(ModelState);
                }

                if (locationDTO == null)
                {
                    return BadRequest(locationDTO);
                }
                var location = await _dbDepartment.CreateAsync(locationDTO);
                _response.Result = location;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetDepartment", new { id = location.Id }, _response);
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

        [HttpDelete("{id:int}", Name = "DeleteDepartment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteDepartment(int id)
        {
            try
            {
                if (id==0)
                {
                    return BadRequest();
                }
                var Department = await _dbDepartment.GetAsync(v => v.Id == id);
                if (Department == null)
                {
                    return NotFound();
                }
                await _dbDepartment.RemoveAsync(Department);
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
        [HttpPut("{id:int}", Name = "UpdateDepartment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateDepartment(int id, [FromBody] DepartmentDTO departmentDTO)
        {
            try
            {
                if (departmentDTO==null)
                {
                    return BadRequest();
                }

                if (await _dbLocation.GetAsync(u => u.Id == departmentDTO.LocationId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Location ID is invalid!");
                    return BadRequest(ModelState);
                }

                _response.Result = await _dbDepartment.UpdateAsync(id, departmentDTO);
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
