using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SisVenda.View;
using SisVenda.Controller;
using Npgsql;
using SisVenda.Model;
using static System.Windows.Forms.LinkLabel;

namespace SisVenda
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        /*VARIAVEIS GLOBAIS PARA VENDA*/
        decimal preco = 0, total = 0;
        int quant = 0, novaQuant = 0;
        private void novoCliente(object sender, EventArgs e)
        {
            errorProvider1.Clear(); //remove os erros para um novo cadastro 

            tabControl1.Visible = true; //deixa visível um tabControl

            abaNovoCliente.Parent = tabControl1; //vincula um tabPage a um tabControl
            tabControl1.SelectedTab = abaNovoCliente; //seleciona uma aba para uso

            abaNovoProduto.Parent = null; //desvincula um tabPage de um tabControl
            abaNovaVenda.Parent = null;
            abaNovoFornecedor.Parent = null;
            abaBuscaCliente.Parent = null;
            abaBuscaProduto.Parent = null;
            abaListarVendas.Parent = null;
            abaBuscaFornecedor.Parent = null;
        }

        private void viewCidade(object sender, LinkLabelLinkClickedEventArgs e)
        {
            viewCidade frmCidade = new viewCidade();
            frmCidade.ShowDialog(); //exibir o form na tela
        }

        private void carregarPrincipal(object sender, EventArgs e)
        {
            //evento load do form1
            carregaCombobox();
        }

        private void carregaCombobox()
        {
            /*
             propriedade da combobox - items :posso colocar item por item manualmente.
             
             
             */
            controllerCidade cCidade = new controllerCidade();

            /* NpgswlDataReader = tipo de dado que armazena o resultado de consulta (select) no banco de dados */
            NpgsqlDataReader dados = cCidade.listaCidade();

            /*uma combobox não consegue exibir os dados do datareader - formato incompativel, tem que converter em datatable
             armazena os dados em formato de tabela
            Datatable aramzena dados no formato de tabela.*/
            DataTable cidade = new DataTable();
            
            //preenche o datable com os dados do datareader, load - carregar
            cidade.Load(dados);//carregar os dados vindo do banco e joga dentro do datatable

            /*datasource - define qual a fonte de dados que a combobox vai usar*/
            comboCidade_cliente.DataSource = cidade;
            
            //DisplayMember = Define qual coluna será exibida pela combobox, qual vai ser exibida como valor da combobox
            comboCidade_cliente.DisplayMember = "nomecidade";

            //valor opcioanl, quando o susuaio selecionar uma cidade, o valor não vai ser o nome da cidade e sim o valor de sua referencia.
            //ValueMember = define qual coluna será usada como valor válido na combobox, qual valor que vou usar qual o usuario capturar o item - selecionar o item
            comboCidade_cliente.ValueMember = "idcidade";
        }

        private void atualizaCombobox(object sender, EventArgs e)
        {
            carregaCombobox();
        }

        private void cadastrarCliente(object sender, EventArgs e)
        {
            
            modeloCliente mCliente = new modeloCliente();
            controllerCliente cCliente = new controllerCliente();
            /*
               if (!String.IsNullOrWhiteSpace(maskedTextBox1.Text)) se não o conteudo do campo estiver vazio ou tem apenas espaço, passo valor para o atributo
               { 
                   mCliente.Cpf = Convert.ToInt64(maskedTextBox1.Text);
               }

            */

            /*if (String.IsNullOrWhiteSpace(maskedTextBox1.Text))
            {
                ErrorProvider.SetError(maskedTextBox1, "Preencha o campo");
                MessageBox.Show("Campo em Branco");
            }*/

            if(validaCliente())
            {
                mCliente.Cpf = Convert.ToInt64(maskedTextBox1.Text);
                mCliente.Nome = textBox1.Text;
                mCliente.Rg = textBox2.Text;
                mCliente.Endereco = textBox3.Text;
                mCliente.IdCidade = Convert.ToInt32(comboCidade_cliente.SelectedValue);
                mCliente.Data_Nascimento = dateTimePicker1.Value;
                mCliente.Telefone = maskedTextBox2.Text;

                string res = cCliente.cadastroCliente(mCliente);

                MessageBox.Show(res);
            }
        }

        private bool validaCliente()
        {
            if(String.IsNullOrWhiteSpace(textBox1.Text))//passar o campo que eu quero que verifica, verifica se é o campoe sta em branco ou só tem espaço
            {
                errorProvider1.Clear();
                textBox1.BackColor = Color.Red; //DEIXA O CAMPO COM O CAMPO DA COR ESCOLHIDA SE NÃO FOR PREENCHIDA
                MessageBox.Show("Campo Nome em branco");
                errorProvider1.SetError(textBox1, "Preencha este campo");
                return false;
            }
            else if (String.IsNullOrWhiteSpace(maskedTextBox1.Text))
            {
                errorProvider1.Clear();//LIMPAR TODOS OS ERRROS PROVIDER DE TODOS OS CAMPOD E SÓ DEIXA O ERRO NO CAMPO A BAIXO
                maskedTextBox1.BackColor = Color.Red;
                MessageBox.Show("Campo CPF em branco");
                errorProvider1.SetError(maskedTextBox1, "Preencha este campo");
                return false;
            }
            else if (String.IsNullOrWhiteSpace(textBox2.Text))
            {
                errorProvider1.Clear();
                textBox2.BackColor = Color.Red;
                MessageBox.Show("Campo RG em branco");
                errorProvider1.SetError(textBox2, "Preencha este campo");
                return false;
            }
            else if (String.IsNullOrWhiteSpace(textBox3.Text))
            {
                errorProvider1.Clear();
                textBox3.BackColor = Color.Red;
                MessageBox.Show("Campo ENDEREÇO em branco");
                errorProvider1.SetError(textBox3, "Preencha este campo");
                return false;
            }
            else if (String.IsNullOrWhiteSpace(maskedTextBox2.Text))
            {
                errorProvider1.Clear();
                maskedTextBox2.BackColor = Color.Red;
                MessageBox.Show("Campo TELEFONE em branco");
                errorProvider1.SetError(maskedTextBox2, "Preencha este campo");
                return false;
            }

            else //se o campo não estiver em branco
            {
                //errorProvider1.Clear();//VAI REMOVER TODOS OS ERROS PROVIDER DE UMA VEZ QUANDO ESTIVER PREENCHIDO TUDO
                return true;
            }
        }
        
        private void novoFornecedor(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            abaNovoFornecedor.Parent = tabControl1;
            tabControl1.SelectedTab = abaNovoFornecedor;

            abaNovoCliente.Parent = null;
            abaNovoProduto.Parent = null;
            abaNovaVenda.Parent = null;
            abaBuscaCliente.Parent = null;
            abaBuscaProduto.Parent = null;
            abaListarVendas.Parent = null;
            abaBuscaFornecedor.Parent = null;
        }

        private void carregaComboboxCidadeFornecedor()
        {

            controllerCidade cCidade = new controllerCidade();

            NpgsqlDataReader dados = cCidade.listaCidade();

            DataTable cidade = new DataTable();

            cidade.Load(dados);

            comboCidade_Fornecedor.DataSource = cidade;
            comboCidade_Fornecedor.DisplayMember = "nomecidade";
            comboCidade_Fornecedor.ValueMember = "idcidade";
        }

        private void atualizaComboboxCidadeFornecedor(object sender, EventArgs e)
        {
            carregaComboboxCidadeFornecedor();
        }
        
        private void cadastrarFornecedor(object sender, EventArgs e)
        {
            modeloFornecedor mFornecedor = new modeloFornecedor();
            controllerFornecedor cFornecedor = new controllerFornecedor();

            if (validaFornecedor())
            {
                mFornecedor.Cnpj = maskedTextBox4.Text;
                mFornecedor.NomeFornecedor = textBox4.Text;
                mFornecedor.Endereco = textBox5.Text;
                mFornecedor.Telefone = maskedTextBox3.Text;
                mFornecedor.Email = textBox7.Text;
                mFornecedor.IdCidade = Convert.ToInt32(comboCidade_Fornecedor.SelectedValue);

                string res = cFornecedor.cadastroFornecedor(mFornecedor);

                MessageBox.Show(res);
            }
        }

        private bool validaFornecedor()
        {
            if (String.IsNullOrWhiteSpace(textBox4.Text))
            {
                errorProvider1.Clear();
                textBox4.BackColor = Color.Red; 
                MessageBox.Show("Campo NOME em branco");
                errorProvider1.SetError(textBox4, "Preencha este campo");
                return false;
            }
            else if (String.IsNullOrWhiteSpace(maskedTextBox4.Text))
            {
                errorProvider1.Clear();
                maskedTextBox4.BackColor = Color.Red;
                MessageBox.Show("Campo CNPJ em branco");
                errorProvider1.SetError(maskedTextBox4, "Preencha este campo");
                return false;
            }
            else if (String.IsNullOrWhiteSpace(maskedTextBox3.Text))
            {
                errorProvider1.Clear();
                maskedTextBox3.BackColor = Color.Red;
                MessageBox.Show("Campo TELEFONE em branco");
                errorProvider1.SetError(maskedTextBox3, "Preencha este campo");
                return false;
            }
            else if (String.IsNullOrWhiteSpace(textBox7.Text))
            {
                errorProvider1.Clear();
                textBox7.BackColor = Color.Red;
                MessageBox.Show("Campo EMAIL em branco");
                errorProvider1.SetError(textBox7, "Preencha este campo");
                return false;
            }
            else if (String.IsNullOrWhiteSpace(textBox5.Text))
            {
                errorProvider1.Clear();
                textBox5.BackColor = Color.Red;
                MessageBox.Show("Campo ENDEREÇO em branco");
                errorProvider1.SetError(textBox5, "Preencha este campo");
                return false;
            }
            else 
            {
                return true;
            }
        }

        private void novoProduto(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            abaNovoProduto.Parent = tabControl1;
            tabControl1.SelectedTab = abaNovoProduto;

            abaNovoCliente.Parent = null;
            abaNovaVenda.Parent = null;
            abaNovoFornecedor.Parent = null;
            abaBuscaCliente.Parent = null;
            abaBuscaProduto.Parent = null;
            abaListarVendas.Parent = null;
            abaBuscaFornecedor.Parent = null;
        }

        private void viewTipoProduto(object sender, LinkLabelLinkClickedEventArgs e)
        {
            viewTipoProduto frmTipo = new viewTipoProduto();
            frmTipo.ShowDialog();
        }

        private void viewMarca(object sender, LinkLabelLinkClickedEventArgs e)
        {
            viewMarca frmMarca = new viewMarca();
            frmMarca.ShowDialog();
        }

        private void viewFornecedor(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Principal frmFfornecedor = new Principal();
            frmFfornecedor.ShowDialog();

        }

        private void cadastrarProduto(object sender, EventArgs e)
        {

            modeloProduto mProduto = new modeloProduto();
            controllerProduto cProduto = new controllerProduto();


            if (validaProduto())
            {
                mProduto.CodigoBarras = maskedTextBox5.Text;
                mProduto.NomeProduto = textBox6.Text;
                mProduto.Validade = dateTimePicker2.Value;
                mProduto.Descricao = textBox8.Text;
                mProduto.PrecoCusto = Convert.ToSingle(maskedTextBox6.Text);
                mProduto.PrecoVenda = Convert.ToSingle(maskedTextBox7.Text);
                mProduto.QtdEstoque = Convert.ToInt32(maskedTextBox8.Text);
                mProduto.TipoProduto = Convert.ToInt32(comboTipoProduto.SelectedValue);
                mProduto.IdMarca = Convert.ToInt32(comboMarca.SelectedValue);
                mProduto.CnpjFornecedor = Convert.ToString(comboFornecedor.SelectedValue);

                string res =  cProduto.cadastroProduto(mProduto);

                MessageBox.Show(res);
            }
        }

        private bool validaProduto()
        {
            if (String.IsNullOrWhiteSpace(maskedTextBox5.Text))
            {
                errorProvider1.Clear();
                maskedTextBox5.BackColor = Color.Red;
                MessageBox.Show("Campo CÓDIGO DE BARRAS em branco");
                errorProvider1.SetError(maskedTextBox5, "Preencha este campo");
                return false;
            }
            else if (String.IsNullOrWhiteSpace(textBox6.Text))
            {
                errorProvider1.Clear();
                textBox6.BackColor = Color.Red;
                MessageBox.Show("Campo NOME DO PRODUTO em branco");
                errorProvider1.SetError(textBox6, "Preencha este campo");
                return false;
            }
            else if (String.IsNullOrWhiteSpace(textBox8.Text))
            {
                errorProvider1.Clear();
                textBox8.BackColor = Color.Red;
                MessageBox.Show("Campo DESCRIÇÃO em branco");
                errorProvider1.SetError(textBox8, "Preencha este campo");
                return false;
            }
            else if (String.IsNullOrWhiteSpace(maskedTextBox6.Text))
            {
                errorProvider1.Clear();
                maskedTextBox6.BackColor = Color.Red;
                MessageBox.Show("Campo PREÇO DE CUSTO em branco");
                errorProvider1.SetError(maskedTextBox6, "Preencha este campo");
                return false;
            }
            else if (String.IsNullOrWhiteSpace(maskedTextBox7.Text))
            {
                errorProvider1.Clear();
                maskedTextBox7.BackColor = Color.Red;
                MessageBox.Show("Campo PREÇO DE VENDA em branco");
                errorProvider1.SetError(maskedTextBox7, "Preencha este campo");
                return false;
            }
            else if (String.IsNullOrWhiteSpace(maskedTextBox8.Text))
            {
                errorProvider1.Clear();
                maskedTextBox8.BackColor = Color.Red;
                MessageBox.Show("Campo QTD DE ESTOQUE em branco");
                errorProvider1.SetError(maskedTextBox8, "Preencha este campo");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void carregaComboboxTipoProduto()
        {

            controllerTipoProduto cTipo = new controllerTipoProduto();
            NpgsqlDataReader dados = cTipo.listaTipoProduto();
            DataTable tipoproduto = new DataTable();

            tipoproduto.Load(dados);

            comboTipoProduto.DataSource = tipoproduto;
            comboTipoProduto.DisplayMember = "nomecategoria";
            comboTipoProduto.ValueMember = "idcategoria";
        }
        private void atualizaComboboxTipo(object sender, EventArgs e)
        {
            carregaComboboxTipoProduto();
        }

        private void atualizaComboboxMarca(object sender, EventArgs e)
        {
            carregaComboboxMarca();
        }

        private void carregaComboboxMarca()
        {
            controllerMarca cMarca = new controllerMarca();
            NpgsqlDataReader dados = cMarca.listaMarca();
            DataTable marca = new DataTable();

            marca.Load(dados);

            comboMarca.DataSource = marca;
            comboMarca.DisplayMember = "nomemarca";
            comboMarca.ValueMember = "idmarca";
        }

        private void atualizaComboboxProdutoFornecedor(object sender, EventArgs e)
        {
            carregaComboboxProdutoFornecedor();
        }

        private void carregaComboboxProdutoFornecedor()
        {
            controllerFornecedor cFornecedor = new controllerFornecedor();
            NpgsqlDataReader dados = cFornecedor.listaFornecedor();
            DataTable fornecedor = new DataTable();

            fornecedor.Load(dados);

            comboFornecedor.DataSource = fornecedor;
            comboFornecedor.DisplayMember = "nomefornecedor";
            comboFornecedor.ValueMember = "cnpj";
        }

        private void consultaCliente(object sender, EventArgs e)
        {
            /*habilita somente a aba de busca de cliente*/
            #region abaBusca
            tabControl1.Visible = true;
            abaBuscaCliente.Parent = tabControl1;
            tabControl1.SelectedTab = abaBuscaCliente;

            abaNovoCliente.Parent = null;
            abaNovoProduto.Parent = null;
            abaNovaVenda.Parent = null;
            abaNovoFornecedor.Parent = null;
            abaBuscaProduto.Parent = null;
            abaListarVendas.Parent = null;
            abaBuscaFornecedor.Parent = null;
            #endregion
        }

        private void buscaCliente(object sender, EventArgs e)
        {
            /*Executa pesquisa de cliente*/
            modeloCliente mCliente = new modeloCliente();
            controllerCliente cCliente = new controllerCliente();
            NpgsqlDataReader cliente;

            if (!String.IsNullOrEmpty(txbClientePesquisa.Text))
            {
                if(radioNomeCliente.Checked)
                {
                    mCliente.Nome = txbClientePesquisa.Text + "%";
                    cliente = cCliente.pesqClienteNome(mCliente);
                    gridBusca(cliente);//método que preencha a grid com os dados do cliente
                }
                else if(radioCpfCliente.Checked)
                {
                    if(txbClientePesquisa.Text.Length == 11)//se o cpf tem todos os 11 numeros, realizar a pesquisar por cpf
                    {
                        mCliente.Cpf = long.Parse(txbClientePesquisa.Text);
                        cliente = cCliente.pesqClienteCpf(mCliente);
                        gridBusca(cliente);//método que preencha a grid com os dados do cliente
                    }
                }
                else
                {
                    cliente = null;
                }
                
            }
            else
            {
                MessageBox.Show("Não foi possivel realizar a consultar");
            }


        }

        private void maskCPF(object sender, EventArgs e)
        {
            txbClientePesquisa.Mask = "000,000,000-00";
        }

        private void maskNome(object sender, EventArgs e)
        {
            txbClientePesquisa.Mask = null;//TIRAR A MASCARA DO CPF
        }

        private void gridBusca(NpgsqlDataReader dados)
        {
            /*Apaga todas as colunas que estiver dentro da dataGridView*/
            dataGridView1.Columns.Clear();

            /*Define a quantidade de coluna da grid = DataReader. 
             * cliente.FieldCount é uma propriedade do ... que conta a qtd de colunas que minha consulta retornou
             minha tabela sempre vai se adaptar ao tamanho da consulta auto.
            dataGridView1.ColumnCount = 7; manualmente, vai mostrar 7 colunas;
            */
            dataGridView1.ColumnCount = dados.FieldCount;

            /*Definir os nomes das colunas da grid - define os nomes dos cabeçalhos na coluna
             uma laço que vai percorrer minhas colunas e retornar os nomes delas
            VAI DO 0 ATÉ O FINAL DA COLUNA. vai 
            */
            for (int i = 0; i < dados.FieldCount; i++)
            {
                dataGridView1.Columns[i].Name = dados.GetName(i);
            }

            /**/
            string[] linha = new string[dados.FieldCount];

            while (dados.Read())
            {
                for (int i = 0; i < dados.FieldCount; i++)
                {
                    linha[i] = dados.GetValue(i).ToString();
                }
                dataGridView1.Rows.Add(linha);
            }
        }

        private void consultaProduto(object sender, EventArgs e)
        {
            #region abaBusca
            tabControl1.Visible = true;
            abaBuscaProduto.Parent = tabControl1;
            tabControl1.SelectedTab = abaBuscaProduto;

            abaNovoCliente.Parent = null;
            abaNovoProduto.Parent = null;
            abaNovaVenda.Parent = null;
            abaNovoFornecedor.Parent = null;
            abaBuscaCliente.Parent = null;
            abaListarVendas.Parent = null;
            abaBuscaFornecedor.Parent = null;
            #endregion
        }

        private void buscaProduto(object sender, EventArgs e)
        {
            modeloProduto mProduto = new modeloProduto();
            controllerProduto cProduto = new controllerProduto();
            NpgsqlDataReader produto;
            
            if (!String.IsNullOrEmpty(txbProdutoPesquisa.Text))
            {
                if (radioNomeProduto.Checked)
                {
                    mProduto.NomeProduto = txbProdutoPesquisa.Text + "%";
                    produto = cProduto.pesqProduto(mProduto);
                    gridBuscaProduto(produto);
                }
                else
                {
                    produto = null;
                }

            }
            else
            {
                MessageBox.Show("Não foi possivel realizar a consultar");
            }
        }

        private void gridBuscaProduto(NpgsqlDataReader dados)
        {
            dataGridView2.Columns.Clear();
            dataGridView2.ColumnCount = dados.FieldCount;

            for (int i = 0; i < dados.FieldCount; i++)
            {
                dataGridView2.Columns[i].Name = dados.GetName(i);
            }

            /**/
            string[] linha = new string[dados.FieldCount];

            while (dados.Read())
            {
                for (int i = 0; i < dados.FieldCount; i++)
                {
                    linha[i] = dados.GetValue(i).ToString();
                }
                dataGridView2.Rows.Add(linha);
            }
        }

        private void novaVenda(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            abaNovaVenda.Parent = tabControl1;
            abaBuscaCliente.Parent = tabControl1;
            abaBuscaProduto.Parent = tabControl1;
            tabControl1.SelectedTab = abaNovaVenda;

            abaNovoCliente.Parent = null;
            abaNovoProduto.Parent = null;
            abaNovoFornecedor.Parent = null;
            abaListarVendas.Parent = null;
            abaBuscaFornecedor.Parent = null;
        }

        //OBJETO KeyPressEventArgs e: se a tecla que o cara clicou 13 - codigo 13 que é enter, realiza o if.
        private void buscaCPFcliente(object sender, KeyPressEventArgs e)
        {
            modeloCliente mCliente = new modeloCliente();
            controllerCliente cCliente = new controllerCliente();

            if(maskCPFClienteVenda.Text.Length == 11)
            {
                if (e.KeyChar == 13)//13=enter
                {
                    mCliente.Cpf = long.Parse(maskCPFClienteVenda.Text);//tipo long não pode ter ponto ou traço - usar propriedade textmask: exludeprompt and literals.
                    NpgsqlDataReader cliente = cCliente.pesqClienteCpf(mCliente);

                    if(!cliente.HasRows)//dentro do cliente se não tiver linhas- não retornou linhas depois da pesquisa do cpf, esta em branco não encontrei o clinete ou não há cpd cadastrado, se tiver encontrei o cliente.
                    {
                        MessageBox.Show("Cliente não encontrado!");
                    }else
                    {
                        while(cliente.Read())//conseguir ler 
                        {
                            txbClienteVenda.Text = cliente.GetValue(0).ToString();
                        }//getvalue: pega valor da grid. linha 0
                    }
                    /*faz uma busca no bd pleo cpg, caso o cliente não exista vai retornar bulo. */
                }
            }
        }

        private void buscaProdutoVenda(object sender, KeyPressEventArgs e)
        {
            modeloProduto mProduto = new modeloProduto();
            controllerProduto cProduto = new controllerProduto();

            if(e.KeyChar == 13)
            {
                /*se seleciona */
                if(rbcodigoProduto.Checked)
                {
                    mProduto.CodigoBarras = txbProdutoVenda.Text;
                    mProduto.NomeProduto = "null%";
                }
                else if(rbnomeProduto.Checked)
                {
                    mProduto.CodigoBarras = "null";
                    mProduto.NomeProduto = txbProdutoVenda.Text + "%";
                }
                NpgsqlDataReader produto = cProduto.listaProdutoVenda(mProduto);

                if (!produto.HasRows)
                {
                    MessageBox.Show("Produto não encontrado!");
                }
                else
                {
                    gridProdutoVenda(produto);
                }
            }
        }

        private void gridProdutoVenda(NpgsqlDataReader dados)
        {
            dataGridView4.Columns.Clear();
            dataGridView4.ColumnCount = dados.FieldCount;

            for (int i = 0; i < dados.FieldCount; i++)
            {
                dataGridView4.Columns[i].Name = dados.GetName(i).ToUpper();
            }
            string[] linha = new string[dados.FieldCount];
            while (dados.Read())
            {
                for (int i = 0; i < dados.FieldCount; i++)
                {
                    linha[i] = dados.GetValue(i).ToString();
                }
                dataGridView4.Rows.Add(linha);
            }
        }


        private void addItensVenda(object sender, DataGridViewCellEventArgs e)
        {
            /*ADICIONA ITEM A VENDA
             CurrentRow - pega a linha que o cara clicou*/
            string[] produto = new string[4];
            produto[0] = dataGridView4.CurrentRow.Cells[0].Value.ToString();//codigo
            produto[1] = dataGridView4.CurrentRow.Cells[1].Value.ToString();//nome
            produto[2] = dataGridView4.CurrentRow.Cells[2].Value.ToString();//preco
            produto[3] = "1";//quantidade

            /*CALCULA E ATUALIZA A VENDA*/
            preco = decimal.Parse(produto[2]);
            quant = Convert.ToInt32(produto[3]);
            total = decimal.Parse(lbTotalItens.Text) + (preco * quant);

            dataGridView5.Rows.Add(produto);
            lbTotalItens.Text = total.ToString();
            lbTotalVenda.Text = total.ToString();

        }

        private void selecionaLinha(object sender, DataGridViewCellEventArgs e)
        {
            //salva a quantidade atual de um item selecionado
            quant = Convert.ToInt32(dataGridView5.CurrentRow.Cells[3].Value);
        }

        

        /*quando mudar a quantidade: era 1 e agora 3. faço a quantidade - nova venda 3 -1 = 2 , 
* ai somo 2 com a quantidade, diferença de quanto tinha para quanto foi. se quanti aumentou 
* é uma subtraçaõ se dimiunuir é uma soma????????????????*/

        private void removerItem(object sender, EventArgs e)
        {
            /*Quanto a contagem de linhas for maior que 0, entra no if
             yes/no - tipos de botoes. MessageBoxButton.vai mostrar varios tipos. o botao que for escolhido vai ser guardado dentro do confirm
            */

            if(dataGridView5.RowCount > 0) { 
                DialogResult confirm = MessageBox.Show("Remover item",
                "Deseja remover este item?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if(confirm == DialogResult.Yes)//se clicar em sim
                {
                    novaQuant = Convert.ToInt32(dataGridView5.CurrentRow.Cells[3].Value); //pega o valor da coluna quantidade
                    preco = decimal.Parse(dataGridView5.CurrentRow.Cells[2].Value.ToString()); //pegar o valor da coluna preco

                    total = total - (novaQuant * preco); //calculo do total
                    lbTotalItens.Text = total.ToString();//atualiza o total
                    lbTotalVenda.Text = total.ToString();//atualiza o total
                    dataGridView5.Rows.Remove(dataGridView5.CurrentRow); //apaga a linha
                }
            }

        }

        private void calculaDesconto(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                total = decimal.Parse(lbTotalItens.Text);
                decimal desc = decimal.Parse(txbDesc.Text) / 100;
                decimal totalVenda = total - (total * desc);
                lbTotalVenda.Text = totalVenda.ToString();
            }
        }

        

        private void atualizaTotal(object sender, DataGridViewCellEventArgs e)
        {
            novaQuant = Convert.ToInt32(dataGridView5.CurrentRow.Cells[3].Value);
            preco = decimal.Parse(dataGridView5.CurrentRow.Cells[2].Value.ToString());

            if (novaQuant > 0)
            {
                if (novaQuant > quant)
                {
                    total = total + ((novaQuant - quant) * preco);
                }
                if (novaQuant < quant)
                {
                    total = total - ((quant - novaQuant) * preco);
                }
                quant = novaQuant;
                lbTotalItens.Text = total.ToString();
                lbTotalVenda.Text = total.ToString();
            }
            else
            {
                dataGridView5.CurrentRow.Cells[3].Value = quant.ToString();
            }
            novaQuant = 0;
        }

        private void cadastrarVenda(object sender, EventArgs e)
        {
            modeloVenda mVenda = new modeloVenda();
            controllerVenda cVenda = new controllerVenda();

            modeloItensVenda mItens = new modeloItensVenda();
            controllerItemVenda cItens = new controllerItemVenda();

            if (!String.IsNullOrEmpty(txbClienteVenda.Text)) 
            {
                if(dataGridView4.Rows.Count > 0)
                {
                    /*dados do cliente e data da venda*/
                    mVenda.CpfCliente = long.Parse(maskCPFClienteVenda.Text);
                    mVenda.DataVenda = DateTime.Now;//pega o horario do computador no agora

                    //Insere uma nova venda
                    NpgsqlDataReader venda = cVenda.cadastroVenda(mVenda);

                    //obtém o id da venda gerada na linha anterior
                    while (venda.Read())
                    {
                        mItens.IdVenda = Convert.ToInt32(venda.GetValue(0));
                        MessageBox.Show(mItens.IdVenda.ToString());
                    }

                    //precorre a grid de itens e insere no banco.rowcount: devolve a qunatidade de linhas
                    for (int l = 0; l < dataGridView5.RowCount; l++)
                    {
                        mItens.IdProduto = dataGridView5.Rows[l].Cells[0].Value.ToString();
                        mItens.QtdProduto = Convert.ToInt32(dataGridView5.Rows[l].Cells[3].Value);

                        mItens.ValorTotal = mItens.QtdProduto *
                            decimal.Parse(dataGridView5.Rows[l].Cells[2].Value.ToString());

                        MessageBox.Show(cItens.inserirItemVenda(mItens));

                    }

                    //finaliza venda, atualizando o valor total
                    mVenda.IdVenda = mItens.IdVenda;
                    mVenda.TotalVenda = decimal.Parse(lbTotalVenda.Text);
                    MessageBox.Show(cVenda.atualizaTotalVenda(mVenda));
                }
                else
                {
                    MessageBox.Show("Não há itens da venda!");
                }

            }
            else
            {
                MessageBox.Show("Nenhum cliente selecionado!");
            }

        }

        

        private void consultaVenda(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            abaListarVendas.Parent = tabControl1;
            abaBuscaCliente.Parent = tabControl1;

            abaNovoCliente.Parent = null;
            abaBuscaProduto.Parent = null;
            abaNovoProduto.Parent = null;
            abaNovoFornecedor.Parent = null;
            abaNovaVenda.Parent = null;
            abaBuscaFornecedor.Parent = null;
        }

        private void pesquisarClienteListaVenda(object sender, EventArgs e)
        {
            modeloCliente mCliente = new modeloCliente();
            controllerCliente cCliente = new controllerCliente();
            NpgsqlDataReader cliente;

            if (!String.IsNullOrEmpty(txbClienteVendaPesquisa.Text))
            {
                /*se seleciona */
                if (rbNomeClienteVenda.Checked)
                {
                    mCliente.Nome = txbClienteVendaPesquisa.Text + "%";
                    cliente = cCliente.pesqClienteNome(mCliente);
                    gridBuscaCVenda(cliente);//método que preencha a grid com os dados do cliente
                }
                else if (rbCpfClienteVenda.Checked)
                {
                    if (txbClienteVendaPesquisa.Text.Length == 11)//se o cpf tem todos os 11 numeros, realizar a pesquisar por cpf
                    {
                        mCliente.Cpf = long.Parse(txbClientePesquisa.Text);
                        cliente = cCliente.pesqClienteCpf(mCliente);
                        gridBuscaCVenda(cliente);//método que preencha a grid com os dados do cliente
                    }
                }

            }
        }

        private void gridBuscaCVenda(NpgsqlDataReader dados)
        {
            dataGridView6.Columns.Clear();
            dataGridView6.ColumnCount = dados.FieldCount;

            for (int i = 0; i < dados.FieldCount; i++)
            {
                dataGridView6.Columns[i].Name = dados.GetName(i).ToUpper();
            }
            string[] linha = new string[dados.FieldCount];
            while (dados.Read())
            {
                for (int i = 0; i < dados.FieldCount; i++)
                {
                    linha[i] = dados.GetValue(i).ToString();
                }
                dataGridView6.Rows.Add(linha);
            }
        }

        private void mostrarVendas(object sender, DataGridViewCellEventArgs e)
        {

            modeloVenda mVenda = new modeloVenda();
            controllerVenda cVenda = new controllerVenda();

            mVenda.CpfCliente = Convert.ToInt64(dataGridView6.CurrentRow.Cells[1].Value.ToString());

            NpgsqlDataReader venda = cVenda.pesqVendaCliente(mVenda);
            gridlistavenda(venda);
        }

        private void gridlistavenda(NpgsqlDataReader vendas)
        {
            dataGridView7.Columns.Clear();
            dataGridView7.ColumnCount = vendas.FieldCount;

            for (int i = 0; i < vendas.FieldCount; i++)
            {
                dataGridView7.Columns[i].Name = vendas.GetName(i).ToUpper();
            }
            string[] linha = new string[vendas.FieldCount];
            while (vendas.Read())
            {
                for (int i = 0; i < vendas.FieldCount; i++)
                {
                    linha[i] = vendas.GetValue(i).ToString();
                }
                dataGridView7.Rows.Add(linha);
            }
        }

        private void mostrarItensVendas(object sender, DataGridViewCellEventArgs e)
        {

            modeloItensVenda mItemvenda = new modeloItensVenda();
            controllerItemVenda cItemvenda = new controllerItemVenda();
            
            mItemvenda.IdVenda = Convert.ToInt32(dataGridView7.CurrentRow.Cells[0].Value.ToString());

            NpgsqlDataReader venda = cItemvenda.pesqItemVenda(mItemvenda);
            gridlistaItensvenda(venda);
        }

        private void gridlistaItensvenda(NpgsqlDataReader vendas)
        {
            dataGridView8.Columns.Clear();
            dataGridView8.ColumnCount = vendas.FieldCount;

            for (int i = 0; i < vendas.FieldCount; i++)
            {
                dataGridView8.Columns[i].Name = vendas.GetName(i).ToUpper();
            }
            string[] linha = new string[vendas.FieldCount];
            while (vendas.Read())
            {
                for (int i = 0; i < vendas.FieldCount; i++)
                {
                    linha[i] = vendas.GetValue(i).ToString();
                }
                dataGridView8.Rows.Add(linha);
            }
        }

    }
}
