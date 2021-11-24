using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using System.Data;

namespace Controlador
{
    public class NArticulo
    {
        //INSERTAR ARTICULO
        public static string Insertar(string codigo,string nombre, string descripcion, byte[] imagen, int idcategoria, int idpresentacion)
        {
            DArticulo Obj = new DArticulo();
            Obj.Codigo = codigo;
            Obj.Nombre = nombre;
            Obj.Descripcion = descripcion;
            Obj.Imagen = imagen;
            Obj.IdCategoria = idcategoria;
            Obj.IdPresentacion = idpresentacion;

            return Obj.Insertar(Obj);

        }

        //EDITAR ARTICULO
        public static string Editar(int idarticulo, string codigo, string nombre, string descripcion, byte[] imagen, int idcategoria, int idpresentacion)
        {
            DArticulo Obj = new DArticulo();

            Obj.IdArticulo = idarticulo;
            Obj.Codigo = codigo;
            Obj.Nombre = nombre;
            Obj.Descripcion = descripcion;
            Obj.Imagen = imagen;
            Obj.IdCategoria = idcategoria;
            Obj.IdPresentacion = idpresentacion;

            return Obj.Editar(Obj);
        }

        //ELIMINAR ARTICULO
        public static string Eliminar(int idarticulo)
        {
            DArticulo Obj = new DArticulo();
            Obj.IdArticulo = idarticulo;
            return Obj.Eliminar(Obj);
        }

        //MOSTRAR ARTICULOS
        public static DataTable Mostrar()
        {

            return new DArticulo().Mostrar();
        }

        //BUSQUEDA FILTRADA POR NOMBRE
        public static DataTable BuscarNombre(string textobuscar)
        {
            DArticulo Obj = new DArticulo();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarNombre(Obj);
        }
    }
}
