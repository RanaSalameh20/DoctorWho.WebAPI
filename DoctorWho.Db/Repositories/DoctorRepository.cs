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
        public async Task<DoctorDto> CreateDoctor(DoctorDto doctorDto)
        {
            var doctorEntity = _mapper.Map<Doctor>(doctorDto);
            _context.Doctors.Add(doctorEntity);
            await _context.SaveChangesAsync();

            var createdDoctorDto = _mapper.Map<DoctorDto>(doctorEntity);
            return createdDoctorDto;
        }

        public async Task<DoctorDto> UpdateDoctor(Doctor doctor, DoctorDto doctorDto)
        {
            var updatedDoctor = _mapper.Map(doctorDto, doctor);
            await _context.SaveChangesAsync();

            var updatedDoctorDto = _mapper.Map<DoctorDto>(updatedDoctor);
            return updatedDoctorDto;
        }
        public async Task DeleteDoctor(int doctorId)
        {
            var doctor = await GetDoctorById(doctorId);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);

            }
            await _context.SaveChangesAsync();
        }
        public async Task<Doctor?> GetDoctorById(int? doctorId)
        {
            if (doctorId.HasValue)
            {
                var doctor = await _context.Doctors
                    .Include(d => d.Episodes)
                    .FirstOrDefaultAsync(d => d.DoctorId == doctorId);
                return doctor;
            }

            return null;
        }

        public async Task<IEnumerable<DoctorDto>> GetAllDoctors()
        {
            var doctors = await _context.Doctors
                .Include(d => d.Episodes)
                .ToListAsync();
            var doctorDtos = _mapper.Map<IEnumerable<DoctorDto>>(doctors);
            return doctorDtos;           
        }

        public async Task<bool> IsDoctorExistAsync(int id)
        {
            return _context.Doctors.Any(d => d.DoctorId == id);
        }
    }
}
