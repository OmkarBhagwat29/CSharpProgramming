using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using System.Linq;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace Aeditive
{
    public class AeditiveComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public AeditiveComponent()
          : base("Test", "test",
              "Does Something",
              "Aeditive", "Testing")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("outline", "outline", "", GH_ParamAccess.item);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {

            pManager.AddPointParameter("points", "pts", "", GH_ParamAccess.list);

            pManager.AddLineParameter("lns", "ls", "", GH_ParamAccess.list);

            pManager.AddPlaneParameter("planes", "plns", "", GH_ParamAccess.list);

        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Curve outline = null;
            if (!DA.GetData(0, ref outline)) return;

            double extension = 75;
            double offsetVal = 30;
            double depth = 5.0;

            Curve[] ofstCrv = outline.Offset(Plane.WorldXY, offsetVal, Rhino.RhinoDoc.ActiveDoc.ModelAbsoluteTolerance, CurveOffsetCornerStyle.Sharp);
            // var segs = ofstCrv[0].DuplicateSegments();

            List<Line> lines = new List<Line>();
            if (ofstCrv[0].TryGetPolyline(out Polyline pl))
            {
                Line[] lns = pl.GetSegments();

                List<Point3d> points = pl.GetRange(0, pl.Count).ToList();


                for (int i = 0; i < lns.Length; i++)
                {

                    Point3d pt = Point3d.Unset;
                    Line ln = Line.Unset;
                    for (int j = 0; j < points.Count; j++)
                    {
                        if (lns[i].From == points[j])
                        {
                            pt = points[j];
                            ln = lns[i];
                            break;
                        }
                        
                    }

                    if (pt != Point3d.Unset)
                    {
                        Vector3d dir = ln.Direction;
                        dir.Unitize();
                        Point3d nextPt = new Point3d(pt) + dir * extension;

                        lines.Add(new Line(pt, nextPt));
                    }

                    pt = Point3d.Unset;
                    ln = Line.Unset;
                    for (int j = 0; j < points.Count; j++)
                    {
                        if (lns[i].To == points[j])
                        {
                            pt = points[j];
                            ln = lns[i];
                            break;
                        }
                    }

                    if (pt != Point3d.Unset)
                    {
                        Vector3d dir = ln.Direction;
                        dir.Unitize();
                        dir = -dir;
                        Point3d nextPt = new Point3d(pt) + dir * extension;

                        lines.Add(new Line(pt, nextPt));
                    }
                }



                DA.SetDataList(1, lines);

            }


            List<Point3d> endPoints = lines.Select(ln => ln.To).ToList();

            

        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("9967e539-d56d-45cd-935a-a8e484c029bf"); }
        }


        public Transform MatricesMultiplication(Transform A, Transform B)
        {
            int rowCount = 4;
            int coulmnCount = 4;
            Transform C = Transform.Identity;
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < coulmnCount; j++)
                {
                    C[i, j] = 0;
                    //double b = B[j, i];
                    for (int k = 0; k < coulmnCount; k++)
                    {
                        C[i, j] += A[i, k] * B[k, j]; 
                    }
                }
            }

            return C;
        }
    }
}
