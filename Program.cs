using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;

namespace VaultDemo
{
    public class Program
    {
        const string KeyVaultSectionName = "KeyVault";
        
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((ctx, builder) => {
                    var x = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json");
                    var vaultSection = x.Build().GetSection(KeyVaultSectionName);
                    
                    if(vaultSection != null)
                    {
                        var keyVaultEndpoint = $"https://{vaultSection["Vault"]}.vault.azure.net/";
                        var tokenProvider = new AzureServiceTokenProvider();
                        var vaultClient = new KeyVaultClient(
                            new KeyVaultClient.AuthenticationCallback(tokenProvider.KeyVaultTokenCallback)
                        );

                        builder.AddAzureKeyVault(
                            keyVaultEndpoint,
                            vaultClient,
                            new DefaultKeyVaultSecretManager());
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
