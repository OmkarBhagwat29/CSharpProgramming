using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;

using System.Linq;
using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;

namespace RevitAutomation
{
    [Transaction(TransactionMode.ReadOnly)]
    public class DbElement : IExternalCommand
    {
        Application _app;
        Document _doc;

        
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            _app = uiApp.Application;
            _doc = uiDoc.Document;


            Reference refPick = uiDoc.Selection.PickObject(ObjectType.Element, "Pick an Element");
            Element elm = _doc.GetElement(refPick);

            //this.ShowBasicElementInfo(elm);
            //this.IdentifyElement(elm);

            //this.ShowParameters(elm, "Element Instance Parameters");

            ElementId elmTypeId = elm.GetTypeId();
            ElementType elmType = (ElementType)_doc.GetElement(elmTypeId);

            //this.ShowParameters(elmType, "Element Type parameters");

            //this.ShowSpecificParameter(elm, BuiltInParameter.HOST_AREA_COMPUTED,"Type parameter");

            this.ShowGeometry(elm);

            return Result.Succeeded;
        }


        public void ShowBasicElementInfo(Element elem)
        {
            // let's see what kind of element we got. 
            // 
            string s = "You Picked:" + "\n";

            s += " Class name = " + elem.GetType().Name + "\n";
            s += " Category = " + elem.Category.Name + "\n";
            s += " Element id = " + elem.Id.ToString() + "\n" + "\n";

            // and, check its type info. 
            // 
            //Dim elemType As ElementType = elem.ObjectType '' this is obsolete. 
            ElementId elemTypeId = elem.GetTypeId();
            ElementType elemType = (ElementType)_doc.GetElement(elemTypeId);

            s += "Its ElementType:" + "\n";
            s += " Class name = " + elemType.GetType().Name + "\n";
            s += " Category = " + elemType.Category.Name + "\n";
            s += " Element type id = " + elemType.Id.ToString() + "\n";

            // finally show it. 

            TaskDialog.Show("Basic Element Info", s);

        }


        public void IdentifyElement(Element elm)
        {
            //An instance of system family has a designated class.
            //you can use it identify the type of element
            //eg. Walls,floors,roofs.
            string s = "";

            if (elm is Wall)
                s = "Wall";
            else if (elm is Floor)
                s = "Floor";
            else if (elm is RoofBase)
                s = "Roof";
            else if (elm is FamilyInstance)
            {
                // AN instance of a componenet family is all FamilyInstance.
                // We will need to further check its category.
                //eg. Doors, Windows, Furnitures
                if (elm.Category.Id.IntegerValue == (int)BuiltInCategory.OST_Doors)
                    s = "Door";
                else if (elm.Category.Id.IntegerValue == (int)BuiltInCategory.OST_Windows)
                    s = "Window";
                else if (elm.Category.Id.IntegerValue == (int)BuiltInCategory.OST_Furniture)
                    s = "Furniture";
                else
                    s = "System family instance"; //eg Plant
            }
            else if (elm is HostObject)
            {
                //check the base class. eg ceiling and floor
                s = "System family instance";
            }
            else
            {
                s = "Other";
            }

            s = "You have picked: " + s;

            TaskDialog.Show("Identify Element", s);
        }

        public void ShowParameters(Element elem, string header)
        {
            var paramSet = elem.GetOrderedParameters();
            string s = string.Empty;

            for (int i = 0; i < paramSet.Count; i++)
            {
                Parameter prm = paramSet[i];
                string name = prm.Definition.Name;

                string val = ParameterToString(prm);
                s += name + "=" + val + "\n";
            }

            TaskDialog.Show(header, s);
        }

        public void ShowSpecificParameter(Element elem, BuiltInParameter bPrm,string header)
        {
            string s = "Parameter not found!!!";
            var prm = elem.get_Parameter(bPrm);
            if (prm != null)
            {
                string val = ParameterToString(prm);
                s = prm.Definition.Name + "=" + val + "\n";
            }

            TaskDialog.Show(header, s);
        }

        public static string ParameterToString(Parameter prm)
        {
            string val = "none";
            if (prm == null)
                return val;

            //to get the parameter value, we need to pause it depending
            // on its storage type

            switch (prm.StorageType)
            {
                case StorageType.None:
                    break;
                case StorageType.Integer:
                    val = prm.AsInteger().ToString();
                    break;
                case StorageType.Double:
                    val = prm.AsDouble().ToString();
                    break;
                case StorageType.String:
                    val = prm.AsString();
                    break;
                case StorageType.ElementId:
                    val = prm.AsElementId().ToString();
                    break;
                default:
                    break;
            }

            return val;
        }

        //SHow geometry information of given element
        public void ShowGeometry(Element elm)
        {
            //set geometry option
            Options opt = _app.Create.NewGeometryOptions();
            opt.DetailLevel = ViewDetailLevel.Fine;

            //get the geometry from the element
            GeometryElement geomElm = elm.get_Geometry(opt);

            string s = geomElm == null ? "No Data!!!" : GeometryElementToString(geomElm);

            TaskDialog.Show("GeomInfo", s);

            //if there is a geometry data retirve it as a string to show it
        }

        //Helper fucntion: Oarse the geometry element by geometry type.
        //Here we look at the top level
        public static string GeometryElementToString(GeometryElement geomElm)
        {
            string s = "";
            var iter = geomElm.GetEnumerator();
            
            

            while(iter.MoveNext())
            {
                GeometryObject geoObj = iter.Current;
                if (geoObj == null)
                    continue;

                if (geoObj is Solid sld)
                {
                    //ex. wall
                    s += "Solid" + "\n";
                }
                else if (geoObj is GeometryInstance geoInst)
                {

                    s += "--Geometry.Instance--" + "\n";

                    geomElm = geoInst.SymbolGeometry;
                    s += GeometryElementToString(geomElm);
                }
                else if (geoObj is Curve cv)
                {
                    s += "Curve" + "\n";
                }
                else if (geoObj is Mesh)
                {
                    Mesh mesh = (Mesh)geoObj;
                    //str += GeometryMeshToString(mesh) 

                    s += "Mesh" + "\n";
                }
                else
                {
                    s += " *** unkown geometry type" +
                         geoObj.GetType().Name;
                }

            }

            return s;
        }
    }
}
