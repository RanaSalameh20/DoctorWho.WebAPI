using AutoMapper;
using DoctorWho.Db.DatabaseContext;
using DoctorWho.Db.Entities;
using DoctorWho.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorWho.Db.Repositories
{
    public class DoctorRepository
    {
        private readonly DoctorWhoCoreDbContext _context;
        private readonly IMapper _mapper;

        public DoctorRepository(DoctorWhoCoreDbContext context
            ,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public DoctorDto CreateDoctor(DoctorDto doctorDto)
        {
            var doctorEntity = _mapper.Map<Doctor>(doctorDto);
            _context.Doctors.Add(doctorEntity);
            _context.SaveChanges();

            var createdDoctorDto = _mapper.Map<DoctorDto>(doctorEntity);
            return createdDoctorDto;
        }

        public void UpdateDoctor(Doctor doctor, DoctorDto doctorDto)
        {
            _mapper.Map(doctorDto, doctor);
        }
        public void DeleteDoctor(int doctorId)
        {
            var doctor = GetDoctorById(doctorId);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);

            }
            _context.SaveChanges();
        }
        public Doctor? GetDoctorById(int? doctorId)
        {
            if (doctorId.HasValue)
            {
                var doctor = _context.Doctors
                    .Include(d => d.Episodes)
                    .FirstOrDefault(d => d.DoctorId == doctorId);
                return doctor;
            }

            return null;
        }

        public IEnumerable<DoctorDto> GetAllDoctors()
        {
            var doctors = _context.Doctors
                .Include(d => d.Episodes)
                .ToList();
            var doctorDtos = _mapper.Map<IEnumerable<DoctorDto>>(doctors);
            return doctorDtos;           
        }

        public Doctor? GetDoctorById(int doctorId)
        {
            return _context.Doctors.FirstOrDefault(d => d.DoctorId == doctorId);
        }
        public bool DoctorExist(int id)
        {
            return _context.Doctors.Any(d => d.DoctorId == id);
        }
    }
}
