using System.Threading.Tasks;

namespace TrueLayerAssignment.Core.ShakespeareTranslator
{
    /// <summary>
    /// Provides translation of English text into Shakespeare's style
    /// </summary>
    public interface IShakespeareTranslator
    {
        /// <summary>
        /// Given Englist text, returns Shakespearian translation
        /// </summary>
        /// <param name="text">Original text</param>
        /// <returns>Translated text</returns>
        Task<string> GetTranslation(string text);
    }
}
