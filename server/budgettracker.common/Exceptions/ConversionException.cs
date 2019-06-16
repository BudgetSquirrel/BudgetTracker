using System;

namespace budgettracker.common.Exceptions
{
    public class ConversionException : Exception
    {
        public ConversionException(Type convertFrom, Type convertTo, string reason)
            : base($"Could not convert {convertFrom.ToString()} to {convertTo.ToString()}. Reason: {reason}")
        {
        }
    }
}
