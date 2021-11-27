using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controlador;

namespace CapaPresentacion
{
    public partial class frmTrabajador : Form
    {
        private bool IsNuevo = false;
        private bool IsEditar = false;

        public frmTrabajador()
        {
            InitializeComponent();
            this.ttMensaje.SetToolTip(this.txtNombre,"Ingrese nombre");
            this.ttMensaje.SetToolTip(this.txtApellidos, "Ingrese apellidos");
            this.ttMensaje.SetToolTip(this.txtUsuario, "Ingrese nombre de usuario");
            this.ttMensaje.SetToolTip(this.txtPassword, "Ingrese password del usuario");
            this.ttMensaje.SetToolTip(this.cbAcceso, "Seleccione nivel de acceso");
            
        }

        //MOSTRAR CONFIRMACION
        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //MOSTRAR ERROR
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //LIMPIAR FORMULARIO
        private void Limpiar()
        {

            this.txtNombre.Text = string.Empty;
            this.txtApellidos.Text = string.Empty;
            this.txtNum_Documento.Text = string.Empty;
            this.txtDireccion.Text = string.Empty;
            this.txtTelefono.Text = string.Empty;
            this.txtEmail.Text = string.Empty;
            this.txtUsuario.Text = string.Empty;
            this.txtPassword.Text = string.Empty;
            this.txtIdtrabajador.Text = string.Empty;

        }
        //HABILITAR CONTROLES
        private void Habilitar(bool valor)
        {

            this.txtNombre.ReadOnly = !valor; //Niega el valor, EJEMPLO: Entra un true lo niega como false.
            this.txtApellidos.ReadOnly = !valor;
            this.txtDireccion.ReadOnly = !valor;
            this.cbSexo.Enabled = valor;
            this.txtNum_Documento.ReadOnly = !valor;
            this.txtTelefono.ReadOnly = !valor;
            this.txtEmail.ReadOnly = !valor;
            this.cbAcceso.Enabled = valor;
            this.txtUsuario.ReadOnly = !valor;
            this.txtPassword.ReadOnly = !valor;
            this.txtIdtrabajador.ReadOnly = !valor;

        }
        //HABILITAR BOTONES
        private void Botones()
        {
            //Se evaluará si IsNuevo o IsEditar están activados como true.
            if (this.IsNuevo || this.IsEditar)
            {

                this.Habilitar(true); //CAJAS DE TEXTO DESACTIVADAS
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;

            }
            else
            {

                this.Habilitar(false); //CUANDO SE HABILITEN LAS CAJAS DE TEXTO
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;
            }

        }

        //OCULTAR COLUMNAS
        private void OcultarColumnas()
        {

            this.dataListado.Columns[0].Visible = false;
            this.dataListado.Columns[1].Visible = false;

        }
        //MOSTRAR
        private void Mostrar()
        {
            this.dataListado.DataSource = NTrabajador.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de registros: " + Convert.ToString(dataListado.Rows.Count);

        }

        //BUSCAR POR NOMBRE
        private void BuscarApellidos()
        {
            this.dataListado.DataSource = NTrabajador.BuscarApellidos(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de registros:" + Convert.ToString(dataListado.Rows.Count);

        }

        //BUSCAR POR NUMERO DE DOCUMENTO
        private void BuscarNum_Documento()
        {
            this.dataListado.DataSource = NTrabajador.BuscarNum_Documento(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de registros:" + Convert.ToString(dataListado.Rows.Count);

        }

        private void frmTrabajador_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Mostrar();
            this.Habilitar(false);
            this.Botones();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cbBuscar.Text.Equals("Documento"))
            {
                this.BuscarNum_Documento();
            }else if (cbBuscar.Text.Equals("Apellidos"))
            {
                this.BuscarApellidos();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Desea eliminar los registros?", "Sistema Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    string Codigo;
                    string rpta = "";

                    //Recorrer todos los registros y verificar si estan marcados para pasarlos al metodo eliminar
                    foreach (DataGridViewRow row in dataListado.Rows)
                    {
                        //Revisa fila por fila si la primer columna es check
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToString(row.Cells[1].Value);
                            rpta = NTrabajador.Eliminar(Convert.ToInt32(Codigo));
                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se elimino correctamente");
                            }
                            else
                            {
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

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEliminar.Checked)
            {

                this.dataListado.Columns[0].Visible = true;

            }
            else
            {

                this.dataListado.Columns[0].Visible = false;
            }
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Para poder hacerles "check" en el DataGridView
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);

            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            //Todo esto viene de los procedimientos almacenados en la db.
            this.txtIdtrabajador.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idtrabajador"].Value);
            this.txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["nombre"].Value);
            this.txtApellidos.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["apellidos"].Value);
            this.cbSexo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["sexo"].Value);
            this.dtFechaNac.Value = Convert.ToDateTime(this.dataListado.CurrentRow.Cells["fecha_nac"].Value);
            this.txtNum_Documento.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["num_documento"].Value);
            this.txtDireccion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["direccion"].Value);
            this.txtTelefono.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["telefono"].Value);
            this.txtEmail.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["email"].Value);
            this.cbAcceso.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["acceso"].Value);
            this.txtUsuario.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["usuario"].Value);
            this.txtPassword.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["password"].Value);

            /*Cuando se haga doble click en el datagrid se llenaran los campos necesarios y este se redireccionará
             al tabpage correspondiente.*/
            this.tabControl1.SelectedIndex = 1; //1 para mostrar el primer tabpage.
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(true);
            this.txtNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                /*LA DEPENDENCIA DEL TRUE DE "IsNuevo" SE ASEMEJA AL PRESIONAR EL BOTON NUEVO Y ESTE VALOR CAMBIA
                 AL MOMENTO DE PRESIONAR GUARDAR SE RECIBE "IsNuevo= TRUE"*/
                string rpta = "";
                if (this.txtNombre.Text == string.Empty || this.txtApellidos.Text == string.Empty || this.txtNum_Documento.Text == string.Empty || this.txtDireccion.Text == string.Empty || this.txtUsuario.Text==string.Empty || this.txtPassword.Text == string.Empty)
                {
                    MensajeError("Falta ingresar nombre de la categoria");
                    errorIcono.SetError(txtNombre, "Ingrese nombre");
                    errorIcono.SetError(txtApellidos, "Ingrese apellidos.");
                    errorIcono.SetError(txtNum_Documento, "Ingrese numero de documento");
                    errorIcono.SetError(txtDireccion, "Ingrese direccion");
                    errorIcono.SetError(txtUsuario,"Ingrese usuario.");
                    errorIcono.SetError(txtUsuario,"Ingrese un password");

                }
                else
                {

                    if (this.IsNuevo)
                    {
                        rpta = NTrabajador.Insertar(
                            this.txtNombre.Text.ToUpper(),
                            this.txtApellidos.Text.ToUpper(),
                            this.cbSexo.Text, 
                            dtFechaNac.Value,
                            txtNum_Documento.Text,
                            txtDireccion.Text,
                            txtTelefono.Text,
                            txtEmail.Text,
                            cbAcceso.Text,
                            txtUsuario.Text,
                            txtPassword.Text);
                    }
                    else
                    {
                        rpta = NTrabajador.Editar(
                            Convert.ToInt32(this.txtIdtrabajador.Text),
                            this.txtNombre.Text.ToUpper(),
                            this.txtApellidos.Text.ToUpper(),
                            this.cbSexo.Text,
                            dtFechaNac.Value,
                            txtNum_Documento.Text,
                            txtDireccion.Text,
                            txtTelefono.Text,
                            txtEmail.Text,
                            cbAcceso.Text,
                            txtUsuario.Text,
                            txtPassword.Text);
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

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.IsEditar = false;
            this.Botones();
            this.Habilitar(false);
            this.Limpiar();
            this.txtIdtrabajador.Text = string.Empty;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!this.txtIdtrabajador.Text.Equals(""))
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
    }
}
