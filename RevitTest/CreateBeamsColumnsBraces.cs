using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitTest
{
    /// <summary>
    /// Create Beams, Columns and Braces according to user's input information
    /// </summary>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    [Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.NoCommandData)]
    public class CreateBeamsColumnsBraces : IExternalCommand
    {
        UIApplication uiApp = null;

        public ArrayList ColumnMaps = new ArrayList(); // list of column type
        public ArrayList BeamMaps = new ArrayList(); //list of beam type
        public ArrayList BraceMaps = new ArrayList(); // list of brace maps

        public SortedList levels = new SortedList(); // list of list sorted by their elevations

        UV[,] _matrixUV; // 2D co-ordinates of matrix

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            uiApp = commandData.Application;

            Transaction tra = new Transaction(uiApp.ActiveUIDocument.Document, "Create Coulmn Beams and Braces");

            tra.Start();

            try
            {
                //get all colums, beams, levels and braces from document
                bool initializeOK = this.Initialize();

                if (!initializeOK)
                {
                    tra.RollBack();
                    return Result.Failed;
                }

                using (CreateBeamsCoulmnsBracesFrom form = new CreateBeamsCoulmnsBracesFrom(this))
                {
                    if (form.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    {
                        tra.RollBack();
                        return Result.Failed;
                    }


                }

                tra.Commit();

                return Result.Succeeded;
            }
            catch(Exception ex)
            {

                message = ex.Message;
                tra.RollBack();
                return Autodesk.Revit.UI.Result.Failed;
            }

        }

        public void CreateMatrix(int xNumber, int yNumber,double distance)
        {
            _matrixUV = new UV[xNumber,yNumber];

            for (int i = 0; i < xNumber; i++)
            {
                for (int j = 0; j < yNumber; j++)
                {
                    _matrixUV[i,j] = new UV(i*distance,j*distance);
                }
            }
        }

        public bool AddInstance(object coulmnObj, object beamObj,object braceObj,int floorNum)
        {

            return true;
            
        }

        private bool Initialize()
        {
            try
            {

                FilteredElementIterator i = new FilteredElementCollector(uiApp.ActiveUIDocument.Document)
                    .OfClass(typeof(Level))
                    .GetElementIterator();
                i.Reset();

                while (i.MoveNext())
                {
                    //add level to list
                    if (i.Current is Level lvl)
                        levels.Add(lvl.Elevation,lvl);
                            
                }

                i = new FilteredElementCollector(uiApp.ActiveUIDocument.Document)
                    .OfClass(typeof(Family))
                    .GetElementIterator();
                i.Reset();

                while (i.MoveNext())
                {
                    if (i.Current is Family fam)
                    {
                        foreach (var elmId in fam.GetFamilySymbolIds())
                        {
                            object symbol = uiApp.ActiveUIDocument.Document.GetElement(elmId);
                            if (symbol is FamilySymbol famType)
                            {
                                if (famType.Category != null)
                                {
                                    string categoryName = famType.Category.Name;

                                    if ("Structural Framing" == categoryName)
                                    {
                                        BeamMaps.Add(new
                                        {
                                            family = famType,
                                            name = famType.Family.Name + ":" + famType.Name
                                        });

                                        BraceMaps.Add(new
                                        {
                                            family = famType,
                                            name = famType.Family.Name + ":" + famType.Name
                                        });
                                    }
                                    else if ("Structural Columns" == categoryName)
                                    {
                                        ColumnMaps.Add(new
                                        {
                                            family = famType,
                                            name = famType.Family.Name + ":" + famType.Name
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
 

            }
            catch
            {

                return false;
            }

            return true;
        }
    }
}
