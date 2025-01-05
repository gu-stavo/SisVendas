using Npgsql;//importar para conseguir manipular o banco
using SisVenda.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SisVenda.Controller
{
    internal class controllerVenda
    {
        public NpgsqlDataReader cadastroVenda(modeloVenda mVenda)
        {
            string sql = "insert into venda(cpfcliente,datavenda)" +
                "values(@cpfcliente,@datavenda) returning idvenda;";

            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectarPG();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                comm.Parameters.AddWithValue("@cpfcliente", mVenda.CpfCliente);
                comm.Parameters.AddWithValue("@datavenda", mVenda.DataVenda);

                return comm.ExecuteReader();
                //return "Venda cadastrada com sucesso!";
            }
            catch (NpgsqlException ex)
            {
                return null;
            }
        }

        public string atualizaTotalVenda(modeloVenda mVenda)
        {
            string sql = "update venda set totalvenda = @totalvenda where idvenda = @idvenda;";

            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectarPG();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                comm.Parameters.AddWithValue("@idvenda", mVenda.IdVenda);
                comm.Parameters.AddWithValue("@totalvenda", mVenda.TotalVenda);

                comm.ExecuteReader();
                return "Venda finalizada!";
            }
            catch (NpgsqlException ex)
            {
                return null;
            }
        }

        public NpgsqlDataReader pesqVendaCliente(modeloVenda modeloVenda)
        {
            string sql = "select venda.idvenda, venda.datavenda, venda.totalvenda from venda " +
                "inner join cliente on cliente.cpf = venda.cpfcliente where " +
                "cliente.cpf = @cpf; ";


            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectarPG();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                comm.Parameters.AddWithValue("@cpf", modeloVenda.CpfCliente);

                return comm.ExecuteReader();

            }
            catch (NpgsqlException erro)
            {
                return null;
            }
        }
    }
}
