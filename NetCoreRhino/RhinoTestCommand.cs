using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rhino;
using Rhino.Commands;
using Rhino.Compute;
using Rhino.Display;
using Rhino.FileIO;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;

//using RhinoComputeTest;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;

namespace RhinoTestCommands
{
    public class RhinoTestCommand : Command
    {
        HttpClient _client = new HttpClient();
        public RhinoTestCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static RhinoTestCommand Instance
        {
            get; private set;
        }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName
        {
            get { return "testNow"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {

            string ghPath = @"C:\Users\Om92\Desktop\TestGhx\createSphere.ghx";

           

            //System.IO.File.

            

            GrasshopperDataTree gdt = new GrasshopperDataTree("RH_IN:Radius");

            Rhino.Input.Custom.GetNumber gn = new GetNumber();
            gn.AcceptNumber(true, true);
            gn.SetCommandPrompt($"Give Radius: ");

            Brep mainBrep = null;
            MyDisplay display = new MyDisplay();
            display.Enabled = true;

            while (gn.Get() == GetResult.Number)
            {
                gn.SetCommandPrompt($"Give Radius [{gn.Number()}]: ");

                gdt.Clear();
                
                gdt.Append(new GrasshopperObject(gn.Number()), new GrasshopperPath(0));

                var inputs = new List<GrasshopperDataTree>() { gdt };

                var result = GrasshopperCompute.EvaluateDefinition(ghPath, inputs);

               

                var data = result[0].InnerTree["{0}"][0].Data;

                var parsed = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
                var obj = Rhino.FileIO.File3dmObject.FromJSON(parsed);

                Brep brp = obj as Brep;

                if (brp != null)
                {
                    display.Brp = brp;
                    doc.Views.Redraw();

                    mainBrep = brp;
                }
            }

            
            if (display != null)
                display.Enabled = false;

            //doc.Views.Redraw();

            if (mainBrep!=null)
                doc.Objects.AddBrep(mainBrep);


            doc.Views.Redraw();

            return Result.Success;
        }
    }


    public class MyDisplay : DisplayConduit
    {
        public Brep Brp;
        public MyDisplay()
        {
        }

        protected override void CalculateBoundingBox(CalculateBoundingBoxEventArgs e)
        {
            if (null != this.Brp)
            {
                var bbx = this.Brp.GetBoundingBox(false);

                e.IncludeBoundingBox(bbx);
            }
        }

        protected override void PostDrawObjects(DrawEventArgs e)
        {
           // e.Display.DrawBrepWires(this.Brp, Color.Red);
        }

        protected override void PreDrawObject(DrawObjectEventArgs e)
        {
            //e.Display.DrawBrepWires(this.Brp, Color.Red);
        }

        protected override void DrawOverlay(DrawEventArgs e)
        {
            if (this.Brp == null)
                return;

            DisplayMaterial mat = new DisplayMaterial(Color.Red,0.75);
            e.Display.DrawBrepShaded(this.Brp, mat);
        }

    }
}
