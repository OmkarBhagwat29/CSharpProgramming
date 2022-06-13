using Rhino.Compute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhinoTestCommands
{
    public static class CustomComputeFuntion
    {
        public static List<GrasshopperDataTree> CreateSphereGh(string filePath,
            List<GrasshopperDataTree> inputs)
        {
            return GrasshopperCompute.EvaluateDefinition(filePath, inputs);

        }
    }
}
