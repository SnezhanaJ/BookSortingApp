﻿using BookStore.Domain.Domain.Models;
using BookStore.Repository.Interface;
using BookStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Implementation
{
    public class PublisherService : IPublisherService
    {
        private readonly IRepository<Publisher> _repository;

        public PublisherService(IRepository<Publisher> repository)
        {
            _repository = repository;
        }
        public void CreateNewPublisher(Publisher p)
        {
            _repository.Insert(p);
        }

        public void DeletePublisher(Guid id)
        {
            var publisher = _repository.Get(id);
            _repository.Delete(publisher);
        }

        public List<Publisher> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public Publisher GetDetails(Guid? id)
        {
            return _repository.Get(id); 
        }

        public void UpdateExistingPublisher(Publisher p)
        {
            _repository.Update(p);
        }
    }
}