﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aark.Netatmo.SDK.Security
{
    /// <summary>
    /// Indoor Welcome Camera of Netatmo.
    /// </summary>
    public class WelcomeCamera: ICommonSecurityDevice
    {
        /// <summary>
        /// Id of the device.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Name of the device.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Date on which the device was setted. 
        /// </summary>
        public DateTime SetupDate { get; set; }
    }
}
