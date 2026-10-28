using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld.Planet;
using TD_Find_Lib;
using UnityEngine;
using Verse;

namespace TDFloatSubMenu
{
    public class FloatSubMenuAndRefresh : FloatSubMenus.FloatSubMenu
    {
	    private readonly ThingQuery owner;
	    private readonly Color color = Color.white;
	    public FloatSubMenuAndRefresh(string label, List<FloatMenuOption> options, ThingQuery query, Color? color = null)
		    : base(label, options)
	    {
		    owner = query;
		    if (color.HasValue)
			    this.color = Color.Lerp(Color.white, color.Value, .2f);
	    }
	    public FloatSubMenuAndRefresh(string label, List<FloatMenuOption> options, ThingQuery query, Texture2D itemIcon, Color? iconColor = null)
		    : base(label, options, itemIcon, iconColor ?? Color.white)
	    {
		    owner = query;
	    }
	    public FloatSubMenuAndRefresh(string label, List<FloatMenuOption> options, ThingQuery query, ThingDef shownItemForIcon, Color? iconColor = null, ThingStyleDef thingStyle = null, bool forceBasicStyle = false)
		    : base(label, options, shownItemForIcon, thingStyle, forceBasicStyle)
	    {
		    owner = query;
		    // base const doesn't take iconColor O_o
		    if (iconColor.HasValue)
			    this.iconColor = iconColor.Value;
	    }

	    public override bool DoGUI(Rect rect, bool colonistOrdering, FloatMenu floatMenu)
	    {
		    var oldColor = GUI.color;
		    GUI.color = color;

		    var result = base.DoGUI(rect, colonistOrdering, floatMenu);

		    GUI.color = oldColor;

		    if (result)
			    owner.RootHolder.NotifyUpdated();

		    return result;
	    }
    }
}
