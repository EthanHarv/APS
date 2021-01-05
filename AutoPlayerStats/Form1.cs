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
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\.lunarclient\logs\launcher\"; // TODO: Work with every client possible
            txtBoxKey.Text = Properties.Settings.Default.HypixelKey;

            MessageBoxManager.Yes = "Whitelist"; // TODO: Probably would be better to just use a dedicated form, forget why I did it this way
            MessageBoxManager.No = "Blacklist";
            MessageBoxManager.Register();

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

        public void OnChanged(object source, FileSystemEventArgs e)
        {
            Form1 frm1 = (Form1)Application.OpenForms["Form1"];

            List<string> text = new List<string>();
            while (!IsFileReady(e.FullPath)) { }; // Wait
            try
            {
                text = File.ReadAllLines(e.FullPath).Reverse().Take(2).ToList();
            }
            catch (Exception ex)
            {
                LogWriter.Write("Exception: " + ex + "\n");
            }
            List<Player> players = new List<Player>();
            foreach (string i in text)
            {
                if (i.Contains("[Client thread/INFO]: [CHAT] ONLINE:"))
                {
                    string[] names = i.Split(new string[] { "[Client thread/INFO]: [CHAT] ONLINE:" }, StringSplitOptions.None)[1].Split(',');
                    foreach (string name in names)
                    {
                        players.Add(new Player(name));
                    }
                    LogWriter.Write("Loaded Players\n");
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
            playerPanel.Invoke((MethodInvoker)delegate { playerPanel.Controls.Clear(); });
            players = players.OrderByDescending(x => x.FKDR).ToList();
            foreach (Player player in players)
            {
                playerPanel.Invoke((MethodInvoker)delegate { playerPanel.Controls.Add(player.Panel); });
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
                LogWriter.Write("Error in getting API uses\n" + ex + "\n");
            }
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

        private void btnSaveKey_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.HypixelKey = txtBoxKey.Text;
            Properties.Settings.Default.Save();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ((Form1)sender).Width = 1216;
            ((Form1)sender).Height = 804;
        }

        private void btnFriends_Click(object sender, EventArgs e)
        {
            Form1 frm1 = (Form1)Application.OpenForms["Form1"];
            List<string> uuids = new List<string>();
            string ownUUID = "";
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

                ownUUID = hypixelData.record.owner;

                var hypxFriendUrl = @"https://api.hypixel.net/friends?key=" + key + "&uuid=" + ownUUID; // Hypixel API Call to get stats
                WebRequest wrGETURLFriend;
                wrGETURLFriend = WebRequest.Create(hypxFriendUrl);

                Stream objStreamFriend;
                objStreamFriend = wrGETURLFriend.GetResponse().GetResponseStream();

                StreamReader objReaderFriend = new StreamReader(objStreamFriend);

                string hypixelFriendResponse = objReaderFriend.ReadLine();

                dynamic hypixelFriendData = JsonConvert.DeserializeObject(hypixelFriendResponse);

                foreach (var uuid in hypixelFriendData.records)
                {
                    if (uuid.uuidReceiver == ownUUID.Replace("-", ""))
                        uuids.Add((string)uuid.uuidSender);
                    else
                        uuids.Add((string)uuid.uuidReceiver);
                }
            }
            catch (Exception ex)
            {
                LogWriter.Write("Error loading friends: " + ex +"\n");
            }
            foreach (string uuid in uuids)
            {
                string name = getNameFromUUID(uuid);
                Console.WriteLine(name);
                Properties.Settings.Default.fList.Add(name);
            }
            string ownName = getNameFromUUID(ownUUID);
            Console.WriteLine(ownName);
            Properties.Settings.Default.fList.Add(ownName);
            Properties.Settings.Default.Save();

            fixLists();

            LogWriter.Write("Added " + uuids.Count + " friends + own name.\n");
        }

        private string getNameFromUUID(string uuid)
        {
            int count = 0;
            int maxtries = 5; // Calling this should be a pretty infrequent event overall (should only be called by "get friends as whitelist," so its fine jacking up the retry number a bit
            while (count <= maxtries)
            {
                count++;
                Form1 frm1 = (Form1)Application.OpenForms["Form1"];
                string mojUrl = @"https://api.mojang.com/user/profile/" + uuid; // Mojang API - Get the user name // TODO: Switch to playerdb API possibly
                HttpWebRequest wrGETURL = (HttpWebRequest)WebRequest.Create(mojUrl);

                wrGETURL.Timeout = 1000;

                var responseCode = ((HttpWebResponse)wrGETURL.GetResponse()).StatusCode;

                Stream objStream;
                objStream = wrGETURL.GetResponse().GetResponseStream();

                StreamReader objReader = new StreamReader(objStream);
                string mojangResponse = objReader.ReadLine();

                if (responseCode != HttpStatusCode.OK) // If nothing is returned
                {
                    LogWriter.Write("Error in getNameFromUUID()\n");
                    throw new Exception("Mojang Not Ok");
                }

                dynamic mojangData = JsonConvert.DeserializeObject(mojangResponse);

                return mojangData.name;
            }
            return null;
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

        private void btnEditList_Click(object sender, EventArgs e)
        {
            FormListEditor editor = new FormListEditor();
            editor.ShowDialog();
        }
    }
}