using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NES_Decom
{
    class GBDisassemble
    {
        public unsafe int Disassembler(byte* codeBuffer, int pc, string outputName)
        {


            string fileName = outputName;

            byte* code = &codeBuffer[pc];

            int opBytes = 1; //the default length of the op



            using (var writer = new StreamWriter(fileName, true))
            {
                int lineCounter = 1;

                writer.AutoFlush = true;

                switch (*code)

                {
                    case 0x00: writer.WriteLine("BRK"); Console.WriteLine(lineCounter + "BRK"); break;
                    case 0x01: writer.WriteLine("ORA    (${0:X2}, X)", code[1]); Console.WriteLine("ORA    (${0:X2}, X)", code[1]); opBytes = 2; break;
                    case 0x05: writer.WriteLine("ORA    ${0:X2}", code[1]); Console.WriteLine("ORA    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0x06: writer.WriteLine("ASL    ${0:X2}", code[1]); Console.WriteLine("ASL    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0x08: writer.WriteLine("PHP"); Console.WriteLine("PHP"); break;
                    case 0x09: writer.WriteLine("ORA    #${0:X2}", code[1]); Console.WriteLine("ORA    #${0:X2}", code[1]); opBytes = 2; break;
                    case 0x0a: writer.WriteLine("ASL    A"); Console.WriteLine("ASL    A"); break;
                    case 0x0d: writer.WriteLine("ORA    ${0:X2}{1:X2}", code[2], code[1]); Console.WriteLine("ORA    ${0:X2}{1:X2}", code[2], code[1]); opBytes = 3; break;
                    case 0x0e: writer.WriteLine("ASL    ${0:X2}{1:X2}", code[2], code[1]); Console.WriteLine("ASL    ${0:X2}{1:X2}", code[2], code[1]); opBytes = 3; break;
                    case 0x10: writer.WriteLine("BPL    ${0:X2}", code[1]); Console.WriteLine("BPL    ${0:X2}", code[1]); break;
                    case 0x11: writer.WriteLine("ORA    (${0:X2}), Y", code[1]); Console.WriteLine("ORA    (${0:X2}), Y", code[1]); opBytes = 2; break;
                    case 0x15: writer.WriteLine("ORA    ${0:X2}, X", code[1]); Console.WriteLine("ORA    ${0:X2}, X", code[1]); opBytes = 2; break;
                    case 0x16: writer.WriteLine("ASL    ${0:X2}, X", code[1]); Console.WriteLine("ASL    ${0:X2}, X", code[1]); opBytes = 2; break;
                    case 0x18: writer.WriteLine("CLC"); Console.WriteLine("CLC"); break;
                    case 0x19: writer.WriteLine("ORA    ${0:X2}{1:X2}, Y", code[2], code[1]); Console.WriteLine("ORA    ${0:X2}{1:X2}, Y", code[2], code[1]); opBytes = 3; break;
                    case 0x1d: writer.WriteLine("ORA    ${0:X2}{1:X2}, X", code[2], code[1]); Console.WriteLine("ORA    ${0:X2}{1:X2}, X", code[2], code[1]); opBytes = 3; break;
                    case 0x1e: writer.WriteLine("ASL    ${0:X2}{1:X2}, X", code[2], code[1]); Console.WriteLine("ASL    ${0:X2}{1:X2}, X", code[2], code[1]); opBytes = 3; break;
                    case 0x20: writer.WriteLine("JSR    ${0:X2}{1:X2}", code[2], code[1]); Console.WriteLine("JSR    ${0:X2}{1:X2}", code[2], code[1]); opBytes = 3; break;
                    case 0x21: writer.WriteLine("AND    (${0:X2}, X)", code[1]); Console.WriteLine("AND    (${0:X2}, X)", code[1]); opBytes = 2; break;
                    case 0x24: writer.WriteLine("BIT    ${0:X2}", code[1]); Console.WriteLine("BIT    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0x25: writer.WriteLine("AND    ${0:X2}", code[1]); Console.WriteLine("AND    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0x26: writer.WriteLine("ROL    ${0:X2}", code[1]); Console.WriteLine("ROL    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0x28: writer.WriteLine("PLP"); Console.WriteLine("PLP"); break;
                    case 0x29: writer.WriteLine("AND    #${0:X2}", code[1]); Console.WriteLine("AND    #${0:X2}", code[1]); opBytes = 2; break;
                    case 0x2a: writer.WriteLine("ROL    A"); Console.WriteLine("ROL    A"); break;
                    case 0x2c: writer.WriteLine("BIT    ${0:X2}{1:X2}", code[2], code[1]); Console.WriteLine("BIT    ${0:X2}{1:X2}", code[2], code[1]); opBytes = 3; break;
                    case 0x2d: writer.WriteLine("AND    ${0:X2}{1:X2}", code[2], code[1]); Console.WriteLine("AND    ${0:X2}{1:X2}", code[2], code[1]); opBytes = 3; break;
                    case 0x2e: writer.WriteLine("ROL    ${0:X2}{1:X2}", code[2], code[1]); Console.WriteLine("ROL    ${0:X2}{1:X2}", code[2], code[1]); opBytes = 3; break;
                    case 0x30: writer.WriteLine("BMI    ${0:X2}", code[1]); Console.WriteLine("AND    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0x31: writer.WriteLine("AND    (${0:X2}), Y", code[1]); Console.WriteLine("AND    (${0:X2}), Y", code[1]); opBytes = 2; break;
                    case 0x35: writer.WriteLine("AND    ${0:X2}, X", code[1]); Console.WriteLine("AND    ${0:X2}, X", code[1]); opBytes = 2; break;
                    case 0x36: writer.WriteLine("ROL    ${0:X2}, X", code[1]); Console.WriteLine("ROL    ${0:X2}, X", code[1]); opBytes = 2; break;
                    case 0x38: writer.WriteLine("SEC"); Console.WriteLine("SEC"); break;
                    case 0x39: writer.WriteLine("AND    ${0:X2}{1:X2}, Y", code[2], code[1]); Console.WriteLine("AND    ${0:X2}{1:X2}, Y", code[2], code[1]); opBytes = 3; break;
                    case 0x3d: writer.WriteLine("AND    ${0:X2}{1:X2}, X", code[2], code[1]); Console.WriteLine("AND    ${0:X2}{1:X2}, X", code[2], code[1]); opBytes = 3; break;
                    case 0x3e: writer.WriteLine("ROL    ${0:X2}{1:X2}, X", code[2], code[1]); Console.WriteLine("ROL    ${0:X2}{1:X2}, X", code[2], code[1]); opBytes = 3; break;
                    case 0x40: writer.WriteLine("RTI"); Console.WriteLine("RTI"); break;
                    case 0x41: writer.WriteLine("EOR    (${0:X2}, X)", code[1]); Console.WriteLine("EOR    (${0:X2}, X)", code[1]); opBytes = 2; break;
                    case 0x45: writer.WriteLine("EOR    ${0:X2}", code[1]); Console.WriteLine("EOR    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0x46: writer.WriteLine("LSR    ${0:X2}", code[1]); Console.WriteLine("LSR    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0x48: writer.WriteLine("PHA"); Console.WriteLine("PHA"); break;
                    case 0x49: writer.WriteLine("EOR    #${0:X2}", code[1]); Console.WriteLine("EOR    #${0:X2}", code[1]); opBytes = 2; break;
                    case 0x4a: writer.WriteLine("LSR    A"); Console.WriteLine("LSR    A"); break;
                    case 0x4c: writer.WriteLine("JMP    ${0:X2}{1:X2}", code[2], code[1]); Console.WriteLine("JMP    ${0:X2}{1:X2}", code[2], code[1]); opBytes = 3; break;
                    case 0x4d: writer.WriteLine("EOR    ${0:X2}{1:X2}", code[2], code[1]); Console.WriteLine("EOR    ${0:X2}{1:X2}", code[2], code[1]); opBytes = 3; break;
                    case 0x4e: writer.WriteLine("LSR    ${0:X2}{1:X2}", code[2], code[1]); Console.WriteLine("LSR    ${0:X2}{1:X2}", code[2], code[1]); opBytes = 3; break;
                    case 0x50: writer.WriteLine("BVC    ${0:X2}", code[1]); Console.WriteLine("BVC    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0x51: writer.WriteLine("EOR    (${0:X2}), Y", code[1]); Console.WriteLine("EOR    (${0:X2}), Y", code[1]); opBytes = 2; break;
                    case 0x55: writer.WriteLine("EOR    ${0:X2}, X", code[1]); Console.WriteLine("EOR    ${0:X2}, X", code[1]); opBytes = 2; break;
                    case 0x56: writer.WriteLine("LSR    ${0:X2}, X", code[1]); Console.WriteLine("LSR    ${0:X2}, X", code[1]); opBytes = 2; break;
                    case 0x58: writer.WriteLine("CLI"); Console.WriteLine("CLI"); break;
                    case 0x59: writer.WriteLine("EOR    ${0:X2}{1:X2}, Y", code[2], code[1]); Console.WriteLine("EOR    ${0:X2}{1:X2}, Y", code[2], code[1]); opBytes = 3; break;
                    case 0x5d: writer.WriteLine("EOR    ${0:X2}{1:X2}, X", code[2], code[1]); Console.WriteLine("EOR    ${0:X2}{1:X2}, X", code[2], code[1]); opBytes = 3; break;
                    case 0x5e: writer.WriteLine("LSR    ${0:X2}{1:X2}, X", code[2], code[1]); Console.WriteLine("LSR    ${0:X2}{1:X2}, X", code[2], code[1]); opBytes = 3; break;
                    case 0x60: writer.WriteLine("RTS"); Console.WriteLine("RTS"); break;
                    case 0x61: writer.WriteLine("ADC    (${0:X2}, X)", code[1]); Console.WriteLine("ADC    (${0:X2}, X)", code[1]); opBytes = 2; break;
                    case 0x65: writer.WriteLine("ADC    ${0:X2}", code[1]); Console.WriteLine("ADC    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0x66: writer.WriteLine("ROR    ${0:X2}", code[1]); Console.WriteLine("ROR    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0x68: writer.WriteLine("PLA"); Console.WriteLine("PLA"); break;
                    case 0x69: writer.WriteLine("ADC    #${0:X2}", code[1]); Console.WriteLine("ADC    #${0:X2}", code[1]); opBytes = 2; break;
                    case 0x6a: writer.WriteLine("ROR    A"); Console.WriteLine("ROR    A"); break;
                    case 0x6c: writer.WriteLine("JMP    ${0:X2}", code[1]); Console.WriteLine("JMP    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0x6d: writer.WriteLine("ADC    ${0:X2}{1:X2}", code[2], code[1]); Console.WriteLine("ADC    ${0:X2}{1:X2}", code[2], code[1]); opBytes = 3; break;
                    case 0x6e: writer.WriteLine("ROR    ${0:X2}{1:X2}, X", code[2], code[1]); Console.WriteLine("ROR    ${0:X2}{1:X2}, X", code[2], code[1]); opBytes = 3; break;
                    case 0x70: writer.WriteLine("BVS    ${0:X2}", code[1]); Console.WriteLine("BVS    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0x71: writer.WriteLine("ADC    (${0:X2}), Y", code[1]); Console.WriteLine("ADC    (${0:X2}), Y", code[1]); opBytes = 2; break;
                    case 0x75: writer.WriteLine("ADC    ${0:X2}, X", code[1]); Console.WriteLine("ADC    ${0:X2}, X", code[1]); opBytes = 2; break;
                    case 0x76: writer.WriteLine("ROR    ${0:X2}, X", code[1]); Console.WriteLine("ROR    ${0:X2}, X", code[1]); opBytes = 2; break;
                    case 0x78: writer.WriteLine("SEI"); Console.WriteLine("SEI"); break;
                    case 0x79: writer.WriteLine("ADC    ${0:X2}{1:X2}, Y", code[2], code[1]); Console.WriteLine("ADC    ${0:X2}{1:X2}, Y", code[2], code[1]); opBytes = 3; break;
                    case 0x7d: writer.WriteLine("ADC    ${0:X2}{1:X2}, X", code[2], code[1]); Console.WriteLine("ADC    ${0:X2}{1:X2}, X", code[2], code[1]); opBytes = 3; break;
                    case 0x7e: writer.WriteLine("ROR    ${0:X2}{1:X2}", code[2], code[1]); Console.WriteLine("ROR    ${0:X2}{1:X2}", code[2], code[1]); opBytes = 3; break;
                    case 0x81: writer.WriteLine("STA    (${0:X2}, X)", code[1]); Console.WriteLine("STA    (${0:X2}, X)", code[1]); opBytes = 2; break;
                    case 0x84: writer.WriteLine("STY    ${0:X2}", code[1]); Console.WriteLine("STY    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0x85: writer.WriteLine("STA    ${0:X2}", code[1]); Console.WriteLine("STA    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0x86: writer.WriteLine("STX    ${0:X2}", code[1]); Console.WriteLine("STX    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0x88: writer.WriteLine("DEY"); Console.WriteLine("DEY"); break;
                    case 0x8a: writer.WriteLine("TXA"); Console.WriteLine("TXA"); break;
                    case 0x8c: writer.WriteLine("STY    ${0:X2}{1:X2}", code[2], code[1]); Console.WriteLine("STY    ${0:X2}{1:X2}", code[2], code[1]); opBytes = 3; break;
                    case 0x8d: writer.WriteLine("STA    ${0:X2}{1:X2}", code[2], code[1]); Console.WriteLine("STA    ${0:X2}{1:X2}", code[2], code[1]); opBytes = 3; break;
                    case 0x8e: writer.WriteLine("STX    ${0:X2}{1:X2}", code[2], code[1]); Console.WriteLine("STX    ${0:X2}{1:X2}", code[2], code[1]); opBytes = 3; break;
                    case 0x90: writer.WriteLine("BCC    ${0:X2}", code[1]); Console.WriteLine("BCC    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0x91: writer.WriteLine("STA    (${0:X2}), Y", code[1]); Console.WriteLine("STA    (${0:X2}), Y", code[1]); opBytes = 2; break;
                    case 0x94: writer.WriteLine("STY    ${0:X2}, X", code[1]); Console.WriteLine("ADC    ${0:X2}, X", code[1]); opBytes = 2; break;
                    case 0x95: writer.WriteLine("STA    ${0:X2}, X", code[1]); Console.WriteLine("STA    ${0:X2}, X", code[1]); opBytes = 2; break;
                    case 0x96: writer.WriteLine("STX    ${0:X2}, Y", code[1]); Console.WriteLine("STX    ${0:X2}, Y", code[1]); opBytes = 2; break;
                    case 0x98: writer.WriteLine("TYA"); Console.WriteLine("TYA"); break;
                    case 0x99: writer.WriteLine("STA    ${0:X2}{1:X2}, Y", code[2], code[1]); Console.WriteLine("STA    ${0:X2}{1:X2}, Y", code[2], code[1]); opBytes = 3; break;
                    case 0x9a: writer.WriteLine("TXS"); Console.WriteLine("TXS"); break;
                    case 0x9d: writer.WriteLine("STA    ${0:X2}{1:X2}, X", code[2], code[1]); Console.WriteLine("STA    ${0:X2}{1:X2}, X", code[2], code[1]); opBytes = 3; break;
                    case 0xa0: writer.WriteLine("LDY    #${0:X2}", code[1]); Console.WriteLine("LDY    #${0:X2}", code[1]); opBytes = 2; break;
                    case 0xa1: writer.WriteLine("LDA    (${0:X2}, X)", code[1]); Console.WriteLine("LDA    (${0:X2}, X)", code[1]); opBytes = 2; break;
                    case 0xa2: writer.WriteLine("LDX    #${0:X2}", code[1]); Console.WriteLine("LDX    #${0:X2}", code[1]); opBytes = 2; break;
                    case 0xa4: writer.WriteLine("LDY    ${0:X2}", code[1]); Console.WriteLine("LDY    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0xa5: writer.WriteLine("LDA    ${0:X2}", code[1]); Console.WriteLine("LDA    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0xa6: writer.WriteLine("LDX    ${0:X2}", code[1]); Console.WriteLine("LDX    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0xa8: writer.WriteLine("TAY"); Console.WriteLine("TAY"); break;
                    case 0xa9: writer.WriteLine("LDA    #${0:X2}", code[1]); Console.WriteLine("LDA    #${0:X2}", code[1]); opBytes = 2; break;
                    case 0xaa: writer.WriteLine("TAX"); Console.WriteLine("TAX"); break;
                    case 0xac: writer.WriteLine("LDY    ${0:X2}{1:X2}", code[2], code[1]); Console.WriteLine("LDY    ${0:X2}{1:X2}", code[2], code[1]); opBytes = 3; break;
                    case 0xad: writer.WriteLine("LDA    ${0:X2}{1:X2}", code[2], code[1]); Console.WriteLine("LDA    ${0:X2}{1:X2}", code[2], code[1]); opBytes = 3; break;
                    case 0xae: writer.WriteLine("LDX    ${0:X2}{1:X2}", code[2], code[1]); Console.WriteLine("LDX    ${0:X2}{1:X2}", code[2], code[1]); opBytes = 3; break;
                    case 0xb0: writer.WriteLine("BCS    ${0:X2}", code[1]); Console.WriteLine("BCS    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0xb1: writer.WriteLine("LDA    (${0:X2}), Y", code[1]); Console.WriteLine("LDA    (${0:X2}), Y", code[1]); opBytes = 2; break;
                    case 0xb4: writer.WriteLine("LDY    ${0:X2}, X", code[1]); Console.WriteLine("LDY    ${0:X2}, X", code[1]); opBytes = 2; break;
                    case 0xb5: writer.WriteLine("LDA    ${0:X2}, X", code[1]); Console.WriteLine("LDA    ${0:X2}, X", code[1]); opBytes = 2; break;
                    case 0xb6: writer.WriteLine("LDX    ${0:X2}, Y", code[1]); Console.WriteLine("LDX    ${0:X2}, Y", code[1]); opBytes = 2; break;
                    case 0xb8: writer.WriteLine("CLV"); Console.WriteLine("CLV"); break;
                    case 0xb9: writer.WriteLine("LDA    ${0:X2}{1:X2}, Y", code[2], code[1]); Console.WriteLine("LDA    ${0:X2}{1:X2}, Y", code[2], code[1]); opBytes = 3; break;
                    case 0xba: writer.WriteLine("TSX"); Console.WriteLine("TSX"); break;
                    case 0xbc: writer.WriteLine("LDY    ${0:X2}{1:X2}, X", code[2], code[1]); Console.WriteLine("LDY    ${0:X2}{1:X2}, X", code[2], code[1]); opBytes = 3; break;
                    case 0xbd: writer.WriteLine("LDA    ${0:X2}{1:X2}, X", code[2], code[1]); Console.WriteLine("LDA    ${0:X2}{1:X2}, X", code[2], code[1]); opBytes = 3; break;
                    case 0xbe: writer.WriteLine("LDX    ${0:X2}{1:X2}, Y", code[2], code[1]); Console.WriteLine("LDX    ${0:X2}{1:X2}, Y", code[2], code[1]); opBytes = 3; break;
                    case 0xc0: writer.WriteLine("CPY    #${0:X2}", code[1]); Console.WriteLine("CPY    #${0:X2}", code[1]); opBytes = 2; break;
                    case 0xc1: writer.WriteLine("CMP    (${0:X2}, X)", code[1]); Console.WriteLine("CMP    (${0:X2}, X)", code[1]); opBytes = 2; break;
                    case 0xc4: writer.WriteLine("CPY    ${0:X2}", code[1]); Console.WriteLine("CPY    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0xc5: writer.WriteLine("CMP    ${0:X2}", code[1]); Console.WriteLine("CMP    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0xc6: writer.WriteLine("DEC    ${0:X2}", code[1]); Console.WriteLine("DEC    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0xc8: writer.WriteLine("INY"); Console.WriteLine("INY"); break;
                    case 0xc9: writer.WriteLine("CMP    #${0:X2}", code[1]); Console.WriteLine("CMP    #${0:X2}", code[1]); opBytes = 2; break;
                    case 0xca: writer.WriteLine("DEX"); Console.WriteLine("DEX"); break;
                    case 0xcc: writer.WriteLine("CPY    ${0:X2}{1:X2}", code[2], code[1]); Console.WriteLine("CPY    ${0:X2}{1:X2}", code[2], code[1]); opBytes = 3; break;
                    case 0xcd: writer.WriteLine("CMP    ${0:X2}{1:X2}", code[2], code[1]); Console.WriteLine("CMP    ${0:X2}{1:X2}", code[2], code[1]); opBytes = 3; break;
                    case 0xce: writer.WriteLine("DEC    ${0:X2}{1:X2}", code[2], code[1]); Console.WriteLine("DEC    ${0:X2}{1:X2}", code[2], code[1]); opBytes = 3; break;
                    case 0xd0: writer.WriteLine("BNE    ${0:X2}", code[1]); Console.WriteLine("BNE    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0xd1: writer.WriteLine("CMP    (${0:X2}), Y", code[1]); Console.WriteLine("CMP    (${0:X2}), Y", code[1]); opBytes = 2; break;
                    case 0xd5: writer.WriteLine("CMP    ${0:X2}, X", code[1]); Console.WriteLine("CMP    ${0:X2}, X", code[1]); opBytes = 2; break;
                    case 0xd6: writer.WriteLine("DEC    ${0:X2}, X", code[1]); Console.WriteLine("DEC    ${0:X2}, X", code[1]); opBytes = 2; break;
                    case 0xd8: writer.WriteLine("CLD"); Console.WriteLine("CLD"); break;
                    case 0xd9: writer.WriteLine("CMP    ${0:X2}{1:X2}, Y", code[2], code[1]); Console.WriteLine("CMP    ${0:X2}{1:X2}, Y", code[2], code[1]); opBytes = 3; break;
                    case 0xdd: writer.WriteLine("CMP    ${0:X2}{1:X2}, X", code[2], code[1]); Console.WriteLine("CMP    ${0:X2}{1:X2}, X", code[2], code[1]); opBytes = 3; break;
                    case 0xde: writer.WriteLine("DEC    ${0:X2}{1:X2}, X", code[2], code[1]); Console.WriteLine("DEC    ${0:X2}{1:X2}, X", code[2], code[1]); opBytes = 3; break;
                    case 0xe0: writer.WriteLine("CPX    #${0:X2}", code[1]); Console.WriteLine("CPX    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0xe1: writer.WriteLine("SBC    (${0:X2}, X)", code[1]); Console.WriteLine("SBC    (${0:X2}, X)", code[1]); opBytes = 2; break;
                    case 0xe4: writer.WriteLine("CPX    ${0:X2}", code[1]); Console.WriteLine("CPX    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0xe5: writer.WriteLine("SPC    ${0:X2}", code[1]); Console.WriteLine("SPC    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0xe6: writer.WriteLine("INC    ${0:X2}", code[1]); Console.WriteLine("INC    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0xe8: writer.WriteLine("INX"); Console.WriteLine("INX"); break;
                    case 0xe9: writer.WriteLine("SBC    #${0:X2}", code[1]); Console.WriteLine("SBC    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0xea: writer.WriteLine("NOP"); Console.WriteLine("NOP"); break;
                    case 0xec: writer.WriteLine("CPX    ${0:X2}{1:X2}", code[2], code[1]); Console.WriteLine("CPX    ${0:X2}{1:X2}", code[2], code[1]); opBytes = 3; break;
                    case 0xed: writer.WriteLine("SBC    ${0:X2}{1:X2}", code[2], code[1]); Console.WriteLine("SBC    ${0:X2}{1:X2}", code[2], code[1]); opBytes = 3; break;
                    case 0xee: writer.WriteLine("INC    ${0:X2}{1:X2}", code[2], code[1]); Console.WriteLine("INC    ${0:X2}{1:X2}", code[2], code[1]); opBytes = 3; break; //https://www.youtube.com/watch?v=RINq7amRdzM
                    case 0xf0: writer.WriteLine("BEQ    ${0:X2}", code[1]); Console.WriteLine("BEQ    ${0:X2}", code[1]); opBytes = 2; break;
                    case 0xf1: writer.WriteLine("SBC    (${0:X2}), Y", code[1]); Console.WriteLine("SBC    (${0:X2}), Y", code[1]); opBytes = 2; break;
                    case 0xf5: writer.WriteLine("SBC    ${0:X2}, X", code[1]); Console.WriteLine("SBC    ${0:X2}, X", code[1]); opBytes = 2; break;
                    case 0xf6: writer.WriteLine("INC    ${0:X2}, X", code[1]); Console.WriteLine("INC    ${0:X2}, X", code[1]); opBytes = 2; break;
                    case 0xf8: writer.WriteLine("SED"); Console.WriteLine("SED"); break;
                    case 0xf9: writer.WriteLine("SBC    ${0:X2}{1:X2}, Y", code[2], code[1]); Console.WriteLine("SBC    ${0:X2}{1:X2}, Y", code[2], code[1]); opBytes = 3; break;
                    case 0xfd: writer.WriteLine("SBC    ${0:X2}{1:X2}, X", code[2], code[1]); Console.WriteLine("SBC    ${0:X2}{1:X2}, X", code[2], code[1]); opBytes = 3; break;
                    case 0xfe: writer.WriteLine("INC    ${0:X2}{1:X2}, X", code[2], code[1]); Console.WriteLine("INC    ${0:X2}{1:X2}, X", code[2], code[1]); opBytes = 3; break;




                        //https://www.youtube.com/watch?v=jSwV-4qs3mM

                }
            }

                return opBytes;
            }
        }
}
