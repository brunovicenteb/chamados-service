using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Chamados.Service.Domain.Interfaces.Repositorios;

namespace Chamados.Service.Tests.Mock;

public class ChamadosRepositorioMock : IChamadosRepositorio
{
    private readonly List<Domain.Entities.Chamados> _Chamados = new List<Domain.Entities.Chamados>();

    public ChamadosRepositorioMock(bool pLoadData)
    {
        if (!pLoadData)
            return;
        Assembly assembly = Assembly.GetExecutingAssembly();
        var resourcePath = string.Format("{0}.{1}", Regex.Replace(assembly.ManifestModule.Name, @"\.(exe|dll)$",
          string.Empty, RegexOptions.IgnoreCase), "Resources.ChamadosData.json");
        using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                _Chamados = JsonConvert.DeserializeObject<List<Domain.Entities.Chamados>>(json);
            }
        }
    }

    public async Task<Domain.Entities.Chamados> AtualizarAsync(Domain.Entities.Chamados chamado)
    {
        return await Task.Run(async () =>
        {
            await Console.Out.WriteAsync(string.Empty);
            Domain.Entities.Chamados c = _Chamados.FirstOrDefault(o => o.Id == chamado.Id);
            return c;
        });
    }

    public async Task<Domain.Entities.Chamados> InserirAsync(Domain.Entities.Chamados chamado)
    {
        return await Task.Run(async () =>
        {
            await Console.Out.WriteAsync(string.Empty);
            chamado.Id = Guid.NewGuid().ToString();
            _Chamados.Add(chamado);
            return chamado;
        });
    }

    public async Task<Domain.Entities.Chamados> PegarChamadoPorIdAsync(string id)
    {
        return await Task.Run(async () =>
        {
            await Console.Out.WriteAsync(string.Empty);
            return _Chamados.FirstOrDefault(o => o.Id == id);
        });
    }

    public async Task<IEnumerable<Domain.Entities.Chamados>> PegarChamadosAsync(int inicio, int limite)
    {
        return await Task.Run(async () =>
        {
            await Console.Out.WriteAsync(string.Empty);
            return _Chamados.Skip(inicio).Take(limite);
        });
    }

    public async Task<long> PegarQuantidadeAsync(bool retornarChamadosFechados)
    {
        return await Task.Run(async () =>
        {
            await Console.Out.WriteAsync(string.Empty);
            if (retornarChamadosFechados)
                return Convert.ToInt64(_Chamados.Count);
            return Convert.ToInt64(_Chamados.Count(o => o.Aberto));
        });
    }
}