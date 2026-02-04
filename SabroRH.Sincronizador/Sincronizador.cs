using SabroRH.Schemas;
using SabroRH.SQLHelper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SabroRH.Sincronizador
{
    public class Sincronizador
    {
        private Configs Config;
        private Helper Helper;
        private SqlConnection Connection;
        private int CurrentTaskIndex = 1;
        private int MaxTasks = 0;
        private readonly int Tasks = 9;
        private const int Tables = 3;
        private int CurrentTaskProgress = 0;
        private HttpHelper HttpHelper;

        public async Task Execute()
        {
            await Passos();
        }

        public async Task Passos()
        {
            try
            {
                // Carrega as configurações do servidor
                await CarregarConfiguracoes();

                // Inicializa o servidor de origem
                await InicializarServidorOrigem();

                // Testa a API
                await TestarAPI();

                // Autentica na API
                await AutenticarAPI();

                // Envia os dados   
                await EnviarDados();

                // Notifica conclusao de sincronização
                Pronto();
            }
            catch (System.Exception ex)
            {
                CloseAll();
                EventNotifier.NotifyStatus(0, "Erro de Sincronização!", 0, ex.Message, MaxTasks, 0);
                return;
            }
        }

        private async Task CarregarConfiguracoes()
        {
            CurrentTaskIndex = 1;
            CurrentTaskProgress = 0;
            MaxTasks = 0;

            Config = new Configs();
            Config.Carregar();

            if (Config.Server == null || Config.User == null || Config.Password == null)
            {
                throw new System.Exception("Configurações inválidas. Verifique o arquivo de configuração.");
            }

            if (this.Config.Databases.Count == 0)
            {
                throw new System.Exception("Nenhuma base de dados configurada. Verifique o arquivo de configuração.");
            }

            MaxTasks = (Config.Databases.Count * Tables) + Tasks;

            EventNotifier.NotifyStatus(CurrentTaskIndex, "Carregando configurações...", CurrentTaskProgress, "Iniciando...", MaxTasks, 1);
            CurrentTaskIndex++;

            await Task.Delay(1000);

            EventNotifier.NotifyStatus(CurrentTaskIndex, "Carregando configurações...", 1, "Pronto!", MaxTasks, 1);
            await Task.Delay(1000);
        }

        private async Task InicializarServidorOrigem()
        {
            EventNotifier.NotifyStatus(CurrentTaskIndex, "Inicializando banco de dados...", 0, "Iniciando...", MaxTasks, 100);
            Helper = new Helper();
            Connection = await Helper.Connect(Config.Server, Config.User, Config.Password, "master");
            if (Connection == null)
            {
                throw new System.Exception("Não foi possível conectar ao servidor de origem.");
            }

            EventNotifier.NotifyStatus(CurrentTaskIndex, "Inicializando banco de dados...", 100, "Pronto!", MaxTasks, 100);
        }

        private async Task TestarAPI()
        {
            CurrentTaskProgress = 0;

            string message = "Verificando conexão com a API remota...";

            CurrentTaskIndex++;
            EventNotifier.NotifyStatus(CurrentTaskIndex, message, CurrentTaskProgress, "Conectando...", MaxTasks, 1);

            HttpHelper = new HttpHelper();
            var token = await HttpHelper.GetAsync<Health>("/health");
            await Task.Delay(1000);

            if (token.Status != "OK")
            {            
                throw new System.Exception("Falha ao conectar com a API remota.");
            }

            EventNotifier.NotifyStatus(CurrentTaskIndex, message, 1, "Pronto!", MaxTasks, 1);
            await Task.Delay(300);
        }

        private async Task AutenticarAPI()
        {
            CurrentTaskProgress = 0;

            string message = "Autenticando na API remota...";

            CurrentTaskIndex++;
            EventNotifier.NotifyStatus(CurrentTaskIndex, message, CurrentTaskProgress, "Autenticando...", MaxTasks, 1);

            HttpHelper = new HttpHelper();
            var authReq = new AutenticacaoRequest
            {
                cnpj = Config.APIUser,
                password = Config.APIPassword
            };

            var response = await HttpHelper.PostAsync<APIResponse<AutenticacaoResponse>>("/auth/tenant", authReq);
            await Task.Delay(1000);

            if (!response.Success)
            {
                throw new System.Exception("Falha de autenticação com a API.");
            }

            HttpHelper = new HttpHelper(response.Data.Token);

            EventNotifier.NotifyStatus(CurrentTaskIndex, message, 1, "Pronto!", MaxTasks, 1);
            await Task.Delay(300);
        }

        private async Task EnviarDados()
        {
            foreach(var db in Config.Databases)
            {
                await EnviarEmpresas(db);
                await EnviarPessoas(db);
                await EnviarRecibos(db);
                // await EnviarDecimo(db);
                await EnviarFerias(db);
            }
        }

        private async Task EnviarEmpresas(string database)
        {
            CurrentTaskProgress = 0;

            string message = $"Enviando Empresas - {database}";

            CurrentTaskIndex++;
            EventNotifier.NotifyStatus(CurrentTaskIndex, message, CurrentTaskProgress, "Consultando...", MaxTasks, 1);

            string sql = $"SELECT EMPRESAID, ESRAZAO, ESNOME, ESEND, ESENDNUM, ESENDCOM, ESBAI, ESEST, ESFONE, ESCEP, ESCGC, ESCID FROM {database}.dbo.Empresas";

            var empresas = await Helper.ExecuteReader(Connection, sql);
            var list = new List<Empresa>();

            while (empresas.Read())
            {
                var empresa = new Empresa
                {
                    cnpj = empresas["ESCGC"].ToString().Trim(),
                    razaoSocial = empresas["ESRAZAO"].ToString().Trim(),
                    fantasia = empresas["ESNOME"].ToString().Trim(),
                    endereco = empresas["ESEND"].ToString().Trim(),
                    numero = empresas["ESENDNUM"].ToString().Trim(),
                    complemento = empresas["ESENDCOM"].ToString().Trim(),
                    bairro = empresas["ESBAI"].ToString().Trim(),
                    uf = empresas["ESEST"].ToString().Trim(),
                    telefone = empresas["ESFONE"].ToString().Trim(),
                    cep = empresas["ESCEP"].ToString().Trim(),
                    codigo = empresas["EMPRESAID"].ToString().Trim(),
                    cidade = empresas["ESCID"].ToString().Trim(),
                };
                list.Add(empresa);
            }

            empresas.Close();
            empresas.Dispose();

            EventNotifier.NotifyStatus(CurrentTaskIndex, message, CurrentTaskProgress, "Enviando...", MaxTasks, list.Count);
            foreach (var empresa in list)
            {
                EventNotifier.NotifyStatus(CurrentTaskIndex, message, ++CurrentTaskProgress, "Enviando...", MaxTasks, list.Count);

                var response = await HttpHelper.PutAsync<APIResponse<Empresa>>($"/integration/empresas/{empresa.codigo}", empresa);
                if (!response.Success)
                {
                    throw new System.Exception($"Falha ao enviar a empresa {empresa.razaoSocial}.");
                }            
            }
            
            EventNotifier.NotifyStatus(CurrentTaskIndex, message, list.Count, "Pronto!", MaxTasks, list.Count);
            await Task.Delay(300);
        }

        private async Task EnviarPessoas(string database)
        {
            CurrentTaskProgress = 0;

            string message = $"Enviando Pessoas - {database}";

            CurrentTaskIndex++;
            EventNotifier.NotifyStatus(CurrentTaskIndex, message, CurrentTaskProgress, "Consultando...", MaxTasks, 1);

            string sql = "SELECT P.PESSOAID as \"ID\", " +
                "C.CONTRATOID," +
                "LTRIM(RTRIM(P.PESNOME)) + ' ' + LTRIM(RTRIM(P.PESSOBNOM)) AS \"NOME\", " +
                "P.PESCPFINS AS \"DOCUMENTO\", " +
                "P.PESNASDAT AS \"NASCIMENTO\", " +
                "LTRIM(RTRIM(LOWER(P.PESENDEMAIL))) AS \"EMAIL\", " +
                "LTRIM(RTRIM(P.PESENDTEL)) as \"TELEFONE\"," +
                "LTRIM(RTRIM(L.DESCRICAO)) AS \"LOTACAO\"," +
                "COALESCE(C.SALARIO, 0) AS \"SALARIO\"," +
                "E.ESCGC, " +
                "G.CARCODCBO AS CBO, " +
                "C.CONDATADM AS \"ADMISSAO\" " +
                $" FROM {database}.dbo.Pessoal P" +
                $"  INNER JOIN {database}.dbo.Contratos C on P.PESSOAID = C.PESSOAID " +
                $"  INNER JOIN {database}.dbo.Cargos G ON C.CONCARADM = G.CARGOID" +
                $"  INNER JOIN {database}.dbo.Lotacao L ON C.LOTACAOID = L.LOTACAOID " +
                $"  INNER JOIN {database}.dbo.Empresas E ON E.EMPRESAID = L.EMPRESAID " +
                $" WHERE C.DATDESLIGA IS NULL OR C.DATDESLIGA  >= '2025-07-01' OR C.DATDESLIGA = '1900-01-01'";

            var pessoas = await Helper.ExecuteReader(Connection, sql);
            var list = new List<Pessoa>();

            while (pessoas.Read())
            {
                var pessoa = new Pessoa
                {
                    codigo = pessoas["ID"].ToString().Trim(),
                    nome = pessoas["NOME"].ToString().Trim(),
                    cpf = pessoas["DOCUMENTO"].ToString().Trim(),
                    cnpjEmpresa = pessoas["ESCGC"].ToString().Trim(),
                    email = pessoas["EMAIL"].ToString().Trim(),
                    telefone = pessoas["TELEFONE"].ToString().Trim(),
                    status = 1,
                    salario = decimal.TryParse(pessoas["SALARIO"].ToString().Trim(), out var salario) ? salario : 0,
                    admissao = DateTime.TryParse(pessoas["ADMISSAO"].ToString().Trim(), out var admissao) ? admissao : DateTime.MinValue,
                    contratoid = pessoas["CONTRATOID"].ToString().Trim(),
                    lotacao = pessoas["LOTACAO"].ToString().Trim(),
                    cbo = pessoas["CBO"].ToString().Trim(),
                };
                list.Add(pessoa);
            }

            pessoas.Close();
            pessoas.Dispose();

            EventNotifier.NotifyStatus(CurrentTaskIndex, message, CurrentTaskProgress, "Enviando...", MaxTasks, list.Count);
            foreach (var empresa in list)
            {
                EventNotifier.NotifyStatus(CurrentTaskIndex, message, ++CurrentTaskProgress, $"Enviando {empresa.nome} ...", MaxTasks, list.Count);

                var response = await HttpHelper.PutAsync<APIResponse<Empresa>>($"/integration/pessoas/{empresa.codigo}", empresa);
                if (!response.Success)
                {
                    throw new System.Exception($"Falha ao enviar a pessoa {empresa.nome}.");
                }
            }

            EventNotifier.NotifyStatus(CurrentTaskIndex, message, list.Count, "Pronto!", MaxTasks, list.Count);
            await Task.Delay(300);
        }

        private async Task EnviarRecibos(string database)
        {
            CurrentTaskProgress = 0;

            string message = $"Enviando Recibos - {database}";

            CurrentTaskIndex++;
            EventNotifier.NotifyStatus(CurrentTaskIndex, message, CurrentTaskProgress, "Consultando...", MaxTasks, 1);

            string sql = $@"
                WITH ContratosFiltrados AS (
                    SELECT c.CONTRATOID, e.PERIODO
                    FROM {database}.dbo.Esocial_Processado_Eventos e
                    JOIN {database}.dbo.Contratos c ON c.CONTRATOID = e.ID_REGISTRO
                    WHERE e.TIPO_EVENTO = 'S1200'
                      AND e.CODSTATUS = '201'
                      AND e.PERIODO >= '202401'
                    GROUP BY c.CONTRATOID, e.PERIODO
                )
                SELECT em.EMPRESAID, em.ESRAZAO, 
                       (CASE WHEN em.EMPTIPO = '3' THEN em.ESCPF 
                             ELSE em.ESCGC END) INSCRICAO_EMPRESA,
                       cr.*, 
                       e.DESCRICAO DESCRI_EVENTO,
                       CASE WHEN e.TIPO_VDB = 'V' THEN 'Vencimento' 
                            WHEN e.TIPO_VDB = 'D' THEN 'Desconto'
                            ELSE 'Base' END TIPO_EVENTO,
                       RTRIM(p.PESNOME) + ' ' + p.PESSOBNOM as NOME_COMPLETO,
                       p.PESCPFINS,
                       CASE WHEN cr.ROTINA = '01' THEN 'Adiantamento Salarial' 
                            WHEN cr.ROTINA = '02' THEN 'Folha Mensal' 
                            WHEN cr.ROTINA = '03' THEN 'Adiantamento 13o Salario' 
                            WHEN cr.ROTINA = '04' THEN '13o Salario'            
                            ELSE '' END DESCRI_ROTINA,
                       cr.PAGAMENTO
                FROM {database}.dbo.CalculosFolha cr
                JOIN ContratosFiltrados cf ON cr.CONTRATOID = cf.CONTRATOID AND cf.PERIODO = cr.ANOMES
                JOIN {database}.dbo.Contratos c on c.CONTRATOID = cr.CONTRATOID and isnull(c.DATDESLIGA,'19000101') = '19000101'
                JOIN {database}.dbo.Empresas em on em.EMPRESAID = c.EMPRESAID
                JOIN {database}.dbo.Pessoal p on p.PESSOAID = c.PESSOAID
                JOIN {database}.dbo.Eventos e on e.EVENTOID = cr.EVENTOID
                WHERE
	                cr.ANOMES >= '202401'
                ORDER BY cr.CONTRATOID, cr.PAGAMENTO, cr.ROTINA, cr.EVENTOID
            ";

            var recibos = await Helper.ExecuteReader(Connection, sql);
            var list = new List<Recibo>();
            string grupo;

            var read = recibos.Read();

            while (read)
            {
                grupo = recibos["CONTRATOID"].ToString() + recibos["PAGAMENTO"].ToString() + recibos["ROTINA"].ToString();

                var recibo = new Recibo
                {
                    cnpjEmpresa = recibos["INSCRICAO_EMPRESA"].ToString().Trim(),
                    cpf = recibos["PESCPFINS"].ToString().Trim(),
                    anoMes = recibos["ANOMES"].ToString().Trim(),
                    rotina = recibos["DESCRI_ROTINA"].ToString().Trim(),
                    dataPagamento = DateTime.TryParse(recibos["PAGAMENTO"].ToString().Trim(), out var pagamento) ? pagamento : DateTime.MinValue,
                };

                while (read && grupo == recibos["CONTRATOID"].ToString() + recibos["PAGAMENTO"].ToString() + recibos["ROTINA"].ToString())
                {
                    var evento = new Evento
                    {
                        descricao = recibos["DESCRI_EVENTO"].ToString().Trim(),
                        tipo = recibos["TIPO_EVENTO"].ToString().Trim(),
                        referencia = decimal.TryParse(recibos["REFERENCIA"].ToString().Trim(), out var referencia) ? referencia : 0,
                        valor = decimal.TryParse(recibos["VALOR"].ToString().Trim(), out var valor) ? valor : 0,
                        eventoId = recibos["EVENTOID"].ToString().Trim()
                    };

                    recibo.eventos.Add(evento);

                    read = recibos.Read();
                }

                list.Add(recibo);
            }

            recibos.Close();
            recibos.Dispose();

            EventNotifier.NotifyStatus(CurrentTaskIndex, message, CurrentTaskProgress, "Enviando...", MaxTasks, list.Count);
            foreach (var empresa in list)
            {
                EventNotifier.NotifyStatus(CurrentTaskIndex, message, ++CurrentTaskProgress, $"Enviando...", MaxTasks, list.Count);

                var response = await HttpHelper.PutAsync<APIResponse<Empresa>>($"/integration/recibos/{empresa.cpf}-{empresa.cnpjEmpresa}", empresa);
                if (!response.Success)
                {
                    MessageBox.Show(JsonSerializer.Serialize(empresa));

                    throw new System.Exception($"Falha ao enviar o recibo.");
                }
            }

            EventNotifier.NotifyStatus(CurrentTaskIndex, message, list.Count, "Pronto!", MaxTasks, list.Count);
            await Task.Delay(300);
        }

        private async Task EnviarDecimo(string database)
        {
            CurrentTaskProgress = 0;

            string message = $"Enviando Décimo - {database}";

            CurrentTaskIndex++;
            EventNotifier.NotifyStatus(CurrentTaskIndex, message, CurrentTaskProgress, "Consultando...", MaxTasks, 1);

            string sql = $@"
                WITH ContratosFiltrados AS (
                    SELECT c.CONTRATOID, e.PERIODO
                    FROM {database}.dbo.Esocial_Processado_Eventos e
                    JOIN {database}.dbo.Contratos c ON c.CONTRATOID = e.ID_REGISTRO
                    WHERE e.TIPO_EVENTO = 'S1200'
                      AND e.CODSTATUS = '201'
                      AND e.PERIODO >= '202401'
                    GROUP BY c.CONTRATOID, e.PERIODO
                )
                SELECT em.EMPRESAID, em.ESRAZAO, 
                       (CASE WHEN em.EMPTIPO = '3' THEN em.ESCPF 
                             ELSE em.ESCGC END) INSCRICAO_EMPRESA,
                       cr.*, 
                       e.DESCRICAO DESCRI_EVENTO,
                       CASE WHEN e.TIPO_VDB = 'V' THEN 'Vencimento' 
                            WHEN e.TIPO_VDB = 'D' THEN 'Desconto'
                            ELSE 'Base' END TIPO_EVENTO,
                       RTRIM(p.PESNOME) + ' ' + p.PESSOBNOM as NOME_COMPLETO,
                       p.PESCPFINS,
                       CASE WHEN cr.ROTINA = '01' THEN 'Adiantamento Salarial' 
                            WHEN cr.ROTINA = '02' THEN 'Folha Mensal' 
                            WHEN cr.ROTINA = '03' THEN 'Adiantamento 13o Salario' 
                            WHEN cr.ROTINA = '04' THEN '13o Salario'            
                            ELSE '' END DESCRI_ROTINA,
                       cr.PAGAMENTO
                FROM {database}.dbo.CalculosFolha cr
                JOIN ContratosFiltrados cf ON cr.CONTRATOID = cf.CONTRATOID AND cf.PERIODO = cr.ANOMES
                JOIN {database}.dbo.Contratos c on c.CONTRATOID = cr.CONTRATOID and isnull(c.DATDESLIGA,'19000101') = '19000101'
                JOIN {database}.dbo.Empresas em on em.EMPRESAID = c.EMPRESAID
                JOIN {database}.dbo.Pessoal p on p.PESSOAID = c.PESSOAID
                JOIN {database}.dbo.Eventos e on e.EVENTOID = cr.EVENTOID
                WHERE cr.ROTINA IN ('03','04')
                AND cr.ANOMES >= '202401'
                ORDER BY cr.CONTRATOID, cr.PAGAMENTO, cr.ROTINA, cr.EVENTOID
            ";

            //MessageBox.Show(sql);

            var recibos = await Helper.ExecuteReader(Connection, sql);
            var list = new List<Recibo>();
            string grupo;

            var read = recibos.Read();

            while (read)
            {
                grupo = recibos["CONTRATOID"].ToString() + recibos["PAGAMENTO"].ToString() + recibos["ROTINA"].ToString();

                var recibo = new Recibo
                {
                    cnpjEmpresa = recibos["INSCRICAO_EMPRESA"].ToString().Trim(),
                    cpf = recibos["PESCPFINS"].ToString().Trim(),
                    anoMes = recibos["ANOMES"].ToString().Trim(),
                    rotina = recibos["DESCRI_ROTINA"].ToString().Trim(),
                    dataPagamento = DateTime.TryParse(recibos["PAGAMENTO"].ToString().Trim(), out var pagamento) ? pagamento : DateTime.MinValue,
                };

                while (read && grupo == recibos["CONTRATOID"].ToString() + recibos["PAGAMENTO"].ToString() + recibos["ROTINA"].ToString())
                {
                    var evento = new Evento
                    {
                        descricao = recibos["DESCRI_EVENTO"].ToString().Trim(),
                        tipo = recibos["TIPO_EVENTO"].ToString().Trim(),
                        referencia = decimal.TryParse(recibos["REFERENCIA"].ToString().Trim(), out var referencia) ? referencia : 0,
                        valor = decimal.TryParse(recibos["VALOR"].ToString().Trim(), out var valor) ? valor : 0,
                        eventoId = recibos["EVENTOID"].ToString().Trim()
                    };

                    recibo.eventos.Add(evento);

                    read = recibos.Read();
                }

                list.Add(recibo);
            }

            recibos.Close();
            recibos.Dispose();

            EventNotifier.NotifyStatus(CurrentTaskIndex, message, CurrentTaskProgress, "Enviando...", MaxTasks, list.Count);
            foreach (var empresa in list)
            {
                EventNotifier.NotifyStatus(CurrentTaskIndex, message, ++CurrentTaskProgress, $"Enviando...", MaxTasks, list.Count);

                var response = await HttpHelper.PutAsync<APIResponse<Empresa>>($"/integration/recibos/{empresa.cpf}-{empresa.cnpjEmpresa}", empresa);
                if (!response.Success)
                {
                    MessageBox.Show(JsonSerializer.Serialize(empresa));

                    throw new System.Exception($"Falha ao enviar o recibo.");
                }
            }

            EventNotifier.NotifyStatus(CurrentTaskIndex, message, list.Count, "Pronto!", MaxTasks, list.Count);
            await Task.Delay(300);
        }

        private async Task EnviarFerias(string database)
        {
            CurrentTaskProgress = 0;

            string message = $"Enviando Férias - {database}";

            CurrentTaskIndex++;
            EventNotifier.NotifyStatus(CurrentTaskIndex, message, CurrentTaskProgress, "Consultando...", MaxTasks, 1);

            string sql = $@"
                WITH ContratosFiltrados AS (
                        SELECT f.FERGOZID
                        FROM {database}.dbo.Esocial_Processado_Eventos e
                        JOIN {database}.dbo.FeriasGozadas f ON f.FERGOZID = e.ID_REGISTRO
                        WHERE e.TIPO_EVENTO = 'S2230'
                          AND e.CODSTATUS = '201'      
                          AND f.DATAPAG >= '2024-01-01'
                        GROUP BY f.FERGOZID
                    )
                    SELECT em.EMPRESAID, em.ESRAZAO, 
                           (CASE WHEN em.EMPTIPO = '3' THEN em.ESCPF 
                                 ELSE em.ESCGC END) INSCRICAO_EMPRESA,
                           cr.*, 
                           e.DESCRICAO DESCRI_EVENTO,
                           CASE WHEN e.TIPO_VDB = 'V' THEN 'Vencimento' 
                                WHEN e.TIPO_VDB = 'D' THEN 'Desconto'
                                ELSE 'Base' END TIPO_EVENTO,
                           RTRIM(p.PESNOME) + ' ' + p.PESSOBNOM as NOME_COMPLETO,
                           p.PESCPFINS,
                           CASE WHEN cr.ROTINA = '07' THEN 'Férias' 
                                WHEN cr.ROTINA = '08' THEN 'Férias Complementar' 
                                ELSE '' END DESCRI_ROTINA,
                           cr.DATAPAG as PAGAMENTO
                    FROM ContratosFiltrados cf
                    JOIN {database}.dbo.FeriasGozadas f on f.FERGOZID = cf.FERGOZID
                    JOIN {database}.dbo.CalculosFerias cr ON cr.CONTRATOID = f.CONTRATOID and cr.DATAIGOZ = f.DATAIGOZ and cr.DATAIPEN = f.DATAIPEN
                    JOIN {database}.dbo.Contratos c on c.CONTRATOID = cr.CONTRATOID and isnull(c.DATDESLIGA,'19000101') = '19000101'
                    JOIN {database}.dbo.Empresas em on em.EMPRESAID = c.EMPRESAID
                    JOIN {database}.dbo.Pessoal p on p.PESSOAID = c.PESSOAID
                    JOIN {database}.dbo.Eventos e on e.EVENTOID = cr.EVENTOID
                      --WHERE {database}.dbo.UFN_DataParaAnoMes(cr.DATAPAG) >= '202401'
                    ORDER BY cr.CONTRATOID, cr.DATAIGOZ, cr.ROTINA, cr.EVENTOID
            ";

            var recibos = await Helper.ExecuteReader(Connection, sql);
            var list = new List<Recibo>();
            string grupo;

            var read = recibos.Read();

            while (read)
            {
                grupo = recibos["CONTRATOID"].ToString() + recibos["PAGAMENTO"].ToString() + recibos["ROTINA"].ToString();

                var data = ((DateTime)recibos["PAGAMENTO"]);
                var recibo = new Recibo
                {
                    cnpjEmpresa = recibos["INSCRICAO_EMPRESA"].ToString().Trim(),
                    cpf = recibos["PESCPFINS"].ToString().Trim(),
                    anoMes = data.ToString("yyyyMM"),  
                    rotina = recibos["DESCRI_ROTINA"].ToString().Trim(),
                    dataPagamento = DateTime.TryParse(recibos["PAGAMENTO"].ToString().Trim(), out var pagamento) ? pagamento : DateTime.MinValue,
                };

                while (read && grupo == recibos["CONTRATOID"].ToString() + recibos["PAGAMENTO"].ToString() + recibos["ROTINA"].ToString())
                {
                    var evento = new Evento
                    {
                        descricao = recibos["DESCRI_EVENTO"].ToString().Trim(),
                        tipo = recibos["TIPO_EVENTO"].ToString().Trim(),
                        referencia = decimal.TryParse(recibos["REFERENCIA"].ToString().Trim(), out var referencia) ? referencia : 0,
                        valor = decimal.TryParse(recibos["VALOR"].ToString().Trim(), out var valor) ? valor : 0,
                        eventoId = recibos["EVENTOID"].ToString().Trim()
                    };

                    recibo.eventos.Add(evento);

                    read = recibos.Read();
                }

                list.Add(recibo);
            }

            recibos.Close();
            recibos.Dispose();

            EventNotifier.NotifyStatus(CurrentTaskIndex, message, CurrentTaskProgress, "Enviando...", MaxTasks, list.Count);
            foreach (var empresa in list)
            {
                EventNotifier.NotifyStatus(CurrentTaskIndex, message, ++CurrentTaskProgress, $"Enviando...", MaxTasks, list.Count);

                if (empresa.eventos.Count == 0)
                    continue;   

                var response = await HttpHelper.PutAsync<APIResponse<Empresa>>($"/integration/recibos/{empresa.cpf}-{empresa.cnpjEmpresa}", empresa);
                if (!response.Success)
               {
                    MessageBox.Show(JsonSerializer.Serialize(empresa));

                    throw new System.Exception($"Falha ao enviar o recibo.");
                }         
            }

            EventNotifier.NotifyStatus(CurrentTaskIndex, message, list.Count, "Pronto!", MaxTasks, list.Count);
            await Task.Delay(300);
        }

        private void CloseAll() {
            if (Connection != null)
            {
                Connection.Close();
                Connection.Dispose();
            }

            Helper = null;
            Config = null;
        }

        private void Pronto()
        {
            CloseAll();

            EventNotifier.NotifyStatus(0, "Sincronização concluída!", 0, "Todas as tarefas foram concluídas com sucesso.", MaxTasks, 1);
            CurrentTaskIndex = 1;
            CurrentTaskProgress = 0;
            MaxTasks = 0;
        }
    }
}
