using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;


namespace CapaDatos
{
    public class DDetalle_Venta
    {
        private int _Iddetalle_venta;
        private int _Idventa;
        private int _Iddetalle_ingreso;
        private int _Cantidad;
        private decimal _Precio_Venta;
        private decimal _Descuento;

        public int Iddetalle_venta { get => _Iddetalle_venta; set => _Iddetalle_venta = value; }
        public int Idventa { get => _Idventa; set => _Idventa = value; }
        public int Iddetalle_ingreso { get => _Iddetalle_ingreso; set => _Iddetalle_ingreso = value; }
        public decimal Precio_Venta { get => _Precio_Venta; set => _Precio_Venta = value; }
        public decimal Descuento { get => _Descuento; set => _Descuento = value; }
        public int Cantidad { get => _Cantidad; set => _Cantidad = value; }

        public DDetalle_Venta() 
        { 

        }

        public DDetalle_Venta(int iddetalle_venta, int idventa, int iddetalle_ingreso, int cantidad,decimal precio_venta, decimal descuento)
        {
            this.Iddetalle_venta = iddetalle_venta;
            this.Idventa = idventa;
            this.Iddetalle_ingreso = iddetalle_ingreso;
            this.Cantidad = cantidad;
            this.Precio_Venta = precio_venta;
            this.Descuento = descuento;
        }

        //METODO INSERTAR
        public string Insertar(DDetalle_Venta Detalle_Venta, ref SqlConnection SqlCon, ref SqlTransaction SqlTra)
        {
            string rpta = "";

            try
            {
                //
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.Transaction = SqlTra;
                SqlCmd.CommandText = "spinsertar_detalle_venta";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIddetalle_Venta = new SqlParameter
                {
                    ParameterName = "@iddetalle_venta",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                SqlCmd.Parameters.Add(ParIddetalle_Venta);

                SqlParameter ParIdventa = new SqlParameter
                {
                    ParameterName = "@idventa",
                    SqlDbType = SqlDbType.Int,
                    Value = Detalle_Venta.Idventa
                };
                SqlCmd.Parameters.Add(ParIdventa);

                SqlParameter ParIddetalle_ingreso = new SqlParameter
                {
                    ParameterName = "@iddetalle_ingreso",
                    SqlDbType = SqlDbType.Int,
                    Value = Detalle_Venta.Iddetalle_ingreso
                };
                SqlCmd.Parameters.Add(ParIddetalle_ingreso);

                SqlParameter ParCantidad = new SqlParameter
                {
                    ParameterName = "@cantidad",
                    SqlDbType = SqlDbType.Int,
                    Value = Detalle_Venta.Cantidad
                };
                SqlCmd.Parameters.Add(ParCantidad);

                SqlParameter ParPrecioVenta = new SqlParameter
                {
                    ParameterName = "@precio_venta",
                    SqlDbType = SqlDbType.Money,
                    Value = Detalle_Venta.Precio_Venta
                };
                SqlCmd.Parameters.Add(ParPrecioVenta);

                SqlParameter ParDescuento = new SqlParameter
                {
                    ParameterName = "@descuento",
                    SqlDbType = SqlDbType.Money,
                    Value = Detalle_Venta.Descuento
                };
                SqlCmd.Parameters.Add(ParDescuento);


                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "No se ingreso el registro";

            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }

            return rpta;

        }
    }
}
