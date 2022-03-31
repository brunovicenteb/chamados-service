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

    //public XArticle Add(XArticle pValue)
    //{
    //    _Articles.Add(pValue);
    //    return pValue;
    //}

    //public long Count()
    //{
    //    return _Articles.Count;
    //}

    //public bool Delete(int pObjectID)
    //{
    //    XArticle a = _Articles.FirstOrDefault(o => o.ID == pObjectID);
    //    if (a == null)
    //        return false;
    //    _Articles.Remove(a);
    //    return true;
    //}

    //public IEnumerable<XArticle> Get(int pLimit, int pStart)
    //{
    //    return _Articles.OrderBy(o => o.Title).Skip(pStart).Take(pLimit);
    //}

    //public XArticle GetObjectByID(int pObjectID)
    //{
    //    return _Articles.FirstOrDefault(o => o.ID == pObjectID);
    //}

    //public XArticle Update(XArticle pValue)
    //{
    //    if (!Delete(pValue.ID))
    //        return null;
    //    _Articles.Add(pValue);
    //    return pValue;
    //}

    public async Task<Domain.Entities.Chamados> AtualizarAsync(Domain.Entities.Chamados chamado)
    {
        return await Task.Run(async () =>
        {
            await Console.Out.WriteAsync(string.Empty);
            Domain.Entities.Chamados c = _Chamados.FirstOrDefault(o => o.ObjectID == chamado.ObjectID);
            return c;
        });
    }

    public async Task<Domain.Entities.Chamados> InserirAsync(Domain.Entities.Chamados chamado)
    {
        return await Task.Run(async () =>
        {
            await Console.Out.WriteAsync(string.Empty);
            _Chamados.Add(chamado);
            return chamado;
        });
    }

    public async Task<Domain.Entities.Chamados> PegarChamadoPorIdAsync(string id)
    {
        return await Task.Run(async () =>
        {
            await Console.Out.WriteAsync(string.Empty);
            return _Chamados.FirstOrDefault(o => o.ObjectID == id);
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

    public async Task<long> PegarQuantidadeAsync(bool apenasAbertos)
    {
        return await Task.Run(async () =>
        {
            await Console.Out.WriteAsync(string.Empty);
            if (apenasAbertos)
                return Convert.ToInt64(_Chamados.Count(o => o.Aberto));
            return Convert.ToInt64(_Chamados.Count);
        });
    }
}