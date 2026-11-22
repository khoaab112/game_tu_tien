using System;
using FishNet.Connection;
using FishNet.Managing;
using FishNet.Object;
using FishNet.Transporting;
using UnityEngine;

namespace TuTien.Prototype
{
    public sealed class PrototypeSession : MonoBehaviour
    {
        public NetworkManager Manager;
        public NetworkObject PlayerPrefab;
        public NetworkObject DummyPrefab;
        private string _localAddress;
        private int _windowWidth;
        private int _windowHeight;

        public static bool HasArgument(string value) =>
            Array.IndexOf(Environment.GetCommandLineArgs(), value) >= 0;

        private void Start()
        {
            var arguments = Environment.GetCommandLineArgs();
            var address = "127.0.0.1";
            ushort port = 7770;
            var addressIndex = Array.IndexOf(arguments, "--address");
            var portIndex = Array.IndexOf(arguments, "--port");
            if (addressIndex >= 0)
            {
                if (addressIndex + 1 >= arguments.Length ||
                    Uri.CheckHostName(arguments[addressIndex + 1]) == UriHostNameType.Unknown)
                {
                    Debug.LogError("Expected a hostname or IP address after --address.");
                    Application.Quit(2);
                    return;
                }
                address = arguments[addressIndex + 1];
            }
            if (portIndex >= 0 && (portIndex + 1 >= arguments.Length ||
                !ushort.TryParse(arguments[portIndex + 1], out port) || port == 0))
            {
                Debug.LogError("Expected a UDP port from 1 to 65535 after --port.");
                Application.Quit(2);
                return;
            }

            Application.runInBackground = true;
            if (HasArgument("--qa-network"))
            {
                // QA fixture: apply to outgoing traffic on each endpoint.
                var simulator = Manager.TransportManager.LatencySimulator;
                simulator.SetLatency(75);
                simulator.SetPacketLoss(0.05);
                simulator.SetEnabled(true);
                Debug.Log("PROTOTYPE_QA_NETWORK delay_ms=75 unreliable_loss_percent=5");
            }
            if (HasArgument("--smoke"))
                Manager.TimeManager.OnRoundTripTimeUpdated += ReportRoundTripTime;
            Manager.SceneManager.OnClientLoadedStartScenes += SpawnPlayer;
            Manager.ServerManager.OnServerConnectionState += SpawnTrainingDummy;
            Manager.TransportManager.Transport.SetPort(port);
            if (HasArgument("--server"))
                Manager.ServerManager.StartConnection();
            else if (Application.isEditor)
            {
                _localAddress = address;
                Manager.ServerManager.OnServerConnectionState += ConnectLocalClient;
                if (!Manager.ServerManager.StartConnection())
                    Debug.LogError("Could not start the QA host. Check whether UDP port 7770 is in use.");
            }
            else if (Application.isBatchMode)
                Manager.ServerManager.StartConnection();
            else
                Manager.ClientManager.StartConnection(address);
        }

        private void ConnectLocalClient(ServerConnectionStateArgs state)
        {
            if (state.ConnectionState != LocalConnectionState.Started) return;
            Manager.ServerManager.OnServerConnectionState -= ConnectLocalClient;
            Manager.ClientManager.StartConnection(_localAddress);
        }

        private void SpawnTrainingDummy(ServerConnectionStateArgs state)
        {
            if (state.ConnectionState != LocalConnectionState.Started || DummyPrefab == null) return;
            var dummy = Instantiate(DummyPrefab, PrototypeVillage.ImageToWorld(1415, 735, 1), Quaternion.identity);
            Manager.ServerManager.Spawn(dummy);
        }

        private void SpawnPlayer(NetworkConnection connection, bool asServer)
        {
            if (!asServer) return;
            var player = Instantiate(PlayerPrefab, PrototypeVillage.PlayerSpawn(connection.ClientId), Quaternion.identity);
            Manager.ServerManager.Spawn(player, connection);
            Manager.SceneManager.AddOwnerToDefaultScene(player);
            Debug.Log($"PROTOTYPE_SPAWN client={connection.ClientId} object={player.ObjectId}");
        }

        private void OnDestroy()
        {
            if (Manager != null && Manager.SceneManager != null)
                Manager.SceneManager.OnClientLoadedStartScenes -= SpawnPlayer;
            if (Manager != null && Manager.TimeManager != null)
                Manager.TimeManager.OnRoundTripTimeUpdated -= ReportRoundTripTime;
            if (Manager != null && Manager.ServerManager != null)
            {
                Manager.ServerManager.OnServerConnectionState -= ConnectLocalClient;
                Manager.ServerManager.OnServerConnectionState -= SpawnTrainingDummy;
            }
        }

        private void ReportRoundTripTime(long milliseconds) =>
            Debug.Log($"PROTOTYPE_QA_RTT ms={milliseconds} tick_hz={Manager.TimeManager.TickRate}");

        private void OnGUI()
        {
            GUI.Label(new Rect(15, 15, 650, 30),
                $"Prototype QA | WASD: move | Run after 2s | Shift: run now | connected={Manager.IsClientStarted}" +
                (Application.isEditor ? "" : " | F11: fullscreen"));
        }

        private void Update()
        {
            if (Application.isEditor || Application.isBatchMode || !Input.GetKeyDown(KeyCode.F11)) return;

            if (Screen.fullScreen)
            {
                var width = _windowWidth > 0 ? _windowWidth : 960;
                var height = _windowHeight > 0 ? _windowHeight : 640;
                Screen.SetResolution(width, height, FullScreenMode.Windowed);
                return;
            }

            _windowWidth = Screen.width;
            _windowHeight = Screen.height;
            var desktop = Screen.currentResolution;
            Screen.SetResolution(desktop.width, desktop.height, FullScreenMode.FullScreenWindow);
        }
    }
}
