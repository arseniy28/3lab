using System;
using PriceList;

namespace PriceList
{
    interface IClonable
    {
        object Clone();
    }

    interface IComparable
    {
        int CompareTo(object obj);
    }

    interface IComparer
    {
        int Compare(object x, object y);
    }

    class PRICE : IClonable, IComparable
    {
        public int id;
        public string storeName;
        public double price;

        public PRICE(int id, string storeName, double price)
        {
            this.id = id;
            this.storeName = storeName;
            this.price = price;
        }

        public object Clone()
        {
            return new PRICE(this.id, this.storeName, this.price);
        }

        public int CompareTo(object obj)
        {
            PRICE other = obj as PRICE;
            if (other == null)
            {
                throw new ArgumentException("Товар не цена");
            }
            return this.id.CompareTo(other.id);
        }
    }

    class PriceComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            PRICE priceX = x as PRICE;
            PRICE priceY = y as PRICE;
            if (priceX == null || priceY == null)
            {
                throw new ArgumentException("Товар не цена");
            }
            return priceX.price.CompareTo(priceY.price);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PRICE[] prices = new PRICE[8];
            Console.WriteLine("Введите данные для 8 продукта:");
            for (int i = 0; i < prices.Length; i++)
            {
                Console.Write("Продукт ID: ");
                int id = int.Parse(Console.ReadLine());
                Console.Write("Магазин: ");
                string storeName = Console.ReadLine();
                Console.Write("Цена: ");
                double price = double.Parse(Console.ReadLine());
                prices[i] = new PRICE(id, storeName, price);
            }
            Array.Sort(prices);
            Console.WriteLine("Продукты отсортированы по ID:");
            foreach (PRICE price in prices)
            {
                Console.WriteLine("ID: {0}, Магазин: {1}, Цена: {2}", price.id, price.storeName, price.price);
            }
            Console.Write("Введите название товара для поиска: ");
            string productName = Console.ReadLine();
            bool found = false;
            foreach (PRICE price in prices)
            {
                if (price.storeName == productName)
                {
                    Console.WriteLine("Найденый продукт - ID: {0}, Магазин: {1}, Цена: {2}", price.id, price.storeName, price.price);
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                Console.WriteLine("Товар не найден");
            }
            Array.Sort(prices, new PriceComparer());
            Console.WriteLine("Товар отсортирован по цене:");
            foreach (PRICE price in prices)
            {
                Console.WriteLine("ID: {0}, Магазин: {1}, Цена: {2}", price.id, price.storeName, price.price);
            }
        }
    }
}