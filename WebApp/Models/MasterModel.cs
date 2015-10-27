using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class MasterModel
    {
        public string Title { get; set; }
        public string ActiveModel { get; set; }

        public MasterModel()
        {
            Title = "Home";
            ActiveModel = "Home";
        }
    }
}