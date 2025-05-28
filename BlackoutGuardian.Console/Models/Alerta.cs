namespace BlackoutGuardian.Console.Models
{
    public class Alerta : BaseEntity
    {
        public string Mensagem { get; set; } = string.Empty;
        public int TotalEventos { get; set; }
    }
}
