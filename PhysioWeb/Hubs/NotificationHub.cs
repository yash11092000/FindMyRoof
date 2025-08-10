using Microsoft.AspNetCore.SignalR;

namespace PhysioWeb.Hubs
{
    public class NotificationHub : Hub
    {
        public NotificationHub()
        {
        }
        public async Task JoinGroup(string GroupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, GroupName);
        }
        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
