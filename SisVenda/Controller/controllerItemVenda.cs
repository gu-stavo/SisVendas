using Npgsql;
using SisVenda.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SisVenda.Controller
{
    internal class controllerItemVenda
    {
        //se precisar de um retorno coloca um tipo se não precisa colocar void
        public string inserirItemVenda(modeloItensVenda mItensVenda)
        {
            string sql = "insert into itensvenda(idvenda,codigobarras,qtdproduto,valortotal)" +
                "values(@idvenda,@codigobarras,@qtdproduto,@valortotal);";

            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectarPG();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                comm.Parameters.AddWithValue("@idvenda", mItensVenda.IdVenda);
                comm.Parameters.AddWithValue("@codigobarras", mItensVenda.IdProduto);
                comm.Parameters.AddWithValue("@qtdproduto", mItensVenda.QtdProduto);
                comm.Parameters.AddWithValue("@valortotal", mItensVenda.ValorTotal);

                comm.ExecuteNonQuery();
                return "Item adicionado!";
            }
            catch (NpgsqlException erro)
            {
                return null;
            }
        }

        public NpgsqlDataReader pesqItemVenda(modeloItensVenda modeloItensVenda)
        {
            string sql = "select produto.nomeproduto, itensvenda.qtdproduto, itensvenda.valortotal from itensvenda " +
                "inner join venda on itensvenda.idvenda = venda.idvenda " +
                "inner join produto on itensvenda.codigobarras = produto.codigobarras " +
                "where itensvenda.idvenda = @idvenda ";


            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectarPG();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                comm.Parameters.AddWithValue("@idvenda", modeloItensVenda.IdVenda);

                return comm.ExecuteReader();

            }
            catch (NpgsqlException erro)
            {
                return null;
            }
        }
    }
}
