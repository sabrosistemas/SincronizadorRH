using SabroRH.Schemas;
using SabroRH.SQLHelper;
using SabroRH.Win.Componentes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SabroRH.Monitor
{
    public partial class FrmConfig : FrmBase
    {
        Configs config = new Configs();

        public FrmConfig()
        {
            InitializeComponent();
        }

        private void FrmConfig_Load(object sender, EventArgs e)
        {
            SetaTitulo("Configurações");

            config.Carregar();

            fServer.Value = config.Server;
            fUser.Value = config.User;
            fPassword.Value = config.Password;
            apiUser.Value = config.APIUser;
            apiPassword.Value = config.APIPassword;

            lstDB.BeginUpdate();
            lstDB.Items.Clear();
          
            foreach (var db in config.Databases)
            {
                var t = new ListViewItem(db)
                {
                    ForeColor = Color.Green,
                    Checked = true
                };
                lstDB.Items.Add(t);
            }

            lstDB.EndUpdate();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            config.Server = fServer.Value;
            config.User = fUser.Value;
            config.Password = fPassword.Value;
            config.APIUser = apiUser.Value;
            config.APIPassword = apiPassword.Value;

            config.Databases.Clear();
            foreach (ListViewItem item in lstDB.Items)
            {
                if (item.Checked)
                {
                    if (!config.Databases.Contains(item.Text))
                    {
                        config.Databases.Add(item.Text);
                    }
                }
            }

            config.Salvar();

            this.Close();
        }

        private async void bTest_Click(object sender, EventArgs e)
        {
            try
            {
                var helper = new Helper();
                var connection = await helper.Connect(fServer.Value, fUser.Value, fPassword.Value, "master");
                var reader = await helper.ExecuteReader(connection, "SELECT name FROM sys.databases WHERE name like 'CenariusRH%'");

                lstDB.BeginUpdate();
                lstDB.Items.Clear();

                while (reader.Read())
                {
                    var dbName = reader.GetString(0);
                    var t = new ListViewItem(dbName);

                    if (config.Databases.Exists(w => w.Equals(dbName)))
                    {
                        t.ForeColor = Color.Green;
                        t.Checked = true;
                    }
                    else
                    {
                        t.ForeColor = Color.Red;
                        t.Checked = false;
                    }

                    lstDB.Items.Add(t);

                    t = null;
                }

                reader.Close();
                reader.Dispose();

                lstDB.EndUpdate();

                MessageBox.Show("Conexão efetuada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao conectar ao banco de dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
