using System.Collections.Generic;

namespace Com.Costoda.ReferenceApp.CoreLib.Menu
{
    public static class MenuFactory
    {
		public static IList<MainMenuItem> GetMenuItems() 
		{
			var items = new List<MainMenuItem>
			{
				new MainMenuItem
				{
					Title = "Token Generator 3000",
					Description = "Token Generator 3000",
                    ImageLocation = "",
				},
				new MainMenuItem
				{
					Title = "User Profile",
					Description = "",
                    ImageLocation = "",
				},
				new MainMenuItem
				{
					Title = "Group Memberships",
					Description = "",
					ImageLocation = "",
				},
			};

			return items;
		}
    }
}
