namespace SabroRH.Monitor
{
    partial class FrmConfig
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
            this.hubLabel1 = new SabroRH.Win.Componentes.HubLabel();
            this.fServer = new SabroRH.Win.Componentes.HubTextBoxLabeled();
            this.fUser = new SabroRH.Win.Componentes.HubTextBoxLabeled();
            this.fPassword = new SabroRH.Win.Componentes.HubTextBoxLabeled();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.hubLabel2 = new SabroRH.Win.Componentes.HubLabel();
            this.apiPassword = new SabroRH.Win.Componentes.HubTextBoxLabeled();
            this.apiUser = new SabroRH.Win.Componentes.HubTextBoxLabeled();
            this.btnConfig = new SabroRH.Win.Componentes.HubButton();
            this.lstDB = new System.Windows.Forms.ListView();
            this.c1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bTest = new SabroRH.Win.Componentes.HubButton();
            this.panelRodape.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRodape
            // 
            this.panelRodape.Controls.Add(this.btnConfig);
            this.panelRodape.Location = new System.Drawing.Point(0, 546);
            this.panelRodape.Size = new System.Drawing.Size(685, 51);
            this.panelRodape.Controls.SetChildIndex(this.btnConfig, 0);
            // 
            // panelTopo
            // 
            this.panelTopo.Size = new System.Drawing.Size(685, 50);
            // 
            // hubLabel1
            // 
            this.hubLabel1.AutoSize = true;
            this.hubLabel1.Font = new System.Drawing.Font("Bahnschrift SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hubLabel1.ForeColor = System.Drawing.Color.Black;
            this.hubLabel1.Location = new System.Drawing.Point(23, 60);
            this.hubLabel1.Name = "hubLabel1";
            this.hubLabel1.Size = new System.Drawing.Size(176, 24);
            this.hubLabel1.TabIndex = 5;
            this.hubLabel1.Text = "Servidor de Dados";
            // 
            // fServer
            // 
            this.fServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fServer.AutoSize = true;
            this.fServer.BackColor = System.Drawing.Color.Gainsboro;
            this.fServer.CasasDecimais = 0;
            this.fServer.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.fServer.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fServer.IsHabilitadoAlteracao = true;
            this.fServer.IsHabilitadoInclusao = true;
            this.fServer.Label = "Servidor SQL Server";
            this.fServer.Location = new System.Drawing.Point(19, 99);
            this.fServer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fServer.MaxLenght = 0;
            this.fServer.MensagemDeErro = null;
            this.fServer.Name = "fServer";
            this.fServer.Obrigatorio = false;
            this.fServer.Senha = false;
            this.fServer.Size = new System.Drawing.Size(658, 69);
            this.fServer.TabIndex = 6;
            this.fServer.TipoDado = SabroRH.Win.Componentes.HubTipoTextField.Texto;
            this.fServer.Value = "";
            // 
            // fUser
            // 
            this.fUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fUser.AutoSize = true;
            this.fUser.BackColor = System.Drawing.Color.Gainsboro;
            this.fUser.CasasDecimais = 0;
            this.fUser.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.fUser.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fUser.IsHabilitadoAlteracao = true;
            this.fUser.IsHabilitadoInclusao = true;
            this.fUser.Label = "Usuário";
            this.fUser.Location = new System.Drawing.Point(19, 163);
            this.fUser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fUser.MaxLenght = 0;
            this.fUser.MensagemDeErro = null;
            this.fUser.Name = "fUser";
            this.fUser.Obrigatorio = false;
            this.fUser.Senha = false;
            this.fUser.Size = new System.Drawing.Size(259, 69);
            this.fUser.TabIndex = 7;
            this.fUser.TipoDado = SabroRH.Win.Componentes.HubTipoTextField.Texto;
            this.fUser.Value = "";
            // 
            // fPassword
            // 
            this.fPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fPassword.AutoSize = true;
            this.fPassword.BackColor = System.Drawing.Color.Gainsboro;
            this.fPassword.CasasDecimais = 0;
            this.fPassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.fPassword.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fPassword.IsHabilitadoAlteracao = true;
            this.fPassword.IsHabilitadoInclusao = true;
            this.fPassword.Label = "Senha";
            this.fPassword.Location = new System.Drawing.Point(309, 163);
            this.fPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fPassword.MaxLenght = 0;
            this.fPassword.MensagemDeErro = null;
            this.fPassword.Name = "fPassword";
            this.fPassword.Obrigatorio = false;
            this.fPassword.Senha = true;
            this.fPassword.Size = new System.Drawing.Size(257, 69);
            this.fPassword.TabIndex = 8;
            this.fPassword.TipoDado = SabroRH.Win.Componentes.HubTipoTextField.Texto;
            this.fPassword.Value = "";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(28, 87);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(634, 1);
            this.panel1.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(28, 453);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(634, 1);
            this.panel2.TabIndex = 11;
            // 
            // hubLabel2
            // 
            this.hubLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.hubLabel2.AutoSize = true;
            this.hubLabel2.Font = new System.Drawing.Font("Bahnschrift SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hubLabel2.ForeColor = System.Drawing.Color.Black;
            this.hubLabel2.Location = new System.Drawing.Point(23, 426);
            this.hubLabel2.Name = "hubLabel2";
            this.hubLabel2.Size = new System.Drawing.Size(151, 24);
            this.hubLabel2.TabIndex = 10;
            this.hubLabel2.Text = "Servidor de API";
            // 
            // apiPassword
            // 
            this.apiPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.apiPassword.AutoSize = true;
            this.apiPassword.BackColor = System.Drawing.Color.Gainsboro;
            this.apiPassword.CasasDecimais = 0;
            this.apiPassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.apiPassword.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.apiPassword.IsHabilitadoAlteracao = true;
            this.apiPassword.IsHabilitadoInclusao = true;
            this.apiPassword.Label = "Senha";
            this.apiPassword.Location = new System.Drawing.Point(363, 463);
            this.apiPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.apiPassword.MaxLenght = 36;
            this.apiPassword.MensagemDeErro = null;
            this.apiPassword.Name = "apiPassword";
            this.apiPassword.Obrigatorio = false;
            this.apiPassword.Senha = true;
            this.apiPassword.Size = new System.Drawing.Size(311, 69);
            this.apiPassword.TabIndex = 13;
            this.apiPassword.TipoDado = SabroRH.Win.Componentes.HubTipoTextField.Texto;
            this.apiPassword.Value = "";
            // 
            // apiUser
            // 
            this.apiUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.apiUser.AutoSize = true;
            this.apiUser.BackColor = System.Drawing.Color.Gainsboro;
            this.apiUser.CasasDecimais = 0;
            this.apiUser.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.apiUser.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.apiUser.IsHabilitadoAlteracao = true;
            this.apiUser.IsHabilitadoInclusao = true;
            this.apiUser.Label = "CNPJ";
            this.apiUser.Location = new System.Drawing.Point(20, 463);
            this.apiUser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.apiUser.MaxLenght = 14;
            this.apiUser.MensagemDeErro = null;
            this.apiUser.Name = "apiUser";
            this.apiUser.Obrigatorio = false;
            this.apiUser.Senha = false;
            this.apiUser.Size = new System.Drawing.Size(311, 69);
            this.apiUser.TabIndex = 12;
            this.apiUser.TipoDado = SabroRH.Win.Componentes.HubTipoTextField.Inteiro;
            this.apiUser.Value = "";
            // 
            // btnConfig
            // 
            this.btnConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConfig.BackColor = System.Drawing.Color.Black;
            this.btnConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfig.FlatAppearance.BorderSize = 0;
            this.btnConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfig.Font = new System.Drawing.Font("Bahnschrift", 12F);
            this.btnConfig.ForeColor = System.Drawing.Color.White;
            this.btnConfig.Location = new System.Drawing.Point(318, 7);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(202, 37);
            this.btnConfig.TabIndex = 5;
            this.btnConfig.Text = "Salvar";
            this.btnConfig.UseVisualStyleBackColor = false;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // lstDB
            // 
            this.lstDB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDB.BackColor = System.Drawing.Color.White;
            this.lstDB.CheckBoxes = true;
            this.lstDB.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.c1});
            this.lstDB.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDB.HideSelection = false;
            this.lstDB.Location = new System.Drawing.Point(27, 233);
            this.lstDB.Name = "lstDB";
            this.lstDB.Size = new System.Drawing.Size(634, 184);
            this.lstDB.TabIndex = 16;
            this.lstDB.TabStop = false;
            this.lstDB.UseCompatibleStateImageBehavior = false;
            this.lstDB.View = System.Windows.Forms.View.Details;
            // 
            // c1
            // 
            this.c1.Text = "Database";
            this.c1.Width = 563;
            // 
            // bTest
            // 
            this.bTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bTest.BackColor = System.Drawing.Color.Gainsboro;
            this.bTest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bTest.FlatAppearance.BorderSize = 0;
            this.bTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bTest.Font = new System.Drawing.Font("Bahnschrift", 12F);
            this.bTest.ForeColor = System.Drawing.Color.White;
            this.bTest.Image = global::SabroRH.Monitor.Properties.Resources.database48x48;
            this.bTest.Location = new System.Drawing.Point(592, 163);
            this.bTest.Name = "bTest";
            this.bTest.Size = new System.Drawing.Size(70, 59);
            this.bTest.TabIndex = 17;
            this.bTest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bTest.UseVisualStyleBackColor = false;
            this.bTest.Click += new System.EventHandler(this.bTest_Click);
            // 
            // FrmConfig
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(685, 597);
            this.Controls.Add(this.bTest);
            this.Controls.Add(this.lstDB);
            this.Controls.Add(this.apiPassword);
            this.Controls.Add(this.apiUser);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.hubLabel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.fPassword);
            this.Controls.Add(this.fUser);
            this.Controls.Add(this.fServer);
            this.Controls.Add(this.hubLabel1);
            this.Name = "FrmConfig";
            this.Load += new System.EventHandler(this.FrmConfig_Load);
            this.Controls.SetChildIndex(this.panelRodape, 0);
            this.Controls.SetChildIndex(this.panelTopo, 0);
            this.Controls.SetChildIndex(this.hubLabel1, 0);
            this.Controls.SetChildIndex(this.fServer, 0);
            this.Controls.SetChildIndex(this.fUser, 0);
            this.Controls.SetChildIndex(this.fPassword, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.hubLabel2, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.apiUser, 0);
            this.Controls.SetChildIndex(this.apiPassword, 0);
            this.Controls.SetChildIndex(this.lstDB, 0);
            this.Controls.SetChildIndex(this.bTest, 0);
            this.panelRodape.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Win.Componentes.HubLabel hubLabel1;
        private Win.Componentes.HubTextBoxLabeled fServer;
        private Win.Componentes.HubTextBoxLabeled fUser;
        private Win.Componentes.HubTextBoxLabeled fPassword;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Win.Componentes.HubLabel hubLabel2;
        private Win.Componentes.HubTextBoxLabeled apiPassword;
        private Win.Componentes.HubTextBoxLabeled apiUser;
        private Win.Componentes.HubButton btnConfig;
        private System.Windows.Forms.ListView lstDB;
        private System.Windows.Forms.ColumnHeader c1;
        private Win.Componentes.HubButton bTest;
    }
}