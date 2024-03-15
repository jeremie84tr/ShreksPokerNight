using System.Text;
using Python.Runtime;

namespace Pyai
{    public class Pyai
    {
        public static void callMJ(string user_message)
        {
            Runtime.PythonDLL = @"C:\\Python312\\python312.dll";
            string pathToVirtualEnv = @"C:\\Python312";

            Environment.SetEnvironmentVariable("PATH", pathToVirtualEnv, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONHOME", pathToVirtualEnv, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONPATH", $"{pathToVirtualEnv}\\Lib\\site-packages;{pathToVirtualEnv}\\Lib", EnvironmentVariableTarget.Process);
                    
                    
            PythonEngine.PythonHome = pathToVirtualEnv;
            PythonEngine.PythonPath = Environment.GetEnvironmentVariable("PYTHONPATH", EnvironmentVariableTarget.Process);
            PythonEngine.Initialize();
            using (Py.GIL())
            {
                dynamic requests = Py.Import("jtiac");
                dynamic url = "http://10.120.29.149:5000/v1/chat/completions";
                Console.WriteLine($"{url}\n");
            }
        }
    }
}