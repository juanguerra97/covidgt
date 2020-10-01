using System;

namespace CovidGtAPI.Common
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
        :base(message)
        { }
    }
}