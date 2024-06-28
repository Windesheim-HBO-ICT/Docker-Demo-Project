using loglens.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace loglens.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private string logFilePath = "/app/logs/messageLog.txt";

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;

            string directoryPath = Path.GetDirectoryName(logFilePath);
            
            // Create the directory if it doesn't exists
            if (!Directory.Exists(directoryPath)) {
                Directory.CreateDirectory(directoryPath);
            }

            // Create the log file if it doesn't exist
            if (!System.IO.File.Exists(logFilePath)) {
                System.IO.File.Create(logFilePath).Close();
            }
        }

        public IActionResult Index() {
            List<Message> messages = new List<Message>();

            string directoryPath = Path.GetDirectoryName(logFilePath);
            if (Directory.Exists(directoryPath)) {
                // Read all lines from the log file
                string[] logEntries = System.IO.File.ReadAllLines(logFilePath);

                foreach (string entry in logEntries) {
                    string[] parts = entry.Split(new[] { ": " }, 2, StringSplitOptions.None);

                    if (parts.Length == 2) {
                        messages.Add(new Message {
                            CreatedAt = DateTime.Parse(parts[0]),
                            Body = parts[1]
                        });
                    }
                }
            }

            return View(messages);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
