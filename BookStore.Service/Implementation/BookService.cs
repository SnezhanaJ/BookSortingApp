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
    public class BooksService : IBookService
    {
        private readonly IRepository<Book> _repository;
        public BooksService(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public void CreateNewBook(Book p)
        {
            _repository.Insert(p);
        }

        public void DeleteBook(Guid id)
        {
            var book = _repository.Get(id);
            _repository.Delete(book);
        }

        public List<Book> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public Book GetDetails(Guid? id)
        {
            return _repository.Get(id);
        }

        public void UpdateExistingBook(Book p)
        {
            _repository.Update(p);
        }
    }

}