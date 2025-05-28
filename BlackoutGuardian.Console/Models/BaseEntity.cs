namespace BlackoutGuardian.Console.Models
{
    /// <summary>
    /// Entidade base com Id e data de criação, reutilizada por vários modelos.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>Identificador único da entidade.</summary>
        public Guid Id { get; init; } = Guid.NewGuid();

        /// <summary>Data/hora (UTC) em que o objeto foi criado.</summary>
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
