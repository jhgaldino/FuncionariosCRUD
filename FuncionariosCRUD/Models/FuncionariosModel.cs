using System.ComponentModel;

namespace FuncionariosCRUD.Models
{
    public class FuncionariosModel
    {
        public int IDFuncionario { get; set; }
        [DisplayName("Nome do Funcionário")]
        public string Nome { get; set; }
        public string Cargo { get; set; }
    }
}
