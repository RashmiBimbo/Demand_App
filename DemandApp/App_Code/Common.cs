using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

public static class Common
{
    public static void PopUp(Page page, Type type, string key, string script, bool addScriptTags)
    {
        ScriptManager.RegisterClientScriptBlock(page, type, key, script, addScriptTags);
    }
}