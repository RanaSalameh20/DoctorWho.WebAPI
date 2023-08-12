using Dapper;
using DoctorWho.Db.DatabaseContext;
using DoctorWho.Db.Entities;
using DoctorWho.Db.ViewsModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DoctorWho.Db.Repositories
{
    public class EpisodeRepository
    {
        private readonly DoctorWhoCoreDbContext _context;
        private readonly AuthorRepository _authorRepository;
        private readonly DoctorRepository _doctorRepository;
        private readonly string _connectionString;

        public EpisodeRepository(DoctorWhoCoreDbContext context
            ,AuthorRepository authorRepository
            ,DoctorRepository doctorRepository
            ,IConfiguration configuration
            )
        {
            _context = context;
            _authorRepository = authorRepository;
            _doctorRepository = doctorRepository;
            _connectionString = configuration.GetConnectionString("DBConnectionString");
        }
        public void CreateEpisode(int seriesNumber, int episodeNumber, string episodeType, string title, DateTime episodeDate, int authorId, int? doctorId)
        {
            var author = _authorRepository.GetAuthorById(authorId);
            if (author != null)
            {
                var episode = new Episode
                {
                    SeriesNumber = seriesNumber,
                    EpisodeNumber = episodeNumber,
                    EpisodeType = episodeType,
                    Title = title,
                    EpisodeDate = episodeDate,
                    Author = author
                };

                var doctor = _doctorRepository.GetDoctorById(doctorId);

                if (doctor != null)
                {
                    episode.Doctor = doctor;
                }

                _context.Episodes.Add(episode);
                _context.SaveChanges();
            }

        }
        public void UpdateEpisode(int episodeId, string newEpisodeTitle, DateTime newEpisodeDate, int newAuthorId)
        {
            var episode = _context.Episodes.FirstOrDefault(e => e.EpisodeId == episodeId);
            var author = _context.Authors.FirstOrDefault(a => a.AuthorId == newAuthorId);

            if (episode != null)
            {
                episode.Title = newEpisodeTitle;
                episode.EpisodeDate = newEpisodeDate;
            }
            else
            {
                Console.WriteLine($"No episode with id = {episodeId}");
                return;
            }
            if (author != null)
            {
                episode.Author = author;
            }


            _context.SaveChanges();
            Console.WriteLine("done");
        }
        public void DeleteEpisode(int episodeId)
        {
            var episode = _context.Episodes.FirstOrDefault(e => e.EpisodeId == episodeId);

            if (episode != null)
            {
                _context.Episodes.Remove(episode);
            }

            _context.SaveChanges();
        }
        public void AddEnemyToEpisode()
        {
            var enemy1 = _context.Enemies.Find(1);
            var enemy2 = _context.Enemies.Find(2);
            var episode = _context.Episodes.Find(2);

            episode.Enemies.Add(enemy1);
            episode.Enemies.Add(enemy2);

            _context.SaveChanges();
        }
        public void addCompanionToEpisode()
        {
            var companion1 = _context.Companions.Find(4);
            var companion2 = _context.Companions.Find(5);
            var companion3 = _context.Companions.Find(6);
            var episode1 = _context.Episodes.Find(4);
            var episode2 = _context.Episodes.Find(5);

            episode1.Companions.Add(companion1);
            episode1.Companions.Add(companion2);
            episode1.Companions.Add(companion3);
            episode2.Companions.Add(companion1);

            _context.SaveChanges();
        }
        public Episode? GetEpisodeById(int episodeId)
        {
            return _context.Episodes.Include(e => e.Companions).FirstOrDefault(e => e.EpisodeId == episodeId);
        }
        public IEnumerable<EpisodeView> EpisodesView()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var episodes = connection.Query<EpisodeView>("SELECT * FROM ViewEpisodes");
                
                return episodes;
            }
        }
    }
}
