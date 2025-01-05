using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisVenda.Model;
using Npgsql;

namespace SisVenda.Controller
{
    internal class controllerFornecedor
    {
        public string cadastroFornecedor(modeloFornecedor modeloFornecedor)
        {
            string sql = "insert into fornecedor(cnpj, nomefornecedor, endereco, telefone, email, idcidade) " +
                "values(@cnpj, @nomefornecedor, @endereco, @telefone, @email, @idcidade)";

            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectarPG();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                comm.Parameters.AddWithValue("@cnpj", modeloFornecedor.Cnpj);
                comm.Parameters.AddWithValue("@nomefornecedor", modeloFornecedor.NomeFornecedor);
                comm.Parameters.AddWithValue("@endereco", modeloFornecedor.Endereco);
                comm.Parameters.AddWithValue("@telefone", modeloFornecedor.Telefone);
                comm.Parameters.AddWithValue("@email", modeloFornecedor.Email);
                comm.Parameters.AddWithValue("@idcidade", modeloFornecedor.IdCidade);

                comm.ExecuteNonQuery();
                return "Fornecedor cadastrado com sucesso!";
            }
            catch (NpgsqlException erro)
            {
                return "Erro ao cadastrar Fornecedor!";
            }
        }

        public NpgsqlDataReader listaFornecedor()
        {
            string sql = "select * from fornecedor";
            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectarPG();
            NpgsqlCommand comm = new NpgsqlCommand( sql, conn);

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
