using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPlayerStats
{
    public partial class PlayerPanel : Panel
    {
        public PlayerPanel(Player player)
        {
            this.player = player;
        }
        public Player player { get; protected set; }

        public void Populate()
        {
            var labelName = new Label();
            labelName.AutoSize = true;
            labelName.Location = new Point(5, 0);
            labelName.Font = new Font(labelName.Font, FontStyle.Bold);
            labelName.Text = player.Name;
            if (player.IsSuspectedParty)
                labelName.Text = player.Name + " (Party)"; // TODO: Temporary display

            var labelStars = new Label();
            labelStars.AutoSize = true;
            labelStars.Location = new Point(5, labelName.Height);
            labelStars.Text = "Stars: " + player.Stars.ToString() + "  Winstreak: " + player.Winstreak.ToString();

            var labelFKDR = new Label();
            labelFKDR.AutoSize = true;
            labelFKDR.Location = new Point(5, labelName.Height + labelStars.Height);
            labelFKDR.Text = "FKDR: " + new string(player.FKDR.ToString().Take(5).ToArray()) + "  Finals: " + player.Finals.ToString();

            this.AutoSize = true;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = getColor(player);

            this.Controls.Add(labelName);
            this.Controls.Add(labelStars);
            this.Controls.Add(labelFKDR);

            this.Click += pClick;
            foreach (Control child in this.Controls)
            {
                child.Click += childClick;
            }
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
