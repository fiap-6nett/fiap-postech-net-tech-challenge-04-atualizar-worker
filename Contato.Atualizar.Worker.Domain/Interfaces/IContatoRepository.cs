using Contato.Atualizar.Worker.Domain.Entities;

namespace Contato.Atualizar.Worker.Domain.Interfaces;

public interface IContatoRepository
{
    public ContatoEntity ObterPorID(Guid id);
    public void AtualizarContato(ContatoEntity contato);
    
}