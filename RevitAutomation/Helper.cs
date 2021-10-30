using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAutomation
{
    public static class Helper
    {
        public static Solid MakeUnion(this List<Solid> solids)
        {
            Solid union = null;
            for (int i = 0; i < solids.Count; i++)
            {
                Solid solid = solids[i];

                if (null != solid
        && 0 < solid.Faces.Size)
                {
                    if (null == union)
                    {
                        union = solid;
                    }
                    else
                    {
                        union = BooleanOperationsUtils
                          .ExecuteBooleanOperation(union, solid,
                            BooleanOperationsType.Union);
                    }
                }
            }
            return union;
        }

    }
}
