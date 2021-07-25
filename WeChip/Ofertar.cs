using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static WeChip.Status;

namespace WeChip
{
    public partial class Ofertar : Form
    {
        private List<Cliente> clientes;
        private List<Oferta> ofertas = new List<Oferta>();
        Cliente cliente_selecionado = null;

        public Ofertar()
        {
            InitializeComponent();
        }

        public Ofertar(List<Cliente> clientes, List<Oferta> ofertas)
        {
            this.clientes = clientes;
            this.ofertas = ofertas;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CarregarComboStatus();
            CarregarProdutos();
        }

        private void CarregarProdutos()
        {
            DataTable produtos = new DataTable();
            produtos.Columns.Add("Selecionar", typeof(bool));
            produtos.Columns.Add("Produto", typeof(string));
            produtos.Columns.Add("Tipo", typeof(string));
            produtos.Columns.Add("Valor", typeof(string));
            produtos.Columns["Selecionar"].DefaultValue = false;

            foreach (var item in Produtos.produtos)
            {
                DataRow linha = produtos.NewRow();
                linha["Produto"] = item.Key.ObterDescricao();
                linha["Tipo"] = item.Value[1].ToString();
                linha["Valor"] = Convert.ToDouble(item.Value[0]).ToString("C2");
                produtos.Rows.Add(linha);

            }
            gdvProdutos.DataSource = produtos;
        }

        private void CarregarComboStatus()
        {
            cbbStatus.DataSource = Enumerados.EnumParaLista<Enumerados.Status>();
            cbbStatus.DisplayMember = "Value";
            cbbStatus.ValueMember = "Key";
            cbbStatus.SelectedIndex = -1;
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Se a tecla digitada não for número
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                //Atribui True no Handled para cancelar o evento
                e.Handled = true;
            }
        }

        private void txtBairro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {

                e.Handled = true;

            }
        }

        private void txtCidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {

                e.Handled = true;

            }
        }

        private void txtEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {

                e.Handled = true;

            }
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            AutoCompleteStringCollection data = new AutoCompleteStringCollection();
            foreach (var item in clientes)
            {
                if (!FinalizaCliente(item.Status))
                    data.Add(item.Nome);
            }
            txtCliente.AutoCompleteCustomSource = data;
        }

        private void txtCPF_TextChanged(object sender, EventArgs e)
        {
            AutoCompleteStringCollection data = new AutoCompleteStringCollection();
            foreach (var item in clientes)
            {
                if (!FinalizaCliente(item.Status))
                    data.Add(item.Cpf);
            }
            txtCPF.AutoCompleteCustomSource = data;
        }

        private void txtCliente_Leave(object sender, EventArgs e)
        {
            if (txtCliente.Text != string.Empty)
            {
                cliente_selecionado = clientes.Find(delegate (Cliente cliente)
                { return cliente.Nome == txtCliente.Text; });
            }
            if (cliente_selecionado != null)
            {
                PreencherDadosCliente(cliente_selecionado);
            }
        }

        private void txtCPF_Leave(object sender, EventArgs e)
        {
            if (txtCPF.Text != string.Empty)
            {
                cliente_selecionado = clientes.Find(delegate (Cliente cliente)
                { return cliente.Cpf == txtCPF.Text; });
            }
            if (cliente_selecionado != null)
            {
                PreencherDadosCliente(cliente_selecionado);
            }
        }

        private void PreencherDadosCliente(Cliente cliente_selecionado)
        {
            this.txtNome.Text = cliente_selecionado.Nome;
            this.lblCpf.Text = cliente_selecionado.Cpf;
            this.txtTelefone.Text = cliente_selecionado.Telefone;
            this.lblCredito.Text = cliente_selecionado.Credito?.ToString("C2");
            this.lblStatus.Text = Enumerados.ObterDescricao(cliente_selecionado.Status);

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOfertar_Click(object sender, EventArgs e)
        {
            List<Enumerados.Produtos> produtos = new List<Enumerados.Produtos>();
            Oferta ofertaAux = new Oferta();
            double somaProdutosSelecionados = 0;
            bool controleSelecaoProdutos = false;
            bool pararExecucao = false;
            Enumerados.Status statusNovo = new Enumerados.Status();

            if (cbbStatus.SelectedValue != null)
                statusNovo = (Enumerados.Status)Enum.Parse(typeof(Enumerados.Status), cbbStatus.SelectedValue.ToString());

            if (ValidarCamposObrigatorios())
            {
                foreach (DataGridViewRow dr in gdvProdutos.Rows)
                {
                    if ((bool)dr.Cells["Selecionar"].Value)
                    {
                        controleSelecaoProdutos = true;
                        if (ValidarEndereco(dr.Cells[2].Value.ToString()))
                        {
                            produtos.Add(Enumerados.ObterEnumPelaDescricao<Enumerados.Produtos>(dr.Cells[1].Value.ToString()));
                            somaProdutosSelecionados += Convert.ToDouble(dr.Cells[3].Value.ToString().Replace("R$ ", ""));
                        }
                        else
                        {
                            somaProdutosSelecionados = 0;
                            produtos = null;
                            controleSelecaoProdutos = false;
                            pararExecucao = true;
                            break;
                        }
                    }
                }

                if (!pararExecucao && ConfirmarVenda(controleSelecaoProdutos, statusNovo, somaProdutosSelecionados))
                {
                    ofertaAux.Cliente = cliente_selecionado;

                    int? numero;
                    if (txtNumero.Text != string.Empty)
                    {
                        numero = Convert.ToInt32(txtNumero.Text);
                    }
                    else
                    {
                        numero = null;
                    }

                    ofertaAux.Endereco_entrega = new Endereco
                    {
                        rua = txtRua.Text,
                        numero = numero,
                        complemento = txtComplemento.Text,
                        bairro = txtBairro.Text,
                        cidade = txtCidade.Text,
                        estado = txtEstado.Text,
                        cep = txtCep.Text
                    };
                    ofertaAux.Produtos = produtos;
                    ofertas.Add(ofertaAux);
                    clientes.Find(a => a.Identificador == cliente_selecionado.Identificador).Status = statusNovo;
                    clientes.Find(a => a.Identificador == cliente_selecionado.Identificador).Credito -= somaProdutosSelecionados;
                    MessageBox.Show("Dados atualizados com sucesso!");
                    LimparTela();
                }
            }
        }

        private void LimparTela()
        {

            txtCliente.Text = string.Empty;
            txtCPF.Text = string.Empty;
            txtNome.Text = string.Empty;
            lblCpf.Text = string.Empty;
            txtTelefone.Text = string.Empty;
            lblCredito.Text = string.Empty;
            lblStatus.Text = string.Empty;
            txtRua.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtComplemento.Text = string.Empty;
            txtBairro.Text = string.Empty;
            txtCidade.Text = string.Empty;
            txtEstado.Text = string.Empty;
            txtCep.Text = string.Empty;
            cbbStatus.SelectedIndex = -1;
            foreach (DataGridViewRow dr in gdvProdutos.Rows)
            {
                dr.Cells[0].Value = false;
            }
            gdvProdutos.ClearSelection();
        }

        private bool ConfirmarVenda(bool produtoSelecionado, Enumerados.Status statusNovo, double valorProdutos)
        {
            if (Status.ContabilizaVenda(statusNovo))
            {
                if (produtoSelecionado == false)
                {
                    MessageBox.Show("Nenhum produto selecionado!");
                    return false;
                }

                if (valorProdutos > cliente_selecionado.Credito)
                {
                    MessageBox.Show("Crédito do cliente insuficiente!");
                    return false;
                }
            }
            else if (produtoSelecionado)
            {
                MessageBox.Show("Não é possível recusar a venda pois há produtos selecionados!");
                return false;
            }
            return true;
        }

        private bool ValidarEndereco(string tipoProduto)
        {
            if ((Enumerados.TipoProduto)Enum.Parse(typeof(Enumerados.TipoProduto), tipoProduto) == Enumerados.TipoProduto.HARDWARE)
            {
                if (txtRua.Text == string.Empty || txtNumero.Text == string.Empty || txtBairro.Text == string.Empty 
                    || txtCidade.Text == string.Empty || txtEstado.Text == string.Empty || txtCep.Text == string.Empty)
                {
                    MessageBox.Show("Para produtos do tipo 'HARDWARE' é necessário que todos os campos do endereço de entrega sejam preenchidos!");
                    return false;
                }
            }
            return true;
        }

        private bool ValidarCamposObrigatorios()
        {
            if (txtNome.Text == string.Empty || txtTelefone.Text == string.Empty || cbbStatus.SelectedItem == null)
            {
                MessageBox.Show("Favor preencher todos os campos obrigatórios: 'Nome', 'Telefone', 'Novo Status'.");
                return false;
            }
            return true;
        }

        private void btnOfertas_Click(object sender, EventArgs e)
        {
            this.Close();
            Ofertas ofertas_form = new Ofertas(ofertas);
            ofertas_form.Show();
        }
    }
}
