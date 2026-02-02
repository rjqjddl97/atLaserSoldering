using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;


namespace RecipeManager
{
    //
    // 요약:
    //     Number of stop bits to use.
    public enum StopBits
    {
        //
        // 요약:
        //     One stop bit.
        One = 0,
        //
        // 요약:
        //     1.5 stop bits.
        One5 = 1,
        //
        // 요약:
        //     Two stop bits.
        Two = 2
    }
    //
    // 요약:
    //     The type of parity to use.
    public enum Parity
    {
        //
        // 요약:
        //     No parity.
        None = 0,
        //
        // 요약:
        //     Odd parity.
        Odd = 1,
        //
        // 요약:
        //     Even parity.
        Even = 2,
        //
        // 요약:
        //     Mark parity.
        Mark = 3,
        //
        // 요약:
        //     Space parity.
        Space = 4
    }
    //
    // 요약:
    //     Handshaking mode to use.
    [Flags]
    public enum Handshake
    {
        //
        // 요약:
        //     No handshaking.
        None = 0,
        //
        // 요약:
        //     Software handshaking.
        XOn = 1,
        //
        // 요약:
        //     Hardware handshaking (RTS/CTS).
        Rts = 2,
        //
        // 요약:
        //     RTS and Software handshaking.
        RtsXOn = 3,
        //
        // 요약:
        //     Hardware handshaking (DTR/DSR) (uncommon).
        Dtr = 4,
        //
        // 요약:
        //     DTR and Software handshaking (uncommon).
        DtrXOn = 5,
        //
        // 요약:
        //     Hardware handshaking with RTS/CTS and DTR/DSR (uncommon).
        DtrRts = 6,
        //
        // 요약:
        //     Hardware handshaking with RTS/CTS and DTR/DSR and Software handshaking (uncommon).
        DtrRtsXOn = 7
    }
}