using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karin.Domain.Tables
{
    /// <summary>
    /// جدول موبایل
    /// </summary>
    [Table(name: "Mobile", Schema = BaseConfig.DefaultValue.Schema)]
    public class Mobile: Inherit.BaseEntity, Interface.IEntity
    {
        #region Properties
        /// <summary>
        /// شماره تلفن
        /// </summary>
        [StringLength(maximumLength: 11, MinimumLength = 11), Required]
        public string CellPhoneNumber { get; set; }
        /// <summary>
        /// کلید شخص
        /// </summary>
        public int PersonId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PersonMobileId { get; set; }
        /// <summary>
        /// وضعیت
        /// </summary>
        public bool Status { get; set; }
        #endregion

        #region ForeignKey
        /// <summary>
        /// کلید خارجی به جدول اشخاص
        /// </summary>
        [ForeignKey(name: "PersonId")]
        public Person Person { get; set; }
        #endregion
    }
}
