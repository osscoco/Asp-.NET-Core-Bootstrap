using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace TP_Corentin_Maxence.Models
{
    public class ProductStore
    {
        public string ConnectionString { get; set; }

        public ProductStore(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }


        public List<Product> GetAllProducts()
        {
            List<Product> list = new List<Product>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from product where id > 0", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Product()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            name = reader["name"].ToString(),
                            description = reader["description"].ToString(),
                            prix = Convert.ToInt32(reader["prix"])
                        });
                    }
                }
            }
            return list;
        }

        public List<Product> SearchProduct(string ProductSearchId)
        {
            List<Product> products = new List<Product>();

            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd1 = new MySqlCommand("select * from product where id like '" + ProductSearchId + "%'", conn);
                conn.Open();
                using (var reader1 = cmd1.ExecuteReader())
                {
                    while (reader1.Read())
                    {
                        products.Add(new Product()
                        {
                            id = Convert.ToInt32(reader1["id"]),
                            name = reader1["name"].ToString(),
                            description = reader1["description"].ToString(),
                            prix = Convert.ToInt32(reader1["prix"])
                        });
                    }
                }
            }
            return products;
        }

        public List<Product> SearchProduct2(string ProductSearchName)
        {
            List<Product> products = new List<Product>();

            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd1 = new MySqlCommand("select * from product where name like '" + ProductSearchName + "%'", conn);
                conn.Open();
                using (var reader1 = cmd1.ExecuteReader())
                {
                    while (reader1.Read())
                    {
                        products.Add(new Product()
                        {
                            id = Convert.ToInt32(reader1["id"]),
                            name = reader1["name"].ToString(),
                            description = reader1["description"].ToString(),
                            prix = Convert.ToInt32(reader1["prix"])
                        });
                    }
                }
            }
            return products;
        }

        public List<Product> SearchProduct3(string ProductSearchPrix)
        {
            List<Product> products = new List<Product>();

            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd1 = new MySqlCommand("select * from product where prix like '" + ProductSearchPrix + "%'", conn);
                conn.Open();
                using (var reader1 = cmd1.ExecuteReader())
                {
                    while (reader1.Read())
                    {
                        products.Add(new Product()
                        {
                            id = Convert.ToInt32(reader1["id"]),
                            name = reader1["name"].ToString(),
                            description = reader1["description"].ToString(),
                            prix = Convert.ToInt32(reader1["prix"])
                        });
                    }
                }
            }
            return products;
        }

    }

}
