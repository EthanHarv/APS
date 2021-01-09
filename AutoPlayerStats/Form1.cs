using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Permissions;
using Newtonsoft.Json;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.Specialized;

namespace AutoPlayerStats
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBoxManager.Yes = "Whitelist"; // TODO: Probably would be better to just use a dedicated form, forget why I did it this way
            MessageBoxManager.No = "Blacklist";
            MessageBoxManager.Register();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\.lunarclient\logs\launcher\"; // TODO: Work with every client possible
            CreateFileWatcher(path); // TODO - Doesn't seem to work with forge (doesn't close log file?) - find a workaround, maybe just constant calls? dunno

            fixLists();

            LogWriter.Write("Started on " + DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss"));
        }

        public void CreateFileWatcher(string path)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = path;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "*.log";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
        }

        public void OnChanged(object source, FileSystemEventArgs e) // Rewrite this entirely - it's decently inefficent and doesnt work in some cases
        {
            Form1 frm1 = (Form1)Application.OpenForms["Form1"];

            List<string> text = new List<string>();
            while (!IsFileReady(e.FullPath)) { }; // Wait for file to be accessable
            try
            {
                text = File.ReadAllLines(e.FullPath).Reverse().Take(2).ToList();
            }
            catch (Exception ex)
            {
                LogWriter.Write("Exception: " + ex);
            }
            List<Player> players = new List<Player>();
            foreach (string i in text)
            {
                if (i.Contains("[Client thread/INFO]: [CHAT] ONLINE:")) // TODO: Add playerjoin/playerleave Autodetect
                {
                    string[] names = i.Split(new string[] { "[Client thread/INFO]: [CHAT] ONLINE:" }, StringSplitOptions.None)[1].Split(',');
                    foreach (string name in names)
                    {
                        players.Add(new Player(name));
                    }
                    LogWriter.Write("Loaded Players");
                }
            }
            if (players.Count == 0)
            {
                return;
            }

            // Populate Controls
            List<Thread> threads = new List<Thread>();
            foreach (Player player in players)
            {
                Player tmp = player;
                Thread thread = new Thread(() => tmp.CallAPI());
                threads.Add(thread);
                thread.Start();
                Thread.Sleep(50);
            }
            foreach (Thread thread in threads)
            {
                thread.Join();
            }
            playerDisplay.Invoke((MethodInvoker)delegate { playerDisplay.Controls.Clear(); }); // Clear all players
            players = players.OrderByDescending(x => x.FKDR).ToList();
            List <PlayerPanel> panels = new List<PlayerPanel>();
            foreach (Player player in players)
            {
                PlayerPanel panel = new PlayerPanel(player);
                panel.Populate();
                panels.Add(panel);
            }

            foreach (PlayerPanel panel in panels)
            {
                playerDisplay.Invoke((MethodInvoker)delegate { playerDisplay.Controls.Add(panel); });
            }
            // Display API count
            Thread.Sleep(2500);
            try
            {
                string key = Properties.Settings.Default.HypixelKey;
                var hypxUrl = @"https://api.hypixel.net/key?key=" + key; // Hypixel API Call to get UUID of keyholder
                WebRequest wrGETURL2;
                wrGETURL2 = WebRequest.Create(hypxUrl);

                Stream objStream2;
                objStream2 = wrGETURL2.GetResponse().GetResponseStream();

                StreamReader objReader2 = new StreamReader(objStream2);

                string hypixelResponse = objReader2.ReadLine();

                dynamic hypixelData = JsonConvert.DeserializeObject(hypixelResponse);

                frm1.lblApi.Invoke((MethodInvoker)delegate { frm1.lblApi.Text = hypixelData.record.queriesInPastMin; });
                frm1.lblApiTotal.Invoke((MethodInvoker)delegate { frm1.lblApiTotal.Text = hypixelData.record.totalQueries; });
            }
            catch (Exception ex)
            {
                LogWriter.Write("Error in getting API uses\n" + ex);
            }

            LogWriter.Write("CONTROLS COUNT " + playerDisplay.Controls.Count.ToString());
        }

        public static bool IsFileReady(string filename)
        {
            try
            {
                using (FileStream inputStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    return inputStream.Length > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void Form1_Resize(object sender, EventArgs e) // TODO: Hopefully make it resizable in the future easily
        {
            ((Form1)sender).Width = 1216;
            ((Form1)sender).Height = 804;
        }

        public static void fixLists()
        {
            List<string> fList = Properties.Settings.Default.fList.Cast<string>().ToList();
            fList = fList.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
            StringCollection fCollec = new StringCollection();
            fCollec.AddRange(fList.ToArray());
            Properties.Settings.Default.fList = fCollec;

            List<string> eList = Properties.Settings.Default.eList.Cast<string>().ToList();
            eList = eList.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
            StringCollection eCollec = new StringCollection();
            eCollec.AddRange(eList.ToArray());
            Properties.Settings.Default.eList = eCollec;

            Properties.Settings.Default.Save();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            this.TopMost = false; // So they dont fight
            UserSettings settings = new UserSettings();
            settings.TopMost = true;
            settings.ShowDialog();
            this.TopMost = true;
        }
    }
}