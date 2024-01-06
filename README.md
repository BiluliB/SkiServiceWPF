# SkiServiceWPF Anwendung

# Disclaimer

## Konfiguration der Startup-Einstellung nach dem Klonen

Nachdem Sie das Projekt geklont haben, müssen Sie die Startup-Einstellungen in Visual Studio konfigurieren, um sicherzustellen, dass die richtigen Projekte beim Starten der Lösung ausgeführt werden.

1. **Öffnen Sie die Lösung in Visual Studio**:

   - Öffnen Sie die `.sln`-Datei des Projekts in Visual Studio.

2. **Einstellung des Startprojekts**:

   - Klicken Sie im Solution Explorer mit der rechten Maustaste auf die Lösung und wählen Sie „Properties“ (Eigenschaften).
   - Wechseln Sie im linken Menü zu „Common Properties“ > „Startup Project“.
   - Wählen Sie entweder „Single startup project“ (Einzelnes Startprojekt) und wählen Sie das Hauptprojekt aus der Liste aus, oder wählen Sie „Multiple startup projects“ (Mehrere Startprojekte) und setzen Sie die gewünschten Projekte auf „Start“.

3. **Anwenden der Einstellungen**:
   - Nachdem Sie Ihre Auswahl getroffen haben, klicken Sie auf „OK“, um die Einstellungen zu speichern.
   - Jetzt können Sie die Lösung starten, und die ausgewählten Projekte werden entsprechend Ihrer Konfiguration gestartet.

Diese Einstellung ist besonders wichtig, wenn Ihre Lösung aus mehreren Projekten besteht, die zusammenarbeiten müssen, wie z.B. einem Frontend und einem Backend.

---

**Um die ListView nach einem Suchvorgang oder Löschen zu aktualisieren, muss zweimal auf "Alle Aufträge" im Preview-Doppel geklickt werden.**

---

# Inhaltsverzeichnis

