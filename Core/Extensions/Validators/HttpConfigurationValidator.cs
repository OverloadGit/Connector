using Core.Data.Configuration;

namespace Core.Extensions.Validators
{
    public static class HttpConfigurationValidator
    {
        public static void HttpClientConfValidator(this HttpClientConfiguration httpClientConfiguration)
        {
            if (httpClientConfiguration == null)
                throw new ArgumentNullException("Configuration data are null");

            if (String.IsNullOrEmpty(httpClientConfiguration.Adres))
                throw new ArgumentNullException("Adres value is null, cant send request");

            if (String.IsNullOrEmpty(httpClientConfiguration.Password))
                throw new ArgumentNullException("Password value is null");

            if (String.IsNullOrEmpty(httpClientConfiguration.User))
                throw new ArgumentNullException("Password value is null");
        }
    }
}
