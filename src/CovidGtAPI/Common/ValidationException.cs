using System;

namespace CovidGtAPI.Common
{
    public class ValidationException : Exception
    {
        public ValidationException(string message)
        :base(message)
        {}
    }
}