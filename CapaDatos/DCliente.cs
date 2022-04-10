using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DCliente
    {
        private int _IdCliente;
        private string _Nombre;
        private string _Apellidos;
        private string _Sexo;
        private DateTime _Fecha_Nacimiento;
        private string _Tipo_Documento;
        private string _Num_Documento;
        private string _Direccion;
        private string _Telefono;
        private string _Email;
        private string _TextoBuscar;

        public int IdCliente { get => _IdCliente; set => _IdCliente = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public string Apellidos { get => _Apellidos; set => _Apellidos = value; }
        public string Sexo { get => _Sexo; set => _Sexo = value; }
        public DateTime Fecha_Nacimiento { get => _Fecha_Nacimiento; set => _Fecha_Nacimiento = value; }
        public string Tipo_Documento { get => _Tipo_Documento; set => _Tipo_Documento = value; }
        public string Num_Documento { get => _Num_Documento; set => _Num_Documento = value; }
        public string Direccion { get => _Direccion; set => _Direccion = value; }
        public string Telefono { get => _Telefono; set => _Telefono = value; }
        public string Email { get => _Email; set => _Email = value; }
        public string TextoBuscar { get => _TextoBuscar; set => _TextoBuscar = value; }

        public DCliente()
        {

        }

        public DCliente(int idcliente, string nombre, string apellidos, string sexo, DateTime fecha_nacimiento,string tipo_documento,string num_documento,string direccion,string telefono,string email,string textobuscar)
        {
            this.IdCliente = idcliente;
            this.Nombre = nombre;
            this.Apellidos = apellidos;
            this.Sexo = sexo;
            this.Fecha_Nacimiento = fecha_nacimiento;
            this.Tipo_Documento = tipo_documento;
            this.Num_Documento = num_documento;
            this.Direccion = direccion;
            this.Telefono = telefono;
            this.Email = email;
            this.TextoBuscar = textobuscar;  

        }

        //MANTENIMIENTOS
        public string Insertar(DCliente Cliente)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCon.Open();
                //
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spinsertar_cliente";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdCliente = new SqlParameter
                {
                    ParameterName = "@idcliente",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                SqlCmd.Parameters.Add(ParIdCliente);

                SqlParameter ParNombre = new SqlParameter
                {
                    ParameterName = "@nombre",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = Cliente.Nombre
                };
                SqlCmd.Parameters.Add(ParNombre);

                SqlParameter ParApellidos = new SqlParameter
                {
                    ParameterName = "@apellidos",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = Cliente.Apellidos
                };
                SqlCmd.Parameters.Add(ParApellidos);

                SqlParameter ParSexo = new SqlParameter
                {
                    ParameterName = "@sexo",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 1,
                    Value = Cliente.Sexo
                };
                SqlCmd.Parameters.Add(ParSexo);

                SqlParameter ParFecha_Nacimiento = new SqlParameter
                {
                    ParameterName = "@fecha_nacimiento",
                    SqlDbType = SqlDbType.Date,
                    Size = 1,
                    Value = Cliente.Fecha_Nacimiento
                };
                SqlCmd.Parameters.Add(ParFecha_Nacimiento);

                SqlParameter ParTipoDocumento = new SqlParameter
                {
                    ParameterName = "@tipo_documento",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = Cliente.Tipo_Documento
                };
                SqlCmd.Parameters.Add(ParTipoDocumento);

                SqlParameter ParNum_Documento = new SqlParameter
                {
                    ParameterName = "@num_documento",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = Cliente.Num_Documento
                };
                SqlCmd.Parameters.Add(ParNum_Documento);

                SqlParameter ParDireccion = new SqlParameter
                {
                    ParameterName = "@direccion",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 100,
                    Value = Cliente.Direccion
                };
                SqlCmd.Parameters.Add(ParDireccion);

                SqlParameter ParTelefono = new SqlParameter
                {
                    ParameterName = "@telefono",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 100,
                    Value = Cliente.Telefono
                };
                SqlCmd.Parameters.Add(ParTelefono);

                SqlParameter ParEmail = new SqlParameter
                {
                    ParameterName = "@email",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = Cliente.Email
                };
                SqlCmd.Parameters.Add(ParEmail);

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "No se ingreso el registro";


            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }

            return rpta;

        }

        public string Editar(DCliente Cliente)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCon.Open();
                //
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "speditar_cliente";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdCliente = new SqlParameter
                {
                    ParameterName = "@idcliente",
                    SqlDbType = SqlDbType.Int,
                    Value = Cliente.IdCliente
                };
                SqlCmd.Parameters.Add(ParIdCliente);

                SqlParameter ParNombre = new SqlParameter
                {
                    ParameterName = "@nombre",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = Cliente.Nombre
                };
                SqlCmd.Parameters.Add(ParNombre);

                SqlParameter ParApellidos = new SqlParameter
                {
                    ParameterName = "@apellidos",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = Cliente.Apellidos
                };
                SqlCmd.Parameters.Add(ParApellidos);

                SqlParameter ParSexo = new SqlParameter
                {
                    ParameterName = "@sexo",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 1,
                    Value = Cliente.Sexo
                };
                SqlCmd.Parameters.Add(ParSexo);

                SqlParameter ParFecha_Nacimiento = new SqlParameter
                {
                    ParameterName = "@fecha_nacimiento",
                    SqlDbType = SqlDbType.Date,
                    Size = 1,
                    Value = Cliente.Fecha_Nacimiento
                };
                SqlCmd.Parameters.Add(ParFecha_Nacimiento);

                SqlParameter ParTipoDocumento = new SqlParameter
                {
                    ParameterName = "@tipo_documento",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = Cliente.Tipo_Documento
                };
                SqlCmd.Parameters.Add(ParTipoDocumento);

                SqlParameter ParNum_Documento = new SqlParameter
                {
                    ParameterName = "@num_documento",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = Cliente.Num_Documento
                };
                SqlCmd.Parameters.Add(ParNum_Documento);

                SqlParameter ParDireccion = new SqlParameter
                {
                    ParameterName = "@direccion",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 100,
                    Value = Cliente.Direccion
                };
                SqlCmd.Parameters.Add(ParDireccion);

                SqlParameter ParTelefono = new SqlParameter
                {
                    ParameterName = "@telefono",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 100,
                    Value = Cliente.Telefono
                };
                SqlCmd.Parameters.Add(ParTelefono);

                SqlParameter ParEmail = new SqlParameter
                {
                    ParameterName = "@email",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = Cliente.Email
                };
                SqlCmd.Parameters.Add(ParEmail);

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "No se ingreso el registro";


            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }

            return rpta;
        }

        public string Eliminar(DCliente Cliente)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCon.Open();
                //
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "speliminar_cliente";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdCliente = new SqlParameter
                {
                    ParameterName = "@idcliente",
                    SqlDbType = SqlDbType.Int,
                    Value = Cliente.IdCliente
                };
                SqlCmd.Parameters.Add(ParIdCliente);

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "No se elimino el registro";


            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }

            return rpta;
        }

        public DataTable Mostrar()
        {
            DataTable DtResultado = new DataTable("cliente");
            SqlConnection SqlCon = new SqlConnection();
            try
            {

                SqlCon.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spmostrar_cliente";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(DtResultado);
            }
            catch (Exception ex)
            {
                DtResultado = null;

            }
            return DtResultado;

        }

        public DataTable BuscarApellidos(DCliente Cliente)
        {

            DataTable DtResultado = new DataTable("cliente");
            SqlConnection SqlCon = new SqlConnection();
            try
            {

                SqlCon.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spbuscar_cliente_apellido";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParTextoBuscar = new SqlParameter
                {
                    ParameterName = "@textobuscar",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = Cliente.TextoBuscar
                };
                SqlCmd.Parameters.Add(ParTextoBuscar);

                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(DtResultado);
            }
            catch (Exception ex)
            {
                DtResultado = null;

            }
            return DtResultado;

        }

        public DataTable BuscarNum_Documento(DCliente Cliente)
        {

            DataTable DtResultado = new DataTable("cliente");
            SqlConnection SqlCon = new SqlConnection();
            try
            {

                SqlCon.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spbuscar_cliente_numero_documento";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParTextoBuscar = new SqlParameter
                {
                    ParameterName = "@textobuscar",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 11,
                    Value = Cliente.TextoBuscar
                };
                SqlCmd.Parameters.Add(ParTextoBuscar);

                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(DtResultado);
            }
            catch (Exception ex)
            {
                DtResultado = null;

            }
            return DtResultado;

        }

    }
}
