
namespace WeChip
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCadastrarCliente = new System.Windows.Forms.Button();
            this.btnCadastrarOferta = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCadastrarCliente
            // 
            this.btnCadastrarCliente.Location = new System.Drawing.Point(33, 48);
            this.btnCadastrarCliente.Name = "btnCadastrarCliente";
            this.btnCadastrarCliente.Size = new System.Drawing.Size(172, 42);
            this.btnCadastrarCliente.TabIndex = 0;
            this.btnCadastrarCliente.Text = "Cadastrar Cliente";
            this.btnCadastrarCliente.UseVisualStyleBackColor = true;
            this.btnCadastrarCliente.Click += new System.EventHandler(this.btnCadastrarCliente_Click);
            // 
            // btnCadastrarOferta
            // 
            this.btnCadastrarOferta.Location = new System.Drawing.Point(33, 106);
            this.btnCadastrarOferta.Name = "btnCadastrarOferta";
            this.btnCadastrarOferta.Size = new System.Drawing.Size(172, 42);
            this.btnCadastrarOferta.TabIndex = 1;
            this.btnCadastrarOferta.Text = "Cadastrar Oferta";
            this.btnCadastrarOferta.UseVisualStyleBackColor = true;
            this.btnCadastrarOferta.Click += new System.EventHandler(this.btnCadastrarOferta_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCadastrarOferta);
            this.Controls.Add(this.btnCadastrarCliente);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCadastrarCliente;
        private System.Windows.Forms.Button btnCadastrarOferta;
    }
}

