using BookStore.Domain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Interface
{
    public interface IBookService
    {

        List<Book> GetAll();
        Book GetDetails(Guid? id);
        void CreateNewBook(Book p);
        void UpdateExistingBook(Book p);
        void DeleteBook(Guid id);
    }
}
