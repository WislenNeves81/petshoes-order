namespace Adapter.Email.Interfaces
{
    public interface IEmailNotificationAdapter
    {
        void SendEmail(string subject, string emailTo, string message);
        void SendPurchaseOrderCreatedMail(string name, string email);
    }
}
