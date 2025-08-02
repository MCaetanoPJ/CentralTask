using CentralTask.Domain.Enums;

namespace CentralTask.Domain.Entidades.Base;

public abstract class Entidade : IEntidade
{
    protected Entidade()
    {
        Id = Guid.NewGuid();
        DataCriacao = DateTime.Now;
        Status = Status.Ativo;
    }
    public Guid Id { get; set; }
    public DateTime DataCriacao { get; private set; }
    public DateTime? DataAlteracao { get; set; }
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
