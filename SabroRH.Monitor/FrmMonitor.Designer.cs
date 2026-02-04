namespace SabroRH.Monitor
{
    partial class FrmMonitor
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
            this.progressPanel = new System.Windows.Forms.Panel();
            this.lblProcesso = new SabroRH.Win.Componentes.HubLabel();
            this.progress_task = new System.Windows.Forms.ProgressBar();
            this.progress_db = new System.Windows.Forms.ProgressBar();
            this.lblTarefa = new SabroRH.Win.Componentes.HubLabel();
            this.btnSinc = new SabroRH.Win.Componentes.HubButton();
            this.btnConfig = new SabroRH.Win.Componentes.HubButton();
            this.panelRodape.SuspendLayout();
            this.progressPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRodape
            // 
            this.panelRodape.Controls.Add(this.btnConfig);
            this.panelRodape.Controls.Add(this.btnSinc);
            this.panelRodape.Location = new System.Drawing.Point(0, 444);
            this.panelRodape.Size = new System.Drawing.Size(985, 51);
            this.panelRodape.Controls.SetChildIndex(this.btnSinc, 0);
            this.panelRodape.Controls.SetChildIndex(this.btnConfig, 0);
            // 
            // panelTopo
            // 
            this.panelTopo.Size = new System.Drawing.Size(985, 50);
            // 
            // progressPanel
            // 
            this.progressPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressPanel.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel.Controls.Add(this.lblProcesso);
            this.progressPanel.Controls.Add(this.progress_task);
            this.progressPanel.Controls.Add(this.progress_db);
            this.progressPanel.Controls.Add(this.lblTarefa);
            this.progressPanel.Location = new System.Drawing.Point(12, 286);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(959, 141);
            this.progressPanel.TabIndex = 6;
            this.progressPanel.Visible = false;
            // 
            // lblProcesso
            // 
            this.lblProcesso.AutoSize = true;
            this.lblProcesso.Font = new System.Drawing.Font("Bahnschrift", 12F);
            this.lblProcesso.ForeColor = System.Drawing.Color.Black;
            this.lblProcesso.Location = new System.Drawing.Point(13, 17);
            this.lblProcesso.Name = "lblProcesso";
            this.lblProcesso.Size = new System.Drawing.Size(105, 24);
            this.lblProcesso.TabIndex = 4;
            this.lblProcesso.Text = "Databases";
            // 
            // progress_task
            // 
            this.progress_task.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progress_task.Location = new System.Drawing.Point(16, 107);
            this.progress_task.Name = "progress_task";
            this.progress_task.Size = new System.Drawing.Size(927, 23);
            this.progress_task.TabIndex = 3;
            // 
            // progress_db
            // 
            this.progress_db.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progress_db.Location = new System.Drawing.Point(16, 45);
            this.progress_db.Name = "progress_db";
            this.progress_db.Size = new System.Drawing.Size(927, 23);
            this.progress_db.TabIndex = 2;
            // 
            // lblTarefa
            // 
            this.lblTarefa.AutoSize = true;
            this.lblTarefa.Font = new System.Drawing.Font("Bahnschrift", 12F);
            this.lblTarefa.ForeColor = System.Drawing.Color.Black;
            this.lblTarefa.Location = new System.Drawing.Point(13, 80);
            this.lblTarefa.Name = "lblTarefa";
            this.lblTarefa.Size = new System.Drawing.Size(67, 24);
            this.lblTarefa.TabIndex = 5;
            this.lblTarefa.Text = "Tarefa";
            // 
            // btnSinc
            // 
            this.btnSinc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSinc.BackColor = System.Drawing.Color.Red;
            this.btnSinc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSinc.FlatAppearance.BorderSize = 0;
            this.btnSinc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSinc.Font = new System.Drawing.Font("Bahnschrift", 12F);
            this.btnSinc.ForeColor = System.Drawing.Color.White;
            this.btnSinc.Location = new System.Drawing.Point(609, 7);
            this.btnSinc.Name = "btnSinc";
            this.btnSinc.Size = new System.Drawing.Size(202, 37);
            this.btnSinc.TabIndex = 3;
            this.btnSinc.Text = "Sincronizar Agora";
            this.btnSinc.UseVisualStyleBackColor = false;
            this.btnSinc.Click += new System.EventHandler(this.btnSinc_Click);
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
            this.btnConfig.Location = new System.Drawing.Point(12, 8);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(202, 37);
            this.btnConfig.TabIndex = 4;
            this.btnConfig.Text = "Configurações";
            this.btnConfig.UseVisualStyleBackColor = false;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // FrmMonitor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(985, 495);
            this.Controls.Add(this.progressPanel);
            this.Name = "FrmMonitor";
            this.ShowInTaskbar = true;
            this.Load += new System.EventHandler(this.FrmMonitor_Load);
            this.Controls.SetChildIndex(this.progressPanel, 0);
            this.Controls.SetChildIndex(this.panelRodape, 0);
            this.Controls.SetChildIndex(this.panelTopo, 0);
            this.panelRodape.ResumeLayout(false);
            this.progressPanel.ResumeLayout(false);
            this.progressPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel progressPanel;
        private Win.Componentes.HubLabel lblTarefa;
        private Win.Componentes.HubLabel lblProcesso;
        private System.Windows.Forms.ProgressBar progress_task;
        private System.Windows.Forms.ProgressBar progress_db;
        private Win.Componentes.HubButton btnSinc;
        private Win.Componentes.HubButton btnConfig;
    }
}

