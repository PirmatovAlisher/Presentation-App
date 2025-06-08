using Microsoft.AspNetCore.SignalR;
using PresentationApp.Models;

namespace PresentationApp.Hubs
{
    public class PresentationHub : Hub
    {
        public async Task JoinPresentation(string presentationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, presentationId);
        }

        public async Task BroadcastDrawing(string presentationId, DrawingPath path)
        {
            await Clients.OthersInGroup(presentationId).SendAsync("ReceiveDrawing", path);
        }

        public async Task BroadcastSlideChange(string presentationId, string slideId)
        {
            await Clients.OthersInGroup(presentationId).SendAsync("ChangeSlide", slideId);
        }

        public async Task BroadcastUserRoleChange(string presentationId, string userName, bool isEditor)
        {
            await Clients.Group(presentationId).SendAsync("UserRoleChanged", userName, isEditor);
        }

        public async Task BroadcastClearCanvas(string presentationId)
        {
            await Clients.OthersInGroup(presentationId).SendAsync("ClearCanvas");
        }
    }
}