using System;

namespace Entity
{
  
    /// <summary>
    /// 微应用
    /// </summary>
    public class OpenPlatformMicroApplication
    {
        public Guid ID { get; set; }
        /// <summary>
        /// app名称
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// logo
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// 介绍
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string AppUrl { get; set; }

        /// <summary>
        /// 创建人ID，即注册开发者平台的用户id
        /// </summary>
        public Guid CreateUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
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

        public Guid PlatformUserID { get; set; }


    }
}