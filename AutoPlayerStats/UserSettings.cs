using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPlayerStats
{
    public partial class UserSettings : Form
    {
        public UserSettings()
        {
            InitializeComponent();
        }

        private void UserSettings_Load(object sender, EventArgs e)
        {
            this.txtBoxKey.Text = Properties.Settings.Default.HypixelKey;
        }

        private void btnSaveKey_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.HypixelKey = txtBoxKey.Text;
            Properties.Settings.Default.Save();
        }

        private void btnEditList_Click(object sender, EventArgs e)
        {
            this.TopMost = false; // no fighting again :)
            FormListEditor editor = new FormListEditor();
            editor.TopMost = true;
            editor.ShowDialog();
            this.TopMost = true;
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
                LogWriter.Write("Error loading friends: " + ex);
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

            LogWriter.Write("Added " + uuids.Count + " friends + own name.");
        }

        private string getNameFromUUID(string uuid)
        {
            int count = 0;
            int maxtries = 5; // Calling this should be a pretty infrequent event overall (should only be called by "get friends as whitelist," so its fine jacking up the retry number a bit
            while (count <= maxtries)
            {
                try
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
                catch (Exception e)
                {
                    LogWriter.Write("Error in getNameFromUUID() " + e);
                }
            }
            return null;
        }

        public static void fixLists() // Exists elsewhere, probably best to bring into some centralized class
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogFolder_Click(object sender, EventArgs e)
        {
            string logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AutoPlayerStats\\");
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = logFilePath,
                UseShellExecute = true,
                Verb = "open"
            });
        }
    }
}
