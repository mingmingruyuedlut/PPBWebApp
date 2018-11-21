using IronPython.Hosting;
using IronPython.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.PythonService
{
    public class LogixService
    {
        /// <summary>
        /// Get python file object without connection
        /// </summary>
        /// <returns></returns>
        public static dynamic GetPythonFileObj()
        {
            var fath = AppContext.BaseDirectory + @"Services\LogixService.py";

            var pyruntime = Python.CreateRuntime();
            var pyengine = pyruntime.GetEngine("Python");
            var paths = pyengine.GetSearchPaths();
            paths.Add(AppContext.BaseDirectory + @"Services");
            paths.Add(AppContext.BaseDirectory + @"Services\Lib");
            pyengine.SetSearchPaths(paths);

            dynamic fileObj = pyruntime.UseFile(fath);
            return fileObj;
        }

        /// <summary>
        /// Get python file object with connection
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="slot"></param>
        /// <returns></returns>
        public static dynamic GetPythonFileObj(string ipAddress, int slot)
        {
            var fileObj = GetPythonFileObj();
            GenerateConnectionInPython(ref fileObj, ipAddress, slot);
            return fileObj;
        }

        /// <summary>
        /// Generate the connection in python through plc address and slot
        /// </summary>
        /// <param name="fileObj"></param>
        /// <param name="ipAddress"></param>
        /// <param name="slot"></param>
        public static void GenerateConnectionInPython(ref dynamic fileObj, string ipAddress, int slot)
        {
            fileObj.set_comn_tag(ipAddress, slot.ToString());
        }


        public static void DownloadTag(ref dynamic fileObj, string tagName, string tagValue)
        {
            fileObj.write_tag(tagName, tagValue);
        }

        public static string UploadTag(ref dynamic fileObj, string tagName)
        {
            var tagValue = fileObj.ex_read(tagName);
            return tagValue.ToString();
        }

        /// <summary>
        /// Just used to import IronPython.Modules and copy the dll to bin folder when build
        /// </summary>
        private void ImportIronPythonModules()
        {
            PythonMath.ceil(0);
        }
    }
}
