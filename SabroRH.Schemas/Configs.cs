using Interacao.Framework;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SabroRH.Schemas
{
    public class Configs
    {
        private string FILENAME = string.Concat(Path.GetDirectoryName(Application.ExecutablePath), "\\sabrorh.xml");
        private const string SECRET = "SaBR0#xml";
        public string Server { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public List<string> Databases { get; set; } = new List<string>();

        public string APIUser { get; set; }
        public string APIPassword { get; set; }

        public void Carregar()
        {
            if (!File.Exists(FILENAME))
            {
                return;
            }

            using (TextReader streamReader = new StreamReader(FILENAME))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Configs));
                var config = (Configs)xmlSerializer.Deserialize(streamReader);
                xmlSerializer = null;

                if (config != null)
                {
                    Server = config.Server;
                    User = config.User;
                    Password = config.Password.Desembaralha(SECRET);                    
                    
                    APIUser = config.APIUser;                    
                    APIPassword = config.APIPassword.Desembaralha(SECRET);
                    
                    Databases = config.Databases ?? new List<string>();
                }
            }
        }

        public void Salvar()
        {
            IO.ApagaArquivo(this.FILENAME);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Configs));
            TextWriter streamWriter = new StreamWriter(this.FILENAME);
            try
            {
                try
                {
                    if (Password != null && Password != string.Empty)
                    {
                        Password = Password.Embaralha(SECRET);
                    }

                    if (APIPassword != null && APIPassword != string.Empty)
                    {
                        APIPassword = APIPassword.Embaralha(SECRET);
                    }

                    xmlSerializer.Serialize(streamWriter, this);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                catch
                {
                }
            }
            finally
            {
                xmlSerializer = null;
                streamWriter = null;
            }
        }
    }
}
