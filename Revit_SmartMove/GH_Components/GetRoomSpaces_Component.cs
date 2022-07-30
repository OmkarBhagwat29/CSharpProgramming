
using Autodesk.Revit.DB;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace Revit_SmartMove.Components
{
    public class GetRoomSpaces_Component : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the RoadNetwork_Component class.
        /// </summary>
        public GetRoomSpaces_Component()
          : base("GetSpaces", "getSpaces",
              "get room spaces as curves",
              "SmartMove", "Extract_Geometry")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("elementId", "elmId", "All room ids", GH_ParamAccess.list);
            pManager.AddNumberParameter("offset", "offset", "offset distance", GH_ParamAccess.item,1);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddCurveParameter("roomBoundary", "roomBdry", "", GH_ParamAccess.list);
            pManager.AddCurveParameter("roomOffsetBoundary","rmOfstBdry","",GH_ParamAccess.list);
            pManager.AddPointParameter("cneter", "center", "", GH_ParamAccess.list);
            pManager.AddTextParameter("roomNames", "rmNames", "", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<ElementId> elmIds = new List<ElementId>();
            double offset = 0;

            if (!DA.SetDataList(0, elmIds)) return;
            DA.SetData(1, offset);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("40EF9A5D-D042-443F-8C87-4979092D0DC1"); }
        }
    }
}