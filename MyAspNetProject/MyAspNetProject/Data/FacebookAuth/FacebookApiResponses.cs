using Newtonsoft.Json;

namespace MyAspNetProject.Models
{
    internal class FacebookUserData
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
    }
    internal class FacebookUserAccessTokenData
    {
        [JsonProperty("is_valid")]
        public bool IsValid { get; set; }
    }

    internal class FacebookUserAccessTokenValidation
    {
        public FacebookUserAccessTokenData Data { get; set; }
    }

    internal class FacebookAppAccessToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}