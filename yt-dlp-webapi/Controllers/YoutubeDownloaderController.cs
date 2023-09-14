using Microsoft.AspNetCore.Mvc;
using yt_dlp_webapi.Model;

namespace yt_dlp_webapi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class YoutubeDownloaderController : ControllerBase
{
    [HttpGet(Name = "GetDownload")]
    public int Download(string url)
    {
        DownloadProcessManager.AddDownloadProcess(url);
        return DownloadProcessManager.DownloadList.Count;
    }

    [HttpGet]
    public IEnumerable<DownloadProcessModel> GetDownloadList()
    { 
        return DownloadProcessManager.DownloadList.ConvertAll((input => new DownloadProcessModel(input)));
    }

    [HttpGet]
    public float GetDownloadPercentage(int index)
    {
        return DownloadProcessManager.DownloadList[index].PercentageOfDownload;
    }
}