using DoctorWho.Db.DatabaseContext;
using DoctorWho.Db.Repositories;
using DoctorWho.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EpisodesController : Controller
    {
        private readonly EpisodeRepository _episodeRepository;
        private readonly AuthorRepository _authorRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly EnemyRepository _enemyRepository;

        public EpisodesController(DoctorWhoCoreDbContext context
            , EpisodeRepository episodeRepository
            , AuthorRepository authorRepository
            , IDoctorRepository doctorRepository
            , EnemyRepository enemyRepository)
        {
            _episodeRepository = episodeRepository;
            _enemyRepository = enemyRepository;
            _authorRepository = authorRepository;
            _doctorRepository = doctorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetAllEpisodes()
        {

            var episodesDtos = await _episodeRepository.GetAllEpisodesFromView();

            return Ok(episodesDtos);
        }

        [HttpPost("CreateEpisode")]
        public async Task<ActionResult<EpisodeDto>> CreateEpisode([FromBody] EpisodeDto episodeDto)
        {
            var author = await _authorRepository.GetAuthorById(episodeDto.AuthorId);  
            if (author == null)
            {
                return BadRequest("The specified author does not exist. Create the author before creating the episode.");
            }

            if (episodeDto.DoctorId.HasValue)
            {
                var doctor = await _doctorRepository.GetDoctorById(episodeDto.DoctorId.Value);
                if (doctor == null)
                {
                    return BadRequest("The specified doctor does not exist. Create the doctor before creating the episode.");
                }
            }
            var createdEpisodeDto = await _episodeRepository.CreateEpisode(episodeDto);

            return Ok(createdEpisodeDto);
        }

        [HttpPost("Episode/{episodeId}/Enemy/{enemyId}")]
        public async Task<ActionResult<EnemyDto>> AddEnemyToEpisode(int episodeId, int enemyId)
        {
            var episode = await _episodeRepository.GetEpisodeById(episodeId);
            var enemy = await _enemyRepository.GetEnemyById(enemyId);

            if (episode == null)
            {
                return BadRequest("The specified episode does not exist");
            }
            if (enemy == null)
            {
                return BadRequest("The specified enemy does not exist");
            }

            var episodeAndEnemyDto = await _episodeRepository.AddEnemyToEpisode(episode, enemy);
           
            return Ok(episodeAndEnemyDto);
        }

        [HttpPost("Episode/Companion")]
        public async Task<ActionResult<CompanionDto>> AddCompanionToEpisode([FromQuery] int episodeId, [FromBody] CompanionDto companionDto)
        {
            var episode = await _episodeRepository.GetEpisodeById(episodeId);

            if (episode == null)
            {
                return BadRequest("The specified episode does not exist");
            }

            var episodeAndCompanionDto = await _episodeRepository.AddCompanionToEpisode(episode, companionDto);

            return Ok(episodeAndCompanionDto);
        }
    }
}
