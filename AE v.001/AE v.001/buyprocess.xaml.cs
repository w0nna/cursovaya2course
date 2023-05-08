using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AE_v._001
{
    /// <summary>
    /// Логика взаимодействия для buyprocess.xaml
    /// </summary>
    public partial class buyprocess : Window
    {
        public buyprocess()
        {
            InitializeComponent();
        }
        public abstract class OrderState
        {
            public abstract void AddProduct(Order order, Product product);
            public abstract void RemoveProduct(Order order, Product product);
            public abstract void CompleteOrder(Order order);
        }
        public class NewOrderState : OrderState
        {
            public override void AddProduct(Order order, Product product)
            {
                order.Products.Add(product);
            }

            public override void RemoveProduct(Order order, Product product)
            {
                order.Products.Remove(product);
            }

            public override void CompleteOrder(Order order)
            {
                throw new InvalidOperationException("Cannot complete new order");
            }
        }

        public class ProcessingOrderState : OrderState
        {
            public override void AddProduct(Order order, Product product)
            {
                throw new InvalidOperationException("Cannot add product to processing order");
            }

            public override void RemoveProduct(Order order, Product product)
            {
                throw new InvalidOperationException("Cannot remove product from processing order");
            }

            public override void CompleteOrder(Order order)
            {
                order.State = new CompletedOrderState();
            }
        }

        public class CompletedOrderState : OrderState
        {
            public override void AddProduct(Order order, Product product)
            {
                throw new InvalidOperationException("Cannot add product to completed order");
            }

            public override void RemoveProduct(Order order, Product product)
            {
                throw new InvalidOperationException("Cannot remove product from completed order");
            }

            public override void CompleteOrder(Order order)
            {
                throw new InvalidOperationException("Order is already completed");
            }
        }
        public class Order
        {
            public int Id { get; set; }
            public List<Product> Products { get; set; }
            public OrderState State { get; set; }

            public Order()
            {
                State = new NewOrderState();
                Products = new List<Product>();
            }

            public void AddProduct(Product product)
            {
                State.AddProduct(this, product);
            }

            public void RemoveProduct(Product product)
            {
                State.RemoveProduct(this, product);
            }

            public void CompleteOrder()
            {
                State.CompleteOrder(this);
            }
        }

        // нов заказ
        Order order = new Order();
        // товар в заказ
        Product product1 = new Product("Телефон", 10000);
        order.AddProduct(product1);
        Product product2 = new Product("Ноутбук", 30000);
        order.AddProduct(product2);

        // изменить заказ котр уже в обработке
        try
        {
            order.AddProduct(new Product("Планшет", 15000));
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }

        // удал
        order.RemoveProduct(product2);

        // завер
        order.CompleteOrder();

    }
}
