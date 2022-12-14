using Cashin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashin.Controllers
{
    public static class BalanceController
    {
        public static void ActualBalance(string tipo, float valor)
        {
            if (tipo == "Gasto")
            {
                User.Saldo -= valor;
            }

            else if (tipo == "Recebimento")
            {
                User.Saldo += valor;
            }
        }
    }
}
