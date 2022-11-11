using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace db_ado_ex.Models
{
    public class ArtigoDAL : IArtigoDAL
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Loja_1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public IEnumerable<Artigo> GetAllArtigos()
        {
            List<Artigo> lstartigo = new List<Artigo>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT Id, Nome, Preco, QtaStock from Artigos", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Artigo artigo = new Artigo();
                    artigo.Id = Convert.ToInt32(rdr["Id"]);
                    artigo.Nome = rdr["Nome"].ToString();
                    artigo.Preco = Convert.ToInt32(rdr["Preco"].ToString());
                    artigo.QtaStock = Convert.ToInt16(rdr["QtaStock"]);
                    lstartigo.Add(artigo);
                }
                con.Close();
            }
            return lstartigo;
        }
        public void AddArtigo(Artigo artigo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "Insert into Artigos (Nome, Preco, QtaStock) Values(@Nome, @Preco, @QtaStock)";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Nome", artigo.Nome);
                cmd.Parameters.AddWithValue("@Preco", artigo.Preco);
                cmd.Parameters.AddWithValue("@QtaStock", artigo.QtaStock);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void UpdateArtigo(Artigo artigo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "Update Artigos set Nome = @Nome, Preco = @Preco, QtaStock = @QtaStock where Id = @Id";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", artigo.Id);
                cmd.Parameters.AddWithValue("@Nome", artigo.Nome);
                cmd.Parameters.AddWithValue("@Preco", artigo.Preco);
                cmd.Parameters.AddWithValue("@QtaStock", artigo.QtaStock);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public Artigo GetArtigo(int? id)
        {
            Artigo artigo = new Artigo();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Artigos WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    artigo.Id = Convert.ToInt32(rdr["Id"]);
                    artigo.Nome = rdr["Nome"].ToString();
                    artigo.Preco = Convert.ToInt32(rdr["Preco"].ToString());
                    artigo.QtaStock = Convert.ToInt16(rdr["QtaStock"]);
                }
            }
            return artigo;
        }
        public void DeleteArtigo(int? id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "Delete from Artigos where Id = @AId";
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

