using System.Globalization;

namespace appFilRouge.Converters
{
    public class NameToColorConverter : IValueConverter
    {
        private static readonly Color[] Palette =
        {
            Color.FromArgb("#F2B86B"),
            Color.FromArgb("#D9C39E"),
            Color.FromArgb("#9BBF7A"),
            Color.FromArgb("#EBC45C"),
        };

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var name = value as string ?? string.Empty;

            // stable hash: string.GetHashCode() changes on every app run in .NET
            int sum = 0;
            foreach (var c in name) sum += c;

            return Palette[sum % Palette.Length];
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}