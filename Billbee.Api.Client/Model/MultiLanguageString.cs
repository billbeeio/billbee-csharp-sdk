namespace Billbee.Api.Client.Model
{
    /// <summary>
    /// Container to hold a language specific translation
    /// </summary>
    public class MultiLanguageString
    {
        /// <summary>
        /// Text representation in the given language
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// ISO language code, that defines the language, the content in <<see cref="Text"/> written in.
        /// </summary>
        public string LanguageCode { get; set; }
    }
}
