using FirebaseAdmin.Messaging;
using his_backend.Models;
namespace his_backend.Services 
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string fcmToken, string title, string body);
        Task SendReminderAsync(int userId, string timeString);
    }

    public class NotificationService : INotificationService
    {
        private readonly AppDbContext _dbContext;
        public NotificationService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task SendNotificationAsync(string fcmToken, string title, string body)
        {
            var message = new Message()
            {
                Token = fcmToken,
                Notification = new Notification() { Title = title, Body = body }
            };
            await FirebaseMessaging.DefaultInstance.SendAsync(message);
        }
        public async Task SendReminderAsync(int userId, string timeString)
    {
        var user = await _dbContext.Usertokens.FindAsync(userId);
        if (user != null && !string.IsNullOrEmpty(user.FcmToken))
        {
            await SendNotificationAsync(
                user.FcmToken,
                "Nhắc nhở lịch khám",
                $"Bạn có lịch khám vào lúc {timeString}. Vui lòng đến sớm 15 phút nhé!"
            );
        }
    }
    }
}