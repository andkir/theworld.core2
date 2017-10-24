using System;
using System.Diagnostics;

namespace theworldcore.Services
{
    public class DebugMessageSender : IMessageSender
    {
        public void SendMessage(string from, string to, string message)
        {
            Debug.WriteLine($"Sending message from {from} to {to}.{Environment.NewLine} {message}");
        }
    }
}