using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace AdsServer
{
    internal class Program
    {
        static Random random = new Random();
        static GetImgsRequest HandleImgsRequest()
        {
            var rndMax = DirectoryExtension.CountFilesInFolder(GetImgsRequest.ADFOLDER_DIR);


            GetImgsRequest getImgsRequest = new GetImgsRequest(random.Next(0, rndMax).ToString());

            return getImgsRequest;
        }
        static void Main(string[] args)
        {
            SqlConnectionHandler sqlConnectionHandler = new SqlConnectionHandler();

            if (Directory.Exists(GetImgsRequest.ADFOLDER_DIR))
                Directory.CreateDirectory(GetImgsRequest.ADFOLDER_DIR);


            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("https://*:8080/");
            listener.Start();

            Thread getContextThread = new Thread(() =>
            {
                for (; ; )
                {
                    try
                    {
                        var t = listener.GetContext();
                        {
                            ThreadHelper.CreateThread(() =>
                            {

                                var context = t;
                                string[] requestUrlSplited = context.Request.Url.AbsolutePath.Split("/", StringSplitOptions.RemoveEmptyEntries);
                                string jsonRespone = "";

                                HandlePreflightRequest(context);


                                switch (requestUrlSplited[0])
                                {
                                    case "imgAds":
                                        var result = HandleImgsRequest();
                                        jsonRespone = JsonConvert.SerializeObject(result);

                                        break;
                                    case "Cookie":

                                        var body = "";
                                        byte[] data = new byte[500];

                                        var length = t.Request.InputStream.Read(data, 0, data.Length);
                                        if (length > 0)
                                        {

                                            body = Encoding.UTF8.GetString(data);
                                            sqlConnectionHandler.NonQuery(@$"INSERT INTO dbo.CookieManager (cookie) VALUES ('{body}')");
                                        }
                                        break;
                                }
                                t.Response.OutputStream.Write(Encoding.UTF8.GetBytes(jsonRespone));
                                t.Response.OutputStream.Close();
                            });
                        }
                    }
                    catch (Exception e)
                    {
#if DEBUG
                        throw;

#else
                         e.PrintExceptionInfo();
#endif
                    }

                }
            });
            getContextThread.IsBackground = false;
            getContextThread.Start();


        }
        static void HandlePreflightRequest(HttpListenerContext context)
        {
            // Respond to the preflight request with the necessary CORS headers
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");


            // Indicate that the preflight response is valid for 3600 seconds (1 hour)
            context.Response.AddHeader("Access-Control-Max-Age", "3600");

            // Send a successful response status
            context.Response.StatusCode = (int)HttpStatusCode.OK;
        }
    }
}
