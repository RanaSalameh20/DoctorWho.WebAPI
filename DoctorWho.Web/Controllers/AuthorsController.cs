using DoctorWho.Db.Repositories;
using DoctorWho.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.Web.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly AuthorRepository _authorRepository;

        public AuthorsController(AuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpPut("{authorId}")]
        public async Task<IActionResult> UpdateAuthor(int authorId, [FromBody] AuthorDto authorDto)
        {
            var author = await _authorRepository.GetAuthorById(authorId);

            if (author == null)
            {
                return NotFound("Author not found");
            }

            await _authorRepository.UpdateAuthor(author, authorDto);
            return NoContent();
        }

    }
}
