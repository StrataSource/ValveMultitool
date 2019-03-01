namespace ValveMultitool.Models.Formats.Lump
{
    public class LumpElementHeaderInfo
    {
        public enum HeaderNameType
        {
            None,
            String,
            Char
        }

        public HeaderNameType Type = HeaderNameType.None;
        public int CharLength = 0;
    }
}
