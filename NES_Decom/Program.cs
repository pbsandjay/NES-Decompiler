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

            //the file check logic is EXTREMELY messy. I'll touch it up later™
            Console.WriteLine("Drag and drop your NES ROM file in this text box then press enter...");
            string fileName = Console.ReadLine();
            string ext = Path.GetExtension(fileName);
            string romName = Path.GetFileName(fileName);
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

            


            Console.WriteLine("Create a text file somewhere then drag and drop that text file into here and press enter...");
            string outputName = Console.ReadLine();
            //string path = Path.GetDirectoryName(outputName);
            //Console.WriteLine(path);
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

            ReadRom(fileName, outputName, romName);

            Console.WriteLine("Decompiling done. This SHOULD have stripped CHR data. A few bytes might have slipped through the cracks.\nCheck your ouput text for the full code.\nPress enter to close");
            Console.Read();
        }

        private unsafe static void ReadRom(string fName, string outputName, string romName)
        {
            string textName = outputName;
            string nesName = romName;
            using (FileStream fs = new FileStream(fName, FileMode.Open))
 
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


                using (var data = new StreamWriter(outputName, true))
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
