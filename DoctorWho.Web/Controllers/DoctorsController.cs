using DoctorWho.Db.Repositories;
using DoctorWho.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class DoctorsController : ControllerBase
    {
        private readonly DoctorRepository _doctorRepository;
        public DoctorsController(DoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DoctorDto>> GetAllDoctors()
        {
            var doctorDtos = _doctorRepository.GetAllDoctors();

            return Ok(doctorDtos);
        }

        [HttpPost("upsert")]
        public ActionResult<DoctorDto> UpsertDoctor([FromBody] DoctorDto doctorDto)
        {

            if (doctorDto.DoctorId > 0)
            {
                var doctorEntity = _doctorRepository.GetDoctorById(doctorDto.DoctorId);

                if (doctorEntity == null)
                {
                    return NotFound("Doctor not found");
                }

                _doctorRepository.UpdateDoctor(doctorEntity, doctorDto);
                return NoContent();
            }

            var createdDoctorDto = _doctorRepository.CreateDoctor(doctorDto);
            return Ok(createdDoctorDto);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            var isDoctorExist = _doctorRepository.DoctorExist(id);

            if (!isDoctorExist)
            {
                return NotFound("Doctor not found");
            }

            _doctorRepository.DeleteDoctor(id);

            return NoContent(); 
        }

    }
}
