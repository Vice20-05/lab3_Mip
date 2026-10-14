using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3_4
{
    public partial class window : Form
    {
        // list of blocked keywords (used by IsBlocked / UI)
        private List<string> keywords = new List<string> { "example.com", "blocked" };
        public window()
        {
            InitializeComponent();
            Trace.Listeners.Add(new TextWriterTraceListener("log.txt"));
            Trace.AutoFlush = true; //write to the file right away
            webBrowser1.ScriptErrorsSuppressed = true; //suppress script errors
            // show the keywords in the ComboBox
            toolStripComboBox1.Items.AddRange(keywords.ToArray());
            Log("App Started");

        }


        // write time (with ms) + message to log.txt, caller does not wait
        private void Log(string message)
        {
          Task.Run(() =>
            {
                Trace.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}: {message}");
            });
        }


        // LINQ: true if any keyword is inside the url
        private bool IsBlocked(string url)
        {
            return keywords.Any(keyword => url.ToLower().Contains(keyword));
        }

        // same LINQ check, run on another thread
        private Task<bool> IsBlockedAsync(string url)
        {
            return Task.Run(() => IsBlocked(url));
        }

        //back but
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
            Log("Back button clicked");
        }

        //home
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://www.google.com");
            Log("Home button clicked");
        }


        //forward
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
            Log("Forward button clicked");
        }

        private void toolStripTextBox1_Click(object sender,EventArgs e)
        {

        }


        //go
        private async void toolStripButton4_Click(object sender, EventArgs e)
        {
            string url = toolStripTextBox1.Text;
            if (await IsBlockedAsync(url))
            {
                MessageBox.Show("This URL is blocked.");
                Log("Blocked URL: " + url);
            }
            else
            {
                webBrowser1.Navigate(url);
                Log("Navigated to: " + url);
            }
        }

        // Enter in the address box works like Go
        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStripButton4_Click(sender, e); // same as clicking Go
                e.SuppressKeyPress = true; // no "ding" sound
            }
        }

        // runs before every page loads (also clicked links), must cancel right away so it stays sync
        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string url = e.Url.ToString();
            if (IsBlocked(url))
            {
                e.Cancel = true; // stop the page from loading
                MessageBox.Show("This URL is blocked.");
                Log("Blocked URL: " + url);
            }
        }

        // after a page opens, show its address in the textbox
        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            toolStripTextBox1.Text = e.Url.ToString();
        }

        // add a new keyword
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            string word = toolStripTextBox2.Text.Trim().ToLower();
            if (word == "") return; // nothing typed

            if (keywords.Any(k => k == word)) // LINQ duplicate check
            {
                MessageBox.Show("This keyword already exists.");
                return;
            }

            keywords.Add(word); // used by the block check
            toolStripComboBox1.Items.Add(word); // shown in the ComboBox
            toolStripTextBox2.Clear();
            Log("Keyword added: " + word);
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
