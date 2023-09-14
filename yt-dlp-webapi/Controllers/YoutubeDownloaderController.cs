using Microsoft.AspNetCore.Mvc;

namespace yt_dlp_webapi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class YoutubeDownloaderController : ControllerBase
{
    [HttpGet]
    public IEnumerable<string> GetDownloadList()
    {
        return new string[]
        {
            "Download2", "Download1",
        };
    }
    
    [HttpGet(Name = "GetDownload")]
    public string Download(string url)
    {
        DownloadProcessManager.AddDownloadProcess(url);
        return "a";
    }
}