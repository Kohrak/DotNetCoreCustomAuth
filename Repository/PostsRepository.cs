using System;
using System.Collections.Generic;
using System.Text;
using Models;
using Repository.Interfaces;

namespace Repository
{
    public class PostsRepository : IPostsRepository
    {
        //To avoid setting up a DB this will store a list of posts
        private List<Post> _postsList;

        public PostsRepository()
        {
            _postsList = PopulatePosts();
        }

        public Post Get(int Id)
        {
            return _postsList.Find(p => p.PostId == Id);
        }

        private List<Post> PopulatePosts()
        {
            return new List<Post>();
        }


    }
}
