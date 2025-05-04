namespace Contato.Atualizar.Worker.Infra.Mensageria.Consumer;

public interface IContatoConsumer
{
    void StartConsuming(CancellationToken cancellationToken);
}