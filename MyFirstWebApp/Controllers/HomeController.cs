
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO;
using Newtonsoft.Json;


//using RhinoTestCommands;



namespace MyFirstWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            #region RhinoCode
            //ComputeServer.AuthToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJwIjoiUEtDUyM3IiwiYyI6IkFFU18yNTZfQ0JDIiwiYjY0aXYiOiJhQTM1bGhSYUdReXdYMWl2RUIwR3lBPT0iLCJiNjRjdCI6ImNGUXI5NlIyL0tLMVhySXJ5TDdTd1B6ZjRkZFVMSUVaWU5oNkxMY3RkRExaTUJ5cmhhZ2w0eDFaVUF0R3V2NGpVZDJjZ2xkVTVGTXNsUVltcjI2UURCUnJrL3dyMlZ6M3FIWE5lV1VqNmp0WTA0UHUxOHF6Ry8wRW5PMXNMTWdZV0J2N3BEa2RTaXo1cUM1T2FGYVZVb0JtTE82QnhxVUpJLzYvaWQwekoyTUZlNGRlMk02VWhDUFZUTjJYS1REMGhzMWRaMzBCM0I1RGpOUERrd1lReVE9PSIsImlhdCI6MTYwODgxMDE5Mn0.6rpvdMse9p82-vtW86hepmUiYG1czrCUvn8knuwMa_8";

            //#region old code
            ////string rhinoFileToRead = @"D:\WORK\IC_Work\#HSS\WIP_CentralModel\HSS_CentralModel.3dm";

            ////File3dm rhFile = File3dm.Read(rhinoFileToRead);

            ////List<InstanceDefinitionGeometry> defs = rhFile.AllInstanceDefinitions.ToList();

            ////foreach (var item in defs)
            ////{
            ////    Guid[] gds = item.GetObjectIds();

            ////    string folderName = Path.Combine(@"D:\WORK\IC_Work\#HSS\Test", item.Name);
            ////    if (Directory.Exists(folderName))
            ////        continue;

            ////    Directory.CreateDirectory(folderName);
            ////    foreach (var gd in gds)
            ////    {
            ////        File3dmObject obj = rhFile.Objects.FindId(gd);

            ////        string rhinoFilePath = Path.Combine(folderName, obj.Id + ".3dm");
            ////        string satFilePath = Path.Combine(folderName, obj.Id + ".sat");

            ////        bool rhF = File3dm.WriteOneObject(rhinoFilePath, obj.Geometry);
            ////        bool satF = File3dm.WriteOneObject(satFilePath, obj.Geometry);



            ////    }

            ////}
            //#endregion

            //Random rand = new Random();
            ////string path = @"D:\WORK\IC_Work\#HSS\Test";
            //Brep[] brps = new Brep[10];
            //for (int i = 0; i < 10; i++)
            //{
            //    double radi = rand.Next(1, 100);
            //    Point3d origin = new Point3d(rand.Next(10, 200), rand.Next(10, 200), rand.Next(10, 200));

            //    Sphere sph = new Sphere(origin, radi);

            //    // do something ie exclusively available in Rhino.Compute
            //    int area = (int)Rhino.Compute.AreaMassPropertiesCompute.Compute(sph.ToBrep()).Area;

            //    //string fileName = area.ToString()+$"_{i}" + ".sat";
            //    //RhinoInside.Resolver.Initialize();
            //    //Rhino.FileIO.FileStp()

            //    //Rhino.Compute.ComputeServer.Post<>("sample",)

            //    File3dm fl = new File3dm();
            //   Guid id = fl.Objects.AddSphere(sph);



            //    string fileSerialized = Convert.ToBase64String(fl.ToByteArray());

            //    //JsonConvert.SerializeObject(fl);


            //    //List<Guid> ids = new List<Guid>() { id };
            //    //var data =  Convert3dm.Convert3dmToSAT(fileSerialized, new List<Guid>() { id });

            //    //Rhino.Compute.

            //    int x = 0;
           // }
            #endregion

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}