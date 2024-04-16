using braidingweb.Models;
using System.Net.Mail;
using System.Net;

namespace braidingweb.Controllers
{
    public class BookingsConfirmController
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUsername = "your_email@gmail.com";
        private readonly string _smtpPassword = "Sogayise007!$";
        private readonly string _confirmationEmail = "letsgetbraided07@gmail.com";

        public interface IBookingManager
        {
            Task<bool> ProcessBookingAsync(Booking booking);
        }

        // Implement the interface
        public class BookingManager : IBookingManager
        {
            public async Task<bool> ProcessBookingAsync(Booking booking)
            {
                // Implement booking processing logic here
                return true; // Placeholder, replace with actual implementation
            }
        }
        public async Task<bool> ProcessBookingAsync(Booking booking)
        {
            // Validate the booking
            if (!ValidateBooking(booking))
            {
                return false;
            }

            // Save the booking to the database
            bool saved = await SaveBookingToDatabaseAsync(booking);
            if (!saved)
            {
                return false;
            }

            // Send confirmation email to user
            bool emailSent = await SendConfirmationEmailAsync(booking);
            if (!emailSent)
            {
                // Handle email sending failure, rollback database transaction if necessary
                return false;
            }

            // Send booking receipt to your email
            bool receiptSent = await SendBookingReceiptEmailAsync(booking);
            if (!receiptSent)
            {
                // Handle email sending failure, rollback database transaction if necessary
                return false;
            }

            return true;
        }

        private bool ValidateBooking(Booking booking)
        {
            // Perform validation logic here
            // You can check if required fields are not empty, validate email format, etc.
            // Return true if the booking is valid, false otherwise
            return !string.IsNullOrWhiteSpace(booking.Name) &&
                   !string.IsNullOrWhiteSpace(booking.Surname) &&
                   !string.IsNullOrWhiteSpace(booking.Email) &&
                   booking.Date > DateTime.Now; // Example validation for date in the future
        }

        private async Task<bool> SaveBookingToDatabaseAsync(Booking booking)
        {
            try
            {
                // Code to save booking to the database asynchronously
                // Example: using Entity Framework Core or another ORM
                // Example: dbContext.Bookings.Add(booking); await dbContext.SaveChangesAsync();
                return true; // Return true if the booking was successfully saved
            }
            catch (Exception ex)
            {
                // Handle database saving error
                return false;
            }
        }

        private async Task<bool> SendConfirmationEmailAsync(Booking booking)
        {
            try
            {
                // Code to send confirmation email to user
                var client = new SmtpClient(_smtpServer, _smtpPort)
                {
                    Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
                    EnableSsl = true
                };

                var message = new MailMessage(_smtpUsername, booking.Email)
                {
                    Subject = "Booking Confirmation",
                    Body = "Your booking has been confirmed. Thank you for choosing  LetsGetBraided."
                };

                await client.SendMailAsync(message);
                return true; // Return true if the email was successfully sent
            }
            catch (Exception ex)
            {
                // Handle email sending error
                return false;
            }
        }

        private async Task<bool> SendBookingReceiptEmailAsync(Booking booking)
        {
            try
            {
                // Code to send booking receipt email to your email
                var client = new SmtpClient(_smtpServer, _smtpPort)
                {
                    Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
                    EnableSsl = true
                };

                var message = new MailMessage(_smtpUsername, _confirmationEmail)
                {
                    Subject = "Booking Receipt",
                    Body = $"New booking received: {booking.Name} {booking.Surname}, {booking.Email}, {booking.Date}, {booking.Time}"
                };

                await client.SendMailAsync(message);
                return true; // Return true if the email was successfully sent
            }
            catch (Exception ex)
            {
                // Handle email sending error
                return false;
            }
        }

    }

}
