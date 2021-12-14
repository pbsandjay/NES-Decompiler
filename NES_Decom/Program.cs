using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NES_Decom
{
    class Program
    {
        static void Main(string[] args)
        {


            bool restart = true;
            while (restart == true)
            {
                Console.WriteLine("Create a text file somewhere then drag and drop that text file into here and press enter...");
                string outputName = Console.ReadLine();
                string extOut = Path.GetExtension(outputName);


                //TODO: Try to rename the input TXT file to <gamename>_disassembled.txt

                string extCheck = ".txt";
                bool trueTXTFile = false;

                while (!extOut.Equals(extCheck) == true && trueTXTFile == false)
                {
                    Console.WriteLine("Not an TEXT file! Try again!");
                    if (trueTXTFile == false)
                    {
                        trueTXTFile = false;
                        Console.WriteLine("Drag and drop your TEXT file in this text box then press enter...");
                        outputName = Console.ReadLine();
                        extOut = Path.GetExtension(outputName);
                        extCheck = ".txt";
                    }

                    else if (extOut.Equals(extCheck))
                    {
                        trueTXTFile = true;
                    }

                }
                Console.WriteLine("Drag and drop your ROM file into this console window...\n");
                string fileName = Console.ReadLine();
                string romName = Path.GetFileName(fileName);
                string ROMext = Path.GetExtension(fileName);
                Console.WriteLine(ROMext);
                bool ROMFile = false;
                while (ROMFile == false)
                {
                    if (ROMext == ".nes")
                    {                        
                        //string fileName, string romName, string ext, string outputName
                        ReadNESRom.NESRom(fileName, romName, ROMext, outputName);
                        ROMFile = true;
                    }

                    if (ROMext == ".gb")
                    {
                        //GBRom();
                        ROMFile = true;

                    }

                    if (ROMext != ".nes" && ROMext != ".gb")
                    {
                        Console.WriteLine("You have not selected a compatible ROM file, try again!\n");
                        Console.WriteLine("Would you like to try again? Y/N");
                        string userSelection = Console.ReadLine();
                        if (userSelection == "Y")
                        {
                            ROMFile = false;
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                    }
                                        using (StreamWriter ending = File.AppendText(outputName))
                    {
                        Console.WriteLine("-----------------------END OF FILE-----------------------");
                        ending.WriteLine("-----------------------END OF FILE-----------------------");

                    }

                    Console.WriteLine("Decompiling done. This SHOULD have stripped CHR data. A few bytes might have slipped through the cracks.\nCheck your ouput text for the full code.\n");


                    /*string TextFileName = Path.GetFileName(outputName);
                    Console.WriteLine(TextFileName);
                    string RenamedFile = (TextFileName + "_Disassembled");
                    if (File.Exists(outputName))
                    {
                        File.Move(outputName, RenamedFile);
                    }
                    */

                    Console.WriteLine("Would you like to convert another ROM file? (Y/N) ");
                    string UserContinue = Console.ReadLine();
                    if (UserContinue == "Y" || UserContinue == "y")
                    {
                        restart = true;
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                    
                }

            }
        }
    }
}
