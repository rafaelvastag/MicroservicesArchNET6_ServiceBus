using Email.Repositories;
using Microsoft.EntityFrameworkCore;
using Email.Context;
using Email.Models;
using System.Threading.Tasks;
using Email.Models.Messages;
using System;

namespace Email.Repositories.Impl
{
    public class EmailRepository : IEmailRepository
    {

        private readonly DbContextOptions<ApplicationDbContext> _context;

        public EmailRepository(DbContextOptions<ApplicationDbContext> context)
        {
            _context = context;
        }

        public async Task SendAndLogEmail(UpdatePaymentResultMessage message)
        {
            EmailLog emailLog = new EmailLog()
            {
                Email = message.Email,
                EmailSent = DateTime.Now,
                Log = $"Order - {message.OrderId} has beed created successfully"
            };

            await using var _db = new ApplicationDbContext(_context);
            _db.EmailLogs.Add(emailLog);
            await _db.SaveChangesAsync();
        }
    }
}
