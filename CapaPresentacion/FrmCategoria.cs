using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//IMPORTE DE CONTROLADOR 
using Controlador;


namespace CapaPresentacion
{
    public partial class FrmCategoria : Form
    {
        //INICIALIZACION DE VARIABLES BOOLEANAS.
        private bool IsNuevo = false;
        private bool IsEditar = false;

        public FrmCategoria()
        {
            InitializeComponent();
            //ttMensaje es del control ToolTip para hacer una interfaz mas amistosa direccionando al usuario.
            this.ttMensaje.SetToolTip(this.txtNombre, "Ingrese nombre de la categoria");
        }

        //MOSTRAR CONFIRMACION
        private void MensajeOk(string mensaje) {
            MessageBox.Show(mensaje,"Sistema de ventas",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        //MOSTRAR ERROR
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //LIMPIAR FORMULARIO
        private void Limpiar() {

            this.txtNombre.Text = string.Empty;
            this.txtDescripcion.Text = string.Empty;
            this.txtIdcategoria.Text = string.Empty;
        
        }
        //HABILITAR CONTROLES
        private void Habilitar(bool valor) {

            this.txtNombre.ReadOnly = !valor; //Niega el valor, EJEMPLO: Entra un true lo niega como false.
            this.txtDescripcion.ReadOnly = !valor;
            this.txtIdcategoria.ReadOnly = !valor;
        
        }
        //HABILITAR BOTONES
        private void Botones() { 
            //Se evaluará si IsNuevo o IsEditar están activados como true.
            if(this.IsNuevo || this.IsEditar) {

                this.Habilitar(true); //CAJAS DE TEXTO DESACTIVADAS
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;

            } else {

                this.Habilitar(false); //CUANDO SE HABILITEN LAS CAJAS DE TEXTO
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;
            }
            
        }

        //OCULTAR COLUMNAS
        private void OcultarColumnas() {

            this.dataListado.Columns[0].Visible = false;
            this.dataListado.Columns[1].Visible = false;

        }
        //MOSTRAR
        private void Mostrar() {
            this.dataListado.DataSource = NCategoria.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de registros: "+ Convert.ToString(dataListado.Rows.Count);
        
        }

        //BUSCAR POR NOMBRE
        private void BuscarNombre()
        {
            this.dataListado.DataSource = NCategoria.BuscarNombre(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de registros:" + Convert.ToString(dataListado.Rows.Count);

        }
        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Mostrar();
            this.Habilitar(false);
            this.Botones();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //Cuando se haga click en este botón es porque se registrará algo.
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(true); //Se habilitarán las cajas de texto.
            this.txtNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try {
                /*LA DEPENDENCIA DEL TRUE DE "IsNuevo" SE ASEMEJA AL PRESIONAR EL BOTON NUEVO Y ESTE VALOR CAMBIA
                 AL MOMENTO DE PRESIONAR GUARDAR SE RECIBE "IsNuevo= TRUE"*/
                string rpta="";
                if(this.txtNombre.Text == string.Empty){
                    MensajeError("Falta ingresar nombre de la categoria");
                    errorIcono.SetError(txtNombre, "Ingrese un nombre");

                } else{

                    if (this.IsNuevo) {
                        rpta = NCategoria.Insertar(
                            this.txtNombre.Text.Trim().ToUpper(),
                            this.txtDescripcion.Text.Trim()
                            );
                    } else{

                        rpta = NCategoria.Editar(
                            Convert.ToInt32(this.txtIdcategoria.Text),
                            this.txtNombre.Text.Trim().ToUpper(),
                            this.txtDescripcion.Text.Trim()
                            );
                    }

                    if (rpta.Equals("OK"))
                    {
                        if (this.IsNuevo)//cuando nuevo es igual a true
                        {
                            this.MensajeOk("Se insertó de forma correcta");
                        }
                        else
                        {
                            this.MensajeOk("Se actualizó de forma correcta");
                        }
                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }
                    this.IsNuevo = false;
                    this.IsEditar = false;
                    this.Botones();
                    this.Limpiar();
                    this.Mostrar();
                }

            } catch (Exception ex) {

                MessageBox.Show(ex.Message+ ex.StackTrace);
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            //idcategoria,nombre y descripcion vienen de los procedimientos almacenados en la db.
            this.txtIdcategoria.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idcategoria"].Value);
            this.txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["nombre"].Value);
            this.txtDescripcion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["descripcion"].Value);

            /*Cuando se haga doble click en el datagrid se llenaran los campos necesarios y este se redireccionará
             al tabpage correspondiente.*/
            this.tabControl1.SelectedIndex = 1; //1 para mostrar el primer tabpage.
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!this.txtIdcategoria.Text.Equals(""))
            {
                this.IsEditar = true;
                this.Botones();
                this.Habilitar(true);
            }
            else
            {
                MensajeError("Debe seleccionar el registro a modificar");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(false);

        }

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEliminar.Checked) {

                this.dataListado.Columns[0].Visible = true;

            } else{

                this.dataListado.Columns[0].Visible = false;
            }
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Para poder hacerles "check" en el DataGridView
            if(e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
               
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Desea eliminar los registros?","Sistema Ventas",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK) {
                    string Codigo;
                    string rpta="";

                    //Recorrer todos los registros y verificar si estan marcados para pasarlos al metodo eliminar
                    foreach(DataGridViewRow row in dataListado.Rows)
                    {
                        //Revisa fila por fila si la primer columna es check
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToString(row.Cells[1].Value);
                            rpta = NCategoria.Eliminar(Convert.ToInt32(Codigo));
                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se elimino correctamente");
                            }
                            else {
                                this.MensajeError(rpta);
                            }
                        }
                    }
                    this.Mostrar();
                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void FrmCategoria_DoubleClick(object sender, EventArgs e)
        {

        }
    }
}
