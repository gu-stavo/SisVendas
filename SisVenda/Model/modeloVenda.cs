using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisVenda.Model
{
    internal class modeloVenda
    {
        private int idVenda;
        private DateTime dataVenda;
        private decimal totalVenda;
        private long cpfCliente;

        public int IdVenda
        {
            get => idVenda; set => idVenda = value;
        }
        
        public DateTime DataVenda
        {
            get => dataVenda; set => dataVenda = value;
        }

        public decimal TotalVenda
        {
            get => totalVenda; set => totalVenda = value;
        }

        public long CpfCliente
        {
            get => cpfCliente; set => cpfCliente = value;
        }
    }

}
