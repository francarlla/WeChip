using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WeChip.Produtos;

namespace WeChip
{
    public partial class Ofertas : Form
    {
        private List<Oferta> ofertas = new List<Oferta>();

        public Ofertas()
        {
            InitializeComponent();
        }

        public Ofertas(List<Oferta> ofertas)
        {
            this.ofertas = ofertas;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CarregarOfertas();
        }

        private void CarregarOfertas()
        {
            DataTable ofertasCadastradas = new DataTable();
            ofertasCadastradas.Columns.Add("Identificador", typeof(int));
            ofertasCadastradas.Columns.Add("Nome Cliente", typeof(string));
            ofertasCadastradas.Columns.Add("CPF", typeof(string));
            ofertasCadastradas.Columns.Add("Telefone", typeof(string));
            ofertasCadastradas.Columns.Add("Status Atual", typeof(string));
            ofertasCadastradas.Columns.Add("Crédito Anterior", typeof(string));
            ofertasCadastradas.Columns.Add("Crédito Disponível", typeof(string));
            ofertasCadastradas.Columns.Add("Endereço Entrega", typeof(string));
            ofertasCadastradas.Columns.Add("Produtos", typeof(string));
            ofertasCadastradas.Columns.Add("Valor Produtos", typeof(string));

            foreach (var oferta in ofertas)
            {
                DataRow linha = ofertasCadastradas.NewRow();
                linha["Identificador"] = oferta.Identificador;
                linha["Nome Cliente"] = oferta.Cliente.Nome;
                linha["CPF"] = oferta.Cliente.Cpf;
                linha["Telefone"] = oferta.Cliente.Telefone;
                linha["Status Atual"] = oferta.Cliente.Status.ObterDescricao();
                
                string produtos = string.Empty;
                double valorProdutos = 0;
                foreach (var produto in oferta.Produtos)
                {
                    produtos += produto.ObterDescricao() + ". ";
                    valorProdutos += Convert.ToDouble(RetornarValorProduto(produto).ToString().Replace("R$ ", ""));
                }

                linha["Crédito Anterior"] = (Convert.ToDouble(oferta.Cliente.Credito) + valorProdutos).ToString("C2");
                linha["Crédito Disponível"] = Convert.ToDouble(oferta.Cliente.Credito).ToString("C2");

                if (oferta.Endereco_entrega.rua != string.Empty && oferta.Endereco_entrega.numero != null && oferta.Endereco_entrega.bairro != string.Empty 
                    && oferta.Endereco_entrega.cidade != string.Empty && oferta.Endereco_entrega.estado != string.Empty && oferta.Endereco_entrega.cep != string.Empty)
                {
                    linha["Endereço Entrega"] = string.Format("Rua " + oferta.Endereco_entrega.rua + ", " + oferta.Endereco_entrega.numero.ToString() + oferta.Endereco_entrega.complemento + ", " + oferta.Endereco_entrega.bairro + ", " + oferta.Endereco_entrega.cidade + ", " + oferta.Endereco_entrega.estado + ", CEP " + oferta.Endereco_entrega.cep);
                }

                linha["Produtos"] = produtos;
                linha["Valor Produtos"] = Convert.ToDouble(valorProdutos).ToString("C2");
                ofertasCadastradas.Rows.Add(linha);

            }
            dgvOfertasCadastradas.DataSource = ofertasCadastradas;
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
