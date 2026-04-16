using FirebaseAdmin.Messaging;
using his_backend.Models;
namespace his_backend.Services 
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string fcmToken, string title, string body);
        Task SendBulkNotificationAsync(List<string> fcmTokens, string title, string body);
    }

    public class NotificationService(AppDbContext dbContext) : INotificationService
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task SendNotificationAsync(string fcmToken, string title, string body)
        {
            var message = new Message()
            {
                Token = fcmToken,
                Notification = new Notification()
                {
                    Title = title,
                    Body = body
                }
            };

            await FirebaseMessaging.DefaultInstance.SendAsync(message);
        }

        public async Task SendBulkNotificationAsync(List<string> fcmTokens, string title, string body)
        {
            var messages = fcmTokens.Select(token => new Message()
            {
                Token = token,
                Notification = new Notification()
                {
                    Title = title,
                    Body = body
                }
            }).ToList();

            var response = await FirebaseMessaging.DefaultInstance.SendEachAsync(messages);
            // Có thể log response nếu cần
        }
    }
}