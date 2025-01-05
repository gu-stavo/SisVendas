using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisVenda.Model;
using Npgsql;

namespace SisVenda.Controller
{
    internal class controllerProduto
    {
        public string cadastroProduto(modeloProduto modeloProduto)
        {
            string sql = "insert into produto(codigobarras, nomeproduto, precocusto, precovenda, qtdestoque, data_validade, descricao, idcategoria, idmarca, cnpjfornecedor) " +
                "values(@codigobarras, @nomeproduto, @precocusto, @precovenda, @qtdestoque, @data_validade, @descricao, @idcategoria, @idmarca, @cnpjfornecedor)";

            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectarPG();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                comm.Parameters.AddWithValue("@codigobarras", modeloProduto.CodigoBarras);
                comm.Parameters.AddWithValue("@nomeproduto", modeloProduto.NomeProduto);
                comm.Parameters.AddWithValue("@precocusto", modeloProduto.PrecoCusto);
                comm.Parameters.AddWithValue("@precovenda", modeloProduto.PrecoVenda);
                comm.Parameters.AddWithValue("@qtdestoque", modeloProduto.QtdEstoque);
                comm.Parameters.AddWithValue("@data_validade", modeloProduto.Validade);
                comm.Parameters.AddWithValue("@descricao", modeloProduto.Descricao);
                comm.Parameters.AddWithValue("@idcategoria", modeloProduto.TipoProduto);
                comm.Parameters.AddWithValue("@idmarca", modeloProduto.IdMarca);
                comm.Parameters.AddWithValue("@cnpjfornecedor", modeloProduto.CnpjFornecedor);

                comm.ExecuteNonQuery();
                return "Produto cadastrado com sucesso!";
            }
            catch (NpgsqlException erro)
            {
                return erro.ToString();
                /*return "Erro ao cadastrar Produto!";*/
            }
        }

        public NpgsqlDataReader pesqProduto(modeloProduto modeloProduto)
        {
            string sql = "select produto.codigobarras, produto.nomeproduto, produto.precocusto, " +
                "produto.precovenda, produto.qtdestoque, produto.data_validade,\r\nproduto.descricao, " +
                "categoria.nomecategoria, marca.nomemarca, fornecedor.nomefornecedor\r\n" +
                "from produto \r\ninner join categoria on produto.idcategoria = categoria.idcategoria\r\n" +
                "inner join marca on produto.idmarca = marca.idmarca\r\n" +
                "inner join fornecedor on produto.cnpjfornecedor = fornecedor.cnpj\r\n" +
                "where produto.nomeproduto like @nomeproduto";


            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectarPG();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                comm.Parameters.AddWithValue("@nomeproduto", modeloProduto.NomeProduto);

                return comm.ExecuteReader();

            }
            catch (NpgsqlException erro)
            {
                return null;
            }
        }

        public NpgsqlDataReader listaProdutoVenda(modeloProduto modeloProduto)
        {
            string sql = "select codigobarras as CodigoBarras, nomeproduto as Produto, precovenda as Preco, qtdestoque as Quantidade" +
                " from produto where codigobarras = @codigobarras or nomeproduto like @nomeproduto;";


            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectarPG();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                comm.Parameters.AddWithValue("@nomeproduto", modeloProduto.NomeProduto);
                comm.Parameters.AddWithValue("@codigobarras", modeloProduto.CodigoBarras);

                return comm.ExecuteReader();//execução no bd

            }
            catch (NpgsqlException erro)
            {
                return null;
            }
        }

        
    }
}
