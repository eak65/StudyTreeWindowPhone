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

            private RestRequest getRequest(string url, HttpMethod method)
            {
                var request = new RestRequest(url, method);
                string token = DataManager.shared().token;
                request.AddHeader("Authorization", "Bearer " + token);
              
                return request;
            }
            public async void updateInformation()
            {
                RestRequest request = getRequest("/api/Profile/getupdates", HttpMethod.Get);
                IRestResponse<UpdateModel> response = await client.Execute<UpdateModel>(request);
                if (response.IsSuccess)
                {
                    UpdateModel updateModel = response.Data;
                    DataManager.shared().update(updateModel);
                }
             //   DataReceivedHandler(response, null);
            }

            public async void postFee(int fee)
            {
                RestRequest request = getRequest("/api/Profile/PostFee", HttpMethod.Post);
                request.AddJsonBody(new FeeModel(fee));
                IRestResponse response = await client.Execute(request);
                if (response.IsSuccess)
                {
                    
                }
                DataReceivedHandler(response, null);
            }


           
         public async void  putProfileSettings(ProfileSettingModel model )
         {
             var request=getRequest("/api/Profile/PutProfileSettings", HttpMethod.Put);
             request.AddJsonBody(model);
             IRestResponse response = await  client.Execute(request);

         }

        public async void updateLocation(LocationModel model)
            {
                    var request=getRequest("/api/Profile/PutUpdateLocation", HttpMethod.Put);
             request.AddJsonBody(model);
             IRestResponse response = await  client.Execute(request);
            }
    /*
        Tutor calls
     */
       
          public async void  getStudySessionsAroundMe(String location, String distance)
            {
                var request = getRequest("/api/TutorSession/GetSessionAroundMe", HttpMethod.Get);
              request.AddQueryParameter("distance", distance);
              request.AddQueryParameter("location", location);
              IRestResponse response = await  client.Execute(request);
            }

      
           public async void   getTutorReviews(int tutorId)
            {
                var request = getRequest("/api/TutorSession/GetReviews",HttpMethod.Get);
               request.AddQueryParameter("tutorId", tutorId);
               IRestResponse response = await  client.Execute(request);
            }
         
        public async void  getTutorMessages(int sessionId)
            {
                      var request = getRequest("/api/TutorSession/GetMessages",HttpMethod.Get);
               request.AddQueryParameter("sessionId", sessionId); 
            }

      
        public async void  deleteTutorSession(int sessionId)
            {
                 var request = getRequest("/api/TutorSession/NotInterested",HttpMethod.Delete);
               request.AddQueryParameter("sessionId", sessionId); 
            }

            public async void postTutorToSession(TutorSessionModel tutorSessionModel)
            {
                   var request = getRequest("/api/TutorSession/PostTutorToSession",HttpMethod.Post);
               request.AddJsonBody(tutorSessionModel);
 
            }
   
        public async void  postTutorMessage( TreeMessage messageModel)
            {
                  var request = getRequest("/api/TutorSession/PostMessage",HttpMethod.Post);
               request.AddJsonBody(messageModel);
            }

             public async void  putTutorReview(ReviewModel model)
            {
                   var request = getRequest("/api/TutorSession/PutReview",HttpMethod.Put);
               request.AddJsonBody(model);
            }

       
            public async void  deleteTutorSubject( String courseName)
            {
                
                   var request = getRequest("/api/Subject/DeleteTutorCourse",HttpMethod.Delete);
                request.AddQueryParameter("CourseName", courseName);

            }

            public async void postTutorSubject(PostSubjectModel model)
            {
                      
                   var request = getRequest("/api/Subject/PostTutorSubject",HttpMethod.Post);
                request.AddJsonBody(model);
            }

        
            public async void getTutorSubject()
            {
                       var request = getRequest("/api/Subject/GetTutorSubjects",HttpMethod.Get);
              var response=  client.Execute<IList<SubjectModel>>(request);
            }
    /*
        Student calls
     */
        
            public async void getStudentSessions()
            {
                 var request = getRequest("/api/StudentSession/GetStudySessions",HttpMethod.Get);
              var response=  client.Execute<IList<StudySession>>(request);
            }

            public async void getStudentSubject()
            {
                var request = getRequest("/api/Subject/GetStudentSubjects", HttpMethod.Get);
                       var response=  client.Execute<IList<SubjectModel>>(request);
            }

            public async void putLoginInformation( LoginModel model)
            {
            
                var request = getRequest("/api/Profile/Login", HttpMethod.Put);
                IRestResponse<LoginResponse> response = await client.Execute<LoginResponse>(request);
                LoginResponse updateModel = response.Data;
                DataManager.shared().myself.updateAccountInformation(updateModel);

            }
/*
 *              NEED SLEEP BEFORE FINISHING
         @DELETE("/api/Subject/DeleteStudentCourse")
         public async void  PostStudentSubject(@Query("CourseName") String courseName,Callback<Response> cb);
        @GET("/api/StudentSession/GetStudySession")
        public async void  getStudentSession(@Query("sessionId") int sessionId,  Callback<ArrayList<StudySession>>cb);
        @GET("/api/StudentSession/GetMessages")
        public async void  getStudentMessages(@Query("sessionId") int sessionId,@Query("tutorId") int tutorId,Callback<ArrayList<TreeMessage>>cb);
        @GET("/api/StudentSession/GetReviews")
        public async void  getStudentReviews(@Query("studentId")int studentId,Callback<Response>cb);
        @GET("/api/StudentSession/GetSessionTime")
        public async void  getSessionTime(@Query("sessionId") int sessionId,Callback<StudySession>cb);

        @GET("/api/University/GetUniversity")
        public async void  getUniversity(Callback<University>university);

        @POST("/api/StudentSession/PostCreateStudySession")
        public async void  postStudySession(@Body StudySessionModel sessionModel, Callback<Response>cb);
        @POST("/api/StudentSession/PostMessage")
        public async void  postStudentMessage(@Body TreeMessage messageModel,Callback<Response>cb);

        @POST("/api/Subject/PostStudentSubject")
        public async void  postStudentSubject(@Body PostSubjectModel model , Callback<Response>cb);


        @PUT("/api/StudentSession/PutAccept")
        public async void  putStudentAccept(@Body AcceptModel acceptModel, Callback<Response>cb);
        @PUT("/api/StudentSession/PutStart")
        public async void  putStudentStart(@Body StartModel startModel, Callback<Response>cb);
        @PUT("/api/StudentSession/PutEndSession")
        public async void  putStudentEndSession(@Body EndSessionModel endSessionModel,Callback<Response>cb);
        @PUT("/api/StudentSession/PutUpdateTimer")
        public async void  putStudentUpdateTimer(@Body TimerModel timerModel,Callback<Response>cb);
        @PUT("/api/StudentSession/PutReview")
        public async void  putStudentReview(@Body ReviewModel reviewMode, Callback<Response>cb);
        @PUT("/api/StudentSession/PutStart")
        public async void  putStartTime(@Body StartModel model, Callback<Response>cb);

        @DELETE("/api/StudentSession/DeleteStudySession")
        public async void  deleteStudentStudySession(@Query("sessionId")int sessionId,Callback<Response>cb);
        @DELETE("/api/StudentSession/DeleteTutor")
        public async void  deleteTutorFromStudySession(@Query("sessionId")int sessionId,@Query("tutorId")int tutorId,Callback<Response>cb);

        @GET("/api/Search/GetNotifiableTutors")
        public async void  getSearchForTutors(@Query("studySessionId") int sessionId, Callback<ArrayList<Tutor>>cb);

        @POST("/api/EventNotification/PostSuggestionTutor")
        public async void  postSuggestTutor(@Body SuggestModel model , Callback<Response>cb);

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
