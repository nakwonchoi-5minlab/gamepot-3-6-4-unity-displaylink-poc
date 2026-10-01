using UnityEngine;
using System.Collections;
using Realtime.LITJson;


public class NVoidInfo
{
    public NVoidInfo()
    {
        theme = "MATERIAL_BLUE";

        headerBackGradient = new string[] { };
        headerTitleColor = "";

        contentBackGradient = new string[] { };
        listHeaderBackGradient = new string[] { };
        listHeaderTitleColor = "";
        listContentBackGradient = new string[] { };
        listContentTitleColor = "";

        footerBackGradient = new string[] { };
        footerButtonGradient = new string[] { };
        footerTitleColor = "";

        headerTitle = "{none}";
        descHTML = "";
        descColor = "";

        listHeaderTitle = "";
        footerTitle = "";
    }


    /* 테마 목록
    MATERIAL_RED
    MATERIAL_BLUE
    MATERIAL_CYAN
    MATERIAL_ORANGE
    MATERIAL_PURPLE
    MATERIAL_DARKBLUE
    MATERIAL_YELLOW
    MATERIAL_GRAPE
    MATERIAL_GRAY
    MATERIAL_GREEN
    MATERIAL_PEACH
    */

    public string theme { get; set; }

    // 배경 색상(gradient)
    public string[] headerBackGradient { get; set; }
    public string headerTitleColor { get; set; }

    public string[] contentBackGradient { get; set; }
    public string[] listHeaderBackGradient { get; set; }
    public string listHeaderTitleColor { get; set; }
    public string[] listContentBackGradient { get; set; }
    public string listContentTitleColor { get; set; }

    public string[] footerBackGradient { get; set; }
    public string[] footerButtonGradient { get; set; }
    public string footerTitleColor { get; set; }

    public string headerTitle { get; set; }
    public string descHTML { get; set; }
    public string descColor { get; set; }

    public string listHeaderTitle { get; set; }
    public string footerTitle { get; set; }


    public string ToJson()
    {
        JsonData data = new JsonData();
        data["theme"] = theme;

        data["headerBackGradient"] = string.Join(",", headerBackGradient);
        data["headerTitleColor"] = headerTitleColor;

        data["contentBackGradient"] = string.Join(",", contentBackGradient);
        data["listHeaderBackGradient"] = string.Join(",", listHeaderBackGradient);
        data["listHeaderTitleColor"] = listHeaderTitleColor;
        data["listContentBackGradient"] = string.Join(",", listContentBackGradient);
        data["listContentTitleColor"] = listContentTitleColor;

        data["footerBackGradient"] = string.Join(",", footerBackGradient);
        data["footerButtonGradient"] = string.Join(",", footerButtonGradient);
        data["footerTitleColor"] = footerTitleColor;

        data["headerTitle"] = headerTitle;
        data["descHTML"] = descHTML;
        data["descColor"] = descColor;

        data["listHeaderTitle"] = listHeaderTitle;
        data["footerTitle"] = footerTitle;

        return data.ToJson();
    }

}
