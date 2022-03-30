#nullable enable

namespace CargoEnvMon.Reader.Infrastructure
{
    public static class ArrayHelper
    {
        public static string? TryGetFromIndex(this string[] arr, int index)
        {
            return arr.Length > index ? arr[index] : null;
        }
        
        public static int? TryGetIntFromIndex(this string[] arr, int index)
        {
            return int.TryParse(arr.TryGetFromIndex(index), out var val)
                ? val
                : null;
        }
        
        public static float? TryGetFloatFromIndex(this string[] arr, int index)
        {
            return float.TryParse(arr.TryGetFromIndex(index), out var val)
                ? val
                : null;
        }
    }
}