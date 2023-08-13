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
        private IMapper _mapper;

        public AuthorRepository(DoctorWhoCoreDbContext context
            ,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        void CreateAuthor(string authorName)
        {
            var author = new Author
            {
                AuthorName = authorName
            };

            _context.Authors.Add(author);
            _context.SaveChanges();

        }
        public Author GetAuthorById(int authorId)
        {
            var author = _context.Authors.FirstOrDefault(a => a.AuthorId == authorId);
            return author;
        }
        public void UpdateAuthor(Author author, AuthorDto authorDto)
        {
            _mapper.Map(authorDto, author);

            _context.SaveChanges();
            
        }
        void DeleteAuthor(int authorId)
        {
            var author = _context.Authors
                .Include(a => a.Episodes)
                .FirstOrDefault(a => a.AuthorId == authorId);

            if (author != null)
            {
                _context.Authors.Remove(author);
            }

            _context.SaveChanges();
        }
    }
}
