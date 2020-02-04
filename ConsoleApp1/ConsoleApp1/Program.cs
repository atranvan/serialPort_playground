//using System;
//using System.Text;
//using System.IO;
////using System.Collections;
////using System.Collections.Generic;
//using System.IO.Ports;
////using System.Text.RegularExpressions;
//using System.ComponentModel;
//using System.Threading;


//namespace test
//{
//    class Program
//    {
//        static void Main()
//        {
//            BackgroundWorker work = new BackgroundWorker();
//            work.DoWork += new DoWorkEventHandler(work_DoWork);
//            work.ProgressChanged += new ProgressChangedEventHandler(work_ProgressChanged);
//            work.WorkerReportsProgress = true;
//            work.RunWorkerAsync();
//            Console.ReadLine();

//            //SerialPort myport = new SerialPort();
//            //myport.BaudRate = 115200;
//            //myport.PortName = "COM34";//"COM33";
//            ////myport.ReadTimeout = 200;
//            ////myport.Handshake = Handshake.None;
//            //myport.Open();
//            //int i = 0;
//            //string s;

//            //Console.WriteLine("Incoming Data:");
//            //while (i < 100000)
//            //{
//            //    s = myport.ReadExisting();
//            //    //s = myport.ReadLine();
//            //    Logger.WriteLine(s);
//            //    i++;
//            //}

//            //Console.WriteLine("Done");
//            //myport.Close();
//            //Logger.SaveLog(true);

//        }
//        static void work_ProgressChanged(object sender, ProgressChangedEventArgs e)
//        {
//            Console.WriteLine(e.ProgressPercentage);
//        }
//        static void work_DoWork(object sender, DoWorkEventArgs e)
//        {
//            BackgroundWorker work = sender as BackgroundWorker;
//            //int grams = -1;
//            SerialPort myport = new SerialPort();
//            myport.BaudRate = 115200;
//            myport.PortName = "COM25";//"COM34";
//            myport.Open();
//            int i = 0;
//            while (i<10)
//            {
//                string s = myport.ReadExisting();
//                Logger.WriteLine(s);
//                int value;
//                if (int.TryParse(s, out value))
//                    {
//                    work.ReportProgress(value);
//                }
//                //if (int.TryParse(s, out value) && value != grams)
//                //{
//                //    grams = value;
//                //    work.ReportProgress(grams);
//                //}
//                    i++;
//            }
//            Logger.SaveLog(true);
//            myport.Close();

//        }

//    }
//}
//public static class Logger
//{
//    public static StringBuilder LogString = new StringBuilder();
//    public static void WriteLine(string str)
//    {
//        Console.WriteLine(str);
//        LogString.Append(str).Append(Environment.NewLine);
//    }
//    public static void Write(string str)
//    {
//        Console.Write(str);
//        LogString.Append(str);

//    }
//    public static void SaveLog(bool Append = false, string Path = @"C:\\Users\\tranvaa\\Documents\\Documents\\test_serial\\Log.txt")
//    {
//        if (LogString != null && LogString.Length > 0)
//        {
//            if (Append)
//            {
//                using (StreamWriter file = System.IO.File.AppendText(Path))
//                {
//                    file.Write(LogString.ToString());
//                    file.Close();
//                    file.Dispose();
//                }
//            }
//            else
//            {
//                using (System.IO.StreamWriter file = new System.IO.StreamWriter(Path))
//                {
//                    file.Write(LogString.ToString());
//                    file.Close();
//                    file.Dispose();
//                }
//            }
//        }
//    }
//}

using System;
using System.IO.Ports;
//using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;


class Program
{

    static void Main()//(string[] args)
    {
        //Thread th = new Thread(ThreadProc);
        //th.IsBackground = true;
        //th.Start();
        //ThreadedQueue<string> _queue = new ThreadedQueue<string>();

        SerialPort sp = new SerialPort("COM25", 115200, Parity.None, 8, StopBits.One);
        sp.DataReceived += Port_OnReceiveDataz; // Add DataReceived Event Handler

        sp.Open();
        sp.WriteLine("$"); //Command to start Data Stream

        

        Console.ReadLine(); // or timer here instead


        sp.WriteLine("!"); //Stop Data Stream Command
        sp.Close();
    }

    //private static void ThreadProc()
    //{
    //    while (true)
    //    {
    //        string inData = _queue.Dequeue();
    //        Console.WriteLine(inData);
    //        //this.Invoke(new SetGridDeleg(DoUpdate), new object[] { inData });
    //    }
    //}


    //private static
    private static void Port_OnReceiveDataz(object sender, SerialDataReceivedEventArgs e)
    {
        SerialPort spL = (SerialPort)sender;

        //byte[] buf = new byte[spL.BytesToRead];
        //Console.WriteLine("DATA RECEIVED!");
        //spL.Read(buf, 0, buf.Length);
        //foreach (Byte b in buf)
        //{
        //    Console.Write(b.ToString() + " ");
        //}
        //Debug.WriteLine(spL.ReadExisting());
        //_queue.Enqueue(spL.ReadExisting());
        Console.WriteLine(spL.ReadExisting());
    }
}

//public class ThreadedQueue<T>
//{
//    private readonly Queue<T> _queue = new Queue<T>();
//    private readonly ManualResetEvent _notEmptyEvt = new ManualResetEvent(false);

//    public WaitHandle WaitHandle { get { return _notEmptyEvt; } }

//    public void Enqueue(T obj)
//    {
//        lock (_queue)
//        {
//            _queue.Enqueue(obj);
//            _notEmptyEvt.Set();
//        }
//    }

//    public T Dequeue()
//    {
//        _notEmptyEvt.WaitOne(Timeout.Infinite);
//        lock (_queue)
//        {
//            var result = _queue.Dequeue();
//            if (_queue.Count == 0)
//                _notEmptyEvt.Reset();
//            return result;
//        }
//    }
//}

