using System.Diagnostics;
using System.Text.RegularExpressions;

namespace yt_dlp_webapi;

public class DownloadProcess
{
    public Process? YtdlProcess { get; set; }
    public bool IsFinished;
    public string Url;
    public float PercentageOfDownload = 0;
    private const string PercentageRegex = @"\d+.\d%";
        
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
                int i = 1;
                YtdlProcess.OutputDataReceived += (sender, args) =>
                {
                    if (args.Data != null)
                        this.PercentageOfDownload = GetDownloadPercentage(args.Data) ?? this.PercentageOfDownload;
                    i++;
                    if (args.Data != null)
                    {
                        Console.WriteLine(GetDownloadPercentage(args.Data));
                        Console.WriteLine(args.Data);
                    }
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

    private float? GetDownloadPercentage(string line)
    {
        try
        {
            string percentage = Regex.Match(line, PercentageRegex).Value;
            return float.Parse(percentage.Trim('%'));
        }
        catch (FormatException exception)
        {
            return null;
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