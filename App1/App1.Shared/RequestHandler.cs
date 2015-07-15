using App1.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;
using System.Diagnostics;
using Windows.ApplicationModel.Activation;
using App1.Model.Logic;
using App1.Model.Transfer;
using RestSharp.Portable;
using RestSharp.Portable.Authenticators.OAuth2.Infrastructure;

namespace App1
{

    public class result
    {

    }
        public class RequestHandler
        {  
            private static string mainUrl ="http://studytree2.azurewebsites.net/api/";
            public event EventHandler DataReceivedHandler = null;
            RestClient client = new RestClient(Constants.url);
           static private RequestHandler _handler;

            static public RequestHandler Shared()
            {
                _handler = new RequestHandler();
                return _handler;
            }
            private RestRequest getRequest(string url, HttpMethod method)
            {
                var request = new RestRequest(url, method);
                string token = DataManager.shared().token;
                request.AddHeader("Authorization", "Bearer " + token);
              
                return request;
            }
            public async Task<StResponse> updateInformation()
            {
                RestRequest request = getRequest("/api/Profile/getupdates", HttpMethod.Get);
                IRestResponse<UpdateModel> response = await client.Execute<UpdateModel>(request);
                if (response.IsSuccess)
                {
                    UpdateModel updateModel = response.Data;
                    DataManager.shared().update(updateModel);   
                    return ResponseFactory.createSuccessResponse();
                    
                }
                return ResponseFactory.createErrorResponse();
            }

            public async Task<StResponse>  postFee(int fee)
            {
                RestRequest request = getRequest("/api/Profile/PostFee", HttpMethod.Post);
                request.AddJsonBody(new FeeModel(fee));
                IRestResponse response = await client.Execute(request);
                if (response.IsSuccess)
                {
                    return ResponseFactory.createSuccessResponse();
                }
                else
                {
                    return ResponseFactory.createErrorResponse();
                }
            }



            public async Task<StResponse> putProfileSettings(ProfileSettingModel model)
         {
             var request=getRequest("/api/Profile/PutProfileSettings", HttpMethod.Put);
             request.AddJsonBody(model);
             IRestResponse response = await  client.Execute(request);
             if (response.IsSuccess)
             {
                 return ResponseFactory.createSuccessResponse();
             }
             else
             {
                 return ResponseFactory.createErrorResponse();
             }

         }

         public async Task<StResponse> updateLocation(LocationModel model)
            {
                    var request=getRequest("/api/Profile/PutUpdateLocation", HttpMethod.Put);
             request.AddJsonBody(model);
             IRestResponse response = await  client.Execute(request);
             if (response.IsSuccess)
             {
                 return ResponseFactory.createSuccessResponse();
             }
             else
             {
                 return ResponseFactory.createErrorResponse();
             }
            }
    /*
        Tutor calls
     */

        public async Task<StResponse> getStudySessionsAroundMe(String location, String distance)
            {
                var request = getRequest("/api/TutorSession/GetSessionAroundMe", HttpMethod.Get);
              request.AddQueryParameter("distance", distance);
              request.AddQueryParameter("location", location);
              IRestResponse<IList<StudySession>> response = await client.Execute<IList<StudySession>>(request);
              if (response.IsSuccess)
              {
                  return ResponseFactory.createSuccessResponse(response.Data);
              }
              else
              {
                  return ResponseFactory.createErrorResponse();
              }
            }


    /*      public async Task<StResponse> getTutorReviews(int tutorId)
            {
                var request = getRequest("/api/TutorSession/GetReviews",HttpMethod.Get);
               request.AddQueryParameter("tutorId", tutorId);
               IRestResponse response = await  client.Execute(request);
            }
*/
           public async Task<StResponse> getTutorMessages(int sessionId)
            {
                      var request = getRequest("/api/TutorSession/GetMessages",HttpMethod.Get);
               request.AddQueryParameter("sessionId", sessionId);
               IRestResponse<IList<TreeMessage>> response = await client.Execute<IList<TreeMessage>>(request);
               if (response.IsSuccess)
               {
                   return ResponseFactory.createSuccessResponse(response.Data);
               }
               else
               {
                   return ResponseFactory.createErrorResponse();
               }
           }


