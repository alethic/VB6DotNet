using System;

namespace VB6DotNet.PortableExecutable
{

    [Flags]
    public enum VB6ExeProjectInfoControlFlags1 : uint
    {

        PictureBoxObject = 0x1,
        LabelObject = 0x2,
        TextBoxObject = 0x4,
        FrameObject = 0x8,
        CommandButtonObject = 0x10,
        CheckBoxObject = 0x20,
        OptionButtonObject = 0x40,
        ComboBoxObject = 0x80,
        ListBoxObject = 0x100,
        HScrollBarObject = 0x200,
        VScrollBarObject = 0x400,
        TimerObject = 0x800,
        PrintObject = 0x1000,
        FormObject = 0x2000,
        ScreenObject = 0x4000,
        ClipboardObject = 0x8000,
        DriveObject = 0x10000,
        DirObject = 0x20000,
        FileListBoxObject = 0x40000,
        MenuObject = 0x80000,
        MDIFormObject = 0x100000,
        AppObject = 0x200000,
        ShapeObject = 0x400000,
        LineObject = 0x800000,
        ImageObject = 0x1000000,

    }

}
