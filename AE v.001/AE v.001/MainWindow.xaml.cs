using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data.OleDb;
using System.Data.Odbc;

using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AE_v._001
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Product> shoppingCart;
        private string connectionString = "Data Source=DESKTOP-AA5HUPA;Initial Catalog=cource;User ID=wannacry; Password = ";

        public MainWindow()
        {
            InitializeComponent();
            shoppingCart = new List<Product>();
        }
        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string productContent = button.Content.ToString();

            // Добавление продукта в корзину
            int productId = GetProductIdFromButtonContent(productContent);
            string productName = GetProductNameFromButtonContent(productContent);
            decimal productPrice = GetProductPriceFromButtonContent(productContent);

            Product product = new Product(productId, productName, productPrice);
            shoppingCart.Add(product);

            MessageBox.Show("Product added to cart.", "Success");
        }

        private int GetProductIdFromButtonContent(string content)
        {
            char letter = content[0];
            int number = int.Parse(content[1].ToString());
            int id = (letter - 'A') * 10 + number;
            return id;
        }

        private string GetProductNameFromButtonContent(string content)
        {
            return "Product " + content;
        }

        private decimal GetProductPriceFromButtonContent(string content)
        {
            return 5.0m;
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            // Обработка покупки (например, сохранение заказа в базе данных)
            SaveCartToDatabase();
        }

        private void SaveCartToDatabase()
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                foreach (Product product in shoppingCart)
                {
                    // Здесь предполагается, что у вас есть таблица "Products" с колонками "Id", "Name" и "Price"
                    string query = $"INSERT INTO Products (Id, Name, Price) VALUES ({product.Id}, '{product.Name}', {product.Price})";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Cart items saved to database.", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving cart items to database: {ex.Message}", "Error");
            }
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }

}
