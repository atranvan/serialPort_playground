using System;
using System.IO.Ports;
using System.Threading;
using System.Collections.Concurrent;

public class PortChat
{
    static bool _continue;
    static SerialPort _serialPort;
    static BlockingCollection<string> outputQueue = new BlockingCollection<string>();
    public static void Main()
    {
        //string name;
        //string message;
        //StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
        Thread readThread = new Thread(Read);


        // Create a new SerialPort object with default settings.
        _serialPort = new SerialPort("COM25", 115200, Parity.None, 8, StopBits.One);
        Console.CancelKeyPress += delegate {
            _continue = false;// call methods to clean up
        };
        // Set the read/write timeouts
        //_serialPort.ReadTimeout = 500;
        //_serialPort.WriteTimeout = 500;

        _serialPort.Open();
        _continue = true;
        readThread.Start();

        //Console.Write("Name: ");
        //name = Console.ReadLine();

        //Console.WriteLine("Type QUIT to exit");

        while (_continue)
        {
            Console.WriteLine(outputQueue.Take());

        }
        //while (_continue)
        //{
        //    message = Console.ReadLine();

        //    if (stringComparer.Equals("quit", message))
        //    {
        //        _continue = false;
        //    }
        //    else
        //    {
        //        //_serialPort.WriteLine(
        //        //    String.Format("<{0}>: {1}", name, message));
        //    }
        //}

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