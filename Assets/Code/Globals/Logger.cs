using System;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


public static class Logger
{
    private static string messageLogPath;
    private static string exceptionLogPath;
    private static int exceptionCounter;
    private static Exception lastException;
    private static object logLock = new object();
    static Logger()
    {
        lock (logLock)
        {
            string logPath = $"{Directory.GetCurrentDirectory()}\\Loggers";
            if(!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            messageLogPath = $"{logPath}\\MessageLog_{DateTime.Now:yyyy_MM_dd}.txt";
            exceptionLogPath = $"{logPath}\\ExceptionLog_{DateTime.Now:yyyy_MM_dd}.txt";
        }
        Task.Run(() => LogMessage("Logging Start"));
    }
    public static async Task LogException(Exception ex)
    {
        await Task.Run(() => 
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"[Exception] Time: {DateTime.Now.ToShortTimeString()} : {ex}");
            string logEntry = sb.ToString();
            lock(logLock)
            {
                
                if(lastException != null && lastException.HResult != ex.HResult)
                {
                    exceptionCounter = 0;
                }
                if (exceptionCounter >= 5)
                {
                    if (exceptionCounter == 5)
                        logEntry = "Last exception reoccuring at Times: ";
                    else
                        logEntry = $" {DateTime.Now.ToShortTimeString()},";
                    
                 
                }
                exceptionCounter++;
                lastException = ex;
                File.AppendAllText(exceptionLogPath ,logEntry);
            }
        });
    }
    public static async Task LogMessage(string message)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"[Messege] Time: {DateTime.Now.ToShortTimeString()} : {message}");
        string logEntry = sb.ToString();
        await Task.Run(() => 
        {
            lock (logLock)
            {
                
                File.AppendAllText(messageLogPath ,logEntry);
            }
        });
    }
}