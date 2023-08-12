using AutoMapper;
using DoctorWho.Db.DatabaseContext;
using DoctorWho.Db.Entities;
using DoctorWho.Db.Repositories;
using DoctorWho.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorWho.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EpisodesController : Controller
    {
        private readonly DoctorWhoCoreDbContext _context;
        private readonly EpisodeRepository _episodeRepository;
        private readonly EnemyRepository _enemyRepository;
        private readonly IMapper _mapper;


        public EpisodesController(DoctorWhoCoreDbContext context
            , EpisodeRepository episodeRepository
            , IMapper mapper
            , EnemyRepository enemyRepository)
        {
            _context = context;
            _episodeRepository = episodeRepository;
            _mapper = mapper;
            _enemyRepository = enemyRepository;
        }


        [HttpGet]
        public ActionResult<IEnumerable<DoctorDto>> GetAllEpisodes()
        {
            var episodesView = _episodeRepository.EpisodesView();

            var episodesDtos = _mapper.Map<IEnumerable<EpisodeDto>>(episodesView);

            return Ok(episodesDtos);
        }

        [HttpPost("CreateEpisode")]
        public ActionResult<EpisodeDto> CreateEpisode([FromBody] EpisodeDto episodeDto)
        {
            var author = _context.Authors.FirstOrDefault(a => a.AuthorId == episodeDto.AuthorId);
            if (author == null)
            {
                return BadRequest("The specified author does not exist. Create the author before creating the episode.");
            }

            if (episodeDto.DoctorId.HasValue)
            {
                var doctor = _context.Doctors.FirstOrDefault(d => d.DoctorId == episodeDto.DoctorId.Value);
                if (doctor == null)
                {
                    return BadRequest("The specified doctor does not exist. Create the doctor before creating the episode.");
                }
            }

            var episodeEntity = _mapper.Map<Episode>(episodeDto);
            _context.Episodes.Add(episodeEntity);
            _context.SaveChanges();

            var createdEpisodeDto = _mapper.Map<EpisodeDto>(episodeEntity);
            return Ok(createdEpisodeDto);
        }

        [HttpPost("AddEnemyToEpisode/{episodeId}/{enemyId}")]
        public ActionResult<EnemyDto> AddEnemyToEpisode(int episodeId, int enemyId)
        {
            var episode = _episodeRepository.GetEpisodeById(episodeId);
            var enemy = _enemyRepository.GetEnemyById(enemyId);

            if (episode == null)
            {
                return BadRequest("The specified episode does not exist");
            }
            if (enemy == null)
            {
                return BadRequest("The specified enemy does not exist");
            }

            episode.Enemies.Add(enemy);

            _context.SaveChanges();

            var enemyDto = _mapper.Map<EnemyDto>(enemy);
            var episodeDto = _mapper.Map<EpisodeDto>(episode);

            var episodeAndEnemyDto = new EpisodeAndEnemyDto
            {
                Episode = episodeDto,
                Enemy = enemyDto

            };

            return Ok(episodeAndEnemyDto);
        }

        [HttpPost("AddCompanionToEpisode/{episodeId}")]
        public ActionResult<CompanionDto> AddCompanionToEpisode(int episodeId, [FromBody] CompanionDto companionDto)
        {
            var episode = _episodeRepository.GetEpisodeById(episodeId);

            if (episode == null)
            {
                return BadRequest("The specified episode does not exist");
            }

            var companionEntity = _mapper.Map<Companion>(companionDto);

            episode.Companions.Add(companionEntity);

            _context.SaveChanges();

            var createdCompanionDto = _mapper.Map<CompanionDto>(companionEntity);

            return Ok(createdCompanionDto);
        }
    }
}
