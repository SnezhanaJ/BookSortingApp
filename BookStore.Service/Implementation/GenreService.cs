using BookStore.Domain.Domain.Models;
using BookStore.Repository.Interface;
using BookStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Implementation
{
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> _repository;

        public GenreService(IRepository<Genre> repository)
        {
            _repository = repository;
        }
        public void CreateNewGenre(Genre p)
        {
            _repository.Insert(p);
        }

        public void DeleteGenre(Guid id)
        {
            var genre = _repository.Get(id);
            _repository.Delete(genre);
        }

        public List<Genre> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public Genre GetDetails(Guid? id)
        {
            return _repository.Get(id);
        }

        public void UpdateExistingGenre(Genre p)
        {
            _repository.Update(p);

        }
    }
}
