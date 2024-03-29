﻿using AspNetReactSignalR.WebApi.Hubs.Interfaces;
using AspNetReactSignalR.WebApi.Models;
using Microsoft.AspNetCore.SignalR;

namespace AspNetReactSignalR.WebApi.Hubs;

public class ChatHub : Hub<IChat>
{
    public static Dictionary<string, (string, string)> lstUsuarios { get; set; } = new Dictionary<string, (string, string)>();

    public async Task EnviarMensaje(Mensaje mensaje)
    {
        if (!string.IsNullOrEmpty(mensaje.Contenido))
        {
            await Clients.Group(mensaje.Sala).RecibirMensaje(mensaje);
        }
        else if (!string.IsNullOrEmpty(mensaje.Usuario))
        {
            lstUsuarios.Add(Context.ConnectionId, (mensaje.Usuario, mensaje.Sala));

            await Groups.AddToGroupAsync(Context.ConnectionId, mensaje.Sala);

            await Clients.GroupExcept(mensaje.Sala, Context.ConnectionId).RecibirMensaje(new Mensaje
            {
                Usuario = mensaje.Usuario,
                Contenido = "Se ha conetdo!",
            });
        }
    }

    public async IAsyncEnumerable<int> Counter()
    {
        for (int i = 0; i < 1000000; i++)
        {
            yield return i;
            await Task.Delay(1000);
        }
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.Client(Context.ConnectionId).RecibirMensaje(new Mensaje
        {
            Usuario = "Host",
            Contenido = "Hola =D"
        });

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await Clients.GroupExcept(lstUsuarios[Context.ConnectionId].Item2, Context.ConnectionId)
        .RecibirMensaje(new Mensaje
        {
            Usuario = "Host",
            Contenido = $"{lstUsuarios[Context.ConnectionId].Item1} salio del _chat."
        });
        await base.OnDisconnectedAsync(exception);
    }
}
