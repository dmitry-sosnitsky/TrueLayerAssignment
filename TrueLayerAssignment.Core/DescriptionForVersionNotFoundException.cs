using System;

namespace TrueLayerAssignment.Core
{
    /// <summary>
    /// Exception thrown when no Pokemon description from the requested game version could be found
    /// </summary>
    [Serializable]
    public class DescriptionForVersionNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DescriptionForVersionNotFoundException"/> class
        /// </summary>
        /// <param name="version">Game version that the description was not found for</param>
        public DescriptionForVersionNotFoundException(GameVersion version)
            : base(ConstructNotFoundMessage(version))
        { }

        private static string ConstructNotFoundMessage(GameVersion version)
        {
            switch (version)
            {
                case GameVersion.Any:
                    return $"No descriptions found";
                default:
                    return $"Description for game version '{version}' not found";
            }
        }
    }
}
