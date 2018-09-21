using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using System.IO;

namespace lab_1
{
    public delegate void TaskDelegate(string file1, string file2);

    public delegate void loggerDelegate(string logMessage);

    public class PoolQueue
    {
        public BlockingCollection<object[]> FuncQueue =
        new BlockingCollection<object[]>(new ConcurrentQueue<object[]>());

        private int queueCount;

        public PoolQueue(int queueCount)
        {
            this.queueCount = queueCount;
            for (int i = 0; i < queueCount; i++)
            {
                var thread = new Thread(threadWork) { IsBackground = true }; ;
                thread.Start();
            }
        }

        public void threadWork()
        {
            while (true)
            {
                object[] task = FuncQueue.Take();
                try
                {

                    ((DirectoryWork)task[0]).copyFile((string)task[1], (string)task[2]);
                }
                catch (ThreadStateException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ThreadAbortException ex)
                {
                    Thread.ResetAbort();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void EnqueueTask(object directoryWork,
            string file1, string file2)
        {
            this.FuncQueue.Add(new object[] { directoryWork, file1, file2 });
        }
    }


    public class DirectoryWork
    {
        private string dirName1;
        private string dirName2;
        private loggerDelegate logger;
        public static int filesCopyNum, dirsCreateNum;
        private PoolQueue poolQueue;

        public DirectoryWork(string dirName1, string dirName2,
            PoolQueue poolQueue, loggerDelegate logger)
        {
            this.poolQueue = poolQueue;
            this.dirName1 = dirName1;
            this.dirName2 = dirName2;
            this.logger = logger;
        }

        public void copyDir(string dirName1, string dirName2)
        {
            filesCopyNum = 0;
            dirsCreateNum = 0;
            directoryRecursion(dirName1, dirName2);
            while (this.poolQueue.FuncQueue.Count != 0) { Thread.Sleep(50); };
        }

        private void directoryRecursion(string dirName1, string dirName2)
        {
            string[] arr;
            if (!Directory.Exists(dirName1))
            {
                return;    
            }
            if (!Directory.Exists(dirName2))
            {
                System.IO.Directory.CreateDirectory(dirName2);
            }
            string[] dirs = Directory.GetDirectories(dirName1);
            string[] files = Directory.GetFiles(dirName1);
            foreach (string dir in dirs)
            {
                arr = dir.Split('\\');
                if (!System.IO.Directory.Exists(dirName2 + "\\" + arr[arr.Length - 1]))
                {
                    System.IO.Directory.CreateDirectory(dirName2 + "\\" + arr[arr.Length - 1]);
                    dirsCreateNum++;
                }
                directoryRecursion(dir, dirName2 + "\\" + arr[arr.Length - 1]);
            }
            copyFile(dirName1, dirName2, files);
            
        }


        private void copyFile(string dirName1, string dirName2, string[] files)
        {
            string[] arr;
            foreach (string file in files)
            {
                arr = file.Split('\\');
                poolQueue.EnqueueTask(this, file, dirName2 + "\\" + arr[arr.Length - 1]);
            }
        }

        public void copyFile(string sourceFieName, string destFileName)
        {
            System.IO.File.Copy(sourceFieName, destFileName);
            DirectoryWork.filesCopyNum++;
        }
    }
    

    static class Program
    {
        public static void logger(string logMessage)
        {

        }

        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("not enough data!");
                return;
            }

            string dir1 = args[0];
            string dir2 = args[1];
      
            PoolQueue poolQueue = new PoolQueue(3);
            DirectoryWork directoryWork = new DirectoryWork(dir1, dir2, poolQueue, null);
            directoryWork.copyDir(dir1, dir2);
            Console.WriteLine(DirectoryWork.filesCopyNum);
            Console.WriteLine(DirectoryWork.dirsCreateNum);
            Console.ReadLine();
        }
    }
}
