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
                        ReadNESRom(fileName, romName, ROMext, outputName);
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


                    Console.WriteLine("Decompiling done. This SHOULD have stripped CHR data. A few bytes might have slipped through the cracks.\nCheck your ouput text for the full code.\n");
                    using (StreamWriter ending = File.AppendText(outputName))
                    {
                        Console.WriteLine("-----------------------END OF FILE-----------------------");
                        ending.WriteLine("-----------------------END OF FILE-----------------------");

                    }
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



        /// <summary>
        /// Reads the Provided NES ROM and Text file to be iterated through and sends translations to text file.
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="outputName"></param>
        /// <param name="romName"></param>
        private unsafe static void ReadNESRom(string fileName, string romName, string ext, string outputName)
        {

            //the file check logic is EXTREMELY messy. I'll touch it up later™
            string fileExt = ".nes";
            bool trueNESFile = false;

            while (!ext.Equals(fileExt) == true && trueNESFile == false)
            {
                Console.WriteLine("Not an NES file! Try again!");
                if (trueNESFile == false)
                {
                    trueNESFile = false;
                    Console.WriteLine("Drag and drop your ROM file in this text box then press enter...");
                    fileName = Console.ReadLine();
                    ext = Path.GetExtension(fileName);
                    romName = Path.GetFileName(fileName);
                    fileExt = ".nes";
                }

                else if (ext.Equals(fileExt))
                {
                    trueNESFile = true;
                }
            }

            string textName = outputName;
            string nesName = romName;
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
 
            {
                

                IList<byte> hexBuffer = new List<byte>();
                int hex;

                fs.Seek(0, SeekOrigin.Current);


                for (int i = 0; i < fs.Length; i++)
                {
                    hex = fs.ReadByte();
                    hexBuffer.Add((byte)hex);
                }

                byte[] byteArray = hexBuffer.ToArray();

                int NESheader = 16; //size of the iNES Header. 16 bytes (0x10 OR 10h)
                int defaultPRG = 16384; //default size of the PRG ROM data, increases by a multiplication of ROM[4] (list how many PRG banks there are at this area of ROM)
                int defaultCHR = 8192; //default size of the CHR ROM data, increases by a multiplication of ROM[5] (list how many CHR banks there are at this area of CHR)

                byte PRGLoc = byteArray[4]; //takes this number and multiplies it by defaultPRG to get the size of the Program Data.
                byte CHRLoc = byteArray[5]; //takes the number stored at this index and multiplies it by the defaultCHR to get the Character Data.

                int PRGSize = defaultPRG * PRGLoc; //get the size in bytes of the PRG 
                int CHRSize = defaultCHR * CHRLoc; //get the size in bytes of the CHR


                int flag6 = byteArray[6];
                string flag6Convert = Convert.ToString(flag6, 2);

                char[] flag6Char = flag6Convert.ToCharArray();

                using (var data = new StreamWriter(outputName, false))
                {


                    if (flag6Char[4] == '0')
                    {
                        data.WriteLine("{0} uses Horizontal Mirroring", romName);  Console.WriteLine("{0} uses Horizontal Mirroring", romName);
                        
                    }
                    else
                    {
                        data.WriteLine("{0} uses Vertical Mirroring", romName); Console.WriteLine("{0} uses Vertical Mirroring", romName);
                    }

                    if (flag6Char[3] == '0')
                    {
                        data.WriteLine("{0} does not use SRAM", romName); Console.WriteLine("{0} does not use SRAM", romName);
                    }
                    else
                    {
                        data.WriteLine("{0} uses SRAM", romName); Console.WriteLine("{0} uses SRAM", romName);
                    }

                    if (flag6Char[2] == '0')
                    {
                        data.WriteLine("{0} does not use a trainer", romName); Console.WriteLine("{0} does not use a trainer", romName);
                    }
                    else
                    {
                        data.WriteLine("{0} uses a trainer", romName); Console.WriteLine("{0} uses a trainer", romName);
                    }

                    if (flag6Char[1] == '0')
                    {
                        data.WriteLine("{0} does not use four-screen VRAM", romName); Console.WriteLine("{0} does not use four-screen VRAM", romName);
                    }
                    else
                    {
                        data.WriteLine("{0} uses four-screen VRAM", romName); Console.WriteLine("{0} uses four-screen VRAM", romName);

                    }



                    data.WriteLine("The full size of {0} is {1}KB", romName, (byteArray.Length / 1024)); Console.WriteLine("The full size of {0} is {1}KB", romName, (byteArray.Length / 1024)); //testing for overall ROM size. 
                    data.WriteLine("The size of the {0} PRG ROM is {1}KB", romName, (byteArray.Length - (CHRSize + NESheader)) / 1024); Console.WriteLine("The size of the NES PRG ROM is {0}KB", (byteArray.Length - (CHRSize + NESheader)) / 1024); //testing for PRG ROM size with the size of the header (0x10) removed from the overall size
                    data.WriteLine("The size of the {0} CHR ROM is {1}KB\n", romName, (byteArray.Length - (PRGSize + NESheader)) / 1024); Console.WriteLine("The size of the NES CHR ROM is {0}KB\n", (byteArray.Length - (PRGSize + NESheader)) / 1024); //testing for CHR ROM size with the size of the header removed from the overall size
                    data.AutoFlush = true;
                }



                fixed (byte* ToArrayBytes = byteArray)
                {
                    NESDisassemble nes = new NESDisassemble();

                    int pc = NESheader; //we want to start the PC at where the the header ends. 

                    while (pc < byteArray.Length - (CHRSize + pc))  //16 -> end of PRG ROM
                    {
                        pc += nes.Disassembler(ToArrayBytes, pc, textName);
                    }

                }
            }
        }
    }
}
