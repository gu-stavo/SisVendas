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
    public partial class viewTipoProduto : Form
    {
        public viewTipoProduto()
        {
            InitializeComponent();
        }

        private void novoTipoProduto(object sender, EventArgs e)
        {
            modeloCategoria mTipoProduto = new modeloCategoria();
            controllerTipoProduto cTipoProduto = new controllerTipoProduto();

            mTipoProduto.NomeCategoria = textBox1.Text.ToUpper();

            string res = cTipoProduto.novoTipoProduto(mTipoProduto);

            MessageBox.Show(res);   
        }
    }
}
