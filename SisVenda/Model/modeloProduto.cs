using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisVenda.Model
{
    internal class modeloProduto
    {
        private string codigoBarras;
        private string nomeProduto;
        private float precoCusto;
        private float precoVenda;
        private int qtdEstoque;
        private DateTime validade;
        private String descricao;
        private int tipoProduto;
        private int idMarca;
        private string cnpjFornecedor;

        public string CodigoBarras
        {
            get => codigoBarras;
            set => codigoBarras = value;
        }

        public string NomeProduto
        {
            get => nomeProduto;
            set => nomeProduto = value;
        }

        public float PrecoCusto
        {
            get => precoCusto;
            set => precoCusto = value;
        }

        public float PrecoVenda
        {
            get => precoVenda;
            set => precoVenda = value;
        }

        public int QtdEstoque
        {
            get => qtdEstoque;
            set => qtdEstoque = value;
        }

        public DateTime Validade
        {
            get => validade;
            set => validade = value;
        }

        public String Descricao
        {
            get => descricao;
            set => descricao = value;
        }

        public int TipoProduto {

            get => tipoProduto;
            set => tipoProduto = value;
        }

        public int IdMarca
        {
            get => idMarca;
            set => idMarca = value;
        }

        public string CnpjFornecedor
        {
            get => cnpjFornecedor;
            set => cnpjFornecedor = value;
        }
    }
}
