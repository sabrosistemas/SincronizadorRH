namespace SabroRH.Schemas
{
    public class AutenticacaoResponse
    {
        public string Token { get; set; }
    }

    public class AutenticacaoRequest
    {
        public string cnpj { get; set; }
        public string password { get; set; }
    }
}
