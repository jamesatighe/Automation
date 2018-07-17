using Automation.Models;
using Automation.ViewModels;
using System.Web.Mvc;
using Automation.DAL;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System;
using X.PagedList;
using System.Text;
using System.Drawing;

namespace Automation.Controllers
{
    public class SCOMController : Controller
    {

        private AutomationContext db = new AutomationContext();

        // GET: SCOM
        public async Task<ActionResult> Index()
        {
            return View(await db.SCOMAlert.ToListAsync());
        }

        public async Task<String> GetSCOMAlerts(string type, int warning, int error)
        {
            if (type == "New")
            {
                DateTime date = new DateTime(1753, 1, 1);
                List<SCOMAlert> scomalert = await db.SCOMAlert.Where(s => s.Resolved == date).ToListAsync();
                var count = scomalert.Count().ToString();
                var link = "SCOMNewLink";
                var title = "SCOM New";
                var results = (count + "," + link + "," + title + "," + warning + "," + error);
                return results;
            }
            else
            {
                DateTime date = DateTime.Today;
                List<SCOMAlert> scomalert = await db.SCOMAlert.Where(s => s.Resolved == date).ToListAsync();
                var count = scomalert.Count().ToString();
                var link = "SCOMResolvedLink";
                var title = "SCOM Resolved Today";
                var results = (count + "," + link + "," + title + "," + warning + "," + error);
                return results;
            }

        }

        public async Task<String> GetSCOMClusterHealth(string warningw, string warninge, string errorw, string errore)
        {
            List<SCOM> scom = await db.SCOM.Where(s => s.Cluster == true).ToListAsync();

            var Warning = scom.Where(s => s.HealthState == "Warning").Count();
            var Error = scom.Where(s => s.HealthState == "Error").Count();
            var Total = scom.Count();
            var link = "SCOMClusterLink";
            var title = "SCOM Cluster Health";


            var results = (Warning + "," + Error + "," + Total + "," + link + "," + title + "," + warningw + "," + warninge + "," + errorw + "," + errore);
            return results;
        }

        public String CreateMetricCard(int Total, string Title, string LinkID, string Label, string content, int? error, int? warning)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class='cardpanel panel-inverse clearfix resizable-card ui-sortable metric " + content + "'>");
            html.AppendLine("<div class='panel-heading'>");
            html.AppendLine("<div class='panel-heading-btn'>");
            html.AppendLine("<a href = '#' class='btn btn-xs btn-icon btn-warning' data-click='threshold-edit' title='Edit Threshold' style='font-size: 0.25em'><i class='fas fa-wrench'></i></a>");
            html.AppendLine("<a href='#' class='btn btn-xs btn-icon btn-circle btn-danger' data-click='panel-remove' style='font-size: 0.25em'><i class='fa fa-times'></i></a>");
            html.AppendLine("</div>");
            //html.AppendLine("<h4 class='panel-title'>" + Title + "</h4>");
            html.AppendLine("</div>");
            html.AppendLine("<div class='panel-body'>");
            html.AppendLine("<div id='panel-threshold' class='panel-threshold' style='display: none'>");
            html.AppendLine("<p id='threshold-warning'>" + warning + "</p>");
            html.AppendLine("<p id='threshold-error'>" + error + "</p>");
            html.AppendLine("<p id='threshold-content'>" + content + "</p");
            html.AppendLine("</div>");
            html.AppendLine("</div>");
            html.AppendLine("<p class='panel-body-title'>");
            html.AppendLine(Title);
            html.AppendLine("<div id='" + LinkID + "' class='value'>");
            html.AppendLine("<div class='cardvalue'>");
            html.AppendLine("<p class='cardprimary-value' data-toggle='modal' data-target='#SCOMNewModal'>");
            html.AppendLine(Total.ToString());
            html.AppendLine("<h2 class='text-center'>" + Label + "</h2>");
            html.AppendLine("</p>");
            html.AppendLine("</div>");
            html.AppendLine("</div>");
            html.AppendLine("</div>");
            html.AppendLine("</div>");

            return html.ToString();
        }

