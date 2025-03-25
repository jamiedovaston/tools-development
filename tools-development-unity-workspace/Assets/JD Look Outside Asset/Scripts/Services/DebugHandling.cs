namespace JD.LookOutside.Utilities
{
    public static class DebugHandling
    {
        public static string FormatError(this string m_Text)
            => string.Format("<color=#FF0000><b>JD Look Outside Error</b> {0} </color>", m_Text);

        public static string Format(this string m_Text)
            => string.Format("<color=#Bfff00><b>JD Look Outside</b> {0} </color>", m_Text);
    }
}