- [Disclaimer](#disclaimer)
- [Überblick](#überblick)
- [Technische Details](#technische-details)
  - [Hervorhebungen](#hervorhebungen)
- [Installation](#installation)
  - [Voraussetzungen](#voraussetzungen)
  - [Schritte zur Installation](#schritte-zur-installation)
  - [JetstreamSkiserviceAPI](#jetstreamskiserviceapi)
- [Benutzung](#benutzung)
  - [Anmeldung](#anmeldung)
  - [Dashboard](#dashboard)
  - [Auftragsverwaltung](#auftragsverwaltung)
  - [Weitere Funktionen](#weitere-funktionen)
- [Konfiguration](#konfiguration)
  - [Schlüsselelemente der Konfiguration](#schlüsselelemente-der-konfiguration)
- [Tests](#tests)
- [Beitrag](#beitrag)
- [Mitwirkende](#mitwirkende)

## Überblick

SkiServiceWPF ist eine WPF-basierte Anwendung zur Verwaltung von Skiservice-Aufträgen. Diese Anwendung ermöglicht es Benutzern, Aufträge zu erstellen, zu bearbeiten und den Status von Skiservice-Aufträgen effizient zu verfolgen.

## Technische Details

Die SkiServiceWPF-Anwendung ist mit dem .NET 8.0 Framework entwickelt, was eine moderne und leistungsstarke Plattform für die Erstellung von Windows-Anwendungen bietet. Dies ermöglicht eine nahtlose Integration mit dem Windows-Betriebssystem und eine hohe Leistungsfähigkeit.

### Hervorhebungen:

- **.NET 8.0**: Die neueste Version des .NET Frameworks bietet verbesserte Performance, Sicherheit und eine breite Palette von APIs, die es ermöglichen, komplexe Anwendungen mit weniger Aufwand zu entwickeln.
- **Windows Presentation Foundation (WPF)**: Für die Benutzeroberfläche verwendet das Projekt WPF, ein UI-Framework, das reichhaltige und interaktive Benutzeroberflächen ermöglicht. Es unterstützt auch eine Trennung von Logik und Darstellung, was die Wartbarkeit und Testbarkeit der Anwendung verbessert.
- **MVVM-Architektur**: Das Model-View-ViewModel (MVVM) Muster wird genutzt, um eine klare Trennung zwischen der Benutzeroberfläche und der Geschäftslogik zu gewährleisten. Dies erleichtert die Wartung und Erweiterung der Anwendung.
- **Dependency Injection**: Durch die Verwendung von Dependency Injection wird die Kopplung zwischen verschiedenen Komponenten der Anwendung reduziert, was zu einer flexibleren und wartbaren Codebasis führt.
- **RestSharp und Newtonsoft.Json**: Für Netzwerkkommunikation und Datenverarbeitung werden Bibliotheken wie RestSharp und Newtonsoft.Json eingesetzt, die robuste und effiziente Methoden für HTTP-Anfragen und JSON-Verarbeitung bieten.
- **Unit Testing**: Die Anwendung setzt auf umfangreiche Unit Tests, um die Zuverlässigkeit und Stabilität der Funktionalitäten sicherzustellen. Dies wird durch die Verwendung von xUnit und Moq für Mocking unterstützt.

Durch diese Kombination von Technologien und Praktiken stellt SkiServiceWPF eine solide Basis für die Entwicklung einer modernen, zuverlässigen und wartbaren Desktop-Anwendung dar.

## Installation

### Voraussetzungen

Um das SkiServiceWPF-Projekt zu installieren und auszuführen, stellen Sie bitte sicher, dass die folgenden Voraussetzungen erfüllt sind:

- .NET 8.0 SDK: Das Projekt wurde mit .NET 8.0 entwickelt, daher ist es notwendig, dass Sie das entsprechende SDK installiert haben.
- Visual Studio oder Visual Studio Code: Diese Entwicklungsumgebungen sind für das Öffnen und Bearbeiten des Projekts empfohlen. Visual Studio bietet eine umfassende Unterstützung für .NET-Projekte und WPF-Anwendungen.

### Schritte zur Installation

1. **Repository klonen:**

   - Verwenden Sie den folgenden Befehl, um das Projekt-Repository zu klonen:
     ```
     git clone https://github.com/BiluliB/SkiServiceWPF.git
     ```

2. **Projekt in der Entwicklungsumgebung öffnen:**

   - Öffnen Sie die geklonte Projektmappe (`*.sln`) in Visual Studio oder Visual Studio Code.

3. **NuGet-Pakete wiederherstellen:**

   - Stellen Sie sicher, dass alle erforderlichen NuGet-Pakete korrekt wiederhergestellt werden. Dies kann automatisch von Visual Studio beim Öffnen des Projekts oder manuell über den NuGet-Paket-Manager erfolgen.

   #### NuGet-Pakete für SkiServiceWPF:

   Die folgenden Pakete müssen für das `SkiServiceWPF`-Projekt installiert werden:

   - Fluent.Ribbon (10.0.4)
   - Gu.Wpf.Adorners (2.1.1)
   - Microsoft.Extensions.Configuration (8.0.0)
   - Microsoft.Extensions.Configuration.Binder (8.0.0)
   - Microsoft.Extensions.Configuration.FileExtensions (8.0.0)
   - Microsoft.Extensions.Configuration.Json (8.0.0)
   - Microsoft.Extensions.DependencyInjection (8.0.0)
   - Microsoft.Extensions.Http (8.0.0)
   - Newtonsoft.Json (13.0.3)
   - RestSharp (110.2.0)

   #### NuGet-Pakete für SkiService.Test:

   Für das `SkiService.Test`-Projekt sind folgende Pakete erforderlich:

   - Microsoft.Extensions.Configuration.Abstractions (8.0.0)
   - Microsoft.NET.Test.Sdk (17.8.0)
   - Moq (4.20.70)
   - xunit (2.6.4)
   - xunit.runner.visualstudio (2.5.6)
   - coverlet.collector (6.0.0)

Stellen Sie sicher, dass alle diese Pakete über den NuGet-Paket-Manager in Ihrer Entwicklungsumgebung installiert sind, um eine reibungslose Funktionalität und Tests des Projekts zu gewährleisten.

4. **Datenbankerstellung mit Entity Framework Core Migrations**:

   - Öffnen Sie die Kommandozeile oder das Terminal im Projektverzeichnis.
   - Führen Sie den Befehl `dotnet ef migrations add InitialCreate` aus, um eine Erstmigration für Ihre Datenbank zu erstellen.
   - Führen Sie anschließend den Befehl `dotnet ef database update` aus, um die Datenbank basierend auf der erstellten Migration zu initialisieren und zu erstellen.

Nachdem Sie diese Schritte abgeschlossen haben, sollten Sie in der Lage sein, das Projekt zu bauen und auszuführen.

### JetstreamSkiserviceAPI

Infos für das JetstreamSkiserviceAPI Backend unter diesem Link:
https://github.com/mahgoe/ICT-Module295-JetstreamWebAPI

## Benutzung

# Disclaimer

**Um die ListView nach einem Suchvorgang oder Löschen zu aktualisieren, muss zweimal auf "Alle Aufträge" im Preview-Doppel geklickt werden.**

Die SkiServiceWPF-Anwendung bietet eine intuitive und benutzerfreundliche grafische Oberfläche, die speziell für die effiziente Verwaltung von Skiservice-Aufträgen entwickelt wurde. Die Benutzung der Anwendung umfasst mehrere Schlüsselaspekte:

### Anmeldung

- Beim Starten der Anwendung werden die Benutzer zu einem Anmeldebildschirm geführt, wo sie sich mit ihren Anmeldedaten einloggen können.
- Der Anmeldevorgang ist sicher und stellt sicher, dass nur autorisierte Benutzer Zugang zum System haben.

### Dashboard

- Nach der Anmeldung gelangen die Benutzer zum Haupt-Dashboard. Hier erhalten sie eine Übersicht über alle aktuellen und anstehenden Skiservice-Aufträge.
- Das Dashboard ist intuitiv gestaltet, um den Benutzern eine effiziente Navigation und Auftragsverwaltung zu ermöglichen.

### Auftragsverwaltung

- Benutzer können neue Skiservice-Aufträge hinzufügen, vorhandene Aufträge bearbeiten und den Status von Aufträgen verfolgen.
- Die Anwendung ermöglicht eine detaillierte Ansicht jedes Auftrags, einschließlich spezifischer Informationen wie Serviceart, Kundeninformationen und Fälligkeitsdatum.

### Weitere Funktionen

- Die Anwendung bietet zusätzliche Funktionen wie die Suche und Filterung von Aufträgen, um Benutzern zu helfen, schnell die benötigten Informationen zu finden.
- Benutzer können auch ihre persönlichen Einstellungen und Präferenzen anpassen, um die Nutzung der Anwendung zu optimieren.

Diese Anleitung bietet einen grundlegenden Überblick über die wichtigsten Funktionen und die Benutzung der SkiServiceWPF-Anwendung. Weitere spezifische Anleitungen oder Hilfestellungen finden Benutzer in der integrierten Hilfe der Anwendung.

## Konfiguration

Die Konfiguration der SkiServiceWPF-Anwendung ist zentralisiert und benutzerfreundlich gestaltet. Alle wichtigen Konfigurationseinstellungen können über die `appsettings.json`-Datei in jedem Unterprojekt vorgenommen werden. Diese Datei bietet eine flexible Möglichkeit, die Anwendung an verschiedene Betriebsumgebungen und Anforderungen anzupassen.

### Schlüsselelemente der Konfiguration:

1. **Datenbankverbindungen**: In der `appsettings.json`-Datei können Sie die Verbindungsstrings für die Datenbank definieren, die von der Anwendung genutzt wird. Dies ermöglicht die Anpassung an unterschiedliche Datenbanksysteme oder Server.

2. **API-Endpunkte**: Für Unterprojekte, die externe APIs nutzen, können die Endpunkt-URLs und zugehörige Konfigurationseinstellungen in dieser Datei festgelegt werden.

3. **Anwendungseinstellungen**: Hier können Sie verschiedene Anwendungsparameter konfigurieren, wie z.B. Logging-Level, Anwendungsspezifische Parameter oder Feature-Toggles.

### Bearbeitung der Konfigurationsdatei:

- Öffnen Sie die `appsettings.json`-Datei im jeweiligen Unterprojekt mit einem Texteditor oder innerhalb Ihrer Entwicklungsumgebung.
- Ändern Sie die Werte entsprechend Ihren Anforderungen.
- Speichern Sie die Datei und starten Sie die Anwendung neu, um die Änderungen zu übernehmen.

### Sicherheitshinweise:

- Achten Sie darauf, sensible Informationen wie Datenbankpasswörter oder API-Schlüssel nicht in der Konfigurationsdatei im Quellcode-Repository zu speichern. Verwenden Sie stattdessen sichere Methoden zur Speicherung solcher Informationen, beispielsweise Umgebungsvariablen oder sichere Konfigurationsspeicher.

Die Flexibilität und Zugänglichkeit der Konfigurationsdatei ermöglichen es, die Anwendung schnell und effizient an unterschiedliche Betriebsbedingungen anzupassen.

## Tests

- Unit-Tests für das Backend befinden sich im `.\SkiServiceWPF\SkiService.Tests\TestResults` -Verzeichnis.

## Beitrag

Interessierte können zum Projekt beitragen, indem sie Pull Requests erstellen oder Verbesserungsvorschläge einreichen.

## Mitwirkende

Das SkiServiceWPF-Projekt wurde durch die gemeinsamen Anstrengungen und die Zusammenarbeit der folgenden Entwickler realisiert:

- **BiluliB** ([Profil ansehen](https://github.com/BiluliB)) - Mitwirkender Entwickler, verantwortlich für verschiedene Aspekte des Projekts, von der Konzeption bis zur Umsetzung.
- **mahgoe** ([Profil ansehen](https://github.com/mahgoe)) - Mitwirkender Entwickler, beteiligt an allen Phasen des Projekts, von der Planung bis zur Implementierung.

Dieses Projekt ist das Ergebnis einer engen Zusammenarbeit, bei der beide Entwickler gleichberechtigt zu Konzeption, Entwicklung, Design und Testing beigetragen haben. Es gab keinen festen Projektleiter, da alle Entscheidungen und Beiträge gemeinsam diskutiert und umgesetzt wurden.
