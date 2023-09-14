
namespace yt_dlp_webapi.Model;

public class DownloadProcessModel
{
    public bool IsFinished { get;}
    public string Url { get;}

    public DownloadProcessModel()
    {
        
    }

    public DownloadProcessModel(DownloadProcess downloadProcess)
    {
        this.Url = downloadProcess.Url;
        this.IsFinished = downloadProcess.IsFinished;
    }

    public override string ToString()
    {
        return $"isFinished: {IsFinished}, url: {Url}";
    }
}