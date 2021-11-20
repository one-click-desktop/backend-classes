using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneClickDesktop.BackendClasses.Model.Resources
{
    public class PCIID
    {
        private string vendorId;
        /// <summary>
        /// Manufacturer unique id
        /// </summary>
        /// <exception cref="ArgumentException">Vendor ID must be lowercase 4 digit hex number</exception>
        public string VendorId
        {
            get => vendorId;
            set
            {
                if (!IsValidId(value))
                    throw new ArgumentException($"Vendor ID {value} must be lowercase 4 digit hex number");
                vendorId = value;
            }
        }

        private string deviceId;
        /// <summary>
        /// Unique Device ID inside manufacturer space
        /// </summary>
        /// <exception cref="ArgumentException">Device ID must be lowercase 4 digit hex number</exception>
        public string DeviceId 
        { 
            get => deviceId;
            set
            {
                if (!IsValidId(value))
                    throw new ArgumentException($"Device ID {value} must be lowercase 4 digit hex number");
                deviceId = value;
            }
        }

        public PCIID (string vendor, string device)
        {
            VendorId = vendor;
            DeviceId = device;
        }

        public override string ToString()
        {
            return $"{VendorId}:{DeviceId}";
        }

        /// <summary>
        /// Checks if given string is valid id.
        /// Valid id must:
        /// 1. Has 4 characters
        /// 2. Be lowercase hexadecimal number
        /// </summary>
        /// <param name="text">tetsing string</param>
        /// <returns>true - valid id, false - otherwise</returns>
        public static bool IsValidId(string text)
        {
            //has length 4
            if (text.Length != 4)
                return false;

            //Is lowercase hexadecimal
            for (int i = 0; i < text.Length; ++i)
            {
                if (!((text[i] >= '0' && text[i] <= '9') || (text[i] >= 'a' && text[i] <= 'f')))
                    return false;
            }

            return true;
        }
    }
}
