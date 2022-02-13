using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmReporteFactura : Form
    {
        private int _Idventa;
        //METODOS SETER Y GETER
        public int Idventa { get => _Idventa; set => _Idventa = value; }
        public FrmReporteFactura()
        {
            InitializeComponent();
        }

       

        private void FrmReporteFactura_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: esta línea de código carga datos en la tabla 'dsPrincipal.spreporte_factura' Puede moverla o quitarla según sea necesario.
                this.spreporte_facturaTableAdapter.Fill(this.dsPrincipal.spreporte_factura,Idventa);
                
            }
            catch(Exception ex)
            {
                // MessageBox.Show(ex.Message + ex.StackTrace);
                this.reportViewer1.RefreshReport();
            }
        }
    }
}
