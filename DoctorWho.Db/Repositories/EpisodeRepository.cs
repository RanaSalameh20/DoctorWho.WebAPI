using AutoMapper;
using Dapper;
using DoctorWho.Db.DatabaseContext;
using DoctorWho.Db.Entities;
using DoctorWho.Db.ViewsModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DoctorWho.Web.Models;
using System;
using DoctorWho.Common.Models;

namespace DoctorWho.Db.Repositories
{
    public class EpisodeRepository
    {
        private readonly DoctorWhoCoreDbContext _context;
        private readonly AuthorRepository _authorRepository;
        private readonly DoctorRepository _doctorRepository;
        private readonly string _connectionString;
        private IMapper _mapper;

        public EpisodeRepository(DoctorWhoCoreDbContext context
            ,AuthorRepository authorRepository
            ,DoctorRepository doctorRepository
            ,IConfiguration configuration
            ,IMapper mapper)
        {
            _context = context;
            _authorRepository = authorRepository;
            _doctorRepository = doctorRepository;
            _connectionString = configuration.GetConnectionString("DBConnectionString");
            _mapper = mapper;   
        }
        public EpisodeDto CreateEpisode(EpisodeDto episodeDto)
        {
            var episodeEntity = _mapper.Map<Episode>(episodeDto);
            _context.Episodes.Add(episodeEntity);
            _context.SaveChanges();

            var createdEpisodeDto = _mapper.Map<EpisodeDto>(episodeEntity);
            return createdEpisodeDto;

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
        public EpisodeAndEnemyDto AddExistingEnemyToExistingEpisode(Episode episode, Enemy enemy)
        {
            episode.Enemies.Add(enemy);

            _context.SaveChanges();

            var enemyDto = _mapper.Map<EnemyDto>(enemy);
            var episodeDto = _mapper.Map<EpisodeDto>(episode);

            var episodeAndEnemyDto = new EpisodeAndEnemyDto
            {
                Episode = episodeDto,
                Enemy = enemyDto

            };
            return episodeAndEnemyDto;
        }
        public EpisodeAndCompanionDto AddNewCompanionToExistingEpisode(Episode episode, CompanionDto companionDto)
        {
            var companionEntity = _mapper.Map<Companion>(companionDto);

            episode.Companions.Add(companionEntity);

            _context.SaveChanges();

            var createdCompanionDto = _mapper.Map<CompanionDto>(companionEntity);
            var episodeDto = _mapper.Map<EpisodeDto>(episode);

            var episodeAndCompanionDto = new EpisodeAndCompanionDto
            {
                Episode = episodeDto,
                Companion = createdCompanionDto

            };
            return episodeAndCompanionDto;
        }
        public Episode? GetEpisodeById(int episodeId)
        {
            return _context.Episodes.Include(e => e.Companions).FirstOrDefault(e => e.EpisodeId == episodeId);
        }
        public IEnumerable<EpisodeDto> GetAllEpisodesFromView()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var allEpisodes = connection.Query<EpisodeView>("SELECT * FROM ViewEpisodes");
                
                var allEpisodesDtos = _mapper.Map<IEnumerable<EpisodeDto>>(allEpisodes);

                return allEpisodesDtos;
            }
        }
    }
}
