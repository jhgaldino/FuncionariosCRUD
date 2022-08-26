using FuncionariosCRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using FuncionariosCRUD.Models;
using System;

namespace FuncionariosCRUD.Controllers
{
    public class Funcionarios : Controller
    {
        string connectionString = "Data Source=PC;Initial Catalog=FuncionariosDB;Integrated Security=True";
        // GET: Funcionarios
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM TBFuncionarios", sqlConnection);
                sqlDataAdapter.Fill(dataTable);
            }
            return View(dataTable);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View(new FuncionariosModel());
        }

        // POST: Funcionarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FuncionariosModel funcionariosModel)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO TBFuncionarios (Nome, Cargo) VALUES (@Nome, @Cargo)", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@Nome", funcionariosModel.Nome);
                    sqlCommand.Parameters.AddWithValue("@Cargo", funcionariosModel.Cargo);
                    sqlCommand.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Funcionarios/Edit/5
        public ActionResult Edit(int id)
        {
            FuncionariosModel funcionariosModel = new FuncionariosModel();
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM TBFuncionarios WHERE IDFuncionario = @IDFuncionario", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@IDFuncionario", id);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
            }
            if (dataTable.Rows.Count == 1)
            {
                funcionariosModel.IDFuncionario = Convert.ToInt32(dataTable.Rows[0][0].ToString());
                funcionariosModel.Nome = dataTable.Rows[0][1].ToString();
                funcionariosModel.Cargo = dataTable.Rows[0][2].ToString();
                return View(funcionariosModel);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

            
        }

        // POST: Funcionarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FuncionariosModel funcionariosModel)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("UPDATE TBFuncionarios SET Nome = @Nome, Cargo = @Cargo WHERE IDFuncionario = @IDFuncionario", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@IDFuncionario", funcionariosModel.IDFuncionario);
                    sqlCommand.Parameters.AddWithValue("@Nome", funcionariosModel.Nome);
                    sqlCommand.Parameters.AddWithValue("@Cargo", funcionariosModel.Cargo);
                    sqlCommand.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
        
        

        // GET: Funcionarios/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("DELETE FROM TBFuncionario WHERE IDFuncionario = @IDFuncionario", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@IDFuncionario",id);

                    sqlCommand.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
