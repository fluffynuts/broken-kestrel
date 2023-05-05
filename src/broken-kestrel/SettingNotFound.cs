namespace broken_kestrel
{
    public class SettingNotFound
        : Exception
    {
        public SettingNotFound(
            string settingName
        ) : base($"Setting not found: '{settingName}'")
        {
        }
    }
}