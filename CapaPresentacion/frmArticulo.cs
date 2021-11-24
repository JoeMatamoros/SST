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
    public partial class frmArticulo : Form
    {
        private bool IsNuevo = false;
        private bool IsEditar = false;
        private static frmArticulo _Instancia;
        public static frmArticulo GetInstancia()
        { 
            if(_Instancia == null)
            {
                _Instancia = new frmArticulo();
            }
            return _Instancia;
        }

        public void setCategoria(string idcategoria, string nombre)
        {
            this.txtIdcategoria.Text = idcategoria;
            this.txtCategoria.Text = nombre;
        }

        public frmArticulo()
        {
            InitializeComponent();
            this.ttMensaje.SetToolTip(this.txtNombre, "Ingrese nombre del articulo");
            this.ttMensaje.SetToolTip(this.pxImagen,"Seleccione imagen de articulo");
            this.ttMensaje.SetToolTip(this.txtCategoria, "Seleccione categoria");
            this.ttMensaje.SetToolTip(this.cbIdpresentacion, "Seleccione presentacion");

            this.txtIdcategoria.Visible = false;
            this.txtCategoria.ReadOnly = true;
            this.LlenarComboPResentacion();
           
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
            this.txtCodigo.Text = string.Empty;
            this.txtNombre.Text = string.Empty;
            this.txtDescripcion.Text = string.Empty;
            this.txtIdcategoria.Text = string.Empty;
            this.txtCategoria.Text = string.Empty;
            this.txtIdarticulo.Text = string.Empty;
            this.pxImagen.Image = global::CapaPresentacion.Properties.Resources.file;

        }
        //HABILITAR CONTROLES
        private void Habilitar(bool valor)
        {
            this.txtCodigo.ReadOnly = !valor;
            this.txtNombre.ReadOnly = !valor;
            this.txtDescripcion.ReadOnly = !valor;
            this.btnBuscarCategoria.Enabled = valor;
            this.cbIdpresentacion.Enabled = valor;
            this.btnCargar.Enabled = valor;
            this.btnLimpiar.Enabled = valor;
            this.txtIdarticulo.ReadOnly = !valor;

        }
        //HABILITAR BOTONES
        private void Botones()
        {
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
            this.dataListado.Columns[6].Visible = false;
            this.dataListado.Columns[8].Visible = false;

        }

        //MOSTRAR
        private void Mostrar()
        {
            this.dataListado.DataSource = NArticulo.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de registros: " + Convert.ToString(dataListado.Rows.Count);

        }

        //BUSCAR POR NOMBRE
        private void BuscarNombre()
        {
            this.dataListado.DataSource = NArticulo.BuscarNombre(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de registros:" + Convert.ToString(dataListado.Rows.Count);

        }

        private void LlenarComboPResentacion()
        {
            cbIdpresentacion.DataSource = NPresentacion.Mostrar();
            cbIdpresentacion.ValueMember = "idpresentacion";
            cbIdpresentacion.DisplayMember = "nombre";
        }

        private void frmArticulo_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Mostrar();
            this.Habilitar(false);
            this.Botones();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            //Crear cuadro de dialogo
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();

            if(result == DialogResult.OK)
            {
                this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
                this.pxImagen.Image = Image.FromFile(dialog.FileName);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pxImagen.Image = global::CapaPresentacion.Properties.Resources.file;
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

                string rpta = "";
                if (this.txtNombre.Text == string.Empty || this.txtIdcategoria.Text == string.Empty || this.txtCodigo.Text == string.Empty)
                {
                    MensajeError("Falta ingresar nombre de la presentacion");
                    errorIcono.SetError(txtNombre, "Ingrese un nombre");
                    errorIcono.SetError(txtCodigo, "Ingrese un valor");
                    errorIcono.SetError(txtCategoria, "Ingrese un valor");

                }
                else
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    this.pxImagen.Image.Save(ms,System.Drawing.Imaging.ImageFormat.Png);
                    byte[] imagen = ms.GetBuffer();

                    if (this.IsNuevo)//Si la variable IsNuevo=true
                    {
                        rpta = NArticulo.Insertar(this.txtCodigo.Text,this.txtNombre.Text.Trim().ToUpper(), this.txtDescripcion.Text.Trim(), imagen, 
                            Convert.ToInt32(txtIdcategoria.Text),Convert.ToInt32(this.cbIdpresentacion.SelectedValue));
                    }
                    else //SI NO ES IsNuevo es IsEditar
                    {
                        rpta = NArticulo.Editar(Convert.ToInt32(this.txtIdarticulo.Text), this.txtCodigo.Text, this.txtNombre.Text.Trim().ToUpper(), this.txtDescripcion.Text.Trim(), imagen,
                            Convert.ToInt32(txtIdcategoria.Text), Convert.ToInt32(this.cbIdpresentacion.SelectedValue));
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!this.txtIdarticulo.Text.Equals("")) //Diferente de vacio
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
            //idpresentacion,nombre y descripcion vienen de los procedimientos almacenados en la db.
            this.txtIdarticulo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idarticulo"].Value);
            this.txtCodigo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["codigo"].Value);
            this.txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["nombre"].Value);
            this.txtDescripcion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["descripcion"].Value);

            byte[] imagenBuffer = (byte[])this.dataListado.CurrentRow.Cells["imagen"].Value;
            System.IO.MemoryStream ms = new System.IO.MemoryStream(imagenBuffer);
            this.pxImagen.Image = Image.FromStream(ms);
            this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;

            this.txtIdcategoria.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idcategoria"].Value);
            this.txtCategoria.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Categoria"].Value);
            this.cbIdpresentacion.SelectedValue = Convert.ToString(this.dataListado.CurrentRow.Cells["idpresentacion"].Value);
            /*Cuando se haga doble click en el datagrid se llenaran los campos necesarios y este se redireccionará
             al tabpage correspondiente.*/
            this.tabControl1.SelectedIndex = 1; //1 para mostrar el primer tabpage.
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
                            rpta = NArticulo.Eliminar(Convert.ToInt32(Codigo));
                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se eliminó correctamente");
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

        private void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            frmVistaCetagoria_Articulo form = new frmVistaCetagoria_Articulo();
            form.ShowDialog();
        }
    }
}
