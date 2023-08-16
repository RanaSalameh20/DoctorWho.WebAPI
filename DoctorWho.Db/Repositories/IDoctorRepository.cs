using DoctorWho.Db.Entities;
using DoctorWho.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWho.Db.Repositories
{
    public interface IDoctorRepository
    {
        Task<DoctorDto> CreateDoctor(DoctorDto doctorDto);
        Task<DoctorDto> UpdateDoctor(Doctor doctor, DoctorDto doctorDto);
        Task DeleteDoctor(int doctorId);
        Task<IEnumerable<DoctorDto>> GetAllDoctors();
        Task<Doctor?> GetDoctorById(int? doctorId);
        Task<bool> IsDoctorExistAsync(int id);
    }
}
