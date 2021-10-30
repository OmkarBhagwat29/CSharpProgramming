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
    public class BuildHouse : IExternalCommand
    {
        Application m_rvtApp;
        Document m_rvtDoc;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication rvtUIApp = commandData.Application;
            UIDocument rvtUIDoc = rvtUIApp.ActiveUIDocument;
            m_rvtApp = rvtUIApp.Application;
            m_rvtDoc = rvtUIDoc.Document;

            var walls = this.CreateBoundaryWalls();

            var doorinstance = this.AddDoor(walls[0]);

            var windows = this.AddWindows(walls[walls.Count - 1]);

            this.addRoof(walls);

            return Result.Succeeded;
        }



        public static double MMtoFt(double mmVal) => mmVal* 0.0032808399;


        public List<Wall> CreateBoundaryWalls()
        {
            //draw rectangular curves
            XYZ p1 = new XYZ(MMtoFt(-7000), MMtoFt(-5000), 0);
            XYZ p2 = new XYZ(MMtoFt(-7000), MMtoFt(5000), 0);
            XYZ p3 = new XYZ(MMtoFt(7000), MMtoFt(5000), 0);
            XYZ p4 = new XYZ(MMtoFt(7000), MMtoFt(-5000), 0);

            List<XYZ> pts = new List<XYZ>() { p1, p2, p3, p4, p1 };

            List<Line> lns = new List<Line>();
            for (int i = 0; i < pts.Count-1; i++)
            {
                Line l = Line.CreateBound(pts[i], pts[i + 1]);
                lns.Add(l);
            }

            List<Element> elms = new List<Element>();
            //create wall using curves
            List<Wall> aWalls = new List<Wall>();

            //get level
            Level lvl1 = new FilteredElementCollector(m_rvtDoc)
                .OfClass(typeof(Level))
                .FirstOrDefault(lv => lv.Name == "Level 1") as Level;

            Level lvl2 = new FilteredElementCollector(m_rvtDoc)
    .OfClass(typeof(Level))
    .FirstOrDefault(lv => lv.Name == "Level 2") as Level;

            if (lvl1 == null || lvl2 == null)
                return null;
                

            using (Transaction tra = new Transaction(m_rvtDoc,"Create walls"))
            {
                tra.Start();

                //aWall = Wall.Create(m_rvtDoc, lns.Select(l => l.Clone()).ToList(), false);

                for (int i = 0; i < lns.Count; i++)
                {
                    Curve c = lns[i].Clone();

                    Wall aWall = Wall.Create(m_rvtDoc, c, lvl1.Id, false);

                    if (aWall == null)
                        continue;

                    aWall.get_Parameter(BuiltInParameter.WALL_HEIGHT_TYPE)
                        .Set(lvl2.Id);

                    //constrain top of wall to level2

                    aWalls.Add(aWall);
                   
                }


                m_rvtDoc.Regenerate();
                m_rvtDoc.AutoJoinElements();

                tra.Commit();

            }

            return aWalls;
        }

        public FamilyInstance AddDoor(Wall hostWall)
        {
            //get door location on wall
            //place door in middle
            
            double yMid = 5000 / 2.0;
            yMid = MMtoFt(yMid);

            XYZ loc = new XYZ(MMtoFt(-7000), yMid, 0);

            XYZ refDir = new XYZ(0, 0, 0);

            //get symbol
            FamilySymbol symbol = new FilteredElementCollector(m_rvtDoc)
                .OfCategory(BuiltInCategory.OST_Doors)
                .Where(d => d.Name == "0915 x 2134mm").First() as FamilySymbol;

            if (symbol == null)
                return null;


            FamilyInstance fI;

            using (Transaction tra = new Transaction(m_rvtDoc, "place door"))
            {
                tra.Start();


                if (!symbol.IsActive)
                    symbol.Activate();

                fI = m_rvtDoc.Create.NewFamilyInstance(loc, symbol, refDir, hostWall, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);



                tra.Commit();
            }


            return fI;
        }

        public List<FamilyInstance> AddWindows(Wall hostWall)
        {
            //get window location;
            double x1 = 3500 / 2;
            x1 = MMtoFt(x1);

            double x2 = -x1;

            XYZ loc1 = new XYZ(x1, MMtoFt(-5000), 0);
            XYZ loc2 = new XYZ(x2, MMtoFt(-5000), 0);

            List<XYZ> locs = new List<XYZ>() {loc1,loc2 };


            //facing direction of window
            XYZ dir = new XYZ(0, -100, 0);

            //get symbol

            var winodw = new FilteredElementCollector(m_rvtDoc)
                .OfCategory(BuiltInCategory.OST_Windows)
                .Where(w => w.Name == "1050 x 1350mm").FirstOrDefault() as FamilySymbol;
            if (winodw == null)
                return null;

            List<FamilyInstance> fis = new List<FamilyInstance>();

            double sillHeight = MMtoFt(1000);
            using (Transaction tra = new Transaction(m_rvtDoc, "place windows"))
            {
                tra.Start();

                if (!winodw.IsActive)
                    winodw.Activate();

                for (int i = 0; i < locs.Count; i++)
                {
                    FamilyInstance fI = m_rvtDoc.Create.NewFamilyInstance(locs[i], winodw, dir, hostWall, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);

                    if (fI == null)
                        continue;
                    //adjust sill heihgt here
                    fI.get_Parameter(BuiltInParameter.INSTANCE_SILL_HEIGHT_PARAM)
                        .Set(sillHeight);


                        fis.Add(fI);
                }

                tra.Commit();
            }

            return fis;
        }

        public RoofBase CreateRoof(List<Wall> hostWalls)
        {
            //get specific roof
            RoofType roof = (RoofType)ElementFiltering.FindFamilyType(m_rvtDoc, typeof(RoofType), "Basic Roof", "Generic - 125mm", null);

            //get level
            Level lvl2 = new FilteredElementCollector(m_rvtDoc)
                .OfClass(typeof(Level))
                .FirstOrDefault(l => l.Name == "Level 2") as Level;


            if (roof == null || lvl2 == null)
                return null;

            //get curve array from walls
            CurveArray cArr = new CurveArray();
            foreach (var w in hostWalls)
            {
                LocationCurve c = w.Location as LocationCurve;
                cArr.Append(c.Curve);
            }

            if (cArr.IsEmpty)
                return null;

            RoofBase newRoof = null;

            using (Transaction tra = new Transaction(m_rvtDoc, "create roof"))
            {
                tra.Start();

                ModelCurveArray mapping;
                FootPrintRoof rf = m_rvtDoc.Create.NewFootPrintRoof(cArr, lvl2, roof, out mapping);

                if (rf.IsValidObject)
                    newRoof = rf as RoofBase;

                tra.Commit();
            }



            return newRoof;
        }


        public void addRoof(List<Wall> walls)
        {
            // hard coding the roof type we will use. 
            // e.g., "Basic Roof: Generic - 400mm" 

            const string roofFamilyName = "Basic Roof";
            const string roofTypeName = "Generic - 125mm"; // "Generic - 400mm" 
            const string roofFamilyAndTypeName =
                roofFamilyName + ": " + roofTypeName;

            // find the roof type 

            RoofType roofType = (RoofType)ElementFiltering.FindFamilyType(
                m_rvtDoc, typeof(RoofType), roofFamilyName, roofTypeName, null);

            if (roofType == null)
            {
                TaskDialog.Show("Revit Intro Lab", "Cannot find (" + roofFamilyAndTypeName + "). Maybe you use a different template? Try with DefaultMetric.rte.");
            }

            // wall thickness to adjust the footprint of the walls 
            // to the outer most lines. 
            // Note: this may not be the best way. 
            // but we will live with this for this exercise. 

            double wallThickness = walls[0].Width;
            double dt = wallThickness / 2.0;
            List<XYZ> dts = new List<XYZ>(5);
            dts.Add(new XYZ(-dt, -dt, 0.0));
            dts.Add(new XYZ(dt, -dt, 0.0));
            dts.Add(new XYZ(dt, dt, 0.0));
            dts.Add(new XYZ(-dt, dt, 0.0));
            dts.Add(dts[0]);

            // set the profile from four walls 

            CurveArray footPrint = new CurveArray();
            for (int i = 0; i <= 3; i++)
            {
                LocationCurve locCurve = (LocationCurve)walls[i].Location;
                XYZ pt1 = locCurve.Curve.GetEndPoint(0) + dts[i];
                XYZ pt2 = locCurve.Curve.GetEndPoint(1) + dts[i + 1];
                Line line = Line.CreateBound(pt1, pt2);
                footPrint.Append(line);
            }

            // get the level2 from the wall 

            ElementId idLevel2 = walls[0].get_Parameter(
                BuiltInParameter.WALL_HEIGHT_TYPE).AsElementId();
            Level level2 = (Level)m_rvtDoc.GetElement(idLevel2);

            // footprint to morel curve mapping 

            ModelCurveArray mapping;

            // create a roof. 

            FootPrintRoof aRoof = m_rvtDoc.Create.NewFootPrintRoof(
               footPrint, level2, roofType, out mapping);

            // setting the slope 
            foreach (ModelCurve modelCurve in mapping)
            {
                aRoof.set_DefinesSlope(modelCurve, true);
                aRoof.set_SlopeAngle(modelCurve, 0.5);
            }
        }

    }
}
