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
                    Log.Information("calling api");
                    HttpResponseMessage response = await client.GetAsync($"{baseUrl}/api/rest/hello");
                    Log.Information("called api");
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
                    Log.Information("called api fail");
                }
                Thread.Sleep(10000); // Gọi lại mỗi 10 giây
            }
        })
        {
            Name = "auto recall ",
            IsBackground = true
        };
        t.Start();
    }

}
