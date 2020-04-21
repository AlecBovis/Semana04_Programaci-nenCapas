using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entity;

namespace Data
{
    public class DPedido
    {
        public List<Pedido> GetPedidos(Pedido pedido)
        {
            SqlParameter[] parameters = null;
            string comandText = string.Empty;
            List<Pedido> pedidos = null;

            try
            {
                comandText = "USP_ListarPedidosEntreFechas";
                parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@Fec1", SqlDbType.DateTime);
                parameters[0].Value = pedido.FechaInicio;
                parameters[1] = new SqlParameter("@Fec2", SqlDbType.DateTime);
                parameters[1].Value = pedido.FechaFin;
                pedidos = new List<Pedido>();

                using (SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.Connection, "USP_ListarPedidosEntreFechas",
                    CommandType.StoredProcedure, parameters))
                {
                    while (reader.Read())
                    {
                        pedidos.Add(new Pedido 
                        {
                            IdPedido = reader["IdPedido"] == null ? 0 : Convert.ToInt32(reader["IdPedido"]),
                            IdCliente = reader["IdCliente"] == null ? string.Empty : Convert.ToString(reader["IdCliente"]),
                            IdEmpleado = reader["IdEmpleado"] == null ? 0 : Convert.ToInt32(reader["IdEmpleado"]),
                            FechaPedido = reader["FechaPedido"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FechaPedido"]),
                            FechaEntrega = reader["FechaEntrega"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FechaEntrega"]),
                            FechaEnvio = reader["FechaEnvio"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FechaEnvio"]),
                            FormaEnvio = reader["FormaEnvio"] == null ? 0 : Convert.ToInt32(reader["FormaEnvio"]),
                            Cargo = reader["Cargo"] == null ? 0 : Convert.ToInt32(reader["Cargo"]),
                            Destinatario = reader["Destinatario"] == null ? string.Empty : Convert.ToString(reader["Destinatario"]),
                            DireccionDestinatario = reader["DireccionDestinatario"] == null ? string.Empty : Convert.ToString(reader["DireccionDestinatario"]),
                            RegionDestinatario = reader["RegionDestinatario"] == null ? string.Empty : Convert.ToString(reader["RegionDestinatario"]),
                            CodPostalDestinatario = reader["CodPostalDestinatario"] == null ? string.Empty : Convert.ToString(reader["CodPostalDestinatario"]),
                            PaisDestinatario = reader["PaisDestinatario"] == null ? string.Empty : Convert.ToString(reader["PaisDestinatario"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pedidos;
        }
    }
}
