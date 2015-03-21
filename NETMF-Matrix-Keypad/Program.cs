using System;
using GHIElectronics.NETMF.FEZ;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace NETMF_Matrix_Keypad
{
    public class Program
    {
        private static readonly Cpu.Pin[] ColumnPins =
        {
            (Cpu.Pin) FEZ_Pin.Digital.Di22,
            (Cpu.Pin) FEZ_Pin.Digital.Di20,
            (Cpu.Pin) FEZ_Pin.Digital.Di24
        };

        private static readonly Cpu.Pin[] RowPins =
        {
            (Cpu.Pin) FEZ_Pin.Digital.Di21,
            (Cpu.Pin) FEZ_Pin.Digital.Di26,
            (Cpu.Pin) FEZ_Pin.Digital.Di25,
            (Cpu.Pin) FEZ_Pin.Digital.Di23
        };

        private static readonly char[][] Keypad =
        {
            new[] {'1', '2', '3'},
            new[] {'4', '5', '6'},
            new[] {'7', '8', '9'},
            new[] {'*', '0', '#'}
        };

        public static void Main()
        {
            try
            {
                var keypad = new Keypad(Keypad, RowPins, ColumnPins)
                {
                    HoldTime = 200
                };

                while (true)
                {
                    var keys = keypad.GetKeys();
                    if (keys.Length > 0)
                        Debug.Print(new string(keys));
                }
            }
            catch (Exception ex)
            {
                Debug.Print("Error: " + ex.Message);
            }
        }
    }
}
