using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisVenda.Model
{
    internal class modeloItensVenda
    {
        private int idVenda;
        private string idProduto;
        private int qtdProduto;
        private decimal valorTotal;

        public int IdVenda
        {
            get => idVenda; set => idVenda = value;
        }

        public string IdProduto
        {
            get => idProduto; set => idProduto = value;
        }

        public int QtdProduto
        {
            get => qtdProduto;
            set => qtdProduto = value;
        }

        public decimal ValorTotal
        {
            get => valorTotal; set => valorTotal = value;
        }


    }
}
