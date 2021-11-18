using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using System.Data;
namespace Controlador
{
   public class NCategoria
    {
        //INSERTAR
        public static string Insertar(string nombre, string descripcion) {

            DCategoria Obj = new DCategoria();
            Obj.Nombre = nombre;
            Obj.Descripcion = descripcion;

            return Obj.Insertar(Obj);
        
        }

        public static string Editar(int idcategoria,string nombre, string descripcion) {
            DCategoria Obj = new DCategoria();

            Obj.IdCategoria = idcategoria;
            Obj.Nombre = nombre;
            Obj.Descripcion = descripcion;

            return Obj.Editar(Obj);
        }

        public static string Eliminar(int idcategoria)
        {
            DCategoria Obj = new DCategoria();
            Obj.IdCategoria = idcategoria;
            return Obj.Eliminar(Obj);
        }

        public static DataTable Mostrar() {

            return new DCategoria().Mostrar();
        }

        public static DataTable BuscarNombre(string textobuscar)
        {
            DCategoria Obj = new DCategoria();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarNombre(Obj);
        }

    }
}
