using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IPostsRepository Posts { get; }
    }
}
