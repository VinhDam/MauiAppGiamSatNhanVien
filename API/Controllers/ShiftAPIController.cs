using API.Models;
using API.Models.DTO.ShiftDTO;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IShiftRepository _dbShift;
        private readonly IMapper _mapper;
        public ShiftAPIController(IShiftRepository dbShift, IMapper mapper)
        {
            this._response = new APIResponse();
            _dbShift=dbShift;
            _mapper=mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllShifts()
        {
            try
            {
                IEnumerable<Shift> shiftList;
                shiftList = await _dbShift.GetAllAsync();
                _response.Result = shiftList;
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
        [HttpGet("{id:int}", Name = "GetShift")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetShift(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var shift = await _dbShift.GetAsync(u => u.Id==id);
                if (shift == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }
                _response.Result = shift;
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
        public async Task<ActionResult<APIResponse>> CreateShift([FromBody] ShiftDTO shiftDTO)
        {
            try
            {
                if (await _dbShift.GetAsync(u => u.Name.ToLower() == shiftDTO.Name.ToLower())!=null)
                {
                    ModelState.AddModelError("ErrorMessages", "Shift already exists!");
                    return BadRequest(ModelState);
                }

                if (shiftDTO == null)
                {
                    return BadRequest(shiftDTO);
                }
                var shift = await _dbShift.CreateAsync(shiftDTO);
                _response.Result = shift;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetShift", new { id = shift.Id }, _response);
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

        [HttpDelete("{id:int}", Name = "DeleteShift")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id==0)
                {
                    return BadRequest();
                }
                var Villa = await _dbShift.GetAsync(v => v.Id == id);
                if (Villa == null)
                {
                    return NotFound();
                }
                await _dbShift.RemoveAsync(Villa);
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
        [HttpPut("{id:int}", Name = "UpdateShift")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] UpdateShiftDTO updateShiftDTO)
        {
            try
            {
                if (updateShiftDTO==null || id!=updateShiftDTO.Id)
                {
                    return BadRequest();
                }

                _response.Result = await _dbShift.UpdateAsync(updateShiftDTO);
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
