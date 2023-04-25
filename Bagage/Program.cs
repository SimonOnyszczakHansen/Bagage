namespace Bagage
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create some bagage objects
            Bagage bagage1 = new Bagage("Denmark", 12);
            Bagage bagage2 = new Bagage("Sverige", 10);
            Bagage bagage3 = new Bagage("Norge", 5);

            //Create the object for sortingSystem
            Sorting sorting = new Sorting();

            //Create the 3 different terminals
            Terminal denmarkTerminal = new Terminal("Denmark");
            Terminal sverigeTerminal = new Terminal("Sverige");
            Terminal norgeTerminal = new Terminal("Norge");

            //Create the 3 different counters
            Counter counter1 = new Counter(1);
            Counter counter2 = new Counter(2);
            Counter counter3 = new Counter(3);
        }
    }
}