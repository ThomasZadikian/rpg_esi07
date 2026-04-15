using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RPG_ESI07.Infrastructure.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RPG_ESI07.Tests.Integration;

public class AuthRateLimitingTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AuthRateLimitingTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                // 1. SUPPRESSION RADICALE DE TOUT CE QUI TOUCHE À EF CORE
                // Supprime le contexte, les options et TOUT fournisseur déjà enregistré
                services.RemoveAll<AppDbContext>();
                services.RemoveAll<DbContextOptions<AppDbContext>>();
                services.RemoveAll<DbContextOptions>();

                // On supprime également les services internes qui causent le conflit de provider
                var efInternalServices = services
                    .Where(d => d.ServiceType.Namespace?.StartsWith("Microsoft.EntityFrameworkCore") == true)
                    .ToList();

                foreach (var service in efInternalServices)
                {
                    services.Remove(service);
                }

                // 2. RÉ-ENREGISTREMENT TOTALEMENT ISOLÉ
                // En utilisant un "InternalServiceProvider" dédié, on empêche le mélange
                // entre les services Npgsql et InMemory.
                var internalServiceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("RateLimitTestsDb")
                           .UseInternalServiceProvider(internalServiceProvider);
                });
            });
        }).CreateClient();
    }

    [Fact]
    public async Task Login_Policy_Triggers_After_Five_Requests()
    {
        var content = new StringContent("{\"username\":\"test\",\"password\":\"pass\"}", Encoding.UTF8, "application/json");

        for (int i = 0; i < 5; i++)
        {
            await _client.PostAsync("/api/auth/login", content);
        }

        var response = await _client.PostAsync("/api/auth/login", content);

        response.StatusCode.Should().Be((HttpStatusCode)429);
    }
}