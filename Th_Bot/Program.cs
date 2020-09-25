using System;
using System.Security.Principal;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace Th_Bot
{
    class Program
    {
        private static ITelegramBotClient botClient;
        private static int id_weather = 0;
        static void Main(string[] args)
        {

            botClient = new TelegramBotClient("1323627427:AAGfE-h4KQYuIp6x47tauyvDn_kO7sXD3Cc") { Timeout = TimeSpan.FromSeconds(10) };;
            //  var user = botClient.GetMeAsync().Result;  thide code just test connection to bot
            botClient.OnMessage += BotOnMessageReceived;
            botClient.OnMessageEdited += BotOnMessageReceived;
            botClient.StartReceiving();
            // Console.WriteLine("Press any key to exit");
            // Console.ReadKey();
			while(true)
            {; }
            botClient.StopReceiving();
        }


        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {

            var message = messageEventArgs.Message;
            if (message?.Type == MessageType.Text)
            {
                operation(message.Chat.Id,message.Text);
                //if (backMessage != "Error" && backMessage !=  "Write name of city")
                //{
                //    Stream stream = System.IO.File.OpenRead(@"Icon/thunderstorm.png");
                //    await botClient.SendPhotoAsync
                //        (
                //            message.Chat.Id,
                //            stream,
                //            backMessage
                //        );
            } 
                else
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Error");
        }

        private  static void operation(long chat_Id, string messageToBot)
        {
            if (messageToBot == "/datatime")
            {
                string send_message =  DateTime.UtcNow.ToString();
                SendTextMessage(chat_Id, send_message);
                return;
            }
            if (messageToBot == "/json")
            {
                string send_message =  cmdGO_Click();
                SendTextMessage(chat_Id, send_message);
                return;
            }
            if (messageToBot == "/weather")
            {
                id_weather = 1;
                SendTextMessage(chat_Id, "Write name of city");
                return;
            }
            else if (id_weather == 1)
            {
                id_weather = 0;
                Weather_Model Weather = get_Weather_InCity(messageToBot);
                if (Weather != null)
                {
                    string response = "";
                    response += Weather.name + "\n";
                    response += "Weather: " + Weather.weather.ElementAt(0).description + "\n";
                    response += "Cloud: " + Weather.clouds.all + "%\n";
                    response += "Wind: " + Weather.wind.speed + " m/s\n";
                    response += "Tempertatur: " + (Math.Round(Weather.main.temp - 273)).ToString() + " °C\n";
                    string icon = Weather.weather.ElementAt(0).icon;
                    SendPhotoMessage(chat_Id, response, icon);
                }
                SendTextMessage(chat_Id, "Error");
                return;
            }
            SendTextMessage(chat_Id, "Error");
        }

        private static string cmdGO_Click()
        {
            RESTClient rClient = new RESTClient();
            string txtRequestURI = "http://dry-cliffs-19849.herokuapp.com/users.json";
            rClient.endPoint = txtRequestURI;
            string strJSON = string.Empty;
            strJSON = rClient.makeRequest();
            return strJSON;
        }
        private static string get_Weather_InCity()
        {
            string api_key = "782792da0e998344afa4a6cc73d7e890";            
            RESTClient rClient = new RESTClient();
            string txtRequestURI = "https://api.openweathermap.org/data/2.5/weather?q=Moscow&appid=782792da0e998344afa4a6cc73d7e890";
            rClient.endPoint = txtRequestURI;
            string strJSON = string.Empty;
            strJSON = rClient.makeRequest();
            return strJSON;
        }
        //private static string get_Weather_InCity(string city)
        //{
        //    string api_key = "782792da0e998344afa4a6cc73d7e890";
        //    RESTClient rClient = new RESTClient();
        //    string txtRequestURI = "https://api.openweathermap.org/data/2.5/weather?q="+ city + "&appid=" + api_key;
        //    rClient.endPoint = txtRequestURI;
        //    string strJSON = string.Empty;
        //    strJSON = rClient.makeRequest();
        //    return strJSON;
        //}


        private static Weather_Model get_Weather_InCity(string city)
        {
            string api_key = "782792da0e998344afa4a6cc73d7e890";
            RESTClient rClient = new RESTClient();
            string txtRequestURI = "https://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=" + api_key;
            rClient.endPoint = txtRequestURI;
            string strJSON = string.Empty;
            strJSON = rClient.makeRequest();
            Weather_Model myDeserializedClass = JsonConvert.DeserializeObject<Weather_Model>(strJSON);
            return myDeserializedClass;
        }

        private async static void SendTextMessage (long id_chat ,string text)
        {
            await botClient.SendTextMessageAsync(id_chat, text);
        }
        private async static void SendPhotoMessage(long id_chat, string text, string FileName)
        {
            Stream stream = File.OpenRead(@"Icon/" + FileName + ".png");
            await botClient.SendPhotoAsync
                (
                    id_chat, 
                    stream,
                    text
                );
        }



    }
}

