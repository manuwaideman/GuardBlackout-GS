namespace BlackoutGuardian.Console.Models
{
    /// <summary>
    /// Representa um usuário que pode fazer login no sistema.
    /// Herda Id e CreatedAt de BaseEntity.
    /// </summary>
    public class Usuario : BaseEntity
    {
        /// <summary>Login utilizado para autenticação.</summary>
        public string NomeUsuario { get; set; } = string.Empty;

        /// <summary>Senha simples (apenas para fins didáticos).</summary>
        public string Senha { get; set; } = string.Empty;

        /// <summary>Nome completo exibido após o login.</summary>
        public string NomeCompleto { get; set; } = string.Empty;
    }
}
