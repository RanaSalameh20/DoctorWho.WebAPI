using AutoMapper;
using DoctorWho.Db.Repositories;
using DoctorWho.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.Web.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly AuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorsController(AuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpPut("{authorId}")]
        public IActionResult UpdateAuthor(int authorId, [FromBody] AuthorDto authorDto)
        {
            var author = _authorRepository.GetAuthorById(authorId);

            if (author == null)
            {
                return NotFound("Author not found");
            }

            _mapper.Map(authorDto, author);
            _authorRepository.SaveChanges(author);

            return NoContent();
        }
    
}
}
