using SisVenda.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;


namespace SisVenda.Controller
{
    internal class controllerTipoProduto
    {
        public string novoTipoProduto (modeloCategoria mTipoProduto)
        {
            string sql = "insert into categoria(nomecategoria) values(@nomecategoria)";

            Connection conexao = new Connection();

            NpgsqlConnection conn = conexao.conectarPG();

            NpgsqlCommand comm = new NpgsqlCommand (sql, conn);

            try
            {
                comm.Parameters.AddWithValue("@nomecategoria", mTipoProduto.NomeCategoria);
                comm.ExecuteNonQuery();
                return "Tipo Produto cadastrado com sucesso!";
            }
            catch(NpgsqlException erro)
            {
                return "Erro ao cadastrar Tipo Produto!";
            }

        }

        public NpgsqlDataReader listaTipoProduto()
        {
            string sql = "select * from categoria";
            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectarPG();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                return comm.ExecuteReader();
            }
            catch (NpgsqlException erro)
            {
                return null;
            }
        }
    }
}
