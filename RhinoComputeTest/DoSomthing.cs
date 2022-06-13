using System;
using Rhino.Compute;
using Rhino.FileIO;
using Rhino.Geometry;


//this project implements Rhino3dmIo and RhinoCompute.cs
namespace RhinoComputeTest
{
    public class DoSomthing
    {
        public Mesh ComputGeneratedMesh { get; set; }
        public String JsonInfo { get; set; }
        public double _radius { get; set; }

        public DoSomthing(double radius)
        {
            this._radius = radius;

            var sph = new Sphere(Point3d.Origin, radius);

            var mesh = MeshCompute.CreateFromBrep(sph.ToBrep());

            this.ComputGeneratedMesh = mesh[0];

            SerializationOptions op = new SerializationOptions();
            op.RhinoVersion = 7;
            this.JsonInfo = this.ComputGeneratedMesh.ToJSON(op);
        }
    }
}
