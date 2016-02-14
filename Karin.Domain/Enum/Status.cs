namespace Karin.Domain.Enum
{
    using System.ComponentModel;
    /// <summary>
    /// وضعیت
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// فعال
        /// </summary>
        [Description(description: "فعال")]
        Enable,
        /// <summary>
        /// غیرفعال
        /// </summary>
        [Description(description: "غیرفعال")]
        Disable,
        /// <summary>
        /// در حال برسی
        /// </summary>
        [Description(description: "در حال برسی")]
        Pending
    }
}
