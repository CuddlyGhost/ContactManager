using Swashbuckle.AspNetCore.Annotations;

namespace ContactManager.Api
{
    public class Contact
    {
        [SwaggerSchema(ReadOnly = true)] // Hiding the ID-Field in the Swagger UI
        public long    id            { get; set; }
        public string? lastName      { get; set; }
        public string? firstName     { get; set; }
        public string? email         { get; set; }
        public string? phone         { get; set; }
        public string? comment       { get; set; }
    }
}
