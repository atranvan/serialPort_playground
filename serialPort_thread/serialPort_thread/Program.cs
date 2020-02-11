using System;
using System.IO.Ports;
using System.Threading;
using System.Collections.Concurrent;
using System.Linq;

public class PortChat
{
    static bool _continue;
    static SerialPort _serialPort;
    static BlockingCollection<string> outputQueue = new BlockingCollection<string>();
    public static void Main()
    {
        Thread readThread = new Thread(Read);

        // Create a new SerialPort object with default settings.
        _serialPort = new SerialPort("COM25", 115200, Parity.None, 8, StopBits.One);

        // Exit on Ctrl + C
        Console.CancelKeyPress += delegate {
            _continue = false;// call methods to clean up
        };


        _serialPort.Open();
        _continue = true;
        readThread.Start();



        while (_continue)
        {
            String[] arr = outputQueue.ToArray();

            if (arr != null)
            {
                Console.WriteLine(arr.LastOrDefault());
                
            }


        }

        readThread.Join();
        _serialPort.Close();
    }

    public static void Read()
    {
        while (_continue)
        {
            try
            {
                //string message = _serialPort.ReadLine();
                outputQueue.Add(_serialPort.ReadLine());
                //Console.WriteLine(message);
            }
            catch (TimeoutException) { }
        }
    }
}