using Cashin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashin.Controllers
{
    public static class LimitController
    {
        public static void ActualLimit(string tipo, float valor)
        {
            if (tipo == "Gasto")
            {
                User.Limite -= valor;
            }

            else if (tipo == "Recebimento")
            {
                User.Saldo += valor;
            }
        }
    }
}
