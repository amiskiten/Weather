namespace Weather.Static;
public static class HttpClientFactoryExtension
{
    private static SocketsHttpHandler AuthOptions() => new()
    {
        SslOptions = new System.Net.Security.SslClientAuthenticationOptions
        {
            EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 |
                              System.Security.Authentication.SslProtocols.Tls13
        },
        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
    };
    public static IHttpClientBuilder AutoSetting(this IHttpClientBuilder builder) =>
    builder
        .ConfigurePrimaryHttpMessageHandler(AuthOptions).ConfigureHttpClient(client =>
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
        })
        .AddTransientHttpErrorPolicy(policyBuilder =>
            policyBuilder.RetryAsync(4)).AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10)));
}
