using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WeChip.Status;

namespace WeChip
{
    //    clienteAux.Endereco_entrega = new Endereco
    //            {
    //                rua = txtRua.Text,
    //                numero = numero,
    //                complemento = txtComplemento.Text,
    //                bairro = txtBairro.Text,
    //                cidade = txtCidade.Text,
    //                estado = txtEstado.Text,
    //                cep = txtCep.Text
    //};

    //int? numero;
    //        if (txtNumero.Text != string.Empty)
    //        {
    //            numero = Convert.ToInt32(txtNumero.Text);
    //        }
    //        else
    //        {
    //            numero = null;
    //        }

    public partial class Ofertar : Form
    {
        public List<Cliente> clientes;

        public Ofertar()
        {
            InitializeComponent();
        }

        public Ofertar(List<Cliente> clientes)
        {
            this.clientes = clientes;
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
            produtos.Columns.Add("Tipo" , typeof(string));
            produtos.Columns.Add("Valor", typeof(string));

            foreach (var item in Produtos.produtos)
            {
                DataRow linha = produtos.NewRow();
                linha["Produto"] = item.Key.GetDescription<Enumerados.Produtos>();
                linha["Tipo"] = item.Value[1].ToString();
                linha["Valor"] = Convert.ToDouble(item.Value[0]).ToString("C2");
                produtos.Rows.Add(linha);
                  
            }
            gdvProdutos.DataSource = produtos;
        }

        private void CarregarComboStatus()
        {
            this.cbbStatus.Items.Add(string.Empty);
            foreach (Enumerados.Status status in Enumerados.EnumToList<Enumerados.Status>())
            {
                this.cbbStatus.Items.Add(status.GetDescription());
            }
            this.cbbStatus.SelectedIndex = 0;
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
            Cliente cliente_selecionado = null;
            if (txtCliente.Text != string.Empty)
            {
                cliente_selecionado = clientes.Find(delegate(Cliente cliente)
                { return cliente.Nome == txtCliente.Text; });
            }
            if (cliente_selecionado != null)
            {
                PreencherDadosCliente(cliente_selecionado);
            }
        }

        private void txtCPF_Leave(object sender, EventArgs e)
        {
            Cliente cliente_selecionado = null;
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
            this.lblStatus.Text = Enumerados.GetDescription(cliente_selecionado.Status);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOfertar_Click(object sender, EventArgs e)
        {
           if (ValidarCamposObrigatorios())
            {
                foreach (DataGridViewRow dr in gdvProdutos.Rows)
                {
                    if (dr.Cells[0].Selected)
                    {
                        if (ValidarEndereco(dr.Cells[2].ToString()))
                        {

                        }
                        MessageBox.Show("Linha " + dr.Index + " foi selecionada");
                    }
                }
            }
        }

        private bool ValidarEndereco(string tipoProduto)
        {
            if ((Enumerados.TipoProduto)Enum.Parse(typeof(Enumerados.TipoProduto), tipoProduto) == Enumerados.TipoProduto.HARDWARE)
            {
                if (txtRua.Text == string.Empty || txtNumero.Text == string.Empty || txtComplemento.Text == string.Empty
                    || txtBairro.Text == string.Empty || txtCidade.Text == string.Empty || txtEstado.Text == string.Empty || txtCep.Text == string.Empty)
                {
                    MessageBox.Show("Para produtos do tipo 'HARDWARE' é necessário que todos os campos do endereço de entrega sejam preenchidos!");
                    return false;
                }
            }
            return true;
        }

        private bool ValidarCamposObrigatorios()
        {
            if (txtNome.Text == string.Empty || txtTelefone.Text == string.Empty)
            {
                MessageBox.Show("Os campo 'Nome' e 'Telefone' do cliente são obrigatórios!");
                return false;
            }
            return true;
        }

    }
}
