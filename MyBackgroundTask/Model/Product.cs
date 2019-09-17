using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBackgroundTask.Model
{
    public class Product
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string No { get; set; }
        public string Context { get; set; }
        public string Status { get; set; }
        public DateTime GetTime { get; set; }
        public DateTime AddTime { get; set; }
    }
}
