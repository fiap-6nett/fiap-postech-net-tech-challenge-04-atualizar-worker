namespace Contato.Atualizar.Worker.Infra.Mensageria;

public class RabbitMqSettings
{
    public string HostName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }  
    public string VirtualHost { get; set; }
}