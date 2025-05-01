using Swashbuckle.AspNetCore.Annotations;

namespace ContactManager.Api
{
    public class Contact
    {
        [SwaggerSchema(ReadOnly = true)] // Hiding the ID-Field in the Swagger UI
        public long ID { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Comment { get; set; }
    }
}
