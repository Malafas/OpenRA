#region Copyright & License Information
/*
 * Copyright 2007-2016 The OpenRA Developers (see AUTHORS)
 * This file is part of OpenRA, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenRA.Mods.Common.FileFormats;

namespace OpenRA.Mods.Common.UtilityCommands
{
	class ListInstallShieldCabContentsCommand : IUtilityCommand
	{
		public string Name { get { return "--list-installshield-cab"; } }

		public bool ValidateArguments(string[] args)
		{
			return args.Length == 2;
		}

		[Desc("DATA.HDR", "Lists the filenames contained within an Installshield CAB volume set")]
		public void Run(ModData modData, string[] args)
		{
			var package = new InstallShieldCABCompression(File.OpenRead(args[1]), null);
			foreach (var volume in package.Contents.OrderBy(kv => kv.Key))
			{
				Console.WriteLine("Volume: {0}", volume.Key);
				foreach (var filename in volume.Value)
					Console.WriteLine("\t{0}", filename);
			}
		}
	}
}