using System;
using System.Security.Claims;
using static Zufallszahlengenerator;
class Event
{
    static void Main()
    {
        //  Create generator object
        Zufallszahlengenerator generator1 =
            new Zufallszahlengenerator("Generator 1");

        // Subscribe to event: number greater than 50
        generator1.Groesser50 += Generator1_Groesser50;

        //  Subscribe to event: even number
         generator1.Gerade += Generator1_Gerade;


        // Generate once
        generator1.Erzeugen();

        // Detach Gerade event
      //  generator1.Gerade -= Generator1_Gerade;

        Console.WriteLine("Gerade event handler detached.");

        //  Generate 5 random numbers
        for (int i = 0; i < 5; i++)
        {
            int number = generator1.Erzeugen();
            Console.WriteLine($"Generated number: {number}");
        }
    }

    // Event handler for numbers > 50
    private static void Generator1_Groesser50(object obj, ZahlEventArgs e)
    {
        Console.WriteLine($"Event Groesser50 triggered: {e.Zahl}");
    }

    // Event handler for even numbers
    private static void Generator1_Gerade(object obj, ZahlEventArgs e)
    {
        Console.WriteLine($"Event Gerade triggered: {e.Zahl}");
    }
}

class Zufallszahlengenerator
{
    static Random zufallsgenerator = new Random();
    public string Name { get; }

    // Delegate definitions (EventHandler style recommended)
    public delegate void ZahlEventHandler(int zahl);

    //  Events
    public event EventHandler<ZahlEventArgs> Groesser50;
    public event EventHandler<ZahlEventArgs> Gerade;
    public Zufallszahlengenerator(string name) => Name = name;
    public int Erzeugen()
    {
        int zufallszahl = zufallsgenerator.Next(1, 101);

        //  Trigger event if number > 50
        if (zufallszahl > 50)
        {
            Groesser50?.Invoke(this, new ZahlEventArgs(zufallszahl));
        }

        //  Trigger event if number is even
        if (zufallszahl % 2 == 0)
        {
            Gerade?.Invoke(this, new ZahlEventArgs(zufallszahl));
        }
        return zufallszahl;

    }
    // git pull test 2
    public class ZahlEventArgs : EventArgs
    {
        public int Zahl { get; }

        public ZahlEventArgs(int zahl)
        {
            Zahl = zahl;
        }
    }
}
