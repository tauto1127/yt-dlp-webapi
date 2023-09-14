using Microsoft.AspNetCore.Mvc;

namespace yt_dlp_webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class YoutubeDownloaderController : ControllerBase
{
    /*

    [HttpGet]
    public IEnumerable<string> GetDownloadList()
    {
        return new string[]
        {
            "Download2", "Download1",
        };
    }*/
    
    [HttpGet(Name = "GetDownload")]
    public string Download(string url)
    {
        DownloadProcess downloadProcess = new DownloadProcess(url);
        return "a";
    }
}