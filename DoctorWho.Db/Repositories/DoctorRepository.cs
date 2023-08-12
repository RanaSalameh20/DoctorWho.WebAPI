using DoctorWho.Db.DatabaseContext;
using DoctorWho.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorWho.Db.Repositories
{
    public class DoctorRepository
    {
        private readonly DoctorWhoCoreDbContext _context;

        public DoctorRepository(DoctorWhoCoreDbContext context)
        {
            _context = context;
        }
        public void CreateDoctor(int doctorNumber, string doctorName,
                DateTime birthDate, DateTime firstEpisodeDate, DateTime lastEpisodeDate)
        {
            var doctor = new Doctor
            {
                DoctorNumber = doctorNumber,
                DoctorName = doctorName,
                BirthDate = birthDate,
                FirstEpisodeDate = firstEpisodeDate,
                LastEpisodeDate = lastEpisodeDate,


            };
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }

        public void UpdateDoctor(int doctorId, string newDoctorName, DateTime newBirthDate, int episodeId)
        {
            var doctor = _context.Doctors.FirstOrDefault(d => d.DoctorId == doctorId);
            var episode = _context.Episodes.FirstOrDefault(e => e.EpisodeId == episodeId);

            if (doctor != null)
            {
                doctor.DoctorName = newDoctorName;
                doctor.BirthDate = newBirthDate;
            }
            else
            {
                Console.WriteLine($"No Doctor with id = {doctorId}");
                return;
            }
            if (episode != null)
            {
                doctor.Episodes.Add(episode);
            }

            _context.SaveChanges();
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

        public IEnumerable<Doctor> GetAllDoctors()
        {
            var doctors = _context.Doctors
                .Include(d => d.Episodes)
                .ToList();
            return doctors;           
        }

        public bool DoctorExist(int id)
        {
            return _context.Doctors.Any(d => d.DoctorId == id);
        }
    }
}
