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
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetAllDoctors()
        {
            var doctorDtos = await _doctorRepository.GetAllDoctors();

            return Ok(doctorDtos);
        }

        [HttpPost("upsert")]
        public async Task<ActionResult<DoctorDto>> UpsertDoctor([FromBody] DoctorDto doctorDto)
        {
            DoctorDto upsartedDoctor;
            if (doctorDto.DoctorId > 0)
            {
                var doctorEntity = await _doctorRepository.GetDoctorById(doctorDto.DoctorId);

                if (doctorEntity == null)
                {
                    return NotFound("Doctor not found");
                }

                upsartedDoctor = await _doctorRepository.UpdateDoctor(doctorEntity, doctorDto);
                return Ok(upsartedDoctor);
            }

            upsartedDoctor = await _doctorRepository.CreateDoctor(doctorDto);
            return Ok(upsartedDoctor);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDoctor(int id)
        {
            var isDoctorExist = await _doctorRepository.IsDoctorExistAsync(id);

            if (!isDoctorExist)
            {
                return NotFound("Doctor not found");
            }

            await _doctorRepository.DeleteDoctor(id);

            return NoContent(); 
        }

    }
}
