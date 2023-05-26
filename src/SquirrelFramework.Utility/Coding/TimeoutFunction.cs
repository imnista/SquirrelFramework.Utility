namespace SquirrelFramework.Utility.Coding
{
    public static class TimeoutFunction
    {
        /// <summary>
        /// Execute a method with timeout check
        /// </summary>
        /// <typeparam name="T">Target method parameter type</typeparam>
        /// <typeparam name="TR">The result type of execution</typeparam>
        /// <param name="timeoutMethod">Target method</param>
        /// <param name="param">Target method parameter</param>
        /// <param name="result">The result of execution</param>
        /// <param name="timeout">Set timeout length</param>
        /// <returns>Is timeout</returns>
        public static Boolean Execute<T, TR>(
            TimeOutDelegate<T, TR> timeoutMethod, T param, out TR? result, TimeSpan timeout)
        {
            var asyncResult = timeoutMethod.BeginInvoke(param, null, null);
            if (!asyncResult.AsyncWaitHandle.WaitOne(timeout, false))
            {
                result = default;
                return true;
            }
            result = timeoutMethod.EndInvoke(asyncResult);
            return false;
        }
    }

    public delegate TR TimeOutDelegate<in T, out TR>(T param);
}