using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karin.Domain.Tables
{
    /// <summary>
    /// جدول اشخاص
    /// </summary>
    [Table(name: "Person", Schema = BaseConfig.DefaultValue.Schema)]
    public class Person : Inherit.BaseEntity, Interface.IEntity
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        [StringLength(maximumLength: 50, MinimumLength = 3), Required]
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        [StringLength(maximumLength: 50, MinimumLength = 3), Required]
        public string LastName { get; set; }
        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public DateTime BrithDay { get; set; }
        /// <summary>
        /// نوع شخص
        /// </summary>
        public int PersonType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ParrentPersonId { get; set; }
        /// <summary>
        /// جنسیت
        /// </summary>
        public int Gender { get; set; }
        #endregion

        #region ForeignKey
        /// <summary>
        /// کلید خارجی به جدول شخاص
        /// </summary>
        [ForeignKey(name: "ParrentPersonId")]
        public Person FPerson { get; set; }
        #endregion

        #region Key
        /// <summary>
        /// کلید به جدول اشخاص
        /// </summary>
        public ICollection<Person> Childs { get; set; }
        /// <summary>
        /// کلید به جدول موبایل
        /// </summary>
        public ICollection<Mobile> Mobiles { get; set; }
        #endregion
    }
}
