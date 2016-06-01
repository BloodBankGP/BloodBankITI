using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankITI.Models
{
    public class PostsComments
    {
        public posts_SelectAll_Result post { get; set; }

        public List<Comments_SelectAllByPostID_Result> comments { get; set; }

    }
}