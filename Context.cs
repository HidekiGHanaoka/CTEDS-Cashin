using Cashin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashin
{
    public class Context
    {
        public User? ActualUser;
        List<Item> ListaItens = new List<Item>();
    }
}
