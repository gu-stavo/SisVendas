using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisVenda.Model;
using Npgsql;

namespace SisVenda.Controller
{
    internal class controllerCliente
    {
        public string cadastroCliente(modeloCliente modeloCliente)
        {
            string sql = "insert into cliente(cpf, nomecliente, rg, data_nascimento, endereco, telefone, idcidade) " +
                "values(@cpf, @nomecliente, @rg, @data_nascimento, @endereco, @telefone, @idcidade)"; //dando enter automaticamente add o + e as aspas

            Connection conexao = new Connection();

            NpgsqlConnection conn = conexao.conectarPG();

            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                comm.Parameters.AddWithValue("@cpf", modeloCliente.Cpf);
                comm.Parameters.AddWithValue("@nomecliente", modeloCliente.Nome);
                comm.Parameters.AddWithValue("@rg", modeloCliente.Rg);
                comm.Parameters.AddWithValue("@data_nascimento", modeloCliente.Data_Nascimento);
                comm.Parameters.AddWithValue("@endereco", modeloCliente.Endereco);
                comm.Parameters.AddWithValue("@telefone", modeloCliente.Telefone);
                comm.Parameters.AddWithValue("@idcidade", modeloCliente.IdCidade);
                //a ordem não importa e sim se esta igual os @

                comm.ExecuteNonQuery(); 

                return "Cliente cadastrado com sucesso!";


            }
            catch (NpgsqlException erro)
            {
                return "Erro ao cadastrar cliente!";
            }
        }

        public NpgsqlDataReader pesqClienteNome(modeloCliente modeloCliente)
        {
            string sql = "select c.nomecliente as \"Cliente\", c.cpf as \"CPF\"," +
                " c.rg as \"RG\",\r\nc.data_nascimento as \"Data de Nascimento\"," +
                " c.endereco as \"Endereço\",\r\nc.telefone as \"Telefone\", cid.nomecidade as \"Cidade\" " +
                "from cliente c inner join\r\ncidade cid on c.idCidade = cid.idcidade " +
                "where c.nomecliente like @nomecliente;";


            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectarPG();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                comm.Parameters.AddWithValue("@nomecliente", modeloCliente.Nome);
                
                return comm.ExecuteReader();

            }
            catch (NpgsqlException erro)
            {
                return null;
            }
        }

        public NpgsqlDataReader pesqClienteCpf(modeloCliente modeloCliente)
        {
            string sql = "select c.nomecliente as \"Cliente\", c.cpf as \"CPF\"," +
                " c.rg as \"RG\",\r\nc.data_nascimento as \"Data de Nascimento\"," +
                " c.endereco as \"Endereço\",\r\nc.telefone as \"Telefone\", cid.nomecidade as \"Cidade\" " +
                "from cliente c inner join\r\ncidade cid on c.idCidade = cid.idcidade " +
                "where c.cpf = @cpf";


            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectarPG();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                comm.Parameters.AddWithValue("@cpf", modeloCliente.Cpf);

                return comm.ExecuteReader();

            }
            catch (NpgsqlException erro)
            {
                return null;
            }
        }
    }
    
}
