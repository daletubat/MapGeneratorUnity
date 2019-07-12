using UnityEditor;

namespace RotaryHeart.Lib.SerializableDictionary
{
    public sealed class Constants
    {
        private const bool DEF_SHOW_PAGES = false;
        private const int DEF_PAGE_COUNT = 15;

        private const string ID_SHOW_PAGES = "RHSD_ShowPages";
        private const string ID_PAGE_COUNT = "RHSD_PageCount";

        public static bool ShowPages
        {
            get
            {
                return EditorPrefs.GetBool(ID_SHOW_PAGES, DEF_SHOW_PAGES);
            }
            set
            {
                EditorPrefs.SetBool(ID_SHOW_PAGES, value);
            }
        }
        public static int PageCount
        {
            get
            {
                return EditorPrefs.GetInt(ID_PAGE_COUNT, DEF_PAGE_COUNT);
            }
            set
            {
                EditorPrefs.SetInt(ID_PAGE_COUNT, value);
            }
        }
    }
}