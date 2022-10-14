using AspNetReactSignalR.WebApi.Models;

namespace AspNetReactSignalR.WebApi.Hubs.Interfaces;

public interface IChat
{
    Task EnviarMensaje(Mensaje mensaje);
    Task RecibirMensaje(Mensaje mensaje);
    Task Counter();
}
