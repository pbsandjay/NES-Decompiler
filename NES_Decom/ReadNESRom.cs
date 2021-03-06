using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace NES_Decom
{
    class ReadNESRom
    {
        /// <summary>
        /// Reads the Provided NES ROM and Text file to be iterated through and sends translations to text file.
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="outputName"></param>
        /// <param name="romName"></param>
        public unsafe static void NESRom(string fileName, string romName, string ext, string outputName)
        {

            //the file check logic is EXTREMELY messy. I'll touch it up later™
            //string fileExt = ".nes";

            /*while (!ext.Equals(fileExt) == true && trueNESFile == false)
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
            */

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
                //bool NESFormat = false;




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
                        data.WriteLine("{0} uses Horizontal Mirroring", romName); Console.WriteLine("{0} uses Horizontal Mirroring", romName);

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

                    //Trying a new way to test file checking logic... Kind of. Will mature over time. 
                    //I might need to take Bikini Bottom and push it somewhere else so it's not just randomly placed within ROM flag scraping code. 
                    bool iNESFormat = false;
                    if (Convert.ToChar(byteArray[0]) == 'N' && Convert.ToChar(byteArray[1]) == 'E' && Convert.ToChar(byteArray[2]) == 'S' && byteArray[3] == 0x1A)
                    {
                        data.WriteLine("This uses the iNES 1.0 ROM header"); Console.WriteLine("This uses the iNES 1.0 ROM header");
                        iNESFormat = true;
                    }
                    if (iNESFormat == true && (byteArray[7] & 0x0c) == 0x08)
                    {
                        data.WriteLine("This uses the iNES 2.0 ROM header"); Console.WriteLine("This uses the iNES 2.0 ROM header");
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
