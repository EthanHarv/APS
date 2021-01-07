using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPlayerStats
{
    public class Player
    {
        public string Name { get; private set; }
        public bool IsNick { get; private set; } = false;
        // TODO - Make dynamic instead of static. Gonna continue using this while I make sure party detection works, but will probably make a branch soon to make this dynamic
        public bool IsSuspectedParty { get; private set; } = false;
        public int Stars { get; private set; } = 0;
        public int Finals { get; private set; } = 0;
        public int Winstreak { get; private set; } = 0;
        public int GamesPlayed { get; private set; } = 0;
        public double FKDR { get; private set; } = 0;
        public Panel Panel { get; private set; } // I need to re-do the whole panel system, probably a new panel class that then has a Player object inside it

        public Player(string name)
        {
            this.Name = name.Trim();
        }

        public void CallAPI()
        {
            Form1 frm1 = (Form1)Application.OpenForms["Form1"];
            int count = 1;
            int maxtries = 3;
            while (count <= maxtries)
            {
                try
                {
                    this.IsNick = false;

                    string uuidUrl = @"https://playerdb.co/api/player/minecraft/" + this.Name; // Switched to playerdb.co as opposed to mojang's API - seems more reliable with uptime

                    HttpWebRequest wrGETURL = (HttpWebRequest)WebRequest.Create(uuidUrl);

                    wrGETURL.Timeout = 1000;

                    var responseCode = ((HttpWebResponse)wrGETURL.GetResponse()).StatusCode;

                    if (responseCode != HttpStatusCode.OK) // If no UUID is returned, we can say the user is a nick.
                    {
                        this.IsNick = true;
                        throw new Exception("Mojang Bad Response");
                    }

                    Stream objStream;
                    objStream = wrGETURL.GetResponse().GetResponseStream();

                    StreamReader objReader = new StreamReader(objStream);
                    string mojangResponse = objReader.ReadLine();

                    dynamic mojangData = JsonConvert.DeserializeObject(mojangResponse);
                    var uuid = mojangData.data.player.id;

                    string key = Properties.Settings.Default.HypixelKey;
                    var hypxUrl = @"https://api.hypixel.net/player?key=" + key + "&uuid=" + uuid; // Hypixel API Call to get stats
                    WebRequest wrGETURL2;
                    wrGETURL2 = WebRequest.Create(hypxUrl);

                    Stream objStream2;
                    objStream2 = wrGETURL2.GetResponse().GetResponseStream();

                    StreamReader objReader2 = new StreamReader(objStream2);

                    string hypixelResponse = objReader2.ReadLine();

                    dynamic hypixelData = JsonConvert.DeserializeObject(hypixelResponse);

                    if (hypixelData.player == null) // Did have a valid UUID, but isn't a valid Hypixel player. AFAIK there is a "success" response but this works too - no particular reason
                    {
                        this.IsNick = true;
                        throw new Exception("Hypixel NonResponse");
                    }

                    // TODO: Customizability

                    this.Stars = (int)hypixelData.player.achievements.bedwars_level;
                    this.GamesPlayed = (int)hypixelData.player.stats.Bedwars.games_played_bedwars;
                    if (hypixelData.player.channel == "PARTY") // NOT reliable, but is a good indication, especially for random parties - May combine with frienddetection, but unsure if API usage intensity would be too much
                        this.IsSuspectedParty = true;
                    if (hypixelData.player.stats.Bedwars.four_four_final_kills_bedwars != null && hypixelData.player.stats.Bedwars.four_four_final_deaths_bedwars != null)
                    {
                        this.FKDR = ((double)hypixelData.player.stats.Bedwars.four_four_final_kills_bedwars / (double)hypixelData.player.stats.Bedwars.four_four_final_deaths_bedwars);
                        this.Finals = hypixelData.player.stats.Bedwars.four_four_final_kills_bedwars;
                    }
                    else
                    {
                        this.FKDR = 0;
                        this.Finals = 0;

                    }
                    if (hypixelData.player.stats.Bedwars.winstreak != null)
                        this.Winstreak = (int)hypixelData.player.stats.Bedwars.winstreak;
                    else
                        this.Winstreak = 0;
                    break;
                }
                catch (WebException)
                {
                    if (count >= maxtries)
                        LogWriter.Write("Nick Detected - API Error thrown\n");
                    this.IsNick = true;
                    count++;
                }
                catch (Exception e)
                {
                    LogWriter.Write("Nick Detected - " + e +"\n");
                    this.IsNick = true;
                    break;
                }
            }


            // TODO: Make new panel system! Customizable controls & such, maybe arrange horizontal instead of vertical like the other overlays do.
            // Also, honestly can't imagine the bwstats guy would be very happy with me for opensourcing all this, but maybe try to integrate their hacker/sniper database?
            // (I have now found their API - unsure if its meant to be public, but will probably integrate soon unless he says otherwise)

            var labelName = new Label();
            labelName.AutoSize = true;
            labelName.Location = new Point(5, 0);
            labelName.Font = new Font(labelName.Font, FontStyle.Bold);
            labelName.Text = Name;
            if (IsSuspectedParty)
                labelName.Text = Name + " (Party)"; // TODO: Temporary! Redo this all during the panel rewrite

            var labelStars = new Label();
            labelStars.AutoSize = true;
            labelStars.Location = new Point(5, labelName.Height);
            labelStars.Text = "Stars: " + Stars.ToString() + "  Winstreak: " + Winstreak.ToString();

            var labelFKDR = new Label();
            labelFKDR.AutoSize = true;
            labelFKDR.Location = new Point(5, labelName.Height + labelStars.Height);
            labelFKDR.Text = "FKDR: " + new string(FKDR.ToString().Take(5).ToArray()) + "  Finals: " + Finals.ToString();

            var panel = new Panel();
            panel.AutoSize = true;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.BackColor = getColor(this);

            panel.Controls.Add(labelName);
            panel.Controls.Add(labelStars);
            panel.Controls.Add(labelFKDR);

            panel.Click += pClick;
            foreach (Control child in panel.Controls)
            {
                child.Click += childClick;
            }

            this.Panel = panel;
        }

        void childClick(object sender, EventArgs e)
        {
            pClick(((Label)sender).Parent, EventArgs.Empty);
        }

        void pClick(object sender, EventArgs e)
        {
            string name = ((Panel)sender).Controls[0].Text;

            DialogResult result = MessageBox.Show("Whitelist or Blacklist " + name, name, MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes) // Whitelist
            {
                if (Properties.Settings.Default.eList.Contains(name))
                {
                    Properties.Settings.Default.eList.Remove(name);
                }
                Properties.Settings.Default.fList.Add(name);
                Properties.Settings.Default.Save();
            }
            else if (result == DialogResult.No) // Blacklist
            {
                if (Properties.Settings.Default.fList.Contains(name))
                {
                    Properties.Settings.Default.fList.Remove(name);
                }
                Properties.Settings.Default.eList.Add(name);
                Properties.Settings.Default.Save();
            }

            Form1.fixLists();
        }

        public static Color getColor(Player player)
        {
            if (Properties.Settings.Default.fList.Contains(player.Name))
            {
                return Color.Green;
            }
            if (Properties.Settings.Default.eList.Contains(player.Name))
            {
                return Color.Red;
            }
            if (player.IsNick)
            {
                return Color.LightBlue;
            }
            if (player.Stars <= 5 && player.FKDR >= 2.25)
            {
                return Color.DarkRed;
            }
            if (player.GamesPlayed <= 10)
            {
                return Color.Yellow;
            }
            if (player.Winstreak >= 5)
            {
                return Color.RoyalBlue;
            }
            if (player.FKDR >= 5)
            {
                return Color.Orange;
            }
            return Color.White;
        }

    }
}
