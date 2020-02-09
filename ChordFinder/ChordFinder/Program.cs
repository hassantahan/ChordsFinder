// I am planning on adding different modes + making the scales have one of every letter.

using System;

namespace ChordFinder
{
    class Program
    {
        //This will probably be removed for a more dynamic way of creating the scale.
        static string[] sChords = { "Ab", "A", "Bb", "B", "C", "C#", "D", "Eb", "E", "F", "F#", "G" };

        static void Main(string[] args)
        {
            string key, mode;
            int[] nChords = new int[7];

            //Get key
        Start:
            Console.Write("Enter key (type 'Exit' to exit): ");
            key = Console.ReadLine();

            //Check if input is exit
            if (key.ToUpper() == "EXIT" || key == null)
                Environment.Exit(1);
            else if (key[0] > 71 || key[0] < 65)
            {
                Console.WriteLine("Invalid key, try again.");
                goto Start;
            }

            //Get mode
        ModeStart:
            Console.Write("Enter mode ('M' for major, 'm' for minor): ");
            mode = Console.ReadLine();

            //Checks if input is 'M' or 'm'
            if (mode[0] != 77 && mode[0] != 109)
            {
                Console.WriteLine("Invalid mode, try again.");
                goto ModeStart;
            }

            string a = "";

            if (mode[0] == 77)
                a = GetScale(key, 0, false);
            else
                a = GetScale(key, 1, false);

            Console.WriteLine("The chords are: " + a);
            Console.WriteLine();
            goto Start;
        }

        //Gets the scale. Note: flatted not developed yet.
        static string GetScale(string key, int mode, bool flatted)
        {
            string a = string.Empty;
            int keyInt;

            //Set the first chord.
            a += key;
            if (mode == 1)
                a += "m";
            a += " ";

            //Set point to sChords array.
            keyInt = FindKey(key);

            for (int i = 1; i < 7; i++)
                a += sChords[IndexNumberForChordArray(ref keyInt, mode, i)] + AdditionsToChord(mode, i) + " ";

            return a;
        }

        //Returns an integer that corresponds to the
        //index of the sChords array.
        static int FindKey(string key)
        {
            switch (key)
            {
                case "G#":
                case "Ab":
                    return 0;
                case "A":
                    return 1;
                case "A#":
                case "Bb":
                    return 2;
                case "B":
                    return 3;
                case "C":
                    return 4;
                case "C#":
                case "Db":
                    return 5;
                case "D":
                    return 6;
                case "D#":
                case "Eb":
                    return 7;
                case "E":
                    return 8;
                case "F":
                    return 9;
                case "F#":
                case "Gb":
                    return 10;
                default:
                    return 11;
            }
        }   

        //Finds the index for the correct chord
        static int IndexNumberForChordArray(ref int start, int mode, int iteration)
        {
            if (mode == 0) 
            {
                //Major key algorithm
                start += 2;
                if (iteration == 3)
                    start -= 1;
                start %= 12;
            }
            else if (mode == 1)
            {
                //Minor key algorithm
                start += 2;
                if (iteration == 2 || iteration == 5)
                    start -= 1;
                start %= 12;
            }
            return start;
        }

        //Adds extra stuff depending on the mode and position in the scale.
        static string AdditionsToChord(int mode, int iteration)
        {
            string s = string.Empty;

            if (mode == 0)
            {
                //Major key algorithm
                if (iteration == 1 || iteration == 2 || iteration == 5)
                    s = "m";
                if (iteration == 6)
                    s = "°";
            }
            else if (mode == 1)
            {
                //Minor key algorithm
                if (iteration == 3 || iteration == 4)
                    s += "m";
                if (iteration == 1)
                    s += "°";
            }
            return s;
        }
    }
}
