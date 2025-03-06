using Microsoft.Data.SqlClient;
using System.Data;
using TDDTesting.Models;

namespace TDDTesting.Data
{
    public class ClienteDataAccessLayer
    {
        string connectionString = "Server=localhost;Database=dbproductos;User Id=sa;Password=Roberto25.; TrustServerCertificate=True";

        // Obtener todos los clientes
        public List<Cliente> GetClientes()
        {
            List<Cliente> listClientes = new List<Cliente>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("cliente_SelectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Cliente cliente = new Cliente
                    {
                        Codigo = Convert.ToInt32(rdr["codigo"]),
                        Cedula = rdr["cedula"].ToString(),
                        Nombres = rdr["nombres"].ToString(),
                        Apellidos = rdr["apellidos"].ToString(),
                        FechaNacimiento = Convert.ToDateTime(rdr["fechaNacimiento"]),
                        Mail = rdr["mail"].ToString(),
                        Telefono = rdr["telefono"].ToString(),
                        Direccion = rdr["direccion"].ToString(),
                        Estado = Convert.ToBoolean(rdr["estado"])
                    };

                    listClientes.Add(cliente);
                }
            }
            return listClientes;
        }

        // Agregar un nuevo cliente
        public int AddCliente(Cliente cliente)
        {
            int codigoGenerado = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("cliente_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cedula", cliente.Cedula);
                cmd.Parameters.AddWithValue("@nombres", cliente.Nombres);
                cmd.Parameters.AddWithValue("@apellidos", cliente.Apellidos);
                cmd.Parameters.AddWithValue("@fechaNacimiento", cliente.FechaNacimiento);
                cmd.Parameters.AddWithValue("@mail", cliente.Mail);
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@estado", cliente.Estado);

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        codigoGenerado = Convert.ToInt32(rdr["Codigo"]);
                    }
                }
                con.Close();
            }
            return codigoGenerado;
        }

        public Cliente GetClienteById(int codigo)
        {
            Cliente cliente = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("cliente_SelectById", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Codigo", codigo);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    cliente = new Cliente
                    {
                        Codigo = Convert.ToInt32(rdr["Codigo"]),
                        Cedula = rdr["Cedula"]?.ToString() ?? string.Empty,
                        Apellidos = rdr["Apellidos"]?.ToString() ?? string.Empty,
                        Nombres = rdr["Nombres"]?.ToString() ?? string.Empty,
                        FechaNacimiento = rdr["FechaNacimiento"] != DBNull.Value ? Convert.ToDateTime(rdr["FechaNacimiento"]) : DateTime.MinValue,
                        Mail = rdr["Mail"]?.ToString() ?? string.Empty,
                        Telefono = rdr["Telefono"]?.ToString() ?? string.Empty,
                        Direccion = rdr["Direccion"]?.ToString() ?? string.Empty,
                        Estado = rdr["Estado"] != DBNull.Value && Convert.ToBoolean(rdr["Estado"])
                    };
                }
                rdr.Close();
                con.Close();
            }
            return cliente;
        }
        public void UpdateCliente(Cliente cliente)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("cliente_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@codigo", cliente.Codigo);
                cmd.Parameters.AddWithValue("@cedula", cliente.Cedula);
                cmd.Parameters.AddWithValue("@nombres", cliente.Nombres);
                cmd.Parameters.AddWithValue("@apellidos", cliente.Apellidos);
                cmd.Parameters.AddWithValue("@fechaNacimiento", cliente.FechaNacimiento);
                cmd.Parameters.AddWithValue("@mail", cliente.Mail);
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@estado", cliente.Estado);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Eliminar un cliente
        public void DeleteCliente(int codigo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("cliente_Delete", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@codigo", codigo);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
