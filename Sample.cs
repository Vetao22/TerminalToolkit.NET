using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TerminalToolkit.NET.Core;
using TerminalToolkit.NET.Controls;

namespace TerminalToolkitDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Initialize the singleton terminal application engine
            var app = TerminalApp.Instance;
            using var cts = new CancellationTokenSource();

            // 1. Render ASCII Banner Title at the top
            app.AddView(new AsciiTitle(x: 4, y: 1, text: "DASHBOARD"));

            // 2. Horizontal navigation tabs
            string[] navTabs = { "System", "Security", "Logs" };
            var tabs = new Tabs(x: 4, y: 6, navTabs);
            app.AddView(tabs);

            // 3. Main container panel
            var mainPanel = new Panel(x: 2, y: 8, width: 76, height: 17, title: "Enterprise Management Suite");
            app.AddView(mainPanel);

            // 4. Section divider inside panel
            app.AddView(new Separator(x: 6, y: 10, width: 68, title: "Configuration Parameters"));

            // 5. Text input for server hostname
            var txtHost = new TextBox(x: 6, y: 12, width: 28, initialText: "prod-server-01");
            app.AddView(txtHost);

            // 6. Numeric counter for port selection
            var numPort = new NumericUpDown(x: 36, y: 12, width: 20, label: "Port", min: 1024, max: 65535, initial: 443);
            app.AddView(numPort);

            // 7. Boolean toggle switch for maintenance mode
            var switchMaintenance = new Switch(x: 6, y: 16, label: "Maintenance Mode", initialState: false);
            app.AddView(switchMaintenance);

            // 8. Visual status badge indicating system health
            var badgeStatus = new Badge(x: 42, y: 16, text: "System Online", style: "success");
            app.AddView(badgeStatus);

            // 9. Another section divider for tools
            app.AddView(new Separator(x: 6, y: 19, width: 68, title: "Utilities & Quick Actions"));

            // 10. Interactive dropdown combobox for theme selection
            string[] themes = { "Dark Mode", "Light Mode", "Neon Matrix", "High Contrast" };
            var comboTheme = new ComboBox(x: 6, y: 21, width: 25, themes);
            app.AddView(comboTheme);

            // 11. Action button to trigger a modal confirmation dialog
            var btnAction = new Button(x: 34, y: 21, text: "Save & Deploy");
            
            MessageBox confirmModal = null;
            btnAction.Clicked += () =>
            {
                if (confirmModal == null)
                {
                    // Create and display a modal popup dialog when clicked
                    confirmModal = new MessageBox(x: 18, y: 11, width: 44, title: "Deployment Confirmation", message: "Do you want to apply these configurations?");
                    confirmModal.Confirmed += () =>
                    {
                        confirmModal.IsVisible = false;
                        app.Stop();
                    };
                    confirmModal.Cancelled += () =>
                    {
                        confirmModal.IsVisible = false;
                    };
                    app.AddView(confirmModal);
                    confirmModal.IsFocused = true;
                }
            };
            app.AddView(btnAction);

            // 12. Contextual help tooltip at the bottom of the panel
            var helpTip = new Tooltip(x: 6, y: 23, width: 68, tipMessage: "Use Tab to navigate controls, Arrow keys to adjust values, and Enter to select.");
            app.AddView(helpTip);

            // Set initial keyboard focus on the first interactive element
            txtHost.IsFocused = true;

            // Background task to safely listen for global application exit via ESC key
            var inputTask = Task.Run(() =>
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    if (Console.KeyAvailable)
                    {
                        var keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.Escape)
                        {
                            app.Stop();
                            cts.Cancel();
                            break;
                        }
                    }
                    Thread.Sleep(50);
                }
            }, cts.Token);

            // Run the main render and event loop asynchronously
            await app.RunAsync(cts.Token);

            Console.WriteLine("\nTerminalToolkit dashboard session terminated gracefully.");
        }
    }
}