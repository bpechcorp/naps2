/*
    NAPS2 (Not Another PDF Scanner 2)
    http://sourceforge.net/projects/naps2/
    
    Copyright (C) 2009       Pavel Sorejs
    Copyright (C) 2012       Michael Adams
    Copyright (C) 2013       Peter De Leeuw
    Copyright (C) 2012-2015  Ben Olden-Cooligan

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Mono.SANE;
using NAPS2.Scan.Exceptions;
using NAPS2.Scan.Images;
using NAPS2.WinForms;

namespace NAPS2.Scan.Sane
{
    public class SaneScanDriver : ScanDriverBase
    {
        public const string DRIVER_NAME = "sane";
        
        private readonly IFormFactory formFactory;

        public SaneScanDriver(IFormFactory formFactory)
        {
            this.formFactory = formFactory;
        }

        public override string DriverName
        {
            get { return DRIVER_NAME; }
        }

        //protected override ScanDevice PromptForDeviceInternal()
        //{
        //    MessageBox.Show(string.Format("{0} scanners available. Choosing first. Choices: {1}",
        //        SANEControl.Scanners.Count, string.Join(", ", SANEControl.Scanners.Select(x => x.Key))));
        //    // TODO
        //    var scanner = SANEControl.Scanners.Select(x => x.Value).FirstOrDefault();
        //    if (scanner == null)
        //    {
        //        return null;
        //    }
        //    MessageBox.Show("id= " + scanner.Deviceid + " |model= " + scanner.Model + " |type= " + scanner.Type + " |vendor= " + scanner.Vendor);
        //    return new ScanDevice(scanner.Deviceid, scanner.Model);
        //}

        protected override ScanDevice PromptForDeviceInternal()
        {
            var deviceList = GetDeviceList();

            if (!deviceList.Any())
            {
                throw new NoDevicesFoundException();
            }

            var form = formFactory.Create<FSelectDevice>();
            form.DeviceList = deviceList;
            form.ShowDialog();
            return form.SelectedDevice;
        }

        protected override List<ScanDevice> GetDeviceListInternal()
        {
            return SANEControl.Scanners.Select(x => x.Value).Select(scanner => new ScanDevice(scanner.Deviceid, scanner.Model)).ToList();
        }

        protected override IEnumerable<ScannedImage> ScanInternal()
        {
            MessageBox.Show("Scan");
            SANEControl.RefreshScanners();
            var scanner = SANEControl.GetScannerByDeviceID(ScanDevice.ID);
            foreach (var option in scanner.Options)
            {
                MessageBox.Show("Option: " + option.Key + " | " + option.Value.CurrentSetting + " | " +
                                option.Value.Switch);
            }
            using (var image = scanner.ScanImage())
            using (var bitmap = new Bitmap(image))
            {
                yield return new ScannedImage(bitmap, ScanBitDepth.C24Bit, ScanProfile.MaxQuality, ScanProfile.Quality);
            }
        }
    }
}
