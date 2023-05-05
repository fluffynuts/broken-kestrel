namespace broken_kestrel
{
    public static class Settings
    {
        public const string BREAK_EARLY = "BreakEarly";
        public const string BREAK_LATER = "BreakLater";
        public const string BREAK_LATER_DELAY_SECONDS = "BreakLaterDelaySeconds";
    }

    public static class ConfigurationSectionExtensions
    {
        public static bool AppSettingFlag(
            this IConfigurationRoot configRoot,
            string settingName
        )
        {
            var raw = configRoot.AppSetting(settingName);
            return BooleanResolutions.TryGetValue(raw, out var result)
                ? result
                : throw new InvalidSettingValue(settingName, raw, BooleanResolutions.Keys.ToArray());
        }

        public static string AppSetting(
            this IConfigurationRoot configRoot,
            string settingName
        )
        {
            var section = configRoot.GetSection("AppSettings");
            if (section is null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            var raw = section[settingName]
                ?? throw new SettingNotFound(settingName);
            return raw;
        }

        public static int AppSettingInt(
            this IConfigurationRoot configRoot,
            string settingName
        )
        {
            var raw = configRoot.AppSetting(settingName);
            return int.TryParse(raw, out var result)
                ? result
                : throw new InvalidSettingValue<int>(settingName, raw);
        }

        private static readonly Dictionary<string, bool> BooleanResolutions = new(StringComparer.OrdinalIgnoreCase)
        {
            ["true"] = true,
            ["yes"] = true,
            ["1"] = true,
            ["on"] = true,

            ["false"] = false,
            ["no"] = false,
            ["0"] = false,
            ["off"] = false
        };
    }
}