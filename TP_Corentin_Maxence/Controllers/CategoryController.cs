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
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            CategoryStore store = HttpContext.RequestServices.GetService(typeof(TP_Corentin_Maxence.Models.CategoryStore)) as CategoryStore;
            return View(store.GetAllCategorys());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Category());
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {

            using (MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;pwd=root;database=joueur;persistsecurityinfo=True"))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("insert into category (name,description) VALUES (@name,@description)", conn);
                cmd.Parameters.AddWithValue("@name", category.name);
                cmd.Parameters.AddWithValue("@description", category.description);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");


        }

        public IActionResult Edit(int id)
        {
            Category category = new Category();
            DataTable dataTable = new DataTable();
            using (MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;pwd=root;database=joueur;persistsecurityinfo=True"))
            {
                conn.Open();
                MySqlDataAdapter cmd = new MySqlDataAdapter("select * from category where id = @id", conn);
                cmd.SelectCommand.Parameters.AddWithValue("@id", id);
                cmd.Fill(dataTable);
            }
            if (dataTable.Rows.Count == 1)
            {
                category.id = Convert.ToInt32(dataTable.Rows[0][0].ToString());
                category.name = dataTable.Rows[0][1].ToString();
                category.description = dataTable.Rows[0][2].ToString();
                return View(category);
            }
            else
                return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            using (MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;pwd=root;database=joueur;persistsecurityinfo=True"))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("update category set name = @name, description = @description where id = @id", conn);
                cmd.Parameters.AddWithValue("@id", category.id);
                cmd.Parameters.AddWithValue("@name", category.name);
                cmd.Parameters.AddWithValue("@description", category.description);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            using (MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;pwd=root;database=joueur;persistsecurityinfo=True"))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("delete from category where id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public IActionResult SearchId(string CategorySearchId)
        {

            CategoryStore store = HttpContext.RequestServices.GetService(typeof(TP_Corentin_Maxence.Models.CategoryStore)) as CategoryStore;
            return View(store.SearchCategory(CategorySearchId));

        }

        public IActionResult SearchName(string CategorySearchName)
        {

            CategoryStore store = HttpContext.RequestServices.GetService(typeof(TP_Corentin_Maxence.Models.CategoryStore)) as CategoryStore;
            return View(store.SearchCategory2(CategorySearchName));

        }
    }
}