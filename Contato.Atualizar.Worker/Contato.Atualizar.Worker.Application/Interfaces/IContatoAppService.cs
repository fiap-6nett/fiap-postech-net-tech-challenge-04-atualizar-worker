using Contato.Atualizar.Worker.Application.Dtos;

namespace Contato.Atualizar.Worker.Application.Interfaces;

public interface IContatoAppService
{
     Task AtualizarContato(AtualizarContatoDto dto);
}