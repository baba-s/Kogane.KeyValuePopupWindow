namespace Kogane
{
    public readonly struct KeyValuePopupWindowData
    {
        public string Key   { get; }
        public string Value { get; }

        public KeyValuePopupWindowData
        (
            string key,
            string value
        )
        {
            Key   = key;
            Value = value;
        }

        internal static KeyValuePopupWindowData CreateDummy()
        {
            return new( string.Empty, string.Empty );
        }
    }
}