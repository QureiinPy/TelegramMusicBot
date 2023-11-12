using System.Resources;
using System.Text;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;
using Microsoft.VisualBasic;
using Telegram.Bot;
using Telegram.Bot.Types;
using VideoLibrary;

namespace TelegramBotMusic
{
    public class HttpWebClient
    {       
        public void DownloadMedia(string url)
        {
            var source = $@"D:\TelegramMusic\";
            var to = $@"D:\TelegramMusic\Converted\";
            var youtube = YouTube.Default;
            var vid = youtube.GetVideo(url);

            var inputPath = source + vid.FullName;
            var outputPath = to + vid.FullName.Substring(0, vid.FullName.Length - 4) + ".mp3";

            System.IO.File.WriteAllBytes(inputPath, vid.GetBytes());

           var convert = new NReco.VideoConverter.FFMpegConverter();
            convert.ConvertMedia(inputPath.Trim(), outputPath.Trim(), "mp3");
        }
    }
}
