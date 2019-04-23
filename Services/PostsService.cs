using System;
using System.Collections.Generic;
using System.Text;
using Models;
using Repository.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class PostsService : IPostsService
    {
        private IPostsRepository _repo { get; }
        public PostsService(IUnitOfWork unitOfWork)
        {
            _repo = unitOfWork.Posts;
        }
        public Post Get(int Id)
        {
           return _repo.Get(Id);
        }
    }
}
