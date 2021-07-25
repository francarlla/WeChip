using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WeChip
{
    public partial class Form1 : Form
    {
        public static List<Cliente> clientes = new List<Cliente>();
        public static List<Oferta> ofertasCadastradas = new List<Oferta>();

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
                Ofertar oferta = new Ofertar(clientes, ofertasCadastradas);
                oferta.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ofertasCadastradas == null || ofertasCadastradas.Count == 0)
                {
                    throw new Exception("Nenhuma oferta cadastrada!");
                }
                Ofertas ofertas_form = new Ofertas(ofertasCadastradas);
                ofertas_form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
