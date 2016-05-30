using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankITI.Models
{
    public class PostComments
    {
        public Posts_GetPostByID_Result post { get; set; }

        public List<Comments_SelectAllByPostID_Result> comments { get; set; }

    }
}