namespace Billbee.Api.Client.Model
{
    public class ParsePlaceholdersQuery
    {
        /// <summary>
        /// The text to parse and replace the placeholders in.
        /// </summary>
        public string TextToParse { get; set; }

        /// <summary>
        /// If true, the string will be handled as html.
        /// </summary>
        public bool IsHtml { get; set; } = false;

        /// <summary>
        /// The ISO 639-1 code of the target language. Using default if not set.
        /// </summary>
        public string Language { get; set; } = null;

        /// <summary>
        /// If true, then the placeholder values are trimmed after usage.
        /// </summary>
        public bool Trim { get; set; } = false;

    }
}