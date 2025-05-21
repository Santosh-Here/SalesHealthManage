namespace SalesHealth.Cores
{
    public static class SD
    {
        public static string SalesApiBase { get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
        public enum ContentType
        {
            Json,
            MultipartFormData
        }
    }
}
