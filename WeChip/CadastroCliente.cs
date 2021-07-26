using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WeChip
{
    public partial class CadastroCliente : Form
    {
        public List<Cliente> clientes;

        public CadastroCliente()
        {
            InitializeComponent();

        }

        public CadastroCliente(List<Cliente> clientes)
        {
            this.clientes = clientes;
            InitializeComponent();

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            PreencherClientesCadastrados();
        }

        private int RecuperarIdentificador()
        {
            if (clientes.Count == 0)
            {
                return 1;
            }
            else
            {
                return clientes.Count + 1;
            }
        }
        private void PreencherClientesCadastrados()
        {
            DataTable clientesCadastrados = new DataTable();
            clientesCadastrados.Columns.Add("ID", typeof(Int32));
            clientesCadastrados.Columns.Add("Nome", typeof(string));
            clientesCadastrados.Columns.Add("CPF", typeof(string));
            clientesCadastrados.Columns.Add("Telefone", typeof(string));
            clientesCadastrados.Columns.Add("Crédito Atual", typeof(string));
            clientesCadastrados.Columns.Add("Status Atual", typeof(string));

            foreach (var cliente in clientes)
            {
                DataRow linha = clientesCadastrados.NewRow();
                linha["ID"] = cliente.Identificador;
                linha["Nome"] = cliente.Nome;
                linha["CPF"] = cliente.Cpf;
                linha["Telefone"] = cliente.Telefone;
                linha["Crédito Atual"] = Convert.ToDouble(cliente.Credito).ToString("C2");
                linha["Status Atual"] = cliente.Status.ObterDescricao();
                clientesCadastrados.Rows.Add(linha);
            }
            grdCliente.DataSource = null;
            grdCliente.DataSource = clientesCadastrados;
        }

        private void btnSalvarCliente_Click(object sender, EventArgs e)
        {
            if (ValidarCamposObrigatorios())
            {

                Cliente clienteAux = new Cliente();
                clienteAux.Identificador = RecuperarIdentificador();
                clienteAux.Status = Enumerados.Status.NomeLivre;
                clienteAux.Nome = txtNome.Text;
                clienteAux.Cpf = txtCpf.Text;
                clienteAux.Telefone = txtTelefone.Text;

                if (txtCredito.Text != string.Empty)
                {
                    clienteAux.Credito = Convert.ToDouble(txtCredito.Text.Replace("R$", ""));
                }
                else
                {
                    clienteAux.Credito = null;
                }

                clientes.Add(clienteAux);
                LimparForm();
                PreencherClientesCadastrados();
            }

        }

        private bool ValidarCamposObrigatorios()
        {
            if (txtNome.Text == string.Empty || txtCpf.Text == string.Empty || txtTelefone.Text == string.Empty)
            {
                MessageBox.Show("Favor preencher todos os campos obrigatórios!");
                return false;
            }
            return true;
        }

        private void LimparForm()
        {
            txtNome.Clear();
            txtCpf.Clear();
            txtTelefone.Clear();
            txtCredito.Clear();
        }

        private void txtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {

                e.Handled = true;

            }
        }

        private void txtCredito_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Se a tecla digitada não for número
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)44)
            {
                //Atribui True no Handled para cancelar o evento
                e.Handled = true;
            }
        }

        private void txtCredito_Leave(object sender, EventArgs e)
        {
            if (txtCredito.Text != string.Empty)
                txtCredito.Text = double.Parse(txtCredito.Text.Replace("R$ ", "")).ToString("C2");
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void txtCpf_Leave(object sender, EventArgs e)
        {
            if (!ValidarCpf())
            {
                MessageBox.Show("CPF inválido!");
                txtCpf.Text = string.Empty;
                txtCpf.Focus();
            }
        }

        private bool ValidarCpf()
        {
            var valor = string.Join("", Regex.Split(txtCpf.Text, @"[^\d]"));

            if (valor.Length != 11)
            {
                return false;
            }

            var igual = true;
            for (int i = 1; i < 11 && igual; i++)
            {
                if (valor[i] != valor[0])
                {
                    igual = false;
                }
            }

            if (igual || valor == "12345678909")
            {
                return false;
            }

            var numeros = new int[11];

            for (int i = 0; i < 11; i++)
            {
                numeros[i] = int.Parse(valor[i].ToString());
            }

            var soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += (10 - i) * numeros[i];
            }

            var resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                {
                    return false;
                }
            }
            else if (numeros[9] != 11 - resultado)
            {
                return false;
            }

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += (11 - i) * numeros[i];
            }

            resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                {
                    return false;
                }
            }
            else
            {
                if (numeros[10] != 11 - resultado)
                {
                    return false;
                }
            }

            return true;
        }

        private void txtTelefone_Leave(object sender, EventArgs e)
        {
            if (!txtTelefone.MaskCompleted)
            {
                MessageBox.Show("Telefone inválido!");
                txtTelefone.Text = string.Empty;
                txtTelefone.Focus();
            }
            
        }
    }
}
