namespace DashReportViewer.Shared.ReportContent
{
    public class TextContent : BaseReportContent
    {
        public string Text { get; set; }
        public string FontSize { get; set; } = "12px";
        public TextHorizontalAlign HorizontalAlign { get; set; } = TextHorizontalAlign.Left;
        public TextVerticalAlign VerticalAlign { get; set; } = TextVerticalAlign.Top;
    }

    public enum TextHorizontalAlign
    {
        Left,
        Center,
        Right
    }

    public enum TextVerticalAlign
    {
        Top,
        Middle,
        Bottom
    }
}
