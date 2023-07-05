using System;

namespace Logger.Utils
{
    public class EnumUtil
    {
        public T ParseEnum<T>(string value) =>
            (T)Enum.Parse(typeof(T), value, true);
    }
}
