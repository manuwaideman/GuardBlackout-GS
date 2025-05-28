using BlackoutGuardian.Console.Models;
using System.Text.Json;

namespace BlackoutGuardian.Console.Services
{
    /// <summary>Gerencia eventos de queda de energia.</summary>
    public class EventoService
    {
        private readonly string _caminho = "eventos.json";
        private readonly JsonSerializerOptions _opts = new() { WriteIndented = true };

        public List<EventoQuedaEnergia> Load()
        {
            if (!File.Exists(_caminho)) return new();
            string json = File.ReadAllText(_caminho);
            return JsonSerializer.Deserialize<List<EventoQuedaEnergia>>(json) ?? new();
        }

        /// <summary>Salva a lista de eventos no arquivo.</summary>
        public void Save(List<EventoQuedaEnergia> eventos)
        {
            string json = JsonSerializer.Serialize(eventos, _opts);
            File.WriteAllText(_caminho, json);
        }

        /// <summary>Registra um novo evento de queda de energia.</summary>
        public bool ConfirmarEvento(Guid id)
        {
            var eventos = Load();
            var ev = eventos.FirstOrDefault(e => e.Id == id);

            // Se não achou ou já está confirmado/encerrado, sai
            if (ev is null || ev.Status != "NOVO") return false;

            ev.Status = "CONFIRMADO";

            Save(eventos);
            return true;
        }

    }
}
