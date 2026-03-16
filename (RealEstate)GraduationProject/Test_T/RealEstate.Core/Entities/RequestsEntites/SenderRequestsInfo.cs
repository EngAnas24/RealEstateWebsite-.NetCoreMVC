using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Entities.RequestsEntites
{
    public class SenderRequestsInfo
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? Email { get; set; }
        public string? Phonenumber { get; set; }

    }
}
