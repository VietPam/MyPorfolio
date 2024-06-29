using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace MyPorfolio.Apis;

public class MyFile
{
    public FileContentResult? loadFileLog()
    {
        try
        {
            DirectoryInfo Logdirectory = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "logs"));
            if (!Logdirectory.Exists)
            {
                return null;
            }
            FileInfo? latestLogFile = Logdirectory.GetFiles()
                                    .OrderByDescending(f => f.LastWriteTime)
                                    .FirstOrDefault();
            if (latestLogFile == null)
            {
                return null;
            }

            byte[] data = File.ReadAllBytes(latestLogFile.FullName);
            return new FileContentResult(data, "text/plain")
            {
                FileDownloadName = latestLogFile.Name
            };
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }
}
