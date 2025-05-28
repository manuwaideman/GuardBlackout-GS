using BlackoutGuardian.Console.Models;

namespace BlackoutGuardian.Console.Services
{
    /// <summary>Serviço de autenticação simples (lista em memória).</summary>
    public class AuthService
    {
        // Lista fixa de usuários – didático.
        private readonly List<Usuario> _users = new()
        {
            new Usuario { NomeUsuario = "admin", Senha = "123", NomeCompleto = "Administrador" },
            new Usuario { NomeUsuario = "user",  Senha = "abc", NomeCompleto = "Usuário Padrão" }
        };

        /// <summary>Valida usuário/senha. Retorna o usuário autenticado ou null.</summary>
        public Usuario? Autenticar()
        {
            System.Console.Write("Usuário: ");
            string? login = System.Console.ReadLine();
            System.Console.Write("Senha  : ");
            string? senha = System.Console.ReadLine();

            return _users.FirstOrDefault(u =>
                u.NomeUsuario.Equals(login, StringComparison.OrdinalIgnoreCase) &&
                u.Senha == senha);
        }
    }
}
