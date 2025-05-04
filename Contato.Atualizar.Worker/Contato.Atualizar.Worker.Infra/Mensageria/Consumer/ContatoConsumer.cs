using System.Text;
using Contato.Atualizar.Worker.Application.Dtos;
using Contato.Atualizar.Worker.Application.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Contato.Atualizar.Worker.Infra.Mensageria.Consumer;

public class ContatoConsumer : IContatoConsumer, IDisposable
{
    private readonly IModel _channel;
    private readonly IConnection _connection;
    private readonly IContatoAppService _appService;

    public ContatoConsumer(IContatoAppService appService, IConfiguration configuration, IConnection rabbitConnection)
    {
        _appService = appService;
        _connection = rabbitConnection;
        _channel = _connection.CreateModel();

        var queueName = configuration["RabbitMQ:QueueName"] ?? "atualizacao-contato";

        _channel.QueueDeclare(queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    public void StartConsuming(CancellationToken cancellationToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"Mensagem recebida: {message}");

            var dto = JsonConvert.DeserializeObject<AtualizarContatoDto>(message);

            _appService.AtualizarContato(dto);
        };

        
        _channel.BasicConsume(queue: "atualizacao-contato", autoAck: true, consumer: consumer);
    }

    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}