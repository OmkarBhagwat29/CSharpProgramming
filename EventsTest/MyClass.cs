using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateEvetsLambdas.EventsTest
{
    public delegate void MyEventHandler(string s);
    public class MyClass
    {
        private string theVal;
        public event MyEventHandler ValueChanged;
        public event EventHandler<MyEventArgs> objectChnaged;
        public string Val
        {
            set
            {
                this.theVal = value;

                //when the value changes, fire the event
                this.ValueChanged(this.theVal);
                this.objectChnaged(this, new MyEventArgs { propChanged = "Val" });
            }
        }
    }


    public class MyEventArgs : EventArgs
    {
        public string propChanged;
    }
}
