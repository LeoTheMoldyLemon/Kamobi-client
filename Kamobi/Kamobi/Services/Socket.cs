using SocketIOClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Kamobi.Services
{
    
    public class Socket
    {
        public SocketIO socket;
        private string host;
        public Socket(String host) {
            socket = new SocketIO(host);
            this.host = host;
        }
        /// <summary>
        /// Requests and waits for the server to send a response, or until the request times out.
        /// </summary>
        /// <param name="requestName">Name of the request that is being sent to the server.</param>
        /// <param name="data">Data sent to the server.</param>
        /// <param name="timeout">Time in miliseconds after which the request should time out.</param>
        /// <returns>A task that results in a JSON string, which is the response from the server, or null if it timed out.</returns>
        public async Task<JsonNode> sendRequest(string requestName, string data="", int timeout = 5000) {
            var promise = new TaskCompletionSource<string>();
            Console.WriteLine("Sending request "+ requestName);
            try
            {
                await socket.EmitAsync(requestName, response =>
                {
                    Console.WriteLine("Received response for " + requestName);
                    Console.WriteLine(promise.TrySetResult((JsonNode.Parse(response.ToString())[0]).ToJsonString()));
                }, data);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                promise.TrySetResult(null);
            }
            var result = await Task.WhenAny(promise.Task, Task.Delay(timeout));
            if (result == promise.Task && promise.Task.Result!=null) {
                return JsonNode.Parse(promise.Task.Result);
            }
            return null;
        }
        public async Task<JsonNode> sendRequest(string requestName, JsonNode data, int timeout = 5000)
        {
            string stringData = data.ToJsonString();
            return await sendRequest(requestName, stringData, timeout);

        }
        /// <summary>
        /// Sets up a listener for server messages.
        /// </summary>
        /// <param name="name">Name of the message it should listen for.</param>
        /// <param name="handler">Function to run with the recieved info.</param>
        /// <returns>True if it managed to connect, false if it didn't.</returns>
        public void setupListener(string name, Action<SocketIOResponse> handler) {
            socket.On(name, handler);
        }
        /// <summary>
        /// Attempts to connect to the server.
        /// </summary>
        /// <param name="timeout">Time in miliseconds after which the request should time out.</param>
        /// <returns>True if it managed to connect, false if it didn't.</returns>
        public async Task<Boolean> Connect(int timeout=5000)
        {
            var connection = socket.ConnectAsync();
            var result = await Task.WhenAny(connection, Task.Delay(timeout));
            if (result == connection){
                return true;
            }
            await socket.DisconnectAsync();
            return false;
        }
    }
}
