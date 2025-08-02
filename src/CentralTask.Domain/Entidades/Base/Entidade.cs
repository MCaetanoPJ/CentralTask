using CentralTask.Domain.Enums;

namespace CentralTask.Domain.Entidades.Base;

public abstract class Entidade : IEntidade
{
    protected Entidade()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        Status = Status.Ativo;
    }
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; set; }
    public Status Status { get; set; }

    public void AtivarInativar()
    {
        if (Status == Status.Ativo)
        {
            Status = Status.Inativo;
        }
        else if (Status == Status.Inativo)
        {
            Status = Status.Ativo;
        }
    }
}