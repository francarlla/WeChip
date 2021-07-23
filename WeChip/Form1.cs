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
    public partial class Form1 : Form
    {
        public static List<Cliente> clientes = new List<Cliente>();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCadastrarCliente_Click(object sender, EventArgs e)
        {
            CadastroCliente cliente = new CadastroCliente(clientes);
            cliente.Show();
        }

        private void btnCadastrarOferta_Click(object sender, EventArgs e)
        {
            try
            {
                if (clientes == null || clientes.Count == 0)
                {
                    throw new Exception("Nenhum cliente cadastrado! Favor fazer o cadastro do cliente.");
                }
                Ofertar oferta = new Ofertar(clientes);
                oferta.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
