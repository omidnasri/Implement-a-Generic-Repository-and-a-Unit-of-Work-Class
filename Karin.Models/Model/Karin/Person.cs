using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karin.Models.Model.Karin
{
    public class Person : BaseModel
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
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
