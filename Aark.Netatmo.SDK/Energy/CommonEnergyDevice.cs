﻿using Aark.Netatmo.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aark.Netatmo.SDK.Energy
{
    /// <summary>
    /// Common properties for an Energy device.
    /// </summary>
    public interface ICommonEnergyDevice
    {
        /// <summary>
        /// Id of the device.
        /// </summary>
        string Id { get; set; }
        /// <summary>
        /// Name of the device.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Date on which the device was setted. 
        /// </summary>
        DateTime SetupDate { get; set; }
    }
}
