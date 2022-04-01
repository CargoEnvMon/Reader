using System;

namespace CargoEnvMon.Reader.Infrastructure
{
    public static class ExceptionsHandler
    {
        private static Action<string> callback;

        public static void OnHandle(Action<string> callback)
        {
            ExceptionsHandler.callback = callback;
        }
        
        public static void Handle(Exception e)
        {
            callback(e.Message);
        }
    }
}