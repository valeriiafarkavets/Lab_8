using System;
namespace Lab_8.Models{
    public class Glyph8x10{
        private byte[] rows = new byte[10];
        public bool GetPixel(int x, int y){
            if (x < 0 || x >= 8 || y < 0 || y >= 10)
                return false;
            int mask = 1 << (7 - x);
            return (rows[y] & mask) != 0;
        }
        public void SetPixel(int x, int y, bool isOn){
            if (x < 0 || x >= 8 || y < 0 || y >= 10)
                return;
            int mask = 1 << (7 - x);
            if (isOn)
                rows[y] = (byte)(rows[y] | mask);
            else
                rows[y] = (byte)(rows[y] & ~mask);
        }
        public void TogglePixel(int x, int y){
            if (x < 0 || x >= 8 || y < 0 || y >= 10)
                return;
            rows[y] = (byte)(rows[y] ^ (1 << (7 - x)));
        }
        public byte[] GetBytes(){
            return (byte[])rows.Clone();
        }
        public void SetBytes(byte[] data){
            if (data == null || data.Length != 10)
                return;
            for (int i = 0; i < 10; i++)
                rows[i] = data[i];
        }
        public string GetHex(){
            return BitConverter.ToString(rows).Replace("-", " ");
        }
    }
}
