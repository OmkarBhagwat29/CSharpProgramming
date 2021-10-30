using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAutomation
{

    public class FamilyCreation
    {
        public static FamilyInstance CreateFamilyByGeometry(Document doc, XYZ location,Solid solidGeom,
            string familyName)
        {

            Application app = doc.Application;
    

            string familyPath = @"C:\ProgramData\Autodesk\RVT 2022\Family Templates\English\Metric Generic Model.rft";

            Document famDoc = app.NewFamilyDocument(familyPath);

            FreeFormElement elm = null;
            using (Transaction t = new Transaction(famDoc, "Add Element"))
            {
                t.Start();
                elm = FreeFormElement.Create(famDoc, solidGeom);

                FamilyManager famMan = famDoc.FamilyManager;
                FamilyType newType = famMan.NewType(familyName);

                //famDoc.FamilyManager.Set(famPara, familyName);
                t.Commit();
            }

           //set material to this freeform

            //load family into document
            Family family = famDoc.LoadFamily(doc, new FamilyLoadOptions());
            
            famDoc.Close(false);

            //get symbol as first symbol of loaded family
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.WherePasses(new FamilySymbolFilter(family.Id));
            FamilySymbol fs = collector.FirstElement() as FamilySymbol;

            //place instance at location 
            FamilyInstance fI = null;
            using (Transaction t = new Transaction(doc, "Place Instance"))
            {
                t.Start();
                if (!fs.IsActive)
                    fs.Activate();
                family.Name = "MyRailing";
                fI = doc.Create.NewFamilyInstance(location, fs, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);

                t.Commit();
            }

            return fI;   
        }
    }


    public class FamilyLoadOptions : IFamilyLoadOptions
    {
        public bool OnFamilyFound(bool familyInUse, out bool overwriteParameterValues)
        {
            overwriteParameterValues = false;
            return familyInUse;
        }

        public bool OnSharedFamilyFound(Family sharedFamily, bool familyInUse,
            out FamilySource source, out bool overwriteParameterValues)
        {
            source = FamilySource.Family;
            overwriteParameterValues = false;
            return familyInUse;
        }
    }
}