        public async Task<StResponse> deleteTutorSession(int sessionId)
            {
                 var request = getRequest("/api/TutorSession/NotInterested",HttpMethod.Delete);
               request.AddQueryParameter("sessionId", sessionId);
               IRestResponse response = await client.Execute(request);
               if (response.IsSuccess)
               {
                   return ResponseFactory.createSuccessResponse();
               }
               else
               {
                   return ResponseFactory.createErrorResponse();
               }
            }

        public async Task<StResponse> postTutorToSession(TutorSessionModel tutorSessionModel)
            {
                   var request = getRequest("/api/TutorSession/PostTutorToSession",HttpMethod.Post);
               request.AddJsonBody(tutorSessionModel);
               IRestResponse response = await client.Execute(request);
               if (response.IsSuccess)
               {
                   return ResponseFactory.createSuccessResponse();
               }
               else
               {
                   return ResponseFactory.createErrorResponse();
               }
 
            }

            public async Task<StResponse> postTutorMessage(TreeMessage messageModel)
            {
                  var request = getRequest("/api/TutorSession/PostMessage",HttpMethod.Post);
               request.AddJsonBody(messageModel);
               IRestResponse response = await client.Execute(request);
               if (response.IsSuccess)
               {
                   return ResponseFactory.createSuccessResponse();
               }
               else
               {
                   return ResponseFactory.createErrorResponse();
               }
            }

        public async Task<StResponse> putTutorReview(ReviewModel model)
            {
                   var request = getRequest("/api/TutorSession/PutReview",HttpMethod.Put);
               request.AddJsonBody(model);
               IRestResponse response = await client.Execute(request);
               if (response.IsSuccess)
               {
                   return ResponseFactory.createSuccessResponse();
               }
               else
               {
                   return ResponseFactory.createErrorResponse();
               }
            }

       
            public async Task<StResponse> deleteTutorSubject( String courseName)
            {
                
                   var request = getRequest("/api/Subject/DeleteTutorCourse",HttpMethod.Delete);
                   request.AddQueryParameter("CourseName", courseName);
                   IRestResponse response = await client.Execute(request);
                   if (response.IsSuccess)
                   {
                       return ResponseFactory.createSuccessResponse();
                   }
                   else
                   {
                       return ResponseFactory.createErrorResponse();
                   }

            }

            public async Task<StResponse> postTutorSubject(PostSubjectModel model)
            {
                      
                   var request = getRequest("/api/Subject/PostTutorSubject",HttpMethod.Post);
                     request.AddJsonBody(model);
                     IRestResponse response = await client.Execute(request);
                if (response.IsSuccess)
                {
                    return ResponseFactory.createSuccessResponse();
                }
                else
                {
                    return ResponseFactory.createErrorResponse();
                }


            }


            public async Task<StResponse> getTutorSubject()
            {
              var request = getRequest("/api/Subject/GetTutorSubjects",HttpMethod.Get);
              IRestResponse<IList<SubjectModel>> response =await client.Execute<IList<SubjectModel>>(request);
              if (response.IsSuccess)
              {
                  DataManager.shared().myself.TutorSubjects.Clear();

                  foreach (SubjectModel subject in response.Data)
                  {
                      DataManager.shared().myself.TutorSubjects.Add(subject.getSubject());
                  }
                  return ResponseFactory.createSuccessResponse();
              }
              else
              {
                  return ResponseFactory.createErrorResponse();
              }
            }
    /*
        Student calls
     */
        
            public async Task<StResponse> getStudentSessions()
            {
                 var request = getRequest("/api/StudentSession/GetStudySessions",HttpMethod.Get);
                 IRestResponse<IList<StudySession>> response = await client.Execute<IList<StudySession>>(request);
                if (response.IsSuccess)
                {
                    DataManager.shared().myself.StudentStudySessions.Clear();

                    foreach (StudySession studySession in response.Data)
                    {
                        DataManager.shared().myself.StudentStudySessions.Add(studySession);
                    }
                    return ResponseFactory.createSuccessResponse();
                }
                else
                {
                    return ResponseFactory.createErrorResponse();
                }

            }

