using System;
using System.IO;
using AutoMapper;
using System.Linq;
using Newtonsoft.Json;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Chamados.Service.Domain.Interfaces.Repositorios;
using Chamados.Service.Domain.Entidades;

namespace Chamados.Service.Tests.Mock;

public class ChamadosRepositorioMock : IRepositorio<Chamado, string>
{
    static ChamadosRepositorioMock()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Chamado, Chamado>();
        });
        _Mapper = new Mapper(configuration);
    }

    private static readonly Mapper _Mapper;

    public ChamadosRepositorioMock(bool pLoadData)
    {
        if (!pLoadData)
            return;
        Assembly assembly = Assembly.GetExecutingAssembly();
        var resourcePath = string.Format("{0}.{1}", Regex.Replace(assembly.ManifestModule.Name, @"\.(exe|dll)$",
          string.Empty, RegexOptions.IgnoreCase), "Recursos.ChamadosData.json");
        using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                _Chamados = JsonConvert.DeserializeObject<List<Chamado>>(json);
            }
        }
    }

    private readonly List<Chamado> _Chamados = new List<Chamado>();

    public async Task<Chamado> AtualizarAsync(Chamado chamado)
    {
        return await Task.Run(async () =>
        {
            await Console.Out.WriteAsync(string.Empty);
            Chamado c = _Chamados.FirstOrDefault(o => o.Id == chamado.Id);
            _Mapper.Map(chamado, c);
            return _Chamados.FirstOrDefault(o => o.Id == chamado.Id);
        });
    }

    public async Task<Chamado> InserirAsync(Chamado chamado)
    {
        return await Task.Run(async () =>
        {
            await Console.Out.WriteAsync(string.Empty);
            chamado.Id = Guid.NewGuid().ToString();
            chamado = Clonar(chamado);
            _Chamados.Add(chamado);
            return chamado;
        });
    }

    public async Task<bool> ExcluirAsync(string id)
    {
        return await Task.Run(async () =>
        {
            await Console.Out.WriteAsync(string.Empty);
            Chamado c = _Chamados.FirstOrDefault(o => o.Id == id);
            if (c == null)
                return false;
            return _Chamados.Remove(c);
        });
    }

    public async Task<Chamado> PegarPorIdAsync(string id)
    {
        return await Task.Run(async () =>
        {
            await Console.Out.WriteAsync(string.Empty);
            return Clonar(_Chamados.FirstOrDefault(o => o.Id == id));
        });
    }

    public async Task<IList<Chamado>> PegarAsync(int inicio, int limite)
    {
        return await Task.Run(async () =>
        {
            await Console.Out.WriteAsync(string.Empty);
            return _Chamados
                .OrderBy(o => o.DataHoraCriacao)
                .Skip(inicio)
                .Take(limite)
                .Select(o => Clonar(o))
                .ToList();
        });
    }

    public async Task<long> PegarQuantidadeAsync()
    {
        return await Task.Run(async () =>
        {
            await Console.Out.WriteAsync(string.Empty);
            return Convert.ToInt64(_Chamados.Count);
        });
    }

    private Chamado Clonar(Chamado chamado)
    {
        if (chamado == null)
            return null;
        return _Mapper.Map<Chamado>(chamado);
    }
}