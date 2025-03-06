/*using Microsoft.Data.SqlClient;
using System.Data;
using TDDTesting.Models;

namespace TDDTesting.Data
{
    public class ProductoDataAccessLayer
    {
        string connectionString = "Data Source=ALEX; Initial Catalog=dbproductos; User ID=sa; Password=Roberto25.; TrustServerCertificate=True;";

        // Obtener todos los productos
        public List<Producto> GetProductos()
        {
            List<Producto> listProductos = new List<Producto>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("producto_SelectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Producto producto = new Producto
                    {
                        Codigo = Convert.ToInt32(rdr["codigo"]),
                        Nombre = rdr["nombre"].ToString(),
                        Precio = Convert.ToDecimal(rdr["precio"]),
                        Stock = Convert.ToInt32(rdr["stock"]),
                        Descripcion = rdr["descripcion"].ToString(),
                        Estado = Convert.ToBoolean(rdr["estado"])
                    };

                    listProductos.Add(producto);
                }
            }
            return listProductos;
        }

        // Agregar un nuevo producto
        public void AddProducto(Producto producto)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("producto_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@precio", producto.Precio);
                cmd.Parameters.AddWithValue("@stock", producto.Stock);
                cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                cmd.Parameters.AddWithValue("@estado", producto.Estado);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Actualizar un producto
        public void UpdateProducto(Producto producto)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("producto_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@codigo", producto.Codigo);
                cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@precio", producto.Precio);
                cmd.Parameters.AddWithValue("@stock", producto.Stock);
                cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                cmd.Parameters.AddWithValue("@estado", producto.Estado);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Eliminar un producto
        public void DeleteProducto(int codigo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("producto_Delete", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@codigo", codigo);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
*/