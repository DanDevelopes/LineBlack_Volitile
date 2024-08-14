using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public static class ScanTasks
{
    private static object taskLock = new object();
    private static List<Identifier> identifiers = new List<Identifier>();
    public static int GetAvailableTask(){
        lock (taskLock)
        {
            int assignedNumber = 0;
            if (identifiers.Count > 0)
            {
                foreach(var identifier in identifiers)
                {
                    if(!identifier.isUsed)
                    {
                        assignedNumber = identifier.id;
                        break;
                    }
                }
            }
            else 
            {
                Identifier identifier = new Identifier();
                identifier.isUsed = true;
                identifier.id = 1;
                assignedNumber = identifier.id;
                identifiers.Add(identifier);
            }
            if( assignedNumber == 0)
            {
                Identifier identifier = new Identifier();
                identifier.isUsed = true;
                identifier.id = identifiers.Max(x => x.id) + 1;
                identifiers.Add(identifier);
                assignedNumber = identifier.id;
            }
            return assignedNumber;
        }
    }
    public static void SetTaskState(int id, bool taskState)
    {
        lock (taskLock)
        {
            var identifier = identifiers.FirstOrDefault(x => x.id == id);
            identifier.taskState = taskState;
        }
    }
    public static bool GetTaskState(int id)
    {
        lock (taskLock)
        {
            var identifier = identifiers.FirstOrDefault(x => x.id == id);
            return identifier.taskState;
        }
    } 
    public static bool IsTaskComplete(int id)
    {
        lock (taskLock)
        {
            try
            {
                var identifier = identifiers.FirstOrDefault(x => x.id == id);
                if(identifier == null)
                {
                    throw new NullReferenceException("Scan task does not exist. make sure scan task is created by using 'int taskId = ScanTasks()' and before calling 'bool taskComplete = taskCompletTaskComplete(taskId)' ");
                }

                if(identifier.taskComplete)
                {
                    identifier.isUsed = false;
                    return identifier.taskComplete;
                }                
                else
                    return identifier.taskComplete;
            }
            catch (Exception ex)
            {
                GD.Print(ex);
                return false;
            }
        }
    }

    internal static void SetTaskComplete(int id)
    {
        lock (taskLock)
        {
            identifiers.FirstOrDefault(x => x.id == id).taskComplete = true;
        }
    }
    public static void SetTaskFinished(int id)
    {
        lock (taskLock)
        {
            identifiers.FirstOrDefault(x => x.id == id).isUsed = false;
        }
    }
}
public class Identifier
{
    public int id;
    public bool taskComplete = false;
    public bool isUsed = false;
    public bool taskState;
}
