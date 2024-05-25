using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceApp.Helpers
{
    public class QueryObject
    {
        public string? Username {get; set; } = null;
        
        [Phone]
        public string? PhoneNumber {get; set;} = null;

        [EmailAddress]
        public string? Email {get; set;} = null;

        public int PageNumber {get; set;} = 1;

        public int PageSize {get; set;} = 5;
    }
}