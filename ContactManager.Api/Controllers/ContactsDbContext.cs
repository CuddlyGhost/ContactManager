using Microsoft.EntityFrameworkCore;

namespace ContactManager.Api.Controllers
{
    public class ContactsDbContext : DbContext
    {
        public ContactsDbContext ( DbContextOptions<ContactsDbContext> options ) : base ( options ) { }
        public DbSet<Contact> Contacts { get; set; }
    }
}