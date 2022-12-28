using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tamirci.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string LogLevel { get; set; }
        public string InsertDate { get; set; }
        public string StackTrace { get; set; }
        public string MachineName { get; set; }

    }
}