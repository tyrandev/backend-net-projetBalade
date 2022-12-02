using System.Text.Json.Serialization;

namespace Domain
{
    public class User
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        //The [JsonIgnore] attribute prevents the password property from being serialized and returned in api responses.
        [JsonIgnore]
        public string Password { get; set; }

        public User()
        {
            
        }
        
        
    }
}