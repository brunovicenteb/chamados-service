using Chamados.Service.Toolkit.Excecoes;
using Chamados.Service.Domain.Interfaces;
using Chamados.Service.Domain.Interfaces.Servicos;
using Chamados.Service.Domain.Interfaces.Repositorios;
using AutoMapper;

namespace Chamados.Service.Application;

public abstract class Servico<T, V, I> : IServico<T, V, I> where T : IEntidade<V>
{
    public Servico(IRepositorio<T, V> repositorio, string nomeDaEntidade)
    {
        _Repositorio = repositorio;
        _NomeDaEntidade = nomeDaEntidade;
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<I, T>();
        });
        _Mapper = new Mapper(configuration);
    }

    private readonly IRepositorio<T, V> _Repositorio;
    private readonly string _NomeDaEntidade;
    private readonly Mapper _Mapper;

    protected abstract bool EhIdentificadorVazio(V id);
    protected abstract void PreencherValoresPadrao(T entidade);

    public async Task<T> PegarPorIdAsync(V id)
    {
        if (EhIdentificadorVazio(id))
            throw new BadRequestException($"Não foi possível encontrar um {_NomeDaEntidade} sem um identificador.");
        IEntidade<V> entidade = await _Repositorio.PegarPorIdAsync(id);
        if (entidade == null)
            throw new NotFoundException($"Não foi possível encontrar o {_NomeDaEntidade} com o identificador informado.");
        return await _Repositorio.PegarPorIdAsync(id);
    }

    public async Task<IList<T>> PegarAsync(int? inicio, int? limite)
    {
        int i = inicio ?? 0;
        int l = Math.Min(50, limite ?? 10);
        return await _Repositorio.PegarAsync(i, l);
    }

    public async Task<long> PegarQuantidadeAsync()
    {
        return await _Repositorio.PegarQuantidadeAsync();
    }

    public async Task<T> AtualizarAsync(T entidade)
    {
        if (EhIdentificadorVazio(entidade.Id))
            throw new BadRequestException($"Não foi possível atualizar um {_NomeDaEntidade} sem um identificador.");
        IEntidade<V> e = await _Repositorio.PegarPorIdAsync(entidade.Id);
        if (e == null)
            throw new NotFoundException($"Não foi possível atualizar o {_NomeDaEntidade} com o identificador informado.");
        return await _Repositorio.AtualizarAsync(entidade);
    }

    public async Task<T> InserirAsync(I entidade)
    {
        T entidadeMapeada = _Mapper.Map<T>(entidade);
        PreencherValoresPadrao(entidadeMapeada);
        return await _Repositorio.InserirAsync(entidadeMapeada);
    }

    public async Task<bool> ExcluirAsync(V id)
    {
        if (EhIdentificadorVazio(id))
            throw new BadRequestException($"Não foi possível excluir um {_NomeDaEntidade} sem um identificador.");
        IEntidade<V> e = await _Repositorio.PegarPorIdAsync(id);
        if (e == null)
            throw new NotFoundException($"Não foi possível excluir o {_NomeDaEntidade} com o identificador informado.");
        return await _Repositorio.ExcluirAsync(id);
    }
}