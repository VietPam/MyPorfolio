using Microsoft.AspNetCore.Http;
using Serilog;

namespace MyPorfolio;

public class MyBot
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public MyBot()
    {
        httpContextAccessor = new HttpContextAccessor();
    }
    public void start()
    {

        Thread t = new Thread(async () =>
        {
            HttpClient client = new HttpClient();
            while (true)
            {
                try
                {
                    string baseUrl = "https://myporfolio-latest.onrender.com";
                    string path = $"{baseUrl}/api/rest/hello";
                    Log.Information($"calling api: {path}");
                    HttpResponseMessage response = await client.GetAsync(path);
                    if (response.IsSuccessStatusCode)
                    {
                        Log.Information("called api success");
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Log.Information(responseBody);
                    }
                    else
                    {
                        Log.Information("called api fail");
                    }
                }

                catch (Exception ex)
                {
                    Log.Error($"An error occurred: {ex}");
                }
                // render service sleep after 15 minutes without receive any request
                // set sleep each 10 minutes
                Thread.Sleep(10* 60* 1000); 
            }
        })
        {
            Name = "auto recall ",
            IsBackground = true
        };
        t.Start();
    }

}
