using System;
using System.Reflection;
using System.Collections.Generic;

namespace BudgetTracker.BudgetSquirrel.Application
{
    public class Utils
    {
        public static T GetObject<T>(Dictionary<string,object> dict)
        {
            Type type = typeof(T);
            var obj = Activator.CreateInstance(type);

            foreach (var kv in dict)
            {
                type.GetProperty(kv.Key).SetValue(obj, kv.Value);
            }
            return (T)obj;
        }
    }
}