namespace Curio.Gameplay
{
    public static class CurrencyToString
    {
        private static string[] suffix = new string[] { "", "k", "M", "B", "T", "Qa", "Qi", "Si", "Sp", "O", "N" };

        #region Public Methods
        public static string Convert(double valueToConvert)
        {
            int scale = 0;
            double v = valueToConvert;
            while (v >= 1000d)
            {
                v /= 1000d;
                scale++;
                if (scale >= suffix.Length)
                    return valueToConvert.ToString("e2"); // overflow, can't display number, fallback to exponential
            }
            return v.ToString("0.##") + suffix[scale];
        }

        #endregion
    }
}