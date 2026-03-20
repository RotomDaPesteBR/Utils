namespace LightningArc.Utils.Results
{
    public partial class Error
    {
        /// <summary>
        /// Defines the code prefixes (the first digit or more) for each error module.
        /// Values are assigned sequentially according to the priority and universality of the module.
        /// </summary>
        public enum ModuleCodes
        {
            /// <summary>
            /// Prefix '1'. Module for general application flow and high-level logic errors.
            /// </summary>
            Application = 1,

            /// <summary>
            /// Prefix '2'. Module for critical infrastructure, environment, and initialization errors.
            /// </summary>
            System = 2,

            /// <summary>
            /// Prefix '3'. Module for persistence and database errors.
            /// </summary>
            Database = 3,

            /// <summary>
            /// Prefix '4'. Module for data input validation and business rule errors.
            /// </summary>
            Validation = 4,

            /// <summary>
            /// Prefix '5'. Module for resource management errors (e.g., not found, already exists).
            /// </summary>
            Resource = 5,

            /// <summary>
            /// Prefix '6'. Module for authentication (identity) and authorization (permission) errors.
            /// </summary>
            Authentication = 6,

            /// <summary>
            /// Prefix '7'. Module for HTTP request processing errors (e.g., payload too large).
            /// </summary>
            Request = 7,

            /// <summary>
            /// Prefix '8'. Module for errors when interacting with external/third-party services.
            /// </summary>
            External = 8,

            /// <summary>
            /// Prefix '9'. Module for low-level network communication errors.
            /// </summary>
            Network = 9,

            /// <summary>
            /// Prefix '10'. Module for concurrency and resource locking errors.
            /// </summary>
            Concurrency = 10,

            /// <summary>
            /// Prefix '11'. Module for disk Input/Output (I/O) operations errors.
            /// </summary>
            IO = 11,
        }
    }
}