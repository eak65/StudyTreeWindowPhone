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

namespace App1.SignalR
{
   public class SignalR
   {
       private HubConnection _connection;
       private IHubProxy _proxy;
       public SignalR(string url)
       {
           _connection = new HubConnection(url);
           _proxy = _connection.CreateHubProxy("chatHub");
           Start();
       }

       public async Task Start()
       {
           _proxy.Subscribe("updateConnectionString").Received += updateConnection;
           _proxy.Subscribe("updateChat").Received += updateChat;

           await _connection.Start();    
       }

       void updateConnection(IList<Newtonsoft.Json.Linq.JToken> obj)
       {
           int personId = 2052;
           _proxy.Invoke("updateConnection",personId);
       }
       void updateChat(IList<Newtonsoft.Json.Linq.JToken> obj)
       {
         TreeMessage message=obj[0].ToObject<TreeMessage>();

           if(!(message.SenderId==DataManager.shared().myself.PersonId))
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
