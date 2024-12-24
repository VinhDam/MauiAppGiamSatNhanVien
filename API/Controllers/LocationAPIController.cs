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
    public class LocationAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ILocationRepository _dbLocation;
        private readonly IMapper _mapper;
        public LocationAPIController(ILocationRepository dbLocation, IMapper mapper)
        {
            this._response = new APIResponse();
            _dbLocation=dbLocation;
            _mapper=mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllLocations()
        {
            try
            {
                IEnumerable<Location> locationList;
                locationList = await _dbLocation.GetAllAsync();
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
        [HttpGet("{id:int}", Name = "GetLocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetLocation(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var location = await _dbLocation.GetAsync(u => u.Id==id);
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
        public async Task<ActionResult<APIResponse>> CreateLocation([FromBody] LocationDTO locationDTO)
        {
            try
            {
                if (await _dbLocation.GetAsync(u => u.Name.ToLower() == locationDTO.Name.ToLower())!=null)
                {
                    ModelState.AddModelError("ErrorMessages", "Location already exists!");
                    return BadRequest(ModelState);
                }

                if (locationDTO == null)
                {
                    return BadRequest(locationDTO);
                }
                var location = await _dbLocation.CreateAsync(locationDTO);
                _response.Result = location;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetLocation", new { id = location.Id }, _response);
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

        [HttpDelete("{id:int}", Name = "DeleteLocation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteLocation(int id)
        {
            try
            {
                if (id==0)
                {
                    return BadRequest();
                }
                var Location = await _dbLocation.GetAsync(v => v.Id == id);
                if (Location == null)
                {
                    return NotFound();
                }
                await _dbLocation.RemoveAsync(Location);
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
        [HttpPut("{id:int}", Name = "UpdateLocation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateLocation(int id, [FromBody] LocationDTO locationDTO)
        {
            try
            {
                if (locationDTO==null)
                {
                    return BadRequest();
                }

                _response.Result = await _dbLocation.UpdateAsync(id, locationDTO);
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
