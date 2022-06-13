
using Rhino;
using Rhino.Compute;
using Rhino.PlugIns;
using System;
using System.Net;
using System.Text;
using System.Threading;

namespace RhinoTestCommands
{
    ///<summary>
    /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    /// class. DO NOT create instances of this class yourself. It is the
    /// responsibility of Rhino to create an instance of this class.</para>
    /// <para>To complete plug-in information, please also see all PlugInDescription
    /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
    /// "Show All Files" to see it in the "Solution Explorer" window).</para>
    ///</summary>
    public class TestMe : Rhino.PlugIns.PlugIn

    {


        //static HttpListener listner = new HttpListener();
        public TestMe()
        {
            Instance = this;
            //ComputeServer.WebAddress = @"http://20.113.32.242/";
            //ComputeServer.ApiKey = "12345abcd";
        }

        ///<summary>Gets the only instance of the TestMe plug-in.</summary>
        public static TestMe Instance
        {
            get; private set;
        }

        // You can override methods here to change the plug-in behavior on
        // loading and shut down, add options pages to the Rhino _Option command
        // and maintain plug-in wide options in a document.


        //protected override LoadReturnCode OnLoad(ref string errorMessage)
        //{
        //    Rhino.Runtime.HostUtils
        //        .RegisterComputeEndpoint("Rhino.CustomEndPoint",
        //        typeof(CustomComputeFuntion));
        //    return base.OnLoad(ref errorMessage);
        //}
    }


    //write own server


}