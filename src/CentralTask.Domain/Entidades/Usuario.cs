using CentralTask.Domain.Entidades.Base;
using CentralTask.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace CentralTask.Domain.Entidades;

public class Usuario : IdentityUser<Guid>, IEntidade
{
    public Usuario(
        string nome,
        string sobrenome,
        string email,
        EnumNivel nivelAcesso)
    {
        ArgumentNullException.ThrowIfNull(nome);

        Id = Guid.NewGuid();
        Nome = nome;
        Sobrenome = sobrenome;
        Email = UserName = email.ToLower().Trim();
        NivelAcesso = nivelAcesso;
    }

    public Usuario()
    {
    }

    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Cpf { get; set; }
    public string NomeCompleto => $"{Nome} {Sobrenome}";
    public string Avatar { get; set; }
    public string AvatarParaEdicao { get; set; }
    public bool AvatarEditado { get; set; } = false;
    public string Telefone { get; set; }
    public string DDD { get; set; }
    public string TelefoneCompleto => $"{PrefixoPais} {DDD} {Telefone}";
    public string PrefixoPais { get; set; }
    public DateTime? DataNascimento { get; set; }
    public Status Status { get; set; }
    public EnumNivel NivelAcesso { get; set; }
    public string ReceberEmail { get; set; }
    public Guid? EnderecoId { get; set; }
    public string DeviceId { get; set; }

    public void Ativar()
    {
        Status = Status.Ativo;
    }

    public void Inativar()
    {
        Status = Status.Inativo;
    }
}
