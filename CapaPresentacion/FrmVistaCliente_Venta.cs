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
    public partial class FrmVistaCliente_Venta : Form
    {
        public FrmVistaCliente_Venta()
        {
            InitializeComponent();
        }

        private void FrmVistaCliente_Venta_Load(object sender, EventArgs e)
        {
            Mostrar();
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
            this.dataListado.DataSource = NCliente.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de registros: " + Convert.ToString(dataListado.Rows.Count);

        }

        //BUSCAR POR APELLIDO
        private void BuscarApellidos()
        {
            this.dataListado.DataSource = NCliente.BuscarApellidos(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de registros:" + Convert.ToString(dataListado.Rows.Count);

        }

        //BUSCAR POR NUMERO DE DOCUMENTO
        private void BuscarNum_Documento()
        {
            this.dataListado.DataSource = NCliente.BuscarNum_Documento(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de registros:" + Convert.ToString(dataListado.Rows.Count);

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cbBuscar.Text.Equals("Apellidos"))
            {
                this.BuscarApellidos();
            }
            else if (cbBuscar.Text.Equals("Documento"))
            {
                this.BuscarNum_Documento();
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            FrmVenta form = FrmVenta.GetInstancia();
            string par1, par2;

            par1 = Convert.ToString(this.dataListado.CurrentRow.Cells["idcliente"].Value);
            par2 = Convert.ToString(this.dataListado.CurrentRow.Cells["apellidos"].Value)+" "+ Convert.ToString(this.dataListado.CurrentRow.Cells["nombre"].Value);

            form.setCliente(par1, par2);
            this.Hide();
        }
    }
}
