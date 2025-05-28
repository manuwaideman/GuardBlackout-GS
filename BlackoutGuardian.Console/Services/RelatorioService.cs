using System.Text.Json;
using BlackoutGuardian.Console.Models;

namespace BlackoutGuardian.Console.Services;

public class RelatorioService
{
    /// <summary>Gera um relatório de eventos de queda de energia.</summary>
    public void GerarSnapshot(IEnumerable<EventoQuedaEnergia> eventos)
    {
        var counts = eventos
            .GroupBy(e => e.Status)
            .ToDictionary(g => g.Key, g => g.Count());

        string fileName = $"relatorio_{DateTime.UtcNow:yyyyMMddHHmm}.json";
        File.WriteAllText(fileName,
            JsonSerializer.Serialize(counts, new JsonSerializerOptions { WriteIndented = true }));

        System.Console.WriteLine($"Relatório salvo em {fileName}");
    }
}
