using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisVenda.Model;
using SisVenda.Controller;
using Npgsql;
using System.Security.Cryptography.X509Certificates;

namespace SisVenda.Controller
{
    class controllerCidade
    {
        //criar um objeto para acessar os atributos
        public string novaCidade (modeloCidade mCidade)
        {
            //string que recebe o código que será executado no servidor
            string sql = "insert into cidade(nomecidade) values(@nomecidade)"; //@não sei os valores

            //objeto de classe Connection que possui os métodos de conectar ao BD
            Connection conexao = new Connection();

            //objeto da classe NpgsqlConnection que mantém a conexao com o BD. faz a conexao e mantem conectado
            NpgsqlConnection conn = conexao.conectarPG();

            //objeto da classe NpgsqlCommand que executa comandos SQL no BD. eexecutar do sistema no servidor. precisa de 2 coisas o codigo que vou executar e comando que vou usar para conectar com o bd.
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            //padrão em outras as controller só muda o comando sql. o resto mantem. e embaixo do comando command usa o objeto criado.

            try
            {
                //valores para cadastrar,alterar, excluir. passagem de valores (parâmetros) para o BD
                comm.Parameters.AddWithValue("@nomecidade", mCidade.NomeCidade);
                //vai passar os valores e vai adicionar um valor e vai trocar o @nomeciade para o valor que eu quero.

                //executar o comando sql no BD. nonquery não é necessarios voltar os dados. é usado para insert, update e delete
                comm.ExecuteNonQuery(); //ExecuteReader vai retornar os valores da tabela - select.
                return "Cidade cadastrada com sucesso!";

            }
            catch(NpgsqlException erro)//variavel erro é uma exeception
            {
                //return erro.ToString(); //retorna o erro do banco
                return "Erro ao cadastrar cidade!";//para usuario uma mensagem generica
            }

            
        }

        public NpgsqlDataReader listaCidade()
        {
            /*depois do insert o bd não retorna os dados e sim uma mensagem. já o select retorna os dados. 
             datareader é um tipo de dados especifico para armazenar , vai fazer um espelho do banco de dados
            os selects são personalisados conforme a necessidade
            */

            string sql = "select * from cidade"; //não tem @, não vou mandar dados.
            Connection conexao = new Connection();
            NpgsqlConnection conn = conexao.conectarPG();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);

            try
            {
                //executar o comando SQL no BD. retorna o coamndo e ao mesmo tempo executa o reader.
                return comm.ExecuteReader(); 
            }
            catch (NpgsqlException erro)
            {
                return null;//não tem informaçaõ
            }
        }
    }
}
