using System;

namespace Entity
{
    /// <summary>
    /// 微应用
    /// </summary>
    public class OpenPlatformMicroApplication
    {
        public Guid Id { get; set; }
        /// <summary>
        /// app名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// logo
        /// </summary>
        public string logo { get; set; }
        /// <summary>
        /// 介绍
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string AppUrl { get; set; }


        public string BackUrl { get; set; }
        /// <summary>
        /// 创建人ID，即注册开发者平台的用户id
        /// </summary>
        public Guid CreatorId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsOpen { get; set; }

        /// <summary>
        /// appid
        /// </summary>
        public string AppID { get; set; }
        /// <summary>
        /// app密钥
        /// </summary>
        public string AppSecret { get; set; }

        public Guid ExamineUserId { get; set; }

        public DateTime? ExamineTime { get; set; }
        /// <summary>
        /// 是否同过审核
        /// </summary>
        public int IsExamine { get; set; }


    }
}