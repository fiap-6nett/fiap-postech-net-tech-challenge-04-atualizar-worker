using Contato.Atualizar.Worker.Application.Dtos;
using Contato.Atualizar.Worker.Application.Interfaces;
using Contato.Atualizar.Worker.Domain.Entities;
using Contato.Atualizar.Worker.Domain.Interfaces;

namespace Contato.Atualizar.Worker.Application.Services;

public class ContatoAppService : IContatoAppService
{
    private readonly IContatoRepository _contatoRepository;
    
    public ContatoAppService(IContatoRepository contatoRepository)
    {
        _contatoRepository = contatoRepository;
    }
    
    public Task AtualizarContato(AtualizarContatoDto dto)
    {
       
        var contato = new ContatoEntity();
        
        contato.SetId(dto.Id);
        contato.SetNome(dto.Nome);
        contato.SetEmail(dto.Email);
        contato.SetTelefone(dto.Telefone);
        contato.SetDdd(dto.Ddd);
        
        _contatoRepository.AtualizarContato(contato);
        
        return Task.CompletedTask;

    }
}