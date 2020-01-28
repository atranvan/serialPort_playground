using System;
using System.Text;
using System.IO;
//using System.Collections;
//using System.Collections.Generic;
using System.IO.Ports;
//using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Threading;


namespace test
{
    class Program
    {
        static void Main()
        {
            BackgroundWorker work = new BackgroundWorker();
            work.DoWork += new DoWorkEventHandler(work_DoWork);
            work.ProgressChanged += new ProgressChangedEventHandler(work_ProgressChanged);
            work.WorkerReportsProgress = true;
            work.RunWorkerAsync();
            Console.ReadLine();

            //SerialPort myport = new SerialPort();
            //myport.BaudRate = 115200;
            //myport.PortName = "COM34";//"COM33";
            ////myport.ReadTimeout = 200;
            ////myport.Handshake = Handshake.None;
            //myport.Open();
            //int i = 0;
            //string s;

            //Console.WriteLine("Incoming Data:");
            //while (i < 100000)
            //{
            //    s = myport.ReadExisting();
            //    //s = myport.ReadLine();
            //    Logger.WriteLine(s);
            //    i++;
            //}

            //Console.WriteLine("Done");
            //myport.Close();
            //Logger.SaveLog(true);

        }
        static void work_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine(e.ProgressPercentage);
        }
        static void work_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker work = sender as BackgroundWorker;
            //int grams = -1;
            SerialPort myport = new SerialPort();
            myport.BaudRate = 115200;
            myport.PortName = "COM34";
            myport.Open();
            int i = 0;
            while (i<10)
            {
                string s = myport.ReadExisting();
                Logger.WriteLine(s);
                int value;
                if (int.TryParse(s, out value))
                    {
                    work.ReportProgress(value);
                }
                //if (int.TryParse(s, out value) && value != grams)
                //{
                //    grams = value;
                //    work.ReportProgress(grams);
                //}
                    i++;
            }
            Logger.SaveLog(true);
            myport.Close();

        }

    }
}
public static class Logger
{
    public static StringBuilder LogString = new StringBuilder();
    public static void WriteLine(string str)
    {
        Console.WriteLine(str);
        LogString.Append(str).Append(Environment.NewLine);
    }
    public static void Write(string str)
    {
        Console.Write(str);
        LogString.Append(str);

    }
    public static void SaveLog(bool Append = false, string Path = @"C:\\Users\\tranvaa\\Documents\\Documents\\test_serial\\Log.txt")
    {
        if (LogString != null && LogString.Length > 0)
        {
            if (Append)
            {
                using (StreamWriter file = System.IO.File.AppendText(Path))
                {
                    file.Write(LogString.ToString());
                    file.Close();
                    file.Dispose();
                }
            }
            else
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(Path))
                {
                    file.Write(LogString.ToString());
                    file.Close();
                    file.Dispose();
                }
            }
        }
    }
}

