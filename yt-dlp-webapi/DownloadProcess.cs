using System.Diagnostics;

namespace yt_dlp_webapi;

public class DownloadProcess
{
    public Process YtdlProcess { get; set; }

    public DownloadProcess(string url)
    {
        ProcessStartInfo processStartInfo = new ProcessStartInfo();
        processStartInfo.FileName = "yt-dlp";
        processStartInfo.Arguments = ytdlArgGenerator(url);
        processStartInfo.CreateNoWindow = true;
        processStartInfo.UseShellExecute = false;
        processStartInfo.RedirectStandardOutput = true;
        processStartInfo.WorkingDirectory = @"/Users/takuto";
        start(url, processStartInfo);
    }

    private async Task start(string url, ProcessStartInfo processStartInfo)
    {
        using (YtdlProcess = new Process())
        {
            YtdlProcess.StartInfo = processStartInfo;
            YtdlProcess.OutputDataReceived += (sender, args) =>
            {
                Console.WriteLine(args.Data);
            };
            YtdlProcess.Start();
            YtdlProcess.WaitForExit();
        }
    }

    string ytdlArgGenerator(string url)
    {
        return $"{addArg(url)}";
    }

    private string addArg(string arg)
    {
        return arg + " ";
    }
}