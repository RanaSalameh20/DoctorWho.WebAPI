using AutoMapper;
using DoctorWho.Db.DatabaseContext;
using DoctorWho.Db.Entities;
using DoctorWho.Db.Repositories;
using DoctorWho.Web.Models;
using DoctorWho.Web.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorWho.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class DoctorsController : ControllerBase
    {
        private readonly DoctorWhoCoreDbContext _context;
        private readonly DoctorRepository _doctorRepository;
        private readonly IMapper _mapper;


        public DoctorsController(DoctorWhoCoreDbContext context
            ,DoctorRepository doctorRepository
            , IMapper mapper)
        {
            _context = context;
            _doctorRepository = doctorRepository;
            _mapper = mapper;

        }

        [HttpGet]
        public ActionResult<IEnumerable<DoctorDto>> GetAllDoctors()
        {
            var doctors = _doctorRepository.GetAllDoctors();
            var doctorDtos = _mapper.Map<IEnumerable<DoctorDto>>(doctors);

            return Ok(doctorDtos);
        }

        [HttpPost("upsert")]
        public ActionResult<DoctorDto> UpsertDoctor([FromBody] DoctorDto doctorDto)
        {  
            Doctor? doctorEntity;

            if (doctorDto.DoctorId > 0)
            {
                doctorEntity = _context.Doctors.FirstOrDefault(d => d.DoctorId == doctorDto.DoctorId);

                if (doctorEntity == null)
                {
                    return NotFound("Doctor not found");
                }

                // Update the doctor entity with values from the DTO
                _mapper.Map(doctorDto, doctorEntity);
            }
            else
            {
                doctorEntity = _mapper.Map<Doctor>(doctorDto);
                _context.Doctors.Add(doctorEntity);
            }

            _context.SaveChanges();

            var upsertedDoctorDto = _mapper.Map<DoctorDto>(doctorEntity);

            return Ok(upsertedDoctorDto);
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

            return NoContent(); // Return 204 No Content on successful deletion
        }

    }
}
