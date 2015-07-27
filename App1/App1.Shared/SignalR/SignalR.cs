using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using App1.Handler;
using App1.Model.Logic;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Transports;
using System.Diagnostics;

namespace App1.SignalR
{
   public class SignalR
   {
       private HubConnection _connection;
       private IHubProxy _proxy;
       public SignalR(string url)
       {
           _connection = new HubConnection(url);
      
            // Add some tracing help
            _connection.TraceLevel = TraceLevels.All;
            _connection.Closed += () => Debug.WriteLine("[Closed]");
            _connection.ConnectionSlow += () => Debug.WriteLine("[ConnectionSlow]");
            _connection.Error += (error) => Debug.WriteLine("[Error] {0}", error.ToString());
            _connection.Received += (payload) => Debug.WriteLine("[Received] {0}", payload);
            _connection.Reconnected += () => Debug.WriteLine("[Reconnected]");
            _connection.Reconnecting += () => Debug.WriteLine("[Reconnecting]");
            _connection.StateChanged +=
                (change) => Debug.WriteLine("[StateChanged] {0} {1}", change.OldState, change.NewState);

            _proxy = _connection.CreateHubProxy("chatHub");

        }

        public async Task Start()
        {
            if (!(_connection.State == ConnectionState.Connected||_connection.State == ConnectionState.Connecting))
            {
                _proxy.Subscribe("updateConnectionString").Received += updateConnection;
                _proxy.Subscribe("updateChat").Received += updateChat;

               await _connection.Start(new LongPollingTransport());
            }
        }

       void updateConnection(IList<Newtonsoft.Json.Linq.JToken> obj)
       {
            int personId = DataManager.shared().myself.PersonId;
           _proxy.Invoke("updateConnection",personId);
       }
       void updateChat(IList<Newtonsoft.Json.Linq.JToken> obj)
       {
         TreeMessage message=obj[0].ToObject<TreeMessage>();
            int myId = DataManager.shared().myself.PersonId;
           if (!(message.SenderId==myId))
             {
               if(message.PreliminaryTutorId==DataManager.shared().myself.PersonId)
                   {
                    UserMessageHandler handler =DataManager.shared().tutorMessageHandler;
                    handler.setUpMessage(message);
                    }
               else
                    {
                    UserMessageHandler handler = DataManager.shared().studentMessageHandler;
                    handler.setUpMessage(message);
                    }
             }
       }

   }
 
}