        public String CreatesplitMetricCard(int Total, int Warning, int Error, string LinkID, string Title, string Label, string content, int? warningw, int? warninge, int? errorw, int? errore)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class='cardpanel panel-inverse clearfix resizable-card ui-sortable split " + content + "'>");
            html.AppendLine("<div class='panel-heading'>");
            html.AppendLine("<div class='panel-heading-btn'>");
            html.AppendLine("<a href = '#' class='btn btn-xs btn-icon btn-warning' data-click='threshold-edit-split' title='Edit Threshold' style='font-size: 0.25em'><i class='fas fa-wrench'></i></a>");
            html.AppendLine("<a href='#' class='btn btn-xs btn-icon btn-circle btn-danger' data-click='panel-remove' style='font-size: 0.25em'><i class='fa fa-times'></i></a>");
            html.AppendLine("</div>");
            //html.AppendLine("<h4 class='panel-title'>" + Title + "</h4>");
            html.AppendLine("</div>");
            html.AppendLine("<div class='panel-body'>");
            html.AppendLine("<div id='panel-threshold' class='panel-threshold' style='display: none'>");
            html.AppendLine("<p id='threshold-warning-w'>" + warningw + "</p>");
            html.AppendLine("<p id='threshold-warning-e'>" + warninge + "</p>");
            html.AppendLine("<p id='threshold-error-w'>" + errorw + "</p>");
            html.AppendLine("<p id='threshold-error-e'>" + errore + "</p>");
            html.AppendLine("<p id='threshold-content'>" + content + "</p");
            html.AppendLine("</div>");
            html.AppendLine("</div>");
            html.AppendLine("<p class='panel-body-title'>");
            html.AppendLine(Title);
            html.AppendLine("<div class='value' id='" + LinkID + "'>");
            html.AppendLine("<div class='cardvalue'>");
            html.AppendLine("<p class='cardprimary-value' data-toggle='modal' data-target='#SCOMNewModal'>");
            html.AppendLine(Total.ToString());
            html.AppendLine("<h2 class='text-center'>" + Label + "</h2>");
            html.AppendLine("</p>");
            html.AppendLine("</div>");
            html.AppendLine("<div class='cardcomparison_wrapper clearfix'>");
            html.AppendLine("<div class='cardleft_comparison'>");
            html.AppendLine("<div class='cardvalue-left'>");
            html.AppendLine("<span class='fa-layers fa-fw fa-3x' style='background: lightgrey'>");
            html.AppendLine("<i class='fas fa-exclamation-triangle' style='color: orange;'></i>");
            html.AppendLine("<span class='fa-layers-counter' style='background: black'>" + Warning.ToString() + "</span>");
            html.AppendLine("</span>");
            html.AppendLine("</div>");
            html.AppendLine("</div>");

            html.AppendLine("<div class='cardright_comparison'>");
            html.AppendLine("<div class='cardvalue-right'>");
            html.AppendLine("<span class='fa-layers fa-fw fa-3x' style='background: lightgrey'>");
            html.AppendLine("<i class='fas fa-exclamation-circle' style='color:red;'></i>");
            html.AppendLine("<span class='fa-layers-counter' style='background: black'>" + Error.ToString() + "</span>");
            html.AppendLine("</span>");
            html.AppendLine("</div>");
            html.AppendLine("</div>");
            html.AppendLine("</div>");
            html.AppendLine("</div>");
            html.AppendLine("</div>");

