using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegisterApp
{
    public class ReceiptPrinter
    {
        private readonly List<Product> _products;

        public ReceiptPrinter(List<Product> products)
        {
            _products = products;
        }

        public void PrintReceipt()
        {
            decimal totalAmount = 0;
            Console.WriteLine("Чек:");
            foreach (var product in _products)
            {
                decimal productTotal = product.Quantity * product.Price;
                Console.WriteLine($"{product.Name} * {product.Quantity} = {productTotal:C}");
                totalAmount += productTotal;
            }
            Console.WriteLine("----------------------------");
            Console.WriteLine($"Итоговая сумма: {totalAmount:C}");
        }
    }
}
