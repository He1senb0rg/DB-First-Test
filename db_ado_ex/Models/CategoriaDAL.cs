using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace db_ado_ex.Models
{
    public class CategoriaDAL : ICategoriaDAL
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Loja_1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public IEnumerable<Categoria> GetAllCategorias()
        {
            List<Categoria> lstcategoria = new List<Categoria>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT Id, Nome from Categoria", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.Id = Convert.ToInt32(rdr["Id"]);
                    categoria.Nome = rdr["Nome"].ToString();
                    lstcategoria.Add(categoria);
                }
                con.Close();
            }
            return lstcategoria;
        }
        public void AddCategoria(Categoria categoria)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "Insert into Categoria (Nome) Values(@Nome)";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Nome", categoria.Nome);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void UpdateCategoria(Categoria categoria)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "Update Categoria set Nome = @Nome where Id = @Id";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", categoria.Id);
                cmd.Parameters.AddWithValue("@Nome", categoria.Nome);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public Categoria GetCategoria(int? id)
        {
            Categoria categoria = new Categoria();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Categoria WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    categoria.Id = Convert.ToInt32(rdr["Id"]);
                    categoria.Nome = rdr["Nome"].ToString();
                }
            }
            return categoria;
        }
        public void DeleteCategoria(int? id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "Delete from Categoria where Id = @AId";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@AId", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}

