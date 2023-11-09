using Cal.Data;
using Microsoft.EntityFrameworkCore;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1.Data;


namespace Cal.Services
{
    public class EmailService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public EmailService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {                    
                DateTime now = DateTime.Now;

                DateTime nextDay = now.AddDays(1);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var events = _context.Events
                        .Include(e => e.AppUser)
                        .Where(e => e.Date.Date == nextDay.Date)
                        .ToList();

                    foreach (var e in events)
                    {
                        SendEmailAsync(e.AppUser.Email, "Напоминание", $"Вы не забыли про{e.Name}?");
                    }
                }
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }

        public async Task SendEmailAsync(string recipientEmail, string subject, string _message)
        {
            UserCredential credential;

            using (var stream = new FileStream("F://Уник//8ao1c5qtb33j0qhouiahd7qvh0javm4h.apps.googleusercontent.com.json", FileMode.Open, FileAccess.Read))
            {
                // Load the credentials
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { GmailService.Scope.GmailSend },
                    "user",
                    CancellationToken.None,
                    new FileDataStore("Gmail.API.Auth.Store")
                ).Result;
            }

            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Calendar"
            });

            var message = new MimeKit.MimeMessage();
            message.From.Add(new MimeKit.MailboxAddress("Testing", "dinsidemh@gmail.com"));
            message.To.Add(new MimeKit.MailboxAddress(recipientEmail, recipientEmail));
            message.Subject = "Календарь";
            message.Body = new MimeKit.TextPart("plain")
            {
                Text = _message
            };

            var emailMessage = new Google.Apis.Gmail.v1.Data.Message
            {
                Raw = Base64UrlEncode(message.ToString())
            };

            var result = service.Users.Messages.Send(emailMessage, "me").Execute();
        }

        private string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");
        }
    }
}
