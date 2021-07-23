using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeChip
{
    public partial class CadastroCliente : Form
    {
        public List<Cliente> clientes;
        int identificador = 1;
        public CadastroCliente()
        {
            InitializeComponent();
            
        }

        public CadastroCliente(List<Cliente> clientes)
        {
            this.clientes = clientes;
            InitializeComponent();

        }

        private void btnSalvarCliente_Click(object sender, EventArgs e)
        {
            if (ValidarCamposObrigatorios()){

                Cliente clienteAux = new Cliente();
                clienteAux.Identificador = identificador;
                clienteAux.Status = Enumerados.Status.NomeLivre;
                clienteAux.Nome = txtNome.Text;
                clienteAux.Cpf = txtCpf.Text;
                clienteAux.Telefone = txtTelefone.Text;
                
                if (txtCredito.Text != string.Empty)
                {
                    clienteAux.Credito = Convert.ToDouble(txtCredito.Text.Replace("R$", ""));
                }else
                {
                    clienteAux.Credito = null;
                }

                clientes.Add(clienteAux);
                LimparForm();
                identificador++;
                grdCliente.AutoSize = true;
                grdCliente.DataSource = null;
                grdCliente.DataSource = clientes;
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
    }
}
