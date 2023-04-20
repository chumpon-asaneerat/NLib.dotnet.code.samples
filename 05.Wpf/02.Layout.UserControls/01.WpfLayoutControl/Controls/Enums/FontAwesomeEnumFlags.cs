#region Using

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

#endregion

namespace WpfLayoutControl.Controls
{
    #region FontAwesomeIcon Enum

    /// <summary>
    /// The FontAwesomeIcon Enum.
    /// </summary>
    public enum FontAwesomeIcon : uint
    {
        None = 0x00000000,
        Home = 0x00000001,
        Back = 0x00000002,
        Close = 0x00000004,
        Add = 0x00000008,
        Edit = 0x00000010,
        Save = 0x00000020,
        Delete = 0x00000040,
        Cut = 0x00000080,
        Copy = 0x00000100,
        Paste = 0x00000200,
        Import = 0x00000400,
        Export = 0x00000800,
        Print = 0x00001000,
        Preview = 0x00002000,
        Search = 0x00004000,
        Refresh = 0x00008000,
        Scan = 0x00010000,
        Yes = 0x00020000,
        No = 0x00040000,
        Ok = 0x00080000,
        Cancel = 0x00100000
    }

    #endregion

    #region FontAwesomeButtons Flags

    /// <summary>
    /// The FontAwesomeButtons Flags.
    /// </summary>
    [Flags]
    public enum FontAwesomeButtons : uint
    {
        None = 0x00000000,
        Home = 0x00000001,
        Back = 0x00000002,
        Close = 0x00000004,
        Add = 0x00000008,
        Edit = 0x00000010,
        Save = 0x00000020,
        Delete = 0x00000040,
        Cut = 0x00000080,
        Copy = 0x00000100,
        Paste = 0x00000200,
        Import = 0x00000400,
        Export = 0x00000800,
        Print = 0x00001000,
        Preview = 0x00002000,
        Search = 0x00004000,
        Refresh = 0x00008000,
        Scan = 0x00010000,
        Yes = 0x00020000,
        No = 0x00040000,
        Ok = 0x00080000,
        Cancel = 0x00100000
    }

    #endregion
}