            public async Task<StResponse> getStudentSubject()
            {
                var request = getRequest("/api/Subject/GetStudentSubjects", HttpMethod.Get);
                IRestResponse<IList<SubjectModel>> response = await client.Execute<IList<SubjectModel>>(request);
                if (response.IsSuccess)
                {
                    DataManager.shared().myself.StudentSubjects.Clear();
                    foreach (SubjectModel model in response.Data)
                    {
                        DataManager.shared().myself.StudentSubjects.Add(model.getSubject());
                    }
                    return ResponseFactory.createSuccessResponse();
                }
                else
                {
                    return ResponseFactory.createErrorResponse();
                }
            }

            public async Task<StResponse> putLoginInformation(LoginModel model)
            {
            
                var request = getRequest("/api/Profile/Login", HttpMethod.Put);
                IRestResponse<LoginResponse> response = await client.Execute<LoginResponse>(request);
                if (response.IsSuccess)
                {
                    LoginResponse updateModel = response.Data;
                    DataManager.shared().myself.updateAccountInformation(updateModel);
                    return ResponseFactory.createSuccessResponse();
                }
                    return ResponseFactory.createErrorResponse();

           

            }

         
             public async Task<StResponse>  deleteStudentSubject(String courseName)
            {
                   var request = getRequest("/api/Subject/DeleteStudentCourse", HttpMethod.Delete);
                  request.AddQueryParameter("courseName", courseName);
                 IRestResponse response = await client.Execute(request);
                 if (response.IsSuccess)
                 {
             
                     return ResponseFactory.createSuccessResponse();
                 }
                
                 return ResponseFactory.createErrorResponse();

           

            }
            
           public async Task<StResponse> getStudentSession(int sessionId)
            {
              var request = getRequest("/api/StudentSession/GetStudySession", HttpMethod.Get);
               request.AddQueryParameter("sessionId", sessionId);
               IRestResponse<StudySession> response = await client.Execute<StudySession>(request);
               if (response.IsSuccess)
               {
                   return ResponseFactory.createSuccessResponse(response.Data);
               }
               return ResponseFactory.createErrorResponse();
            }

     
        public async Task<StResponse>  getStudentMessages(int sessionId, int tutorId)
            {
                var request = getRequest("/api/StudentSession/GetMessages", HttpMethod.Get);
                request.AddQueryParameter("sessionId", sessionId);
                request.AddQueryParameter("tutorId", tutorId);
                IRestResponse<IList<TreeMessage>> response = await client.Execute<IList<TreeMessage>>(request);
            if (response.IsSuccess)
            {
                return ResponseFactory.createSuccessResponse(response.Data);
            }
            else
            {
                return ResponseFactory.createErrorResponse();
            }
            }

        /*
        public async Task<StResponse>getStudentReviews(int studentId)
            {
                var request = getRequest("/api/StudentSession/GetReviews", HttpMethod.Get);
                request.AddQueryParameter("studentId", studentId);
            IRestResponse response = await client.Execute(request);
            if (response.IsSuccess)
            {
                return ResponseFactory.createSuccessResponse(response.Data);
            }
            else
            {
                return ResponseFactory.createErrorResponse();
            }
            }*/
       
        public async Task<StResponse> getSessionTime( int sessionId)
            {
                var request = getRequest("/api/StudentSession/GetSessionTime", HttpMethod.Get);
                request.AddQueryParameter("sessionId", sessionId);
                IRestResponse<StudySession> response = await client.Execute<StudySession>(request);
                if (response.IsSuccess)
                {
                    return ResponseFactory.createSuccessResponse(response.Data);
                }
                else
                {
                    return ResponseFactory.createErrorResponse();
                }
            }


