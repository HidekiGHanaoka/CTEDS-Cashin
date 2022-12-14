using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashin.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty ;
        public string Email { get; set; } = string.Empty ;
        public float Saldo { get; set; } = 0;
        public float Limite { get; set; } = 0;
    }
}
