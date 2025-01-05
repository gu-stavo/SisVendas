using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisVenda.Model
{
    class modeloCliente
    {
        private long cpf;
        private string nome;
        private string rg;
        private string endereco;
        private string telefone;
        private DateTime data_nascimento;
        private int idCidade;

        public long Cpf
        {
            get => cpf;
            set => cpf = value;
        }

        public string Nome
        {
            get => nome;
            set => nome = value;
        }

        public string Rg
        {
            get => rg;
            set => rg = value;
        }

        public string Endereco
        {
            get => endereco;
            set => endereco = value;
        }

        public string Telefone
        {
            get => telefone;
            set => telefone = value;
        }

        public DateTime Data_Nascimento
        {
            get => data_nascimento;
            set => data_nascimento = value;
        }

        public int IdCidade
        {
            get => idCidade;
            set => idCidade = value;

        }
            

    }
}
