using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegisterApp
{
    // Интерфейс для кассового аппарата
    public interface ICashRegister
    {
        void PrintReceipt(List<Product> products);
    }
}
