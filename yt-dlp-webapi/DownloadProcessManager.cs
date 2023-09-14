namespace yt_dlp_webapi;

public class DownloadProcessManager
{
    public static List<DownloadProcess> DownloadList;

    public static void AddDownloadProcess(DownloadInfo downloadInfo)
    {
        
    }

    public static void AddDownloadProcess(string url)
    {
        DownloadProcess downloadProcess = new DownloadProcess(url);
        Task.Run(async () =>
        {
            downloadProcess.Start();
        });
        DownloadList.Add(downloadProcess);
    }
}