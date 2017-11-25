using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    /// <summary>
    /// Generic wrapper above UnityEngine.Debug logging methods. 
    /// </summary>
    public class DebugLog
    {

        // ReSharper disable once InconsistentNaming due to private modifier
        private static bool isLoggingEnabled
        {
            get { return Debug.isDebugBuild; }
        }
        
        public static void Log(object message)
        {
            if (isLoggingEnabled) Debug.Log(message);
        }

        public static void Log(object message, Object context)
        {
            if (isLoggingEnabled) Debug.Log(message, context);
        }

        public static void LogFormat(string format, params object[] args)
        {
            if (isLoggingEnabled) Debug.LogFormat(format, args);
        }
        
        public static void LogFormat(Object context, string format, params object[] args)
        {
            if (isLoggingEnabled) Debug.LogFormat(context, format, args);    
        }
        
        public static void LogAssertion(object message)
        {
            if (isLoggingEnabled) Debug.LogAssertion(message);
        }

        public static void LogAssertion(object message, Object context)
        {
            if (isLoggingEnabled) Debug.LogAssertion(message, context);
        }
        
        public static void LogAssertionFormat(string format, params object[] args)
        {
            if (isLoggingEnabled) Debug.LogAssertionFormat(format, args);
        }
        
        public static void LogAssertionFormat(Object context, string format, params object[] args)
        {
            if (isLoggingEnabled) Debug.LogAssertionFormat(context, format, args);
        }

        public static void LogError(object message)
        {
            if (isLoggingEnabled) Debug.LogError(message);
        }

        public static void LogError(object message, Object context)
        {
            if (isLoggingEnabled) Debug.LogError(message, context);
        }
        
        public static void LogErrorFormat(string format, params object[] args)
        {
            if (isLoggingEnabled) Debug.LogErrorFormat(format, args);
        }
        
        public static void LogErrorFormat(Object context, string format, params object[] args)
        {
            if (isLoggingEnabled) Debug.LogErrorFormat(context, format, args);
        }

        public static void LogException(Exception exception)
        {
            if (isLoggingEnabled) Debug.LogException(exception);
        }
        
        public static void LogException(Exception exception, Object context)
        {
            if (isLoggingEnabled) Debug.LogException(exception, context);
        }
        
        public static void LogWarning(object message)
        {
            if (isLoggingEnabled) Debug.LogWarning(message);
        }
        
        public static void LogWarning(object message, Object context)
        {
            if (isLoggingEnabled) Debug.LogWarning(message, context);
        }
        
        public static void LogWarningFormat(string format, params object[] args)
        {
            if (isLoggingEnabled) Debug.LogWarningFormat(format, args);
        }
        
        public static void LogWarningFormat(Object context, string format, params object[] args)
        {
            if (isLoggingEnabled) Debug.LogWarningFormat(context, format, args);
        }
        
        
    }
}