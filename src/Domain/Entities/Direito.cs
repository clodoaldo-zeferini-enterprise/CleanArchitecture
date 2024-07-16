using NetCore.Base.Enum; 
namespace Domain.Entities
{
    public class Direito
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid ProgramaId { get; set; }
        public EStatus Status { get; set; }
        public string NomeDoUsuario { get; set; }
        public string NomeDoPrograma { get; set; }
        public bool SelecionarRegistros { get; set; }
        public bool InserirRegistros { get; set; }
        public bool AlterarRegistros { get; set; }
        public bool ExcluirRegistros { get; set; }
        public bool ExecutarRelatorios { get; set; }
    }
}
