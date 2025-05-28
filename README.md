# Blackout Guardian (Console)  
_Sistema de Monitoramento de Quedas de Energia – FIAP 3ESPX (2025)_

---

## 🎯 Finalidade do sistema
O **Blackout Guardian** demonstra, em aplicativo **console** C# (.NET 8), como registrar, confirmar e analisar ocorrências de blecautes:

* **Registro** de novas falhas (coordenadas geográficas).  
* **Confirmação** de eventos para validação comunitária.  
* **Geração de alertas automáticos** quando há muitos eventos confirmados em curto intervalo.  
* **Relatórios** (snapshot JSON) com a contagem de eventos por status.  
* **Logs** completos de todas as ações para auditoria.  

O programa foi desenvolvido como exercício de boas práticas (encapsulamento, herança, tratamento de erros, estrutura em camadas) exigidas pela disciplina.

---

## ⚙️ Dependências
| Requisito | Versão mínima | Link |
|-----------|---------------|------|
| .NET SDK  | **8.0** (LTS) | <https://dotnet.microsoft.com/download> |
| Git       | 2.x           | <https://git-scm.com> |

> Não utiliza bancos de dados externos nem bibliotecas NuGet adicionais;  
> persistência é feita em arquivos JSON locais usando `System.Text.Json`.

---

## 🚀 Instruções de execução

```bash
# 1. Clonar o projeto
git clone https://github.com/<seu-usuario>/GuardBlackout-GS.git
cd GuardBlackout-GS

# 2. Executar
dotnet run --project BlackoutGuardian.Console

Login inicial (hard-code): admin / 123

Menu principal:
1. Listar Eventos
2. Registrar Evento
3. Confirmar Evento
4. Gerar Relatório
5. Listar Alertas
6. Sair
Arquivos gerados (eventos.json, alertas.json, logs.json, relatorio_*.json) ficam na pasta do executável.


Estrutura de pastas
BlackoutGuardian.Console/
│
├─ BlackoutGuardian.Console.csproj   ← projeto C#
├─ Program.cs                        ← ponto de entrada (login + menu)
│
├─ Models/                           ← entidades de domínio
│   ├─ BaseEntity.cs                 (Id, CreatedAt)
│   ├─ EventoQuedaEnergia.cs         (Latitude, Longitude, Status…)
│   ├─ Usuario.cs                    (NomeUsuario, Senha, etc.)
│   ├─ LogEvento.cs                  (registro de auditoria)
│   └─ Alerta.cs                     (mensagem, totalEventos)
│
├─ Services/                         ← regras de negócio / persistência
│   ├─ AuthService.cs                (login em memória)
│   ├─ EventoService.cs              (CRUD + confirmar)
│   ├─ LogService.cs                 (grava / lê logs JSON)
│   ├─ AlertaService.cs              (gera e lista alertas)
│   └─ RelatorioService.cs           (gera snapshot de status)
│
├─ evidencias/                       ← capturas de tela exigidas
└─ .gitignore                        ← ignora bin/, obj/, .vs/, etc.

Integrantes:
Manoella Herrerias Waideman - rm98906
Felipe Capriotti da Silva Santos - rm98460
Victor Hugo Andrade - rm598460
