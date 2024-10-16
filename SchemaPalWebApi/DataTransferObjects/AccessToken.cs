namespace SchemaPalWebApi.DataTransferObjects
{
    public class AccessToken
    {
        public string Token { get; set; }

        public DateTime ExpirationDateUtc { get; set; }
    }
}
