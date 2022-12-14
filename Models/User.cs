using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashin.Models
{
    static class User
    {
        public static Guid Id { get; set; }
        public static string Nome { get; set; } = string.Empty ;
        public static string Email { get; set; } = string.Empty ;
        public static float Saldo { get; set; } = 0;
        public static float Limite { get; set; } = 0;
        public static List<Item> ListaItens = new List<Item>();
    }
}
