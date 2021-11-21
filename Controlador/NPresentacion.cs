using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos; //Modelo
using System.Data; //Para poder usar variables y funciones de sql

namespace Controlador
{ 
    public class NPresentacion
    {
        //INSERTAR
        public static string Insertar(string nombre, string descripcion)
        {
            DPresentacion Obj = new DPresentacion();
            Obj.Nombre = nombre;
            Obj.Descripcion = descripcion;

            return Obj.Insertar(Obj);

        }

        //EDITAR PRESENTACION
        public static string Editar(int idpresentacion, string nombre, string descripcion)
        {
            DPresentacion Obj = new DPresentacion();

            Obj.IdPresentacion = idpresentacion;
            Obj.Nombre = nombre;
            Obj.Descripcion = descripcion;

            return Obj.Editar(Obj);
        }

        //ELIMINAR PRESENTACION
        public static string Eliminar(int idpresentacion)
        {
            DPresentacion Obj = new DPresentacion();
            Obj.IdPresentacion = idpresentacion;
            return Obj.Eliminar(Obj);
        }

        //MOSTRAR PRESENTACIONES
        public static DataTable Mostrar()
        {

            return new DPresentacion().Mostrar();
        }

        //BUSQUEDA FILTRADA POR NOMBRE
        public static DataTable BuscarNombre(string textobuscar)
        {
            DPresentacion Obj = new DPresentacion();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarNombre(Obj);
        }
    }
}
