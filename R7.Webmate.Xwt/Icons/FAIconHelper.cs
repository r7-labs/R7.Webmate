//
//  FAImageHelper.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2019 Roman M. Yagodin
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using Xwt;
using Xwt.Drawing;

namespace R7.Webmate.Xwt.Icons
{
    public static class FAIconHelper
    {
        public static Image GetIcon (FAIconStyle style, string name)
        {
            // TODO: Cache loaded images
            return Image.FromFile ($"./icons/{style.ToString ().ToLowerInvariant ()}/{name}.svg");
        }

        public static Image GetIcon (string name)
        {
            return GetIcon (FAIconStyle.Solid, name);
        }
    }
}
