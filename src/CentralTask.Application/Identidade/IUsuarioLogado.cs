using CentralTask.Domain.Entidades;
using CentralTask.Domain.Enums;

namespace CentralTask.Application.Identidade;

public interface IUsuarioLogado
{
    bool EstaLogado();
    Guid? ObterId();
    string ObterToken();
    EnumNivel? ObterNivel();
}
