namespace BlackoutGuardian.Console.Models
{
    /// <summary>Registro de ação executada no sistema.</summary>
    public class LogEvento : BaseEntity
    {
        public string Usuario { get; set; } = string.Empty;
        public string Acao { get; set; } = string.Empty;   // e.g. "CONFIRMAR_EVENTO"
        public string Detalhe { get; set; } = string.Empty;   // texto livre
    }
}

