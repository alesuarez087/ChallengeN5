//using Azure.Core;
using ChallengeN5;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace APIRest;

public interface IPermissionPublisher
{
    public Task RequestPermission(Permission model);

    public Task ModifyPermission(Permission model);
    public Task GetPermissions();
}

public class PermissionPublisher : IPermissionPublisher
{
    public readonly IProducer<Null, string> _producer;

    public PermissionPublisher(IProducer<Null, string> producer)
    {
        this._producer = producer;
    }

    public async Task RequestPermission(Permission model)
    {
        await this._producer.ProduceAsync("request-permission", new Message<Null, string> { Value = JsonConvert.SerializeObject(model) });
    }
    public async Task ModifyPermission(Permission model)
    {
        await this._producer.ProduceAsync("modify-permission", new Message<Null, string> { Value = JsonConvert.SerializeObject(model) });
    }
    public async Task GetPermissions()
    {
        await this._producer.ProduceAsync("get-permission", new Message<Null, string> { });
    }
}