using System.Text.Json.Serialization;

namespace TrueLayerAssignment.Web.DataContracts
{
    /// <summary>
    /// Request validation result
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// Error message
        /// </summary>
        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Determines whether validation is successful or not
        /// </summary>
        [JsonIgnore]
        public bool Success { get; private set; }

        internal static ValidationResult Ok()
        {
            return new ValidationResult
            {
                Success = true
            };
        }

        internal static ValidationResult Failed(string errorMessage)
        {
            return new ValidationResult
            {
                Success = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
