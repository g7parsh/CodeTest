using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
"Find the year with most people alive"
The idea is that you get an input of people, in this case randomly, and then you keep track of the Delta in the population
When more people are born rather than dead, the delta goes up and vice-versa.  
The point where the sum of deltas is highest is when the largest amount of people are alive
 */
namespace SciGamesTestCS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Number of people");
            int NumPeople;
            NumPeople = Convert.ToInt32(Console.ReadLine());
            List<Person> people = new List<Person>();
            Random Random = new Random();
            //Years only span from 1900 - 2000
            int[] PopDelta = new int[101];
            for (int i = 0; i < NumPeople; i++)
            {
                Person temp = new Person(Random);
                people.Add(temp);
            }
            foreach (Person p in people)
            {
                PopDelta[p.BirthYear - 1900] += 1;

                PopDelta[p.DeathYear - 1900] -= 1;
                //Console.WriteLine(p);
            }
            int MaxPop = 0;
            int MaxYear = 0;
            int Pop = 0;
            for(int i = 0; i <101; i++)
            {
                Pop += PopDelta[i];
                if (Pop > MaxPop)
                {
                    MaxPop = Pop;
                    MaxYear = i;
                }

            }
            Console.WriteLine("Max Year = {0} with {1} people",MaxYear+1900,MaxPop);
            Console.ReadKey();
        }
    }
    class Person
    {
        // Generate Random People;
        public int BirthYear = 0;
        public int DeathYear = 0;
        public Person(Random seed)
        {
            BirthYear = seed.Next(1900, 2001);
            DeathYear = seed.Next(BirthYear,2001 );
           /* if (DeathYear > 2000)
            {
                this.DeathYear = 2000;
            }*/
        }

        public override string ToString()
        {
            return "Person was born " + BirthYear.ToString() + " And died " + DeathYear.ToString() + '\n';
        }
    }
}
