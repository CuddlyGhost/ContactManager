using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace ContactManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {

        private readonly ContactsDbContext _context;

        public ContactController ( ContactsDbContext context )
        {
            _context = context;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Retrieves all contacts.",
            Description = "Returns all contacts, or 404 if none were found."
        )]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts ()
        {
            return await _context.Contacts.ToListAsync ();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Retrieves a contact by ID.",
            Description = "Returns contact with the provided ID, or 404 if none were found."
        )]
        public async Task<ActionResult<Contact>> GetContact ( long id )
        {
            var contact = await _context.Contacts.FindAsync ( id );

            if ( contact == null )
            {
                return NotFound ();
            }

            return contact;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a contact.",
            Description = "Returns the created contact with status 201."
        )]
        public async Task<ActionResult<Contact>> CreateContact ( Contact contact )
        {
            _context.Contacts.Add ( contact );
            await _context.SaveChangesAsync ();
            return CreatedAtAction ( nameof ( GetContact ), new { id = contact.id }, contact );
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Updates a contact by ID.",
            Description = "Returns the updated contact with status 200, or 404 if the desired ID was not found."
        )]
        public async Task<ActionResult<Contact>> UpdateContact ( long id, Contact updatedContact )
        {
            var currentContactResult = await GetContact ( id );
            if ( currentContactResult == null )
            {
                return NotFound ();
            }

            var currentContact = currentContactResult.Value;

            currentContact.firstName    = updatedContact.firstName;
            currentContact.lastName     = updatedContact.lastName;
            currentContact.email        = updatedContact.email;
            currentContact.phone        = updatedContact.phone;
            currentContact.comment      = updatedContact.comment;

            await _context.SaveChangesAsync ();

            return Ok ( currentContact );
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deletes a contact by ID.",
            Description = "Returns the a confirmation of the deleted contact with status 200, or status 404 if it was not found."
        )]
        public async Task<ActionResult> DeleteContact ( long id )
        {
            var contactResult = await GetContact ( id );
            if ( contactResult == null )
            {
                return NotFound ();
            }

            var contact = contactResult.Value;
            _context.Contacts.Remove ( contact );
            await _context.SaveChangesAsync ();

            return Ok ( contact );
        }
    }
}
