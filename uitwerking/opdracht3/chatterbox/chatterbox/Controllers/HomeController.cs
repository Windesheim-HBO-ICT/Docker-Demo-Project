using chatterbox.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace chatterbox.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private string messageFilePath = "/app/files/messages.txt";

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;

            string directoryPath = Path.GetDirectoryName(messageFilePath);
            
            // Create the directory if it doesn't exists
            if (!Directory.Exists(directoryPath)) {
                Directory.CreateDirectory(directoryPath);
            }

            // Create the log file if it doesn't exist
            if (!System.IO.File.Exists(messageFilePath)) {
                System.IO.File.Create(messageFilePath).Close();
            }
        }

        public IActionResult Index() {
            List<Message> messages = new List<Message>();

            string directoryPath = Path.GetDirectoryName(messageFilePath);
            if (Directory.Exists(directoryPath)) {
                // Read all lines from the log file
                string[] messageEntries = System.IO.File.ReadAllLines(messageFilePath);

                foreach (string entry in messageEntries) {
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

        [HttpPost]
        public IActionResult SaveMesssage(string body) {
            // Ensure the directory exists
            string directoryPath = Path.GetDirectoryName(messageFilePath);
            if (!Directory.Exists(directoryPath)) {
                Directory.CreateDirectory(directoryPath);
            }

            // Append the message to the log file
            using (StreamWriter streamWriter = new StreamWriter(messageFilePath, true)) {
                streamWriter.WriteLine($"{DateTime.Now}: {body}");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
