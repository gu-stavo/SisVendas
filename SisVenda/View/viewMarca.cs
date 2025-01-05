using SisVenda.Controller;
using SisVenda.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SisVenda.View
{
    public partial class viewMarca : Form
    {
        public viewMarca()
        {
            InitializeComponent();
        }

        private void novaMarca(object sender, EventArgs e)
        {
            modeloMarca mMarca = new modeloMarca();
            controllerMarca cMarca = new controllerMarca();

            mMarca.NomeMarca = textBox1.Text.ToUpper();

            string res = cMarca.novaMarca(mMarca);

            MessageBox.Show(res);
        }

    }
}
