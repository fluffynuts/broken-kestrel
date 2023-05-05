namespace broken_kestrel
{
    public class InvalidSettingValue
        : Exception
    {
        public InvalidSettingValue(
            string settingName,
            string raw,
            string[] valid
        ) : base($@"The configured value of '{
            raw
        }' for '{
            settingName
        }' is invalid. Valid values are any one of: [ {
            string.Join(", ",
                valid.Select(s => $"\"{s}\"")
            )
        } ]")
        {
        }
    }

    public class InvalidSettingValue<TSetting> : Exception
    {
        public InvalidSettingValue(
            string settingName,
            string raw
        ) : base(
            $"The configured value '{raw}' for '{settingName}' is not valid for the expected type: {typeof(TSetting)}")
        {
        }
    }
}