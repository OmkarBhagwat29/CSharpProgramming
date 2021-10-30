using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAutomation
{
    public class HelloWorldApp : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
           
            return Result.Succeeded;

        }

        public Result OnStartup(UIControlledApplication application)
        {
            TaskDialog.Show("My Dialog Title", "Hello World from App!");
            return Result.Succeeded;
        }
    }
}
