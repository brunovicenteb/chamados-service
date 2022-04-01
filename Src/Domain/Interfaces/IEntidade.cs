namespace Chamados.Service.Domain.Interfaces
{
    public interface IEntidade<T> : IEquatable<T>
    {
        T Id { get; set; }
    }
}