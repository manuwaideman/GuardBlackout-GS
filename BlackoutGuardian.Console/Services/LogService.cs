using System.Text.Json;
using BlackoutGuardian.Console.Models;

namespace BlackoutGuardian.Console.Services
{
    /// <summary>Grava e lê logs em logs.json</summary>
    public class LogService
    {
        private readonly string _caminho = "logs.json";
        private readonly JsonSerializerOptions _opts = new() { WriteIndented = true };

        /// <summary>Cria o serviço de logs.</summary>
        public void Append(LogEvento log)
        {
            var lista = Load();
            lista.Add(log);
            var json = JsonSerializer.Serialize(lista, _opts);
            File.WriteAllText(_caminho, json);
        }

        /// <summary>Carrega os logs do arquivo.</summary>
        public List<LogEvento> Load()
        {
            if (!File.Exists(_caminho)) return new();
            var json = File.ReadAllText(_caminho);
            return JsonSerializer.Deserialize<List<LogEvento>>(json) ?? new();
        }
    }
}
