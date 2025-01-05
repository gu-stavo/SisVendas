using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace SisVenda.Controller
{
    internal class Connection
    {
        static string Server = "localhost";
        static string Port = "5432";
        static string Database = "SisVendas";
        static string User = "postgres";
        static string Password = "123456";

        NpgsqlConnection conn = null;

        string connString = "Server=" + Server + ";Port=" + Port +
        ";UserID=" + User + ";password=" + Password + ";Database=" + Database + ";";

        public NpgsqlConnection conectarPG()
        {
            try
            {
                conn = new NpgsqlConnection(connString);
                conn.Open();
                return conn; /*Vai retornar se tem uma conexao com o banco ou não*/
            }
            catch (NpgsqlException erro)
            {
                return null;
            } 
        }

        public NpgsqlConnection desconectarPG()
        {
            conn.Close();
            return null;

        }

       
    }
}
