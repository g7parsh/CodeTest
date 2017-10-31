using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


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
            //Output File for visuals
                 
            string path = Environment.CurrentDirectory + @"DataDisplay.txt";

            if (!File.Exists(path))
            {
                File.CreateText(path);
            }
            StreamWriter file = new StreamWriter(path);
           
            Console.WriteLine("Enter Number of people");
            int NumPeople;
            NumPeople = Convert.ToInt32(Console.ReadLine());
            Bitmap image = new Bitmap(100,NumPeople );
            
            
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
                file.WriteLine("{0}:{1}", 1900 + i, Pop);
                //DrawPipes(file,Pop,i);
                FillPixel(image, Pop, i);
                if (Pop > MaxPop)
                {

                    MaxPop = Pop;
                    
                    MaxYear = i;
                }

            }
            Console.WriteLine("Max Year = {0} with {1} people",MaxYear+1900,MaxPop);
            Bitmap TrimmedImage = new Bitmap(image, new Size(100,MaxPop));
            TrimmedImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
            TrimmedImage.Save(Environment.CurrentDirectory + "DataImage.bmp");

            image.Dispose();
            file.Close();
            Console.ReadKey();
        }
        public static void FillPixel(Bitmap image, int numPeople, int Year)
        {
            for (int i = 0; i < numPeople; i++)
            {
                if (i > 1000)
                {
                    image.SetPixel(Year, i, Color.Red);
                }
                else if (i > 100)
                {
                    image.SetPixel(Year, i, Color.Blue);
                }
                else if (i > 10)
                {
                    image.SetPixel(Year, i, Color.Yellow);
                }
                else
                {
                    image.SetPixel(Year, i, Color.Green);

                }
            }
                
            
        }
        public static void DrawPipes(StreamWriter file,int numPipes, int year)
        {
            file.Write(1900 + year);
            for (int i = 0; i < numPipes; i++)
            {
                file.Write('|');
            }
            file.Write('\n');

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
