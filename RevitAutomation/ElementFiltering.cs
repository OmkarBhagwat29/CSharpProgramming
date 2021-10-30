using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;

using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAutomation
{
    [Transaction(TransactionMode.Manual)]
    public class ElementFiltering : IExternalCommand
    {
        //  member variables 
        Application m_rvtApp;
        Document m_rvtDoc;

        public Result Execute(
            ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            //  Get the access to the top most objects. 
            UIApplication rvtUIApp = commandData.Application;
            UIDocument rvtUIDoc = rvtUIApp.ActiveUIDocument;
            m_rvtApp = rvtUIApp.Application;
            m_rvtDoc = rvtUIDoc.Document;

            //pick a window
            //Reference refObj = rvtUIDoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, "Pick a Window");

            //if (refObj == null)
            //    return Result.Cancelled;


            //Element elm = FindFmailyType("Basic Wall",m_rvtDoc);


            return Result.Succeeded;

            //.OfCategory(BuiltInCategory.OST_Windows).ToElements();

            //  ...

        }


        public static Element FindFamilyType(Document doc,Type famClass,
            string famName,string famTypeName,Nullable<BuiltInCategory> cat = null)
        {

            FilteredElementCollector clc = new FilteredElementCollector(doc);

            if (cat.HasValue)
                clc.OfCategory(cat.Value);

            var elm = clc.OfClass(famClass)
            .Where(w => w.Name == famTypeName && w.get_Parameter(BuiltInParameter.SYMBOL_FAMILY_NAME_PARAM).AsString()==famName)
            .FirstOrDefault();

            


            return elm;
        }


        public List<Element> FindLongWalls()
        {
            double length = 60.0;

            //narrow down search to wall
            FilteredElementCollector clc = new FilteredElementCollector(m_rvtDoc)
            .OfClass(typeof(Wall));

            //define a filter by yparameter
            //1st arg - value provider
            BuiltInParameter lenPara = BuiltInParameter.CURVE_ELEM_LENGTH;
            int iLenParam = (int)lenPara;
            ParameterValueProvider paramValueProvidder = 
                new ParameterValueProvider(new ElementId(iLenParam));

            //2ndd evaluator
            FilterNumericGreater eval = new FilterNumericGreater();

            //3rd rule value
            double ruleVal = length;

            //4th - epsilon
            const double eps = 1E-06;

            //define a rule
            var filterRule =
                new FilterDoubleRule(paramValueProvidder, eval, ruleVal, eps);

            //create new filter
            var paramFilter = new ElementParameterFilter(filterRule);

            var elms = clc.WherePasses(paramFilter).ToElements().ToList();

            return elms;
        }

        public static List<Element> FindElements(Document doc, Type targetType, string targetName, Nullable<BuiltInCategory> category = null)
        {
            FilteredElementCollector clc = new FilteredElementCollector(doc).OfClass(targetType);
            if (category.HasValue)
                clc.OfCategory(category.Value);

            var elms = clc.ToElements()
                  .Where(e => e.Name == targetName);



            return elms.ToList();

        }
    }
    
}
