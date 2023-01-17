namespace AlinSpace.Docker
{
    public class EnvironmentVariable
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public EnvironmentVariable()
        {
        }

        public EnvironmentVariable(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public static EnvironmentVariable New(string key, string value)
        {
            return new EnvironmentVariable(key, value);
        }
    }
}
