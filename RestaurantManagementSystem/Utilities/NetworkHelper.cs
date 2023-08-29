using System.Net.Sockets;
using System.Net;

namespace RestaurantManagementSystem.Utilities {
    public static class NetworkHelper {
        public static string GetLocalIp() {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList) {
                if (ip.AddressFamily == AddressFamily.InterNetwork) {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }


    }
}
