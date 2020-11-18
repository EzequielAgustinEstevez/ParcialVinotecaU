using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ParcialVinoteca
{
    public partial class frmVinos : Form
    {
        Datos oDatos = new Datos(@"Data Source=EZEQUIELEST7C22;Initial Catalog=TUP_PARCIAL_VINOTECA;Integrated Security=True Source=EZEQUIELEST7C22;Initial Catalog=TUP_PARCIAL_VINOTECA;Integrated Security=True");
        Vino[] aVino = new Vino[tam];
        const int tam = 1000;
        int c;

    public frmVinos()
        {
            InitializeComponent();
            for (int i = 0; i < tam; i++)
            {
                aVino[i] = null;
            }
        }

        private void cargarCombo(ComboBox combo, string nombreTabla)
        {
            DataTable tabla = new DataTable();

            tabla = oDatos.consultarTabla(nombreTabla);//Recorrer tabla
            combo.DataSource = tabla; //tabla
            combo.ValueMember = tabla.Columns[0].ColumnName; //ID
            combo.DisplayMember = tabla.Columns[1].ColumnName; //Nombre

            combo.DropDownStyle = ComboBoxStyle.DropDownList; //No permite edicion en la lista (Validacion)
            
        }
        private void cargarLista(string nombreTabla)
        {
            oDatos.leerTabla(nombreTabla);
            c = 0;
            while (oDatos.pLector.Read())
            {
                Vino v = new Vino();

                if (!oDatos.pLector.IsDBNull(0))
                    v.pCodigo = oDatos.pLector.GetInt32(0);
                if (!oDatos.pLector.IsDBNull(1))
                    v.pNombre = oDatos.pLector.GetString(1);
                if (!oDatos.pLector.IsDBNull(2))
                    v.pBodega = oDatos.pLector.GetInt32(2);
                if (!oDatos.pLector.IsDBNull(3))
                    v.pPresentacion = oDatos.pLector.GetInt32(3);
                if (!oDatos.pLector.IsDBNull(4))
                    v.pPrecio = oDatos.pLector.GetDouble(4);
                if (!oDatos.pLector.IsDBNull(5))
                    v.pFecha = oDatos.pLector.GetDateTime(5);

                aVino[c] = v;
                c++;
            }
            //limpiando datos
            oDatos.pLector.Close();
            oDatos.desconectar();
            lstVinos.Items.Clear();

            //cargargar items al listobox
            for (int i = 0; i < c; i++)
            {
                lstVinos.Items.Add(aVino[i].ToString());
            }
        }
        private void frmVinos_Load(object sender, EventArgs e)
        {
            cargarCombo(cboBodega,"bodegas");
            cargarLista("Vinos");
            txtCodigo.Enabled = false; //recuadro código habilitado
            lstVinos.SelectedIndex = 0;//incia con el primer item seleccionado

        }
        private void cargarCampos(int posicion)
        {
            txtCodigo.Text = aVino[posicion].pCodigo.ToString();
            txtNombre.Text = aVino[posicion].pNombre.ToString();
            cboBodega.SelectedValue = aVino[posicion].pBodega;
            if (rbt375.Checked)
                aVino[posicion].pPresentacion = 1;
            else
                aVino[posicion].pPresentacion = 2;
            txtPrecio.Text = aVino[posicion].pPrecio.ToString();
            dtpFecha.Value = aVino[posicion].pFecha;

        }

        private void lstVinos_SelectedIndexChanged(object sender, EventArgs e) //si cambia lo seleccionado se ejecuta cargar campos
        {
            cargarCampos(lstVinos.SelectedIndex);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmVinos_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro que desea salir ? ", "Saliendo",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question,
                               MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        private bool validacion() 
        {

            if (txtNombre.Text==string.Empty)
            {
                MessageBox.Show("Debe ingresar un vino");
                txtNombre.Focus();
                return false;
            }
            if (cboBodega.SelectedIndex == -1)
            {
                MessageBox.Show("Debe ingresar una bodega");
                cboBodega.Focus();
                return false;
            }
            if (rbt375.Checked == false && rbt750.Checked == false )
            {
                MessageBox.Show("Debe ingresar una presentacion");
                rbt375.Focus();
                return false;
            }
            if (txtPrecio.Text==string.Empty)
            {
                MessageBox.Show("Debe ingresar un precio");
                txtPrecio.Focus();
                return false;
            }
            if (dtpFecha.Value == DateTime.Today)
            {
                MessageBox.Show("Debe ingresar una fecha");
                dtpFecha.Focus();
                return false;
            }

            return true;

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            txtCodigo.Enabled= false;
            string consultaSQL = "";
            if (validacion())
            {
                int i = lstVinos.SelectedIndex;
                aVino[i].pNombre = txtNombre.Text;
                aVino[i].pBodega = Convert.ToInt32(cboBodega.SelectedValue);
                if (rbt375.Checked)
                    aVino[i].pPresentacion = 1;
                else
                    aVino[i].pPresentacion = 2;
                aVino[i].pPrecio = Convert.ToDouble(txtPrecio.Text);
                aVino[i].pFecha = dtpFecha.Value;

                consultaSQL = "UPDATE Vinos SET nombre =  ' " + aVino[i].pNombre + " ' , " +
                                                           "bodega = " + aVino[i].pBodega + "," +
                                                           "presentacion =  " + aVino[i].pPresentacion + "," +
                                                           "precio = " + aVino[i].pPrecio + ", " +
                                                           "fecha = '" + aVino[i].pFecha + "'" +
                                                           "WHERE codigo = " + aVino[i].pCodigo;
                oDatos.actualizarBD(consultaSQL);
                cargarLista("Vinos");
            }
        }
    }
}
