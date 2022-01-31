using BenchmarkDotNet.Attributes;
using System.Drawing;
using WInterop;
using WInterop.Gdi;

namespace Performance;

// [DisassemblyDiagnoser]
[MemoryDiagnoser]
public unsafe class Fonts
{
    private FontHandle _font;
    private DeviceContext _deviceContext;

    [GlobalSetup]
    public void GlobalSetup()
    {
        LogicalFont logicalFont = new()
        {
            Height = -22,
            CharacterSet = CharacterSet.Default,
            OutputPrecision = OutputPrecision.TrueType,
            Weight = FontWeight.Normal
        };

        logicalFont.FaceName.CopyFrom("Calibri");
        _font = Gdi.CreateFontIndirect(logicalFont);
        _deviceContext = Gdi.GetDeviceContext();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        _font.Dispose();
    }

    [Benchmark]
    public Size MeasureFont()
    {
        (_, _, Rectangle bounds) = Gdi.DrawText(
            in _deviceContext,
            "ComboBoxes with ScrollBars",
            // "Calendar",
            new Rectangle(0, 0, int.MaxValue, int.MaxValue),
            TextFormat.Center | TextFormat.VerticallyCenter | TextFormat.CalculateRectangle | TextFormat.EditControl | TextFormat.HidePrefix,
            leftMargin: 5,
            rightMargin: 7);

        return bounds.Size;
    }
}
