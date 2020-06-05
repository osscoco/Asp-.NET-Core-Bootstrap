using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TP_Corentin_Maxence.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Connections;
using System.Data.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace TP_Corentin_Maxence.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            ProductStore store = HttpContext.RequestServices.GetService(typeof(TP_Corentin_Maxence.Models.ProductStore)) as ProductStore;
            return View(store.GetAllProducts());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Product());
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {

            using (MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;pwd=root;database=joueur;persistsecurityinfo=True"))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("insert into product (name,description,prix) VALUES (@name,@description,@prix)", conn);
                cmd.Parameters.AddWithValue("@name", product.name);
                cmd.Parameters.AddWithValue("@description", product.description);
                cmd.Parameters.AddWithValue("@prix", product.prix);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");


        }

        public IActionResult Edit(int id)
        {
            Product product = new Product();
            DataTable dataTable = new DataTable();
            using (MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;pwd=root;database=joueur;persistsecurityinfo=True"))
            {
                conn.Open();
                MySqlDataAdapter cmd = new MySqlDataAdapter("select * from product where id = @id", conn);
                cmd.SelectCommand.Parameters.AddWithValue("@id", id);
                cmd.Fill(dataTable);
            }
            if (dataTable.Rows.Count == 1)
            {
                product.id = Convert.ToInt32(dataTable.Rows[0][0].ToString());
                product.name = dataTable.Rows[0][1].ToString();
                product.description = dataTable.Rows[0][2].ToString();
                product.prix = Convert.ToInt32(dataTable.Rows[0][3].ToString());
                return View(product);
            }
            else
                return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            using (MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;pwd=root;database=joueur;persistsecurityinfo=True"))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("update product set name = @name, description = @description, prix = @prix where id = @id", conn);
                cmd.Parameters.AddWithValue("@id", product.id);
                cmd.Parameters.AddWithValue("@name", product.name);
                cmd.Parameters.AddWithValue("@description", product.description);
                cmd.Parameters.AddWithValue("@prix", product.prix);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            using (MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;pwd=root;database=joueur;persistsecurityinfo=True"))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("delete from product where id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public IActionResult SearchId(string ProductSearchId)
        {

            ProductStore store = HttpContext.RequestServices.GetService(typeof(TP_Corentin_Maxence.Models.ProductStore)) as ProductStore;
            return View(store.SearchProduct(ProductSearchId));

        }

        public IActionResult SearchName(string ProductSearchName)
        {

            ProductStore store = HttpContext.RequestServices.GetService(typeof(TP_Corentin_Maxence.Models.ProductStore)) as ProductStore;
            return View(store.SearchProduct2(ProductSearchName));

        }

        public IActionResult SearchPrix(string ProductSearchPrix)
        {

            ProductStore store = HttpContext.RequestServices.GetService(typeof(TP_Corentin_Maxence.Models.ProductStore)) as ProductStore;
            return View(store.SearchProduct3(ProductSearchPrix));

        }
    }

}