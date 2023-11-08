﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    public class Pp_DefectTotal : IKeyGUID
    {
        //生产班组
        [Key]
        public Guid GUID { get; set; }
        [StringLength(20)]
        public string Prolot { get; set; }//生产LOT

        [StringLength(20)]
        public string Promodel { get; set; }//生产机种

        [StringLength(200)]
        public string Prolinename { get; set; }//生产组别 

        [StringLength(200)]
        public string Prodate { get; set; } //生产日期
        [StringLength(10)]
        public string Proorder { get; set; } //生产订单
        public int Proorderqty { get; set; }//订单数量

        public int Prorealqty { get; set; }//生产实绩

        public int Pronobadqty { get; set; }//无不良数量

        public int Probadtotal { get; set; }//不良件数（同一LOT集计数量）      

        public decimal Prodirectrate { get; set; }//直行率（同一LOT集计数量） 
        public decimal Probadrate { get; set; }//不良率（同一LOT集计数量） 
        [StringLength(255)]
        public string Udf001 { get; set; }
        [StringLength(255)]
        public string Udf002 { get; set; }
        [StringLength(255)]
        public string Udf003 { get; set; }

        public Decimal Udf004 { get; set; }

        public Decimal Udf005 { get; set; }

        public Decimal Udf006 { get; set; }
        [Required]
        public byte isDelete { get; set; }	//13	//	删除标记
        [StringLength(500)]
        public string Remark { get; set; }

        [StringLength(50)]
        public string Creator { get; set; }
        public DateTime? CreateTime { get; set; }

        [StringLength(50)]
        public string Modifier { get; set; }
        public DateTime? ModifyTime { get; set; }


    }
}