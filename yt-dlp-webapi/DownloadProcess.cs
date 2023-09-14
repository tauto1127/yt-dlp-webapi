using System.Diagnostics;
using System.Text.RegularExpressions;

namespace yt_dlp_webapi;

public class DownloadProcess
{
    public Process? YtdlProcess { get; set; }
    public bool IsFinished;
    public string Url;
    private string PercentageRegex = @"\d+.\d%";
        
    public DownloadProcess(string url)
    {
        this.IsFinished = false;
        this.Url = url;
        ProcessStartInfo processStartInfo = new ProcessStartInfo();
        processStartInfo.FileName = "yt-dlp";
        processStartInfo.Arguments = YtdlArgGenerator(url);
        processStartInfo.CreateNoWindow = true;
        processStartInfo.UseShellExecute = false;
        processStartInfo.RedirectStandardOutput = true;
        processStartInfo.WorkingDirectory = @"/Users/takuto";
        YtdlProcess = new Process();
        YtdlProcess.StartInfo = processStartInfo;
    }

    public async Task Start()
    {
        if (!IsFinished)
        {
            Console.WriteLine("start!");
            using (YtdlProcess)
            {
                YtdlProcess.OutputDataReceived += (sender, args) =>
                {
                    Console.WriteLine(args.Data);
                    Console.WriteLine(GetDownloadPercentage(args.Data));
                };
                YtdlProcess.Start();
                YtdlProcess.BeginOutputReadLine();
                YtdlProcess.WaitForExit();
                this.YtdlProcess = null;
                this.IsFinished = true;
            }
        }
        else
        {
            throw new NullReferenceException("すでに実行されています");
        }
    }

    private string GetDownloadPercentage(string line)
    {
        try
        {
            return Regex.Match(line, PercentageRegex).Value;
        }
        catch (FormatException exception)
        {
            return "1000";
        }
    }

    private string YtdlArgGenerator(string url)
    {
        return $"{AddArg(url)}";
    }

    private static string AddArg(string arg)
    {
        return arg + " ";
    }
}