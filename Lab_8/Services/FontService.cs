using System;
using System.Collections.Generic;
using Lab_8.Models;
namespace Lab_8.Services{
    public class FontService{
        private Dictionary<char, Glyph8x10> letters = new Dictionary<char, Glyph8x10>();
        public FontService(){
            Add('A', "7E 81 81 FF 81 81 81 81 00 00");
            Add('B', "FE 81 81 FE 81 81 81 FE 00 00");
            Add('C', "7E 81 80 80 80 80 81 7E 00 00");
            Add('D', "FC 82 81 81 81 81 82 FC 00 00");
            Add('E', "FF 80 80 FC 80 80 80 FF 00 00");
            Add('F', "FF 80 80 FC 80 80 80 80 00 00");
            Add('G', "7E 81 80 8F 81 81 81 7E 00 00");
            Add('H', "81 81 81 FF 81 81 81 81 00 00");
            Add('I', "7E 18 18 18 18 18 18 7E 00 00");
            Add('J', "1F 04 04 04 04 84 84 78 00 00");
            Add('K', "82 84 88 F0 88 84 82 81 00 00");
            Add('L', "80 80 80 80 80 80 80 FF 00 00");
            Add('M', "81 C3 A5 99 81 81 81 81 00 00");
            Add('N', "81 C1 A1 91 89 85 83 81 00 00");
            Add('O', "7E 81 81 81 81 81 81 7E 00 00");
            Add('P', "FE 81 81 FE 80 80 80 80 00 00");
            Add('Q', "7E 81 81 81 89 85 82 7D 00 00");
            Add('R', "FE 81 81 FE 88 84 82 81 00 00");
            Add('S', "7F 80 80 7E 01 01 01 FE 00 00");
            Add('T', "FF 18 18 18 18 18 18 18 00 00");
            Add('U', "81 81 81 81 81 81 81 7E 00 00");
            Add('V', "81 81 81 81 42 42 24 18 00 00");
            Add('W', "81 81 81 81 99 A5 C3 81 00 00");
            Add('X', "81 42 24 18 18 24 42 81 00 00");
            Add('Y', "81 42 24 18 18 18 18 18 00 00");
            Add('Z', "FF 02 04 08 10 20 40 FF 00 00");

            Add('0', "7E 81 83 85 89 91 A1 7E 00 00");
            Add('1', "18 38 18 18 18 18 18 7E 00 00");
            Add('2', "7E 81 01 06 18 60 80 FF 00 00");
            Add('3', "FE 01 01 3E 01 01 01 FE 00 00");
            Add('4', "86 86 86 FF 06 06 06 06 00 00");
            Add('5', "FF 80 80 FE 01 01 01 FE 00 00");
            Add('6', "7E 80 80 FE 81 81 81 7E 00 00");
            Add('7', "FF 01 02 04 08 10 10 10 00 00");
            Add('8', "7E 81 81 7E 81 81 81 7E 00 00");
            Add('9', "7E 81 81 7F 01 01 01 7E 00 00");

            Add(' ', "00 00 00 00 00 00 00 00 00 00");
            Add('!', "18 18 18 18 18 00 18 00 00 00");
            Add('-', "00 00 00 7E 00 00 00 00 00 00");
            Add('.', "00 00 00 00 00 00 18 18 00 00");
        }
        private void Add(char symbol, string hex){
            string[] parts = hex.Split(' ');
            byte[] data = new byte[10];
            for (int i = 0; i < 10; i++)
                data[i] = Convert.ToByte(parts[i], 16);
            Glyph8x10 glyph = new Glyph8x10();
            glyph.SetBytes(data);
            letters[symbol] = glyph;
        }
        public Glyph8x10 GetGlyph(char symbol){
            symbol = char.ToUpperInvariant(symbol);
            if (letters.ContainsKey(symbol))
                return letters[symbol];
            return letters[' '];
        }
        public void SaveGlyph(char symbol, Glyph8x10 glyph){
            letters[char.ToUpperInvariant(symbol)] = glyph;
        }
    }
}
