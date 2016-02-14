using System.ComponentModel;

namespace Karin.Domain.Enum
{
    public enum PersonType
    {
        /// <summary>
        /// مرد
        /// </summary>
        [Description(description: "مرد")]
        Man,
        /// <summary>
        /// مرد
        /// </summary>
        [Description(description: "زن")]
        Woman,
        /// <summary>
        /// مرد
        /// </summary>
        [Description(description: "پسر")]
        Son,
        /// <summary>
        /// مرد
        /// </summary>
        [Description(description: "دختر")]
        Girl,
    }
}
