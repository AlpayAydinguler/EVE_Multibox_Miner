using EVE_Multibox_Miner.Models;
using EVE_Multibox_Miner.Services;
using EVE_Multibox_Miner.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace EVE_Multibox_Miner
{
    public partial class MainForm : Form
    {
        private ClientManagerService clientManager;
        private List<ClientProfileControl> clientControls = new List<ClientProfileControl>();

        public MainForm()
        {
            InitializeComponent();
            clientManager = ClientManagerService.Instance;
            clientManager.StateChanged += ClientManager_StateChanged;
            clientManager.AllClientsDocked += ClientManager_AllClientsDocked;
        }

        private void ClientManager_StateChanged(string clientName, MiningState state)
        {
            Invoke((Action)(() =>
            {
                var control = clientControls.Find(c => c.Profile.Name == clientName);
                if (control != null)
                {
                    control.SetState(state);
                }

                txtLog.AppendText($"{DateTime.Now:T} - {clientName}: {state}{Environment.NewLine}");
            }));
        }

        private void ClientManager_AllClientsDocked()
        {
            Invoke((Action)(() =>
            {
                txtLog.AppendText($"All clients docked - shutting down{Environment.NewLine}");

                // Shutdown logic
                if (IsUserAdministrator())
                {
                    Process.Start("shutdown", "/s /t 0");
                }
                else
                {
                    MessageBox.Show("Program needs administrator rights to shut down computer");
                }
            }));
        }

        private bool IsUserAdministrator()
        {
            var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var principal = new System.Security.Principal.WindowsPrincipal(identity);
            return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }

        private void btnAddClient_Click(object sender, EventArgs e)
        {
            var profile = new ClientProfile
            {
                Name = $"Client {clientControls.Count + 1}",
                IsActive = true
            };

            var control = new ClientProfileControl(profile);
            flowLayoutPanel.Controls.Add(control);
            clientControls.Add(control);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
            btnStart.Enabled = false;
            btnStop.Enabled = true;

            foreach (var control in clientControls)
            {
                clientManager.AddClient(control.Profile);
            }

            clientManager.Start();
            txtLog.AppendText($"Mining started with {clientControls.Count} clients{Environment.NewLine}");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            clientManager.Stop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            txtLog.AppendText($"Mining stopped{Environment.NewLine}");
        }
    }
}