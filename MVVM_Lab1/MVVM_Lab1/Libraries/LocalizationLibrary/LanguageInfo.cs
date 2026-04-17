namespace MVVM_Lab1.Libraries.LocalizationLibrary
{
    /// <summary>
    /// Информация о языке
    /// </summary>
    public class LanguageInfo
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string NativeName { get; set; }

        public LanguageInfo(string code, string name, string nativeName)
        {
            Code = code;
            Name = name;
            NativeName = nativeName;
        }

        public override string ToString()
        {
            return NativeName;
        }
    }
}