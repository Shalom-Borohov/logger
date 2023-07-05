using System;
using System.IO;
using System.Linq;

namespace Logger.Utils
{
    public class DotEnv
    {
        public void Load(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            File.ReadAllLines(filePath).ToList().ForEach(SetEnvVar);
        }

        private void SetEnvVar(string line)
        {
            var parts = line.Split('=', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 2)
            {
                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }
    }
}