            public async Task<StResponse> getUniversity()
            {
               var request = getRequest("/api/University/GetUniversity", HttpMethod.Get);
                IRestResponse<University> response = await client.Execute<University>(request);
                if (response.IsSuccess)
                {
                    return ResponseFactory.createSuccessResponse(response.Data);
                }
                else
                {
                    return ResponseFactory.createErrorResponse();
                }
            }

    
            public async Task<StResponse> postStudySession(StudySessionModel sessionModel)
            {
               var request = getRequest("/api/StudentSession/PostCreateStudySession", HttpMethod.Post);
                request.AddJsonBody(sessionModel);
                IRestResponse response = await client.Execute(request);
                if (response.IsSuccess)
                {
                    return ResponseFactory.createSuccessResponse();
                }
                else
                {
                    return ResponseFactory.createErrorResponse();
                }

            }
           public async Task<StResponse>   postStudentMessage(TreeMessage messageModel)
            {
                 var request = getRequest("/api/StudentSession/PostMessage", HttpMethod.Post);
                 request.AddJsonBody(messageModel);
                 IRestResponse response = await client.Execute(request);
                 if (response.IsSuccess)
                 {
                     return ResponseFactory.createSuccessResponse();
                 }
                 else
                 {
                     return ResponseFactory.createErrorResponse();
                 }
            }

   
        public async Task<StResponse>   postStudentSubject( PostSubjectModel model )
            {
                var request = getRequest("/api/Subject/PostStudentSubject", HttpMethod.Post);
                request.AddJsonBody(model);
                IRestResponse response = await client.Execute(request);
                if (response.IsSuccess)
                {
                    return ResponseFactory.createSuccessResponse();
                }
                else
                {
                    return ResponseFactory.createErrorResponse();
                }
            }


            public async Task<StResponse> putStudentAccept(AcceptModel acceptModel)
            {
                var request = getRequest("/api/StudentSession/PutAccept", HttpMethod.Put);
                request.AddJsonBody(acceptModel);
                IRestResponse response = await client.Execute(request);
                if (response.IsSuccess)
                {
                    return ResponseFactory.createSuccessResponse();
                }
                else
                {
                    return ResponseFactory.createErrorResponse();
                }

            }
 
            public async Task<StResponse> putStudentStart(StartModel startModel)
            {
                 var request = getRequest("/api/StudentSession/PutStart", HttpMethod.Put);
                 request.AddJsonBody(startModel);
                 IRestResponse response = await client.Execute(request);
                 if (response.IsSuccess)
                 {
                     return ResponseFactory.createSuccessResponse();
                 }
                 else
                 {
                     return ResponseFactory.createErrorResponse();
                 }
            }
   
            public async Task<StResponse> putStudentEndSession(EndSessionModel endSessionModel)
            {
                              var request = getRequest("/api/StudentSession/PutEndSession", HttpMethod.Put);
                              request.AddJsonBody(endSessionModel);
                              IRestResponse response = await client.Execute(request);
                              if (response.IsSuccess)
                              {
                                  return ResponseFactory.createSuccessResponse();
                              }
                              else
                              {
                                  return ResponseFactory.createErrorResponse();
                              }
   
            }
        public async Task<StResponse>   putStudentUpdateTimer(TimerModel timerModel)
            {
                  var request = getRequest("/api/StudentSession/PutUpdateTimer", HttpMethod.Put);
                  request.AddJsonBody(timerModel);
                  IRestResponse response = await client.Execute(request);
                  if (response.IsSuccess)
                  {
                      return ResponseFactory.createSuccessResponse();
                  }
                  else
                  {
                      return ResponseFactory.createErrorResponse();
                  }
            }

        public async Task<StResponse> putStudentReview(ReviewModel reviewModel)
            {
               var request = getRequest("/api/StudentSession/PutReview", HttpMethod.Put);
               request.AddJsonBody(reviewModel);
               IRestResponse response = await client.Execute(request);
               if (response.IsSuccess)
               {
                   return ResponseFactory.createSuccessResponse();
               }
               else
               {
                   return ResponseFactory.createErrorResponse();
               }
            }

            public async Task<StResponse> putStartTime(StartModel model)
            {
                var request = getRequest("/api/StudentSession/PutStart", HttpMethod.Put);
                request.AddJsonBody(model);
                IRestResponse response = await client.Execute(request);
                if (response.IsSuccess)
                {
                    return ResponseFactory.createSuccessResponse();
                }
                else
                {
                    return ResponseFactory.createErrorResponse();
                }

            }

            public async Task<StResponse> deleteStudentStudySession(int sessionId)
            {
                     var request = getRequest("/api/StudentSession/DeleteStudySession", HttpMethod.Delete);
                request.AddQueryParameter("sessionId", sessionId);
                IRestResponse response = await client.Execute(request);
                if (response.IsSuccess)
                {
                    return ResponseFactory.createSuccessResponse();
                }
                else
                {
                    return ResponseFactory.createErrorResponse();
                }

            }
        public async Task<StResponse>  deleteTutorFromStudySession(int sessionId,int tutorId)
            {
               var request = getRequest("/api/StudentSession/DeleteTutor", HttpMethod.Delete);
            request.AddQueryParameter("sessionId", sessionId);
            request.AddQueryParameter("tutorId", tutorId);
            IRestResponse response = await client.Execute(request);

            if (response.IsSuccess)
            {
                return ResponseFactory.createSuccessResponse();
            }
            else
            {
                return ResponseFactory.createErrorResponse();
            }
            }

        /*
        public async Task<StResponse>   getSearchForTutors(int sessionId)
            {
              var request = getRequest("/api/Search/GetNotifiableTutors", HttpMethod.Get);
            request.AddQueryParameter("studySessionId", sessionId);

            }
            */
            public async Task<StResponse> postSuggestTutor(SuggestModel model)
            {
              var request = getRequest("/api/EventNotification/PostSuggestionTutor", HttpMethod.Post);
                request.AddJsonBody(model);
                IRestResponse response =await client.Execute(request);
                if (response.IsSuccess)
                {
                    return ResponseFactory.createSuccessResponse();
                }
                else
                {
                    return ResponseFactory.createErrorResponse();
                }
            }

    /*
    Subject Calls
     */




       //  @PUT("/api/BrainTree/UpdateMerchant")
    // Merchant Calls
/*        @PUT("/api/BrainTree/UpdateMerchant")
        public async void  updateMerchant(@Body Merchant merchant,Callback<Response>cb);

        @GET("/api/BrainTree/GetMerchant")
        public async void  getMerchant(Callback<MerchantAccount>merchantCallback);
        @POST("/api/BrainTree/CreateMerchant")
        public async void  createMerchant(@Body MerchantCreateModel model, Callback<Response>cb);

    // CUSTOMER BRAINTREE CALLS
        @PUT("/api/BrainTree/PutCardDefault")
        public async void  putCardDefault(@Query("cardToken") String cardToken,Callback<Response>cb);
        @DELETE("/api/BrainTree/DeleteCreditCard")
        public async void  deleteCreditCard(@Query("Token") String token,Callback<Response>cb);
        @POST("/api/BrainTree/StorePaymentMethod")
        public async void  postNonceToCardVault(@Body PaymentModel nonce,Callback<Response>cb);
        @GET("/api/BrainTree/GetCustomer")
        public async void  getCustomer(Callback<Map>cardModel);
        @GET("/api/BrainTree/GetClientToken")
        public async void  getClientToken(Callback<ClientTokenModel>cb);
 * */






            public async Task CreatePerson(App1.Model.Logic.CreatePerson person)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(mainUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                   
                
                    HttpResponseMessage response;
                    // HTTP POST

                    response = await client.PostAsJsonAsync("Profile/CreateProfile", person);
                    if (response.IsSuccessStatusCode && DataReceivedHandler != null)
                    {
                        var responseBodyAsText = response.Content.ReadAsStringAsync().Result;

                        DataReceivedHandler(response, null);
                    }
                }
            }

            public async Task Login(string usernameOrEmail, string password)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                    HttpResponseMessage response;
                    // HTTP POST
                    StringContent theContent = new StringContent("userName=" + usernameOrEmail + "&password=" + password, Encoding.UTF8, "application/x-www-form-urlencoded");
                    response = await client.PostAsync(new Uri("http://studytreebroker2.azurewebsites.net/hander/jwt"), theContent);
                    if (response.IsSuccessStatusCode && DataReceivedHandler != null)
                    {
                        var responseBodyAsText = response.Content.ReadAsStringAsync().Result;


                        var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                        if (localSettings.Values.ContainsKey("token") ==false)
                        {
                            localSettings.Values.Add("token", responseBodyAsText);
                        }
                        else
                        {
                            localSettings.Values["token"] = responseBodyAsText;
                        }
                            DataManager.shared().token = responseBodyAsText;
                        DataReceivedHandler(response, null);
                    }
                }
            }

        }




}