            return html.ToString();
        }



        public string CreateDoughnutGraph(string Title, int Bad, string div, string content)
        {

            String[] inlabels = new String[] { "Error", "Warning", "OK" };
            String[] indata = new String[] { "10", "4", "23" };



            StringBuilder labels = new StringBuilder("labels: [");
            StringBuilder data = new StringBuilder("data: [ ");
            StringBuilder colours = new StringBuilder("backgroundColor: [ ");


            foreach (var item in inlabels)
            {
                labels.Append("'" + item + "',");
            }
            foreach (var item in indata)
            {
                data.Append("'" + item + "',");
                Random rnd = new Random();
                Color randomColour = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                string hexColour = randomColour.R.ToString("X2") + randomColour.G.ToString("X2") + randomColour.B.ToString("X2");
                int r = Convert.ToInt16(randomColour.R);
                int g = Convert.ToInt16(randomColour.G);
                int b = Convert.ToInt16(randomColour.B);
                var colour = "'rgba(" + r + "," + g + "," + b + ",1.0)'";
                colours.Append(colour + ",");
            }

            labels.Length--;
            data.Length--;
            colours.Length--;
            labels.Append("],");
            data.Append("]");
            colours.Append("],");

            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class='cardpanel doughnutgraph panel-inverse ui-sortable resizable-card '>");
            html.AppendLine("<div class='panel-heading'>");
            html.AppendLine("<div class='panel-heading-btn'>");
            html.AppendLine("<a href='#' class='btn btn-xs btn-icon btn-circle btn-danger' data-click='panel-remove' style='font-size: 0.25em'><i class='fa fa-times'></i></a>");
            html.AppendLine("</div>");
            html.AppendLine("</div>");
            html.AppendLine("<div class='panel-body'>");
            html.AppendLine("<p class='panel-body-title'>");
            html.AppendLine("Panel Body Title");
            html.AppendLine("<canvas id='column_" + div + "'></canvas>");
            html.AppendLine("</div>");
            html.AppendLine("</div>");

            html.AppendLine("<script type='text/javascript'>");
            html.AppendLine("var ctx = document.getElementById('column_" + div + "');");
            html.AppendLine("var GPOPieChart = new Chart(ctx,");
            html.AppendLine("{");
            html.AppendLine("type: 'doughnut',");
            html.AppendLine("data: {");

            html.AppendLine(labels.ToString());
            html.AppendLine("datasets: [");
            html.AppendLine("{");
            html.AppendLine(colours.ToString());
            html.AppendLine("label: 'PieChart',");
            html.AppendLine(data.ToString());
            html.AppendLine("}");
            html.AppendLine("]");
            html.AppendLine("},");
            html.AppendLine("options: {");
            html.AppendLine("title: {");
            html.AppendLine("display: true,");
            html.AppendLine("responsive: true,");
            html.AppendLine("text: 'GPO Health'");
            html.AppendLine("}");
            html.AppendLine("}");
            html.AppendLine("});");

            return html.ToString();
        }

        public string CreateColumn()
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class='clearfix column-heading'>");
            html.AppendLine("<a href = '#' class='btn btn-xs btn-icon btn-danger' data-click='column-remove' title='Remove Column' style='float: right; font-size: 0.25em'><i class='fa fa-times'></i></a>");
            html.AppendLine("</div>");

            return html.ToString();
        }


        //Create Modal function. Creates the threshold modal dialog. For editing and new threshold settings.

        public string CreateModal(string metricType, string type)
        {
            if (metricType == "split")
            {
                StringBuilder html = new StringBuilder();
                html.AppendLine("<div id = 'ThresholdModal' class='modal fade'>");
                html.AppendLine("<div class='modal-dialog modal-dialog-centered' role='document'>");
                html.AppendLine("<div class='modal-content'>");
                html.AppendLine("<div class='modal-header'>");
                html.AppendLine("<h4 class='modal-title'>Set Alert Threshold</h4>");
                html.AppendLine("<button type = 'button' class='close' data-dismiss='modal' aria-label='Close'>");
                html.AppendLine("<span aria-hidden='true'>x</span>");
                html.AppendLine("</button>");
                html.AppendLine("</div>");
                html.AppendLine("<div class='modal-body'>");
                html.AppendLine("<div id='Threshold'>");
                html.AppendLine("<p>Please set the warning and error thresholds for this split metric card.</p>");
                html.AppendLine("<p>");
                html.AppendLine("<h3>Warning State</h4>");
                html.AppendLine("<p>");
                html.AppendLine("<label for='warningw'>Warning Threshold</label>");
                html.AppendLine("<input class='float-right' type='number' id='warningw' min='0' step='1' />");
                html.AppendLine("</p>");
                html.AppendLine("<p>");
                html.AppendLine("<label for='warninge'>Error Threshold</label>");
                html.AppendLine("<input class='float-right' type='number' id='warninge' min='0' step='1' />");
                html.AppendLine("<p>");
                html.AppendLine("</p>");
                html.AppendLine("<p>");
                html.AppendLine("<h3>Error State</h4>");
                html.AppendLine("<p>");
                html.AppendLine("<label for='errorw'>WarningThreshold</label>");
                html.AppendLine("<input class='float-right' type='number' id='errorw' min='0' step='1' />");
                html.AppendLine("</p>");
                html.AppendLine("<p>");
                html.AppendLine("<label for='errore'>Error Threshold</label>");
                html.AppendLine("<input class='float-right' type='number' id='errore' min='0' step='1' />");
                html.AppendLine("</p>");
                html.AppendLine("</p>");
                html.AppendLine("</div>");
                html.AppendLine("<div id='ThresholdButtons'>");
                if (type == "Edit")
                {
                    html.AppendLine("<button id='Threshold-" + metricType + "-edit' data-dismiss='modal' class='btn btn bg-success btn-lg'>OK</button>");
                }
                else
                {
                    html.AppendLine("<button id='Threshold-" + metricType + "' data-dismiss='modal' class='btn btn bg-success btn-lg'>OK</button>");
                }
                html.AppendLine("<button data-dismiss='modal' class='btn btn bg-danger btn-lg'>Cancel</button>");
                html.AppendLine("</div>");
                html.AppendLine("</div>");
                html.AppendLine("</div>");
                html.AppendLine("</div>");
                html.AppendLine("</div>");

                return html.ToString();
            }
            else
            {
                StringBuilder html = new StringBuilder();
                html.AppendLine("<div id = 'ThresholdModal' class='modal fade'>");
                html.AppendLine("<div class='modal-dialog modal-dialog-centered' role='document'>");
                html.AppendLine("<div class='modal-content'>");
                html.AppendLine("<div class='modal-header'>");
                html.AppendLine("<h4 class='modal-title'>Set Alert Threshold</h4>");
                html.AppendLine("<button type = 'button' class='close' data-dismiss='modal' aria-label='Close'>");
                html.AppendLine("<span aria-hidden='true'>x</span>");
                html.AppendLine("</button>");
                html.AppendLine("</div>");
                html.AppendLine("<div class='modal-body'>");
                html.AppendLine("<div id='Threshold'>");
                html.AppendLine("<p> Please set the warning and error threshold for this metric card.</p>");
                html.AppendLine("<p>");
                html.AppendLine("<label for='warning'>Warning Threshold</label>");
                html.AppendLine("<input class='float-right'  type='number' id='warning' min='0' step='1' />");
                html.AppendLine("</p>");
                html.AppendLine("<p>");
                html.AppendLine("<label for='error'>Error Threshold</label>");
                html.AppendLine("<input  class='float-right' type='number' id='error' min='0' step='1' />");
                html.AppendLine("</p>");
                html.AppendLine("</div>");
                html.AppendLine("<div id='ThresholdButtons'>");
                if (type == "Edit")
                {
                    html.AppendLine("<button id='Threshold-" + metricType + "-edit' data-dismiss='modal' class='btn btn bg-success btn-lg'>OK</button>");
                }
                else
                {
                    html.AppendLine("<button id='Threshold-" + metricType + "' data-dismiss='modal' class='btn btn bg-success btn-lg'>OK</button>");
                }
                html.AppendLine("<button data-dismiss='modal' class='btn btn bg-danger btn-lg'>Cancel</button>");
                html.AppendLine("</div>");
                html.AppendLine("</div>");
                html.AppendLine("</div>");
                html.AppendLine("</div>");
                html.AppendLine("</div>");

                return html.ToString();
            }



            
        }



        [OutputCache(NoStore = true, Location = System.Web.UI.OutputCacheLocation.Client, Duration = 3)]
        public async Task<PartialViewResult> GetPartial(string partialName, string sortOrder, int? page, int? pageLength)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.PageLength = pageLength;

            if (partialName == "_SCOM")
            {
                //Get new alerts and add to ViewBag for display.
                DateTime date = new DateTime(1753, 1, 1);
                List<SCOMAlert> scomalert = await db.SCOMAlert.Where(s => s.Resolved == date).ToListAsync();
                ViewBag.SCOMNew = scomalert.Count();
                var scom = await db.SCOM.ToListAsync();

                return PartialView(partialName, scom);
            }
            else if (partialName == "_SCOMAlertNew")
            {
                var viewModel = new List<SCOMNewViewModel>();
                List<SCOMAlert> scomalert = await db.SCOMAlert.ToListAsync();

                foreach (var alert in scomalert)
                {
                    viewModel.Add(new ViewModels.SCOMNewViewModel()
                    {
                        AlertHealth = alert.AlertHealth,
                        AlertName = alert.AlertName,
                        Created = alert.Created,
                        MonitoredObject = alert.MonitoredObject,
                        MonitorPath = alert.MonitorPath,
                        PrincipalName = alert.PrincipalName
                    });
                }

                if (!String.IsNullOrWhiteSpace(sortOrder))
                {
                    if (sortOrder == "name_desc")
                        viewModel = viewModel.OrderByDescending(s => s.AlertName).ToList();
                    else if (sortOrder == "date")
                        viewModel = viewModel.OrderBy(s => s.Created).ToList();
                    else if (sortOrder == "date_desc")
                        viewModel = viewModel.OrderByDescending(s => s.Created).ToList();
                    else if (sortOrder == "alerthealth")
                        viewModel = viewModel.OrderBy(s => s.AlertHealth).ToList();
                    else if (sortOrder == "alerthealth_desc")
                        viewModel = viewModel.OrderByDescending(s => s.AlertHealth).ToList();
                    else if (sortOrder == "monitorpath")
                        viewModel = viewModel.OrderBy(s => s.MonitorPath).ToList();
                    else if (sortOrder == "monitorpath_desc")
                        viewModel = viewModel.OrderByDescending(s => s.MonitorPath).ToList();
                    else if (sortOrder == "monitorobject")
                        viewModel = viewModel.OrderBy(s => s.MonitoredObject).ToList();
                    else if (sortOrder == "monitorobject_desc")
                        viewModel = viewModel.OrderByDescending(s => s.MonitoredObject).ToList();
                    else if (sortOrder == "principalname")
                        viewModel = viewModel.OrderBy(s => s.PrincipalName).ToList();
                    else if (sortOrder == "principalname_desc")
                        viewModel = viewModel.OrderByDescending(s => s.PrincipalName).ToList();
                }
                else
                {
                    viewModel = viewModel.OrderBy(s => s.AlertName).ToList();
                }

                int pageSize = pageLength ?? 5;
                int pageNumber = page ?? 1;
                return PartialView(partialName, (IPagedList)viewModel.ToPagedList(pageNumber, pageSize));
            }

            else if (partialName == "_SCOMClusters")
            {
                List<SCOM> scom = await db.SCOM.ToListAsync();

                if (!String.IsNullOrWhiteSpace(sortOrder))
                {
                    if (sortOrder == "time_desc")
                        scom = scom.OrderByDescending(s => s.Time).ToList();
                    else if (sortOrder == "time")
                        scom = scom.OrderBy(s => s.Time).ToList();
                    else if (sortOrder == "cluster")
                        scom = scom.OrderByDescending(s => s.Cluster).ToList();
                    else if (sortOrder == "cluster_desc")
                        scom = scom.OrderBy(s => s.Cluster).ToList();
                    else if (sortOrder == "server")
                        scom = scom.OrderByDescending(s => s.Server).ToList();
                    else if (sortOrder == "server_desc")
                        scom = scom.OrderBy(s => s.Server).ToList();
                    else if (sortOrder == "healthstate")
                        scom = scom.OrderByDescending(s => s.HealthState).ToList();
                    else if (sortOrder == "healthstate_desc")
                        scom = scom.OrderBy(s => s.HealthState).ToList();
                    else if (sortOrder == "inmaintenancemode")
                        scom = scom.OrderByDescending(s => s.InMaintenanceMode).ToList();
                    else if (sortOrder == "inmaintenancemode_desc")
                        scom = scom.OrderBy(s => s.InMaintenanceMode).ToList();
                    else if (sortOrder == "displayname_desc")
                        scom = scom.OrderByDescending(s => s.DisplayName).ToList();
                }
                else
                {
                    scom = scom.OrderBy(s => s.DisplayName).ToList();
                }

                int pageSize = pageLength ?? 5;
                int pageNumber = page ?? 1;
                return PartialView(partialName, (IPagedList)scom.ToPagedList(pageNumber, pageSize));
            }

            else
            {

                var viewModel = new List<SCOMResolvedViewModel>();
                List<SCOMAlert> scomalert = await db.SCOMAlert.ToListAsync();

                foreach (var alert in scomalert)
                {
                    viewModel.Add(new ViewModels.SCOMResolvedViewModel()
                    {
                        AlertName = alert.AlertName,
                        MonitorPath = alert.MonitorPath,
                        MonitoredObject = alert.MonitoredObject,
                        Resolved = alert.Resolved,
                        ResolvedBy = alert.ResolvedBy,
                        MonitoredObjectHealth = alert.MonitoredObjectHealth,
                        IsMonitorAlert = alert.IsMonitorAlert

                    });

                }
                return PartialView(partialName, viewModel);
            }
        }

        public void ParentViewBag(string message)
        {
            ViewBag.AlertName = message;
        }

        //Functions to save and load the HTML cach for a user (if exists)

        [HttpPost]
        [ValidateInput(false)]
        public void SaveHTML(string HTML)
        {
            var user = User.Identity.Name;
            var cache = db.Cache.Where(c => c.UserName == user).FirstOrDefault();

            var tempModel = new Cache
            {
                ID = cache.ID,
                UserName = cache.UserName,
                HTMLCache = HTML,
                LastLogOn = cache.LastLogOn,
                LastLogOff = cache.LastLogOff,
            };
            db.Entry(cache).CurrentValues.SetValues(tempModel);
            db.SaveChanges();
        }

        [OutputCache(NoStore = true, Location = System.Web.UI.OutputCacheLocation.Client, Duration = 3)]
        public async Task<string> LoadHTML()
        {
            var user = User.Identity.Name;
            var cache = db.Cache.Where(c => c.UserName == user).FirstOrDefault();
            var HTML = cache.HTMLCache;
            return HTML;
        }



    }
}