using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SisVenda.Controller;
using SisVenda.Model;
using Npgsql;

namespace SisVenda.View
{
    public partial class viewCidade : Form
    {
        public viewCidade()
        {
            InitializeComponent();
        }

        private void novaCidade(object sender, EventArgs e)
        {
            //cria dois objetos.
            modeloCidade mCidade = new modeloCidade();
            controllerCidade cCidade = new controllerCidade();

            //armazenar os dados do FORM nos atributos. se tenho mais valores coloco todos os valores os 10 valores.
            mCidade.NomeCidade = textBox1.Text.ToUpper(); //lugar onde será inserido tal valor.


            //envia os dados para o banco(método de cadastro)
            string res = cCidade.novaCidade(mCidade);

            MessageBox.Show(res);

        }
    }
}
