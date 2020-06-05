using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace TP_Corentin_Maxence.Models
{
    public class CategoryStore
    {
        public string ConnectionString { get; set; }

        public CategoryStore(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }


        public List<Category> GetAllCategorys()
        {
            List<Category> list = new List<Category>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from category where id > 0", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Category()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            name = reader["name"].ToString(),
                            description = reader["description"].ToString(),
                        });
                    }
                }
            }
            return list;
        }



        public List<Category> SearchCategory(string CategorySearchId)
        {
            List<Category> categorys = new List<Category>();

            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd1 = new MySqlCommand("select * from category where id like '" + CategorySearchId + "%'", conn);
                conn.Open();
                using (var reader1 = cmd1.ExecuteReader())
                {
                    while (reader1.Read())
                    {
                        categorys.Add(new Category()
                        {
                            id = Convert.ToInt32(reader1["id"]),
                            name = reader1["name"].ToString(),
                            description = reader1["description"].ToString(),
                        });
                    }
                }
            }
            return categorys;
        }

        public List<Category> SearchCategory2(string CategorySearchName)
        {
            List<Category> categorys = new List<Category>();

            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd1 = new MySqlCommand("select * from category where name like '" + CategorySearchName + "%'", conn);
                conn.Open();
                using (var reader1 = cmd1.ExecuteReader())
                {
                    while (reader1.Read())
                    {
                        categorys.Add(new Category()
                        {
                            id = Convert.ToInt32(reader1["id"]),
                            name = reader1["name"].ToString(),
                            description = reader1["description"].ToString(),
                        });
                    }
                }
            }
            return categorys;
        }
    }
}
