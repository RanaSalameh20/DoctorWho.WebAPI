using AutoMapper;
using Dapper;
using DoctorWho.Db.DatabaseContext;
using DoctorWho.Db.Entities;
using DoctorWho.Db.ViewsModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DoctorWho.Web.Models;
using DoctorWho.Common.Models;

namespace DoctorWho.Db.Repositories
{
    public class EpisodeRepository
    {
        private readonly DoctorWhoCoreDbContext _context;
        private readonly AuthorRepository _authorRepository;
        private readonly DoctorRepository _doctorRepository;
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public EpisodeRepository(DoctorWhoCoreDbContext context
            , AuthorRepository authorRepository
            , DoctorRepository doctorRepository
            , IConfiguration configuration
            , IMapper mapper)
        {
            _context = context;
            _authorRepository = authorRepository;
            _doctorRepository = doctorRepository;
            _connectionString = configuration.GetConnectionString("DBConnectionString");
            _mapper = mapper;
        }
        public async Task<EpisodeDto> CreateEpisode(EpisodeDto episodeDto)
        {
            var episodeEntity = _mapper.Map<Episode>(episodeDto);
            _context.Episodes.Add(episodeEntity);
            await _context.SaveChangesAsync();

            var createdEpisodeDto = _mapper.Map<EpisodeDto>(episodeEntity);
            return createdEpisodeDto;

        }
        public async Task UpdateEpisode(Episode episode, EpisodeDto episodeDto)
        {
            _mapper.Map(episodeDto, episode);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteEpisode(int episodeId)
        {
            var episode = await _context.Episodes.FirstOrDefaultAsync(e => e.EpisodeId == episodeId);

            if (episode != null)
            {
                _context.Episodes.Remove(episode);
            }

            await _context.SaveChangesAsync();
        }
        public async Task<EpisodeAndEnemyDto> AddEnemyToEpisode(Episode episode, Enemy enemy)
        {
            episode.Enemies.Add(enemy);

            await _context.SaveChangesAsync();

            var enemyDto = _mapper.Map<EnemyDto>(enemy);
            var episodeDto = _mapper.Map<EpisodeDto>(episode);

            var episodeAndEnemyDto = new EpisodeAndEnemyDto
            {
                Episode = episodeDto,
                Enemy = enemyDto

            };
            return episodeAndEnemyDto;
        }
        public async Task<EpisodeAndCompanionDto> AddCompanionToEpisode(Episode episode, CompanionDto companionDto)
        {
            var companionEntity = _mapper.Map<Companion>(companionDto);

            episode.Companions.Add(companionEntity);

            await _context.SaveChangesAsync();

            var createdCompanionDto = _mapper.Map<CompanionDto>(companionEntity);
            var episodeDto = _mapper.Map<EpisodeDto>(episode);

            var episodeAndCompanionDto = new EpisodeAndCompanionDto
            {
                Episode = episodeDto,
                Companion = createdCompanionDto

            };
            return episodeAndCompanionDto;
        }
        public async Task<Episode?> GetEpisodeById(int episodeId)
        {
            return await _context.Episodes.Include(e => e.Companions).FirstOrDefaultAsync(e => e.EpisodeId == episodeId);
        }
        public async Task<IEnumerable<EpisodeDto>> GetAllEpisodesFromView()
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var allEpisodes = await connection.QueryAsync<EpisodeView>("SELECT * FROM ViewEpisodes");

            var allEpisodesDtos = _mapper.Map<IEnumerable<EpisodeDto>>(allEpisodes);

            return allEpisodesDtos;
        }
    }
}
