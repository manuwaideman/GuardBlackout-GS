using BlackoutGuardian.Console.Models;
using BlackoutGuardian.Console.Services;


// ---------- INICIALIZAÇÃO ----------
EventoService eventoService = new();
LogService logService = new();
AlertaService alertaService = new();
RelatorioService relatorioService = new();

/* ---------- LOGIN ---------- */
AuthService auth = new();
Usuario? usuarioLogado;

// Loop até autenticação bem-sucedida
do
{
    Console.Clear();
    Console.WriteLine("=== LOGIN BLACKOUT GUARDIAN ===\n");
    usuarioLogado = auth.Autenticar();

    if (usuarioLogado is null)
    {
        Console.WriteLine("\nCredenciais inválidas. Pressione qualquer tecla e tente de novo...");
        Console.ReadKey();
    }
} while (usuarioLogado is null);

// ---------- LOG DE AUTENTICAÇÃO ----------
Console.WriteLine($"\nBem-vindo, {usuarioLogado.NomeCompleto}!");
Console.WriteLine("Pressione qualquer tecla para abrir o menu...");
Console.ReadKey();

/* ---------- MENU PRINCIPAL ---------- */
int opcao;
do
{
    Console.Clear();
    Console.WriteLine("=== MENU BLACKOUT GUARDIAN ===");
    Console.WriteLine("1. Listar Eventos");
    Console.WriteLine("2. Registrar Evento");
    Console.WriteLine("3. Confirmar Evento");
    Console.WriteLine("4. Gerar Relatório");
    Console.WriteLine("5. Listar Alertas");
    Console.WriteLine("6. Sair");
    Console.Write("Escolha: ");

    int.TryParse(Console.ReadLine(), out opcao);

    switch (opcao)
    {
        case 1: Listar(); break;
        case 2: Registrar(); break;
        case 3: Confirmar(); break;
        case 4: GerarRelatorio(); break;
        case 5: ListarAlertas(); break;
        case 6: Console.WriteLine("Saindo..."); break;
        default: Console.WriteLine("Opção inválida!"); break;
    }

    if (opcao != 6)
    {
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

} while (opcao != 6);

/* ---------- MÉTODOS LOCAIS ---------- */
// Lista todos os eventos registrados
void Listar()
{
    var eventos = eventoService.Load();
    if (eventos.Count == 0)
    {
        Console.WriteLine("Nenhum evento cadastrado.");
        return;
    }

    foreach (var e in eventos)
        Console.WriteLine($"{e.Id} | {e.Latitude},{e.Longitude} | {e.CreatedAt:g} | {e.Source} | {e.Status}");
}

// Registra um novo evento de queda de energia
void Registrar()
{
    try
    {
        var ev = new EventoQuedaEnergia
        {
            Latitude = LerDouble("Latitude : "),
            Longitude = LerDouble("Longitude: "),
            Source = "APP",
            Status = "NOVO"
        };

        var lista = eventoService.Load();
        lista.Add(ev);
        eventoService.Save(lista);

        logService.Append(new LogEvento
        {
            Usuario = usuarioLogado!.NomeUsuario,
            Acao = "REGISTRAR_EVENTO",
            Detalhe = ev.Id.ToString()
        });

        Console.WriteLine("Evento registrado!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }
}

// Confirma um evento existente pelo ID
void Confirmar()
{
    Console.Write("ID do evento a confirmar: ");
    if (!Guid.TryParse(Console.ReadLine(), out var id))
    {
        Console.WriteLine("ID inválido.");
        return;
    }

    if (eventoService.ConfirmarEvento(id))
    {
        logService.Append(new LogEvento
        {
            Usuario = usuarioLogado!.NomeUsuario,
            Acao = "CONFIRMAR_EVENTO",
            Detalhe = id.ToString()
        });

        // após cada confirmação verificamos se geramos alerta
        alertaService.CheckAndGenerate(eventoService.Load());

        Console.WriteLine("Evento confirmado!");
    }
    else
    {
        Console.WriteLine("Evento não encontrado ou já confirmado/encerrado.");
    }
}

// Gera um relatório com os eventos registrados
void GerarRelatorio()
{
    relatorioService.GerarSnapshot(eventoService.Load());
}

// Lista todos os alertas gerados
void ListarAlertas()
{
    var alertas = alertaService.Load();
    if (alertas.Count == 0)
    {
        Console.WriteLine("Nenhum alerta registrado.");
        return;
    }

    foreach (var a in alertas)
        Console.WriteLine($"{a.CreatedAt:g} | {a.Mensagem} (Eventos: {a.TotalEventos})");
}

/* ---------- UTILITÁRIO ---------- */
// Lê um valor double do usuário, com validação
double LerDouble(string prompt)
{
    double valor;
    Console.Write(prompt);
    while (!double.TryParse(Console.ReadLine(), out valor))
        Console.Write("Valor inválido. Tente novamente: ");
    return valor;
}