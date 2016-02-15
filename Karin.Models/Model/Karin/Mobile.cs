using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karin.Models.Model.Karin
{
    public class Mobile : BaseModel
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
        /// <summary>
        /// کلید خارجی به جدول موبایل
        /// </summary>
        [ForeignKey(name: "PersonMobileId")]
        public Mobile FMobile { get; set; }
        #endregion

        #region Key
        /// <summary>
        /// کلید به جدول موبایل
        /// </summary>
        public ICollection<Mobile> Childs { get; set; }
        #endregion
    }
}
