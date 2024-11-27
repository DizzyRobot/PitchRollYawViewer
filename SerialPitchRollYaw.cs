using System;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Threading;
using System.Threading.Tasks;

namespace PitchRollYawViewer
{
    class PRY
    {
        public float Pitch { get; set; }
        public float Roll { get; set; }
        public float Yaw { get; set; }
    }

    internal class SerialPitchRollYaw : IDisposable
    {
        private ManagementEventWatcher _watcher;
        private SerialPort _port;
        private string _buffer = "";

        //

        private string[] _availablePorts;


        public event EventHandler<PRY> NewData;
        public event EventHandler<string[]> NewPorts;

        public SerialPitchRollYaw()
        {
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();

            WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent");

            _watcher = new ManagementEventWatcher(query);
            // do it async so it is performed in the UI thread
            _watcher.EventArrived += (s, e) => Task.Factory.StartNew(CheckForNewPorts, CancellationToken.None, TaskCreationOptions.None, scheduler);
            _watcher.Start();
        }

        public void Connect(string port)
        {
            Disconnect();

            _buffer = "";

            _port = new SerialPort(port, 115200, Parity.None, 8, StopBits.One);
            _port.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
            _port.Open();
        }
        public void CheckForNewPorts()
        {
            var ports = SerialPort.GetPortNames().OrderBy(s => s).ToArray();

            if (_availablePorts == null ||
                !Enumerable.SequenceEqual(ports, _availablePorts))
            {
                _availablePorts = ports;
                NewPorts?.Invoke(null, _availablePorts);
            }
        }

        //

        private void DataReceived(object s, SerialDataReceivedEventArgs e)
        {
            _buffer += _port.ReadExisting();

            if (_buffer.Contains('\n'))
            {
                var a = _buffer.Split('\n');
                if (a.Length > 1)
                {
                    _buffer = a.Last();
                    ParseCsvLine(a.First());
                }
            }
        }
        private void ParseCsvLine(string line)
        {
            var a = line.Trim().Split(',');

            if (a.Length == 3 &&
                float.TryParse(a[0], out var p)&&
                float.TryParse(a[1], out var r) &&
                float.TryParse(a[2], out var y))
            {
                var pry = new PRY { Pitch = p, Roll = r, Yaw = y };
                NewData?.Invoke(null, pry);
            }
        }

        private void Disconnect() 
        {
            if (_port == null) return;
            _port.Dispose();
        }
        public void Dispose()
        {
            Disconnect();
            _watcher.Dispose();
        }
    }
}
