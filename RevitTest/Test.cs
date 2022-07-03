using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitTest
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    [Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.NoCommandData)]
    public class Test : IExternalCommand
    {
        
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Application app = commandData.Application.Application;
            Document activeDoc = commandData.Application.ActiveUIDocument.Document;

            #region Task Dialog Sample
            // Study how to create a revit style dialog using task dialog API by following
            // code snippet.  

            // Creates a Revit task dialog to communicate information to the interactive user.
            TaskDialog mainDialog = new TaskDialog("Hello, Revit!");
            mainDialog.MainInstruction = "Hello, Revit!";
            mainDialog.MainContent =
                "This sample shows how a basic ExternalCommand can be added to the Revit user interface."
                + " It uses a Revit task dialog to communicate information to the interactive user.\n"
                + "The command links below open additional task dialogs with more information.";

            // Add commmandLink to task dialog
            mainDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink1,
                                      "View information about the Revit installation");
            mainDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink2,
                                      "View information about the active document");

            // Set common buttons and default button. If no CommonButton or CommandLink is added,
            // task dialog will show a Close button by default.
            mainDialog.CommonButtons = TaskDialogCommonButtons.Close;
            mainDialog.DefaultButton = TaskDialogResult.Close;

            // Set footer text. Footer text is usually used to link to the help document.
            mainDialog.FooterText =
                "<a href=\"http://usa.autodesk.com/adsk/servlet/index?siteID=123112&id=2484975 \">"
                + "Click here for the Revit API Developer Center</a>";

            TaskDialogResult tResult = mainDialog.Show();

            // If the user clicks the first command link, a simple Task Dialog 
            // with only a Close button shows information about the Revit installation. 
            if (TaskDialogResult.CommandLink1 == tResult)
            {
                TaskDialog dialog_CommandLink1 = new TaskDialog("Revit Build Information");
                dialog_CommandLink1.MainInstruction =
                    "Revit Version Name is: " + app.VersionName + "\n"
                    + "Revit Version Number is: " + app.VersionNumber + "\n"
                    + "Revit Version Build is: " + app.VersionBuild;

                dialog_CommandLink1.Show();

            }

            // If the user clicks the second command link, a simple Task Dialog 
            // created by static method shows information about the active document.
            else if (TaskDialogResult.CommandLink2 == tResult)
            {
                TaskDialog.Show("Active Document Information",
                    "Active document: " + activeDoc.Title + "\n"
                    + "Active view name: " + activeDoc.ActiveView.Name);
            }
            #endregion

            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
