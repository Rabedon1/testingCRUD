using Microsoft.Data.SqlClient;
using System.Data;
using TDDTesting.Models;

namespace TDDTesting.Data
{
    public class OpinionClienteDataAccessLayer
    {
        string connectionString = "Data Source=ALEX; Initial Catalog=dbproductos; User ID=sa; Password=Roberto25.; TrustServerCertificate=True;";

        // Obtener todas las opiniones
        public List<OpinionCliente> GetOpiniones()
        {
            List<OpinionCliente> listOpiniones = new List<OpinionCliente>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("opinionCliente_SelectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    OpinionCliente opinion = new OpinionCliente
                    {
                        OpinionID = Convert.ToInt32(rdr["OpinionID"]),
                        ClienteID = Convert.ToInt32(rdr["ClienteID"]),
                        ProductoID = rdr["ProductoID"] != DBNull.Value ? Convert.ToInt32(rdr["ProductoID"]) : (int?)null,
                        Calificacion = Convert.ToInt32(rdr["Calificacion"]),
                        Comentario = rdr["Comentario"].ToString(),
                        Fecha = Convert.ToDateTime(rdr["Fecha"])
                    };

                    listOpiniones.Add(opinion);
                }
            }
            return listOpiniones;
        }

        // Agregar una nueva opinión
        public void AddOpinionCliente(OpinionCliente opinion)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("opinionCliente_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClienteID", opinion.ClienteID);
                cmd.Parameters.AddWithValue("@ProductoID", opinion.ProductoID ?? (object)DBNull.Value);  // Puede ser null
                cmd.Parameters.AddWithValue("@Calificacion", opinion.Calificacion);
                cmd.Parameters.AddWithValue("@Comentario", opinion.Comentario);
                cmd.Parameters.AddWithValue("@Fecha", opinion.Fecha);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Actualizar una opinión existente
        public void UpdateOpinionCliente(OpinionCliente opinion)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("opinionCliente_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OpinionID", opinion.OpinionID);
                cmd.Parameters.AddWithValue("@ClienteID", opinion.ClienteID);
                cmd.Parameters.AddWithValue("@ProductoID", opinion.ProductoID ?? (object)DBNull.Value);  // Puede ser null
                cmd.Parameters.AddWithValue("@Calificacion", opinion.Calificacion);
                cmd.Parameters.AddWithValue("@Comentario", opinion.Comentario);
                cmd.Parameters.AddWithValue("@Fecha", opinion.Fecha);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Eliminar una opinión
        public void DeleteOpinionCliente(int opinionID)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("opinionCliente_Delete", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OpinionID", opinionID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
