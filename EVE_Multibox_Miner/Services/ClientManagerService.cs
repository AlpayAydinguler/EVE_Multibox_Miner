using EVE_Multibox_Miner.Models;
using EVE_Multibox_Miner.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace EVE_Multibox_Miner.Services
{
    public class ClientManagerService
    {
        private static ClientManagerService _instance;
        public static ClientManagerService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ClientManagerService();
                }
                return _instance;
            }
        }

        private List<ClientProfile> clients = new List<ClientProfile>();
        private Dictionary<string, MiningState> clientStates = new Dictionary<string, MiningState>();
        private Dictionary<string, DateTime> lastActionTimes = new Dictionary<string, DateTime>();
        private System.Windows.Forms.Timer mainTimer = new System.Windows.Forms.Timer();

        public event Action<string, MiningState> StateChanged;
        public event Action AllClientsDocked;

        public ClientManagerService()
        {
            mainTimer.Interval = 1000;
            mainTimer.Tick += MainTimer_Tick;
        }

        public void Start()
        {
            mainTimer.Start();
        }

        public void Stop()
        {
            mainTimer.Stop();
        }

        public void AddClient(ClientProfile profile)
        {
            clients.Add(profile);
            clientStates[profile.Name] = MiningState.Mining;
            lastActionTimes[profile.Name] = DateTime.Now;
        }

        public void RemoveClient(string name)
        {
            var client = clients.FirstOrDefault(c => c.Name == name);
            if (client != null)
            {
                clients.Remove(client);
                clientStates.Remove(name);
                lastActionTimes.Remove(name);
            }
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            foreach (var client in clients.Where(c => c.IsActive))
            {
                var timeSinceLast = DateTime.Now - lastActionTimes[client.Name];

                if (timeSinceLast.TotalSeconds >= client.LoopInterval)
                {
                    ProcessClient(client);
                }
            }

            CheckDockingStatus();
        }

        private void ProcessClient(ClientProfile client)
        {
            try
            {
                WindowHelper.ActivateWindow(client.WindowArea);

                switch (clientStates[client.Name])
                {
                    case MiningState.Mining:
                        MiningService.PerformMiningCycle(client);
                        break;
                    case MiningState.Compressing:
                        MiningService.PerformCompression(client);
                        break;
                    case MiningState.Docking:
                        DockingService.PerformDocking(client);
                        break;
                }

                lastActionTimes[client.Name] = DateTime.Now;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error processing client {client.Name}: {ex.Message}");
                SetClientState(client.Name, MiningState.Error);
            }
        }

        private void CheckDockingStatus()
        {
            if (clients.All(c => clientStates[c.Name] == MiningState.Docked))
            {
                AllClientsDocked?.Invoke();
            }
        }

        public void SetClientState(string clientName, MiningState state)
        {
            if (clientStates.ContainsKey(clientName))
            {
                clientStates[clientName] = state;
                StateChanged?.Invoke(clientName, state);
            }
        }
    }
}