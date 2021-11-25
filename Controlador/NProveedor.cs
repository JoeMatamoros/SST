using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using System.Data;

namespace Controlador
{
    public class NProveedor
    {
        public static string Insertar(string razon_proveedor,string sector_comercial,string tipo_documento,string num_documento,string direccion,string telefono,string email,string url)
        {

            DProveedor Obj = new DProveedor();
            Obj.Razon_Social = razon_proveedor;
            Obj.Sector_Comercial = sector_comercial;
            Obj.Tipo_Documento = tipo_documento;
            Obj.Num_Documento = num_documento;
            Obj.Direccion = direccion;
            Obj.Telefono = telefono;
            Obj.Email = email;
            Obj.Url = url;

            return Obj.Insertar(Obj);

        }

        public static string Editar(int idproveedor, string razon_proveedor, string sector_comercial, string tipo_documento, string num_documento, string direccion, string telefono, string email, string url)
        {
            DProveedor Obj = new DProveedor();
            Obj.IdProveedor = idproveedor;
            Obj.Razon_Social = razon_proveedor;
            Obj.Sector_Comercial = sector_comercial;
            Obj.Tipo_Documento = tipo_documento;
            Obj.Num_Documento = num_documento;
            Obj.Direccion = direccion;
            Obj.Telefono = telefono;
            Obj.Email = email;
            Obj.Url = url;

            return Obj.Editar(Obj);
        }

        public static string Eliminar(int idproveedor)
        {
            DProveedor Obj = new DProveedor();
            Obj.IdProveedor = idproveedor;
            return Obj.Eliminar(Obj);
        }

        public static DataTable Mostrar()
        {

            return new DProveedor().Mostrar();
        }

        public static DataTable BuscarRazon_Social(string textobuscar)
        {
            DProveedor Obj = new DProveedor();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarRazon_Social(Obj);
        }

        public static DataTable BuscarNum_Documento(string textobuscar)
        {
            DProveedor Obj = new DProveedor();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarNum_Documento(Obj);
        }

    }
}
