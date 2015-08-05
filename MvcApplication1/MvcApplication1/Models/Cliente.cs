using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Mvc;
public class Cliente
{
    public int ClienteID { get; set; }
    public string Nome { get; set; }
    public string Endereco { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Sexo { get; set; }


    ConnectionStringSettings getString = WebConfigurationManager.ConnectionStrings["banco"] as
ConnectionStringSettings;

    public List<Cliente> GetClientes()
    {
        List<Cliente> lista = new List<Cliente>();
        if (getString != null)
        {
            using (SqlConnection con = new SqlConnection(getString.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("ListarClientes", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader rd = null;
                try
                {
                    con.Open();
                    rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rd.Read())
                    {
                        Cliente cliente = new Cliente();
                        cliente.ClienteID = Convert.ToInt16(rd["clienteiD"]);
                        cliente.Nome = rd["nome"].ToString();
                        cliente.Endereco = rd["endereco"].ToString();
                        cliente.Cidade = rd["cidade"].ToString();
                        cliente.Estado = rd["estado"].ToString();
                        cliente.Email = rd["email"].ToString();
                        cliente.Sexo = rd["sexo"].ToString();
                        lista.Add(cliente);
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
                return lista.ToList();
            }
        }
        return null;
    }
    public void NovoCliente(Cliente cliente)
    {
        if (getString != null)
        {
            using (SqlConnection con = new SqlConnection(getString.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("InserirCliente", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@clienteID", cliente.ClienteID);
                cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@endereco", cliente.Endereco);
                cmd.Parameters.AddWithValue("@cidade", cliente.Cidade);
                cmd.Parameters.AddWithValue("@estado", cliente.Estado);
                cmd.Parameters.AddWithValue("@telefone", cliente.Telefone);
                cmd.Parameters.AddWithValue("@email", cliente.Email);
                cmd.Parameters.AddWithValue("@sexo", cliente.Sexo);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }

   