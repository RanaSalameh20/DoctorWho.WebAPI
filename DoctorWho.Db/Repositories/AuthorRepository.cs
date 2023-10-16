using AutoMapper;
using DoctorWho.Db.DatabaseContext;
using DoctorWho.Db.Entities;
using DoctorWho.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorWho.Db.Repositories
{
    public class AuthorRepository
    {
        private readonly DoctorWhoCoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorRepository(DoctorWhoCoreDbContext context
            ,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<AuthorDto> CreateAuthor(AuthorDto authorDto)
        {
            var author = _mapper.Map<Author>(authorDto);
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            var createdAuthorDto = _mapper.Map<AuthorDto>(author);
            return createdAuthorDto;
        }
        public async Task<Author?> GetAuthorById(int authorId)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.AuthorId == authorId);
            return author;
        }
        public async Task UpdateAuthor(Author author, AuthorDto authorDto)
        {
            _mapper.Map(authorDto, author);

            await _context.SaveChangesAsync();
            
        }
        public async Task DeleteAuthor(int authorId)
        {
            var author = await _context.Authors
                .Include(a => a.Episodes)
                .FirstOrDefaultAsync(a => a.AuthorId == authorId);

            if (author != null)
            {
                _context.Authors.Remove(author);
            }

            await _context.SaveChangesAsync();
        }
    }
}
