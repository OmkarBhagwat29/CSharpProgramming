using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using BH.Adapter.Revit;
using BH.oM.Data.Requests;
using BH.oM.Base;
using Autodesk.Revit.ApplicationServices;

namespace RevitAutomation
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.ReadOnly)]
    public class TestCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            Application app = uiApp.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;


            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(FloorType));

            string s = "";

            foreach (FloorType wall in collector)
            {
                s += wall.Name + "\r\n";
            }

            TaskDialog.Show("Revit Intro Lab", "Wall Types (in main instruction):\n\n" + s);


            return Autodesk.Revit.UI.Result.Succeeded;
        }

        #region selection
        public Element SelectObject(UIDocument iDoc,Document doc)
        {
            return doc.GetElement(iDoc
                .Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element).ElementId);
        }
        #endregion

        #region filter elements

        /// <summary>
        /// Get all element types in project
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public List<Family> AllFamilies(Document doc)
        {

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            var types = collector.OfClass(typeof(Family))
                .ToElements()
                .Select(e => e as Family)
                .ToList();

            return types;
        }

        /// <summary>
        /// Get all built in categories
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public List<BuiltInCategory> AllBuiltInCategories(Document doc)
        {
            return Enum.GetValues(typeof(BuiltInCategory))
                .Cast<BuiltInCategory>()
                .ToList();

        }

        public List<FamilySymbol> AllFamilyAllFamilySymbols(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            var famTypes = collector.OfClass(typeof(FamilySymbol))
                .ToElements()
                .Select(e => e as FamilySymbol)
                .ToList();
                

            return famTypes;
        }




        /// <summary>
        /// get elements of this category
        /// </summary>
        /// <param name="category"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public List<Element> AllElements_Category(BuiltInCategory category, Document doc) {

            ElementCategoryFilter filter = new ElementCategoryFilter(category,false);

            FilteredElementCollector collector = new FilteredElementCollector(doc);

            var elms = collector.WherePasses(filter).WhereElementIsNotElementType()
                .ToElements()
                .ToList();

            return elms;
        }


        /// <summary>
        /// Get elements of this element type
        /// </summary>
        /// <param name="elmType"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public List<FamilySymbol> AllElements_FamilyType(Family famType, Document doc)
        {

            var symIds = famType.GetFamilySymbolIds().ToList();
            var famSymbs = symIds.Select(f => doc.GetElement(f) as FamilySymbol).ToList();

            return famSymbs;
        }


        public List<FamilyInstance> AllInstances(FamilySymbol symobl, Document doc)
        {
            FamilyInstanceFilter filter = new FamilyInstanceFilter(doc, symobl.Id);
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            var instances = collector.WherePasses(filter)
            .ToElements()
            .Select(e => e as FamilyInstance)
            .ToList();

            return instances;
        }

        public List<Element> GetElementsOnLevel(Level level, Document doc)
        {
            ElementLevelFilter filter = new ElementLevelFilter(level.Id);
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            var elms = collector.WherePasses(filter)
                .ToElements()
                .ToList();

            return elms;
        }

        #endregion

        XYZ GetCurveNormal(Curve curve)
        {
            IList<XYZ> pts = curve.Tessellate();
            int n = pts.Count;

            //Debug.Assert(1 < n,
            //  "expected at least two points "
            //  + "from curve tessellation");

            XYZ p = pts[0];
            XYZ q = pts[n - 1];
            XYZ v = q - p;
            XYZ w, normal = null;

            if (2 == n)
            {
                //Debug.Assert(curve is Line,
                //  "expected non-line element to have "
                //  + "more than two tessellation points");

                // for non-vertical lines, use Z axis to 
                // span the plane, otherwise Y axis:

                double dxy = Math.Abs(v.X) + Math.Abs(v.Y);

                w = (dxy > 1.0e-9)
                  ? XYZ.BasisZ
                  : XYZ.BasisY;

                normal = v.CrossProduct(w).Normalize();
            }
            else
            {
                int i = 0;
                while (++i < n - 1)
                {
                    w = pts[i] - p;
                    normal = v.CrossProduct(w);
                    if (!normal.IsZeroLength())
                    {
                        normal = normal.Normalize();
                        break;
                    }
                }

#if DEBUG
                {
                    XYZ normal2;
                    while (++i < n - 1)
                    {
                        w = pts[i] - p;
                        normal2 = v.CrossProduct(w);
                        //Debug.Assert(normal2.IsZeroLength()
                        //  || Util.IsZero(normal2.AngleTo(normal)),
                        //  "expected all points of curve to "
                        //  + "lie in same plane");
                    }
                }
#endif // DEBUG

            }
            return normal;
        }
    }
}
