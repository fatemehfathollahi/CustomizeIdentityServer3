using System.Web.Optimization;

namespace SecurityService.SSO.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;
            bundles.UseCdn = true;

            #region Javascript
            /* JQuery */
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            /* JQuery */

            /* Bootstrap */
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            /* Bootstrap */

            bundles.Add(new ScriptBundle("~/bundles/Panel").Include(
                "~/Scripts/Panel.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                        "~/Scripts/site.js"));

            /* Plugins */
            bundles.Add(new ScriptBundle("~/bundles/tinymce").Include(
                      "~/Scripts/tinymce/tinymce.js"));

            bundles.Add(new ScriptBundle("~/bundles/calendar").Include(
                      "~/Scripts/Calendar/jalali.js",
                      "~/Scripts/Calendar/calendar.js",
                      "~/Scripts/Calendar/calendar-setup.js",
                      "~/Scripts/Calendar/lang/calendar-fa.js"));

            bundles.Add(new ScriptBundle("~/bundles/sidemenu").Include(
                      "~/Scripts/jquery.sidr.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/select").Include(
                      "~/Scripts/select2.js",
                      "~/Scripts/i18n/fa.js"));

            bundles.Add(new ScriptBundle("~/bundles/jsonbeautifier").Include(
                      "~/Scripts/jjsonviewer.js"));

            bundles.Add(new ScriptBundle("~/bundles/uploader").Include(
                      "~/Scripts/Jcrop.js",
                      "~/Scripts/EditorTemplates/Uploader/uploader.js"));

            bundles.Add(new ScriptBundle("~/bundles/textarea").Include(
                      "~/Scripts/jquery.simplyCountable.js"));

            bundles.Add(new ScriptBundle("~/bundles/switch").Include(
                      "~/Scripts/bootstrap-switch.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                      "~/Scripts/MdBootstrapPersianDateTimePicker/jalaali.js",
                      "~/Scripts/MdBootstrapPersianDateTimePicker/jquery.Bootstrap-PersianDateTimePicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/timepicker").Include(
                      "~/Scripts/bootstrap-clockpicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/scroll").Include(
                      "~/Scripts/jquery.nicescroll.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/JsTree").Include(
                    "~/Scripts/JsTree/JsTree.js",
                     "~/Scripts/JsTree/jstree.contextmenu.js",
                     "~/Scripts/JsTree/jstree.search.js",
                      "~/Scripts/JsTree/jstree.dnd.js"

                    ));
            bundles.Add(new ScriptBundle("~/bundles/Dropzone").Include(
                "~/Scripts/dropzone/dropzone-amd-module.min.js",
                "~/Scripts/dropzone/dropzone.min.js",
                "~/Scripts/dropzone/Config.js"
            ));


            /* Plugins */

            /* Editor templates */
            bundles.Add(new ScriptBundle("~/Scripts/tinymce/bundletinymce.min.js").Include(
                      "~/Scripts/tinymce/tinymce.min.js",
                      "~/Scripts/tinymce/plugins/media/plugin.min.js",
                      "~/Scripts/html.js"));

            bundles.Add(new ScriptBundle("~/bundles/editor/datetime").Include(
                      "~/Scripts/bootstrap-datepicker.js",
                      "~/Scripts/bootstrap-datepicker.fa.js",
                      "~/Scripts/EditorTemplates/DateTime/datetime*"));

            bundles.Add(new ScriptBundle("~/bundles/editor/timepicker").Include(
                "~/Scripts/bootstrap-clockpicker.js",
                "~/Scripts/EditorTemplates/TimePicker/timePicker*"));
            /* Editor templates */

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                      "~/Scripts/DataTables/jquery.dataTables.js",
                      "~/Scripts/dataTables.bootstrap-rtl.js",
                      "~/Scripts/DataTables/dataTables.fixedHeader.js",
                      "~/Scripts/datatables.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables-rowreorder").Include(
                      "~/Scripts/DataTables/dataTables.rowReorder.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/ColorPicker").Include(
                "~/Scripts/ColorPicker/jscolor.js"



            ));

            #endregion Javascript

            #region Content
            /* Bootstrap */
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-rtl.css"));

            /* Required Bootstrap */
            bundles.Add(new StyleBundle("~/Content/bootstrap-flipped").Include(
                      "~/Content/bootstrap-flipped.css",
                      "~/Content/bootstrap-flipped-override.css"));

            /* Required Bootstrap */
            bundles.Add(new StyleBundle("~/Content/bootstrap-theme").Include(
                      "~/Content/bootstrap-theme.css"));
            /* Bootstrap */

            bundles.Add(new StyleBundle("~/Content/font-awesome").Include(
                      "~/Content/font-awesome.css"));

            bundles.Add(new StyleBundle("~/Content/switch").Include(
                      "~/Content/bootstrap-switch/bootstrap3/bootstrap-switch.css"));

            bundles.Add(new StyleBundle("~/Content/jstree").Include(
                      "~/Content/JsTree/proton/style.min.css"));

            bundles.Add(new StyleBundle("~/Content/site").Include(
                      "~/Content/site-rtl.css"));

            bundles.Add(new StyleBundle("~/Content/Panel").Include(
                "~/Content/Panel.min.css"));

            bundles.Add(new StyleBundle("~/Content/tooltip").Include(
                      "~/Content/balloon.css"));

            bundles.Add(new StyleBundle("~/Content/jsonbeautifier").Include(
                      "~/Content/jjsonviewer.css"));

            bundles.Add(new StyleBundle("~/Content/select").Include(
                      "~/Content/css/select2.css",
                      "~/Content/select2-bootstrap-rtl.css"));

            bundles.Add(new StyleBundle("~/Content/datepicker").Include(
                      "~/Content/MdBootstrapPersianDateTimePicker/jquery.Bootstrap-PersianDateTimePicker.css"));

            bundles.Add(new StyleBundle("~/Content/timepicker").Include(
                      "~/Content/bootstrap-clockpicker.css"));

            /* Editor templates */
            bundles.Add(new ScriptBundle("~/Content/editor/datetime").Include(
                      "~/Content/bootstrap-datepicker.css"));

            bundles.Add(new StyleBundle("~/Content/uploader").Include(
                      "~/Content/Jcrop.css"));
            /* Editor templates */

            bundles.Add(new StyleBundle("~/Content/datatables").Include(
                      "~/Content/dataTables.bootstrap-rtl.css",
                      "~/Content/DataTables/css/fixedHeader.bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/datatables-rowreorder").Include(
                      "~/Content/DataTables/css/rowReorder.dataTables.min.css"));

            bundles.Add(new StyleBundle("~/Content/ColorPicker").Include(
                "~/Content/ColorPicker/_colorpicker.css"));
            bundles.Add(new StyleBundle("~/Content/Dropzone").Include(
                "~/Content/Dropzone/basic.min.css",
                "~/Content/Dropzone/dropzone.min.css"
                ));

            #endregion Content
        }
    }
}