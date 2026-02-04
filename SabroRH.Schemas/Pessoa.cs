using System;

namespace SabroRH.Schemas
{
    public class Pessoa
    {
        public string codigo { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public string cpf { get; set; }
        public string cnpjEmpresa { get; set; }
        public string lotacao { get; set; }
        public string cbo { get; set; }
        public int status { get; set; }
        public decimal salario { get; set; }
        public DateTime admissao { get; set; }
        
        public string contratoid { get; set; }
    }
}
