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
    public class ElementModification : IExternalCommand
    {
        Application m_rvtApp;
        Document m_rvtDoc;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication rvtUIApp = commandData.Application;
            UIDocument rvtUIDoc = rvtUIApp.ActiveUIDocument;
            m_rvtApp = rvtUIApp.Application;
            m_rvtDoc = rvtUIDoc.Document;

            Reference refElm = rvtUIDoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, "Pick an element");
            Element elm = m_rvtDoc.GetElement(refElm);

            Wall aWall = (Wall)elm;
            if (aWall != null)
            {
                //this.ModifyFamilyTypeOfAnInstance(aWall);
                this.ModifyParameterOfInstance(aWall);
            }

            return Result.Succeeded;

        }

        public void ModifyFamilyTypeOfAnInstance(Element elem)
        {
            // Constant to this function. 
            // this is for wall. e.g., "Basic Wall: Exterior - Brick on CMU" 
            // you can modify this to fit your need. 
            // 
            const string wallFamilyName = "Basic Wall";
            const string wallTypeName = "Exterior - Brick on CMU";
            const string wallFamilyAndTypeName =
                             wallFamilyName + ": " + wallTypeName;

            // for simplicity, we assume we can only modify a wall 
            if (!(elem is Wall))
            {
                TaskDialog.Show("Revit Intro Lab",
                  "Sorry, I only know how to modify a wall. Please select a wall.");
                return;
            }
            Wall aWall = (Wall)elem;

            // keep the message to the user.
            string msg = "Wall changed: " + "\n" + "\n";

            // (1) change its family type to a different one. 
            // (You can enhance this to import symbol if you want.)  

            Element newWallType = ElementFiltering.FindFamilyType(m_rvtDoc,
                typeof(WallType), wallFamilyName, wallTypeName, null);

            if (newWallType != null)
            {
                aWall.WallType = (WallType)newWallType;
                msg = msg + "Wall type to: " + wallFamilyAndTypeName + "\n";
            }

        }

        public void ModifyParameterOfInstance(Wall aWall)
        {
            const double _mmToFeet = 0.0032808399;
            Level lvl2 = (Level)ElementFiltering.FindElements(m_rvtDoc,
                typeof(Level), "Level 2", null)[0];

            if (lvl2 == null)
                return;

            using (Transaction tra = new Transaction(m_rvtDoc, "connect height"))
            {
                tra.Start();

                bool sucess = aWall.get_Parameter(BuiltInParameter.WALL_HEIGHT_TYPE)
                .Set(lvl2.Id);

                if (sucess)
                {
                    sucess = aWall.get_Parameter(BuiltInParameter.WALL_TOP_OFFSET)
                        .Set(1000*_mmToFeet);
                }

                //this.SetWallLocationCurve(aWall);

                this.ModifyElementByTransformUtilsMethods(aWall);

                m_rvtDoc.Regenerate();
                tra.Commit();
            }


        }

        public void SetWallLocationCurve(Wall aWall)
        {
            const double _mmToFeet = 0.0032808399;
            LocationCurve wallLocation = (LocationCurve)aWall.Location;

            //create new line bound
            XYZ p1 = new XYZ(1000 * _mmToFeet, 0, 0);
            XYZ p2 = wallLocation.Curve.GetEndPoint(1);

            wallLocation.Curve = Line.CreateBound(p1, p2);

            return;

        }

        public void ModifyElementByTransformUtilsMethods(Element elm)
        {
            const double _mmToFeet = 0.0032808399;

            XYZ v = new XYZ(1000 * _mmToFeet, 1000 * _mmToFeet, 0);

            //move
            ElementTransformUtils.MoveElement(m_rvtDoc, elm.Id, v);

            //rotate 15 degress around z axis
            XYZ pt1 = XYZ.Zero;
            XYZ pt2 = XYZ.BasisZ;
            Line axis = Line.CreateBound(pt1, pt2);

            ElementTransformUtils.RotateElement(m_rvtDoc, elm.Id, axis, Math.PI / 12);
        }
    }
}
