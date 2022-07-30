// See https://aka.ms/new-console-template for more information

using DataStructures.Examples;
using System.Numerics;

public class Program
{
    static void Main()
    {
        int[] arr = { -4, -34, 6, 78, 0, -53, 193 };


        CircularLinkedList<int> cl = new CircularLinkedList<int>();

        foreach (var a in arr)
        {
            cl.AddLast(a);
        }

        var data = cl.GetEnumerator();
        int i = 0;
        while (true)
        {
            Console.WriteLine(data.Current);

            data.MoveNext();

            if (i > 10)
                break;
            
            i++;
        }
    }




    static bool IsPalindrome(string word)
    {
        word = word.Replace(" ", "");
        //Console.WriteLine(word);
        var cs = word.ToCharArray();
      
        int j = cs.Length-1;
        for (int i = 0; i < j; i++)
        {

            var temp = cs[i];
            
            var c = cs[j];

            if (temp != c)
                return false;


            j--;

        }


        return true;
    }

    //
}



public class GenericArrray<T>
{

    T[] a;
    public GenericArrray(T[] x)
    {
        a = x;
    }

    public T GetData(int index)=> a [index];

    public void Print() => a.ToList().ForEach(a => Console.WriteLine(a));

    public void Reverse()
    {
        int j = a.Length-1;

        for (int i = 0; i < j; i++)
        {
            T temp;
            temp = a [i];
            a[i] = a[j];
            a[j] = temp;
            j--;
        }
    }

}


public class Animal
{
    long lifeSpan;
    float weight;

    public Animal(long lifeSpan, float weight)
    {
        this.lifeSpan = lifeSpan;
        this.weight = weight;
    }

    public void Print()
    {
        Console.WriteLine("Maximum longevity: " + lifeSpan + " in years");
        Console.WriteLine("Maximum weight: " + weight + " in kgs");
    }
}

class Aquatic : Animal
{
    bool scale;
    public Aquatic(long years,float kg,Boolean skin):base(years,kg)
    {
        this.scale = skin;
    }

    public void Print()
    {
        base.Print();
        Console.WriteLine("Has Scales? " + scale);
    }
}

class Land : Animal 
{
    short vision;
    public Land(long years,float kg, short vision) : base(years, kg)
    {
        this.vision = vision;
    }

}


//upper bound generic
public class AnimalWorld<T> where T : Animal
{
    //type parameter is limited to Animal and its sub classes
    T[] listOfAnimals;

    public AnimalWorld(T[] listOfAnimals)
    {
        this.listOfAnimals = listOfAnimals;
    }

    public static void Vitality(AnimalWorld<T> animals)
    {
        foreach (var a in animals.listOfAnimals)
        {
            a.Print();
        }
    }
}


