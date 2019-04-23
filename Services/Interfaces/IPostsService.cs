using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Services.Interfaces
{
    public interface IPostsService
    {
        Post Get(int Id);
    }
}
