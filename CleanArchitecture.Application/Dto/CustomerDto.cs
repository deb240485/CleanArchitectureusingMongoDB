using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Dto
{
    public class CustomerDto
    {
        public string Id { get; set; } = string.Empty;

        //public Guid Id { get; set; }
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public Int64 phonenumber { get; set; }        
        public string? email { get; set; }
        public DateTime lastupdatedon { get; set; }
        public Int32 lastupdatedby { get; set; }
    }
}
