using ChallengeN5;
using ChallengeN5.Data;
using ChallengeN5.Data.Context;
using ChallengeN5.Domain;
using Confluent.Kafka;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

string bts = "localhost:9092";
var aor = AutoOffsetReset.Earliest;
CancellationTokenSource token = new();

IHost _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddScoped<IPermissionServices, PermissionServices>();
    services.AddScoped<IPermissionData, PermissionData>();
    services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer("Data Source=DESKTOP-3V2VL8N;Initial Catalog=ChallengeN5;Trusted_Connection=SSPI;MultipleActiveResultSets=true;Trust Server Certificate=true");//, o => o.MigrationsAssembly("Data"));
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });
}).Build();

var configGetPermissions = new ConsumerConfig
{
    GroupId = "permission-group",
    BootstrapServers = bts,
    AutoOffsetReset = aor,
    EnableAutoCommit = true
};

using var consumer = new ConsumerBuilder<Null, string>(configGetPermissions).Build();
consumer.Subscribe("get-permission");
consumer.Subscribe("modify-permission");
consumer.Subscribe("request-permission");

try
{
    var service = _host.Services.GetService<IPermissionServices>();
    while (true && service != null)
    {
        var response = consumer.Consume(token.Token);
        if (response.Message != null)
        {
            switch (response.TopicPartitionOffset.Topic)
            {
                case "request-permission":
                    var alt = JsonConvert.DeserializeObject<Permission>(response.Message.Value);
                    var insert = await service.RequestPermission(alt);
                    Console.WriteLine($"Id: {insert.Id}, Name: {insert.NameEmployee}, Suriname: {insert.SurinameEmployee}, Permission: {insert.PermissionType?.Description}.");
                    break;
                case "modify-permission":
                    var mod = JsonConvert.DeserializeObject<Permission>(response.Message.Value);
                    var update = await service.ModifyPermission(mod);
                    Console.WriteLine($"Id: {update.Id}, Name: {update.NameEmployee}, Suriname: {update.SurinameEmployee}, Permission: {update.PermissionType?.Description}.");
                    break;
                case "get-permission":
                    var list = await service.GetPermissions();
                    foreach (Permission permission in list)
                    {
                        Console.WriteLine($"Id: {permission.Id}, Name: {permission.NameEmployee}, Suriname: {permission.SurinameEmployee}, Permission: {permission.PermissionType?.Description}.");
                    }
                    break;
            }           
        }
    }
}
catch (Exception)
{
    throw;
}
