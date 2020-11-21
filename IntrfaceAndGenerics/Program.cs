using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//includes the namesapce that contains INotifyPropertyChanged
using System.ComponentModel;

namespace IntrfaceAndGenerics
{
    interface IStorable
    {
        void Save();
        void Load();
        Boolean NeedSave { get; set; }

        void DoSomething();
    }

    interface IEncyptable
    {
        void Encrypt();
        void Decrypt();

        void DoSomething();
    }


    class Document : IStorable,IEncyptable
    {
        private string name;
        private bool mNeedSave = false;
        public string DocName 
        {
            get { return name; }
            set
            {
                name = value;
                NotifyProtpertyChanged("name");
            }
        }

        //INotifyPropertyChanged required the implementation of 1 event
        public event PropertyChangedEventHandler PropertyChnaged;

        //Utility funtion to call the propertyChanged event
        private void NotifyProtpertyChanged(string propName)
        {
            PropertyChnaged(this, new PropertyChangedEventArgs(propName));
        }

        public bool NeedSave
        { get { return mNeedSave; } 
            set 
            { 
                mNeedSave = value;
                NotifyProtpertyChanged("NeedSave");
            }
        }

        public Document(string s)
        {
            name = s;
            Console.WriteLine($"Created a document with name {name}");
        }


        public void Decrypt()
        {
            Console.WriteLine("Decrypting the document");
        }

        void IStorable.DoSomething()
        {
            Console.WriteLine("IStorable's DoSomething() method");
        }

        void IEncyptable.DoSomething()
        {
            Console.WriteLine("IsEncyptable's DoSomething() method");
        }

        public void Encrypt()
        {
            Console.WriteLine("Encrypting the document");
        }

        public void Load()
        {
            Console.WriteLine("Loading the document");
        }

        public void Save()
        {
            Console.WriteLine("Saving the document");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Document d = new Document("Test Document");

            #region Interface casting
            //if (d is IStorable)
            //    d.Save();

            //IStorable intStor = d as IStorable;

            //if(intStor!=null)
            //    intStor.Load();


            #endregion

            #region inherited from multiple interfaces
            //d.Load();
            //d.Save();
            //d.Encrypt();
            //d.Decrypt();
            #endregion


            #region Explicit interface implementation
            //if (d is IStorable iSt)
            //    iSt.DoSomething();

            //if (d is IEncyptable iEn)
            //    iEn.DoSomething();
            #endregion


            d.PropertyChnaged += D_PropertyChnaged;

            d.DocName = "New Document";
            d.NeedSave = true;

            Console.ReadKey();
        }

        private static void D_PropertyChnaged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine($"Documen object property called {e.PropertyName} changed");
        }
    }
}
