using SabroRH.Sincronizador;
using SabroRH.Win.Componentes;
using System;
using System.Windows.Forms;

namespace SabroRH.Monitor
{
    public partial class FrmMonitor : FrmBase
    {
        public FrmMonitor()
        {
            InitializeComponent();
        }

        private void FrmMonitor_Load(object sender, EventArgs e)
        {
            this.SetaTitulo("Sabro RH - Sincronizador");

            EventNotifier.Status += new EventNotifier.StatusNotification(AtualizarStatus);
        }

        private void AtualizarStatus(int processo, string mensagemProcesso, int tarefa, string mensagemTarefa, int maxGlobal, int maxTask)
        {
            progressPanel.Visible = true;

            lblProcesso.Text = $"Processo {processo} de {maxGlobal}: {mensagemProcesso}";
            lblTarefa.Text = $"Tarefa: {mensagemTarefa}";

            progress_db.Maximum = maxGlobal;
            progress_db.Value = processo;

            progress_task.Maximum = maxTask;
            progress_task.Value = tarefa;

            Application.DoEvents();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            var form = new FrmConfig();
            form.ShowDialog();
        }

        private async void btnSinc_Click(object sender, EventArgs e)
        {
            var core = new Sincronizador.Sincronizador();
            await core.Execute();
        }
    }
}
