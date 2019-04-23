using System;
using System.Collections.Generic;
using System.Text;
using Repository.Interfaces;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IPostsRepository Posts { get; }

        public UnitOfWork()
        {
            Posts = new PostsRepository();
        }
    }
}
