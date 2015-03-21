using System.Threading;
using Microsoft.SPOT.Hardware;

namespace NETMF_Matrix_Keypad
{
    class Keypad
    {
        public char[][] Keymap
        {
            get;
            private set;
        }

        public Cpu.Pin[] RowPins
        {
            get;
            private set;
        }

        public Cpu.Pin[] ColumnPins
        {
            get;
            private set;
        }

        public int HoldTime
        {
            get;
            set;
        }

        public Keypad(char[][] userKeymap, Cpu.Pin[] rows, Cpu.Pin[] cols)
        {
            Keymap = userKeymap;
            RowPins = rows;
            ColumnPins = cols;

            HoldTime = 500;
        }

        public char[] GetKeys()
        {
            var s = "";

            for (var i = 0; i < ColumnPins.Length; i++)
            {
                using (var output = new OutputPort(ColumnPins[i], false))
                {
                    for (var j = 0; j < RowPins.Length; j++)
                    {
                        using (var row = new InputPort(RowPins[j], false, Port.ResistorMode.PullUp))
                        {
                            if (!row.Read())
                                s += Keymap[j][i];
                        }
                    }
                    output.Write(true);
                }
                using (new InputPort(ColumnPins[i], false, Port.ResistorMode.Disabled))
                {
                }
            }

            var result = new char[s.Length];
            for (var i = 0; i < s.Length; i++)
                result[i] = s[i];

            if (result.Length > 0)
                Thread.Sleep(HoldTime);
            return result;
        }
    }
}
