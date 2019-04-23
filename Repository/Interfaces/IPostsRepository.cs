using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Repository.Interfaces
{
    public interface IPostsRepository
    {
        Post Get(int Id);
    }
}
