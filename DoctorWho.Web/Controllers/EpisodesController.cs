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
        private readonly DoctorRepository _doctorRepository;
        private readonly EnemyRepository _enemyRepository;

        public EpisodesController(DoctorWhoCoreDbContext context
            , EpisodeRepository episodeRepository
            , AuthorRepository authorRepository
            , DoctorRepository doctorRepository
            , EnemyRepository enemyRepository)
        {
            _episodeRepository = episodeRepository;
            _enemyRepository = enemyRepository;
            _authorRepository = authorRepository;
            _doctorRepository = doctorRepository;
        }


        [HttpGet]
        public ActionResult<IEnumerable<DoctorDto>> GetAllEpisodes()
        {

            var episodesDtos = _episodeRepository.GetAllEpisodesFromView();

            return Ok(episodesDtos);
        }

        [HttpPost("CreateEpisode")]
        public ActionResult<EpisodeDto> CreateEpisode([FromBody] EpisodeDto episodeDto)
        {
            var author = _authorRepository.GetAuthorById(episodeDto.AuthorId);  
            if (author == null)
            {
                return BadRequest("The specified author does not exist. Create the author before creating the episode.");
            }

            if (episodeDto.DoctorId.HasValue)
            {
                var doctor = _doctorRepository.GetDoctorById(episodeDto.DoctorId.Value);
                if (doctor == null)
                {
                    return BadRequest("The specified doctor does not exist. Create the doctor before creating the episode.");
                }
            }
            var createdEpisodeDto = _episodeRepository.CreateEpisode(episodeDto);

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

            var episodeAndEnemyDto = _episodeRepository.AddExistingEnemyToExistingEpisode(episode, enemy);
           
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

            var episodeAndCompanionDto = _episodeRepository.AddNewCompanionToExistingEpisode(episode, companionDto);

            return Ok(episodeAndCompanionDto);
        }
    }
}
