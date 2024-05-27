using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CashRegisterApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Установка кодировки консоли на UTF-8 для корректного вывода символов
            Console.OutputEncoding = Encoding.UTF8;
            // Пример JSON запроса
            string jsonRequest = @"
            [
                { 'Name': 'Хлеб', 'Quantity': 2, 'Price': 49.99 },
                { 'Name': 'Молоко', 'Quantity': 1, 'Price': 44.99 },
                { 'Name': 'Сливки', 'Quantity': 5, 'Price': 15.99 },
                { 'Name': 'Чипсы', 'Quantity': 1, 'Price': 169.99 },
                { 'Name': 'Twix', 'Quantity': 1, 'Price': 35.99 },
                { 'Name': 'Чудо молочный коктейль', 'Quantity': 1, 'Price': 49.99 },
                { 'Name': 'Яблоки', 'Quantity': 3, 'Price': 29.99 }
            ]";

            try
            {
                // Валидация и десериализация JSON запроса
                var products = ValidateAndDeserializeProducts(jsonRequest);

                // Создание экземпляра кассового аппарата и печать чека
                ICashRegister cashRegister = new MockCashRegister();
                cashRegister.PrintReceipt(products);
            }
            catch (JsonReaderException ex)
            {
                Console.WriteLine($"Ошибка чтения JSON: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка валидации данных: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Неизвестная ошибка: {ex.Message}");
            }
        }
        static List<Product> ValidateAndDeserializeProducts(string json)
        {
            var products = new List<Product>();

            // Проверка правильности структуры JSON
            var jsonArray = JArray.Parse(json);
            foreach (var item in jsonArray)
            {
                var product = item.ToObject<Product>();

                // Проверка наличия необходимых полей
                if (string.IsNullOrEmpty(product.Name))
                {
                    throw new ArgumentException("Название товара не может быть пустым.");
                }
                if (product.Quantity <= 0)
                {
                    throw new ArgumentException($"Количество товара '{product.Name}' должно быть больше нуля.");
                }
                if (product.Price <= 0)
                {
                    throw new ArgumentException($"Цена товара '{product.Name}' должна быть больше нуля.");
                }

                products.Add(product);
            }
            return products;
        }
    }
}