using BookStore.Domain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Interface
{
    public interface IGenreService
    {
        List<Genre> GetAll();
        Genre GetDetails(Guid? id);
        void CreateNewGenre(Genre p);
        void UpdateExistingGenre(Genre p);
        void DeleteGenre(Guid id);
    }
}
