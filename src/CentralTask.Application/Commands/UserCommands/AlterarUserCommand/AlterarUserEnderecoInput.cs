namespace CentralTask.Application.Commands.UserCommands.AlterarUserCommand;

public class AlterarUserEnderecoInput
{
    public string Logradouro { get; set; }
    public string Numero { get; set; }
    public string Cep { get; set; }
    public string Bairro { get; set; }
    public string Complemento { get; set; }
    public Guid CidadeId { get; set; }
    public Guid EstadoId { get; set; }
}