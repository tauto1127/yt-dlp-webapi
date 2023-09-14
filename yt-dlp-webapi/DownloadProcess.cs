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
        YtdlProcess = new Process();
        YtdlProcess.StartInfo = processStartInfo;
    }

    public async Task start()
    {
        Console.WriteLine("start!");
        using (YtdlProcess)
        {
            YtdlProcess.OutputDataReceived += (sender, args) =>
            {
                Console.WriteLine(args.Data);
            };
            YtdlProcess.Start();
            YtdlProcess.BeginOutputReadLine();
            YtdlProcess.WaitForExit();
            Console.WriteLine("終わりました");
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