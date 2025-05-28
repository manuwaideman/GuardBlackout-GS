using System.Text.Json;
using BlackoutGuardian.Console.Models;

namespace BlackoutGuardian.Console.Services;

/// <summary>Gerencia alertas de eventos de queda de energia.</summary>
public class AlertaService
{
    private readonly string _file = "alertas.json";
    private readonly JsonSerializerOptions _opts = new() { WriteIndented = true };

    // Configurável: 3 confirmações em 10 min
    private const int LIMITE_EVENTOS = 3;
    private static readonly TimeSpan JANELA = TimeSpan.FromMinutes(10);

    /// <summary>Carrega a lista de alertas do arquivo.</summary>
    public List<Alerta> Load()
    {
        if (!File.Exists(_file)) return new();
        return JsonSerializer.Deserialize<List<Alerta>>(File.ReadAllText(_file)) ?? new();
    }

    /// <summary>Salva a lista de alertas no arquivo.</summary>
    public void Save(List<Alerta> lista) =>
        File.WriteAllText(_file, JsonSerializer.Serialize(lista, _opts));

    /// <summary>Gera alerta se houve N confirmações em X minutos.</summary>
    public void CheckAndGenerate(IEnumerable<EventoQuedaEnergia> eventos)
    {
        // pega só CONFIRMADOS dentro da janela
        var recentes = eventos
            .Where(e => e.Status == "CONFIRMADO" &&
                        e.CreatedAt >= DateTime.UtcNow - JANELA)
            .ToList();

        if (recentes.Count < LIMITE_EVENTOS) return;              // nada a fazer

        var alertas = Load();
        alertas.Add(new Alerta
        {
            Mensagem = $"Foram confirmados {recentes.Count} eventos em {JANELA.TotalMinutes} min.",
            TotalEventos = recentes.Count
        });
        Save(alertas);
    }
}
