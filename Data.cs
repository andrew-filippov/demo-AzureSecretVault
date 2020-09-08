using System;

namespace VaultDemo {
    /// <summary>
    /// DTO
    /// </summary>
    public class Data
    {
        /// <summary>
        /// Data generation time.
        /// </summary>
        /// <value></value>
        public DateTime Timestamp { get; private set; }

        /// <summary>
        /// Value, retrieved from Azure Key Vault.
        /// </summary>
        /// <value></value>
        public string Value { get; private set; }

        public Data(string value)
        {
            Value = value;
            Timestamp = DateTime.UtcNow;
        }
    }
}