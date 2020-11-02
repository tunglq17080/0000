using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPTApp.Models
{
    public class Trainer
    {
        public static object Name { get; internal set; }
        public int Id { get; set; }
        public string FullName { get; set; }
        public string WorkingPlace { get; set; }
        public string PhoneNumber { get; set; }
        public Topic topId { get; set; }
        public Topic Topic { get; set; }
    }
}