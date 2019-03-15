using System;
using System.Linq;
using Commons.Music.Midi;

namespace MidiConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var access = MidiAccessManager.Default;
            var output = access.OpenOutputAsync(access.Outputs.Last().Id).Result;
            var music = MidiMusic.Read(System.IO.File.OpenRead("../../../midi/avengers.mid"));
            var player = new MidiPlayer(music, output);
            int HowHardPressed = 0;
            string StrengthOfPress = "";

            player.PlayAsync();

            player.Dispose();


            
            

            if (access.Inputs.Count() > 0)
            {
                var input = access.OpenInputAsync(access.Inputs.Last().Id).Result;
                byte[] data = null;
                input.MessageReceived += (o, e) =>
                {
                    data = new byte[e.Length];
                    Array.Copy(e.Data, e.Start, data, 0, e.Length);



                    if (e.Data[1] != 0 && e.Data[0] == 144)
                    {

                        var note = e.Data[1] % 12 + 1;
                        string noteString = "N/A";
                        switch (note)
                        {
                            case 1:
                                noteString = "C";
                                break;
                            case 2:
                                noteString = "C#/D-";
                                break;
                            case 3:
                                noteString = "D";
                                break;
                            case 4:
                                noteString = "D#/E-";
                                break;
                            case 5:
                                noteString = "E";
                                break;
                            case 6:
                                noteString = "F";
                                break;
                            case 7:
                                noteString = "F#/G-";
                                break;
                            case 8:
                                noteString = "G";
                                break;
                            case 9:
                                noteString = "G#/A-";
                                break;
                            case 10:
                                noteString = "A";
                                break;
                            case 11:
                                noteString = "A#/B-";
                                break;
                            case 12:
                                noteString = "B";
                                break;
                            default:
                                noteString = "N/A";
                                break;
                        }
                        int octave = e.Data[1] / 12;
                        if (e.Data[1] != 0 && e.Data[0] != 144)
                        {
                            Console.Write("Not A NOTE?!?!?!?!?!?!?!??!?!!?!?");
                            Console.Write($"Note Played: {noteString}\t Octave Played: {octave.ToString()}\t");
                            Console.Write($"Data[0]: {e.Data[0]}\t");
                            Console.Write($"Note #: {e.Data[1]}\t");
                        }
                        else
                        {
                            /*

                   
                            Console.Write($"Note Played: {noteString}\t Octave Played: {octave.ToString()}\t");
                            Console.Write($"Data[0]: {e.Data[0]}\t");
                            Console.Write($"Note #: {e.Data[1]}\t");
                            Console.WriteLine($"Data[2]: {e.Data[2]}");
                            */
                            HowHardPressed = e.Data[2];
                            if (HowHardPressed < 1)
                            {
                                StrengthOfPress = "StoppedPressing";
                            }
                            if (HowHardPressed > 0)
                            {
                                StrengthOfPress = "Barely";
                            }
                            if (HowHardPressed > 30)
                            {
                                StrengthOfPress = "Softly";
                            }
                            if (HowHardPressed > 55)
                            {
                                StrengthOfPress = "a healthy ammount";
                            }
                            if (HowHardPressed > 80)
                            {
                                StrengthOfPress = "Hard";
                            }
                            if (HowHardPressed > 110)
                            {
                                StrengthOfPress = "Jesus Why ?!?!";
                            }
                            if (HowHardPressed != 0)
                            {
                                Console.WriteLine("You played: " + noteString + " " + octave.ToString() + "th octave " + StrengthOfPress);
                            }
                        }


                    }
                };
            }
            while (true) ;
        }
    }
}
