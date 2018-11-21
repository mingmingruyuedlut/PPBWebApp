using Newtonsoft.Json;
using PPB.DBManager.Managers;
using PPB.DBManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPB.Controllers
{
    public class ProjectFileController : Controller
    {
        // GET: ProjectMgmt
        public ActionResult Index()
        {
            return View();
        }

        #region Area Configuration

        public ActionResult AreaConfig()
        {
            var areaSummary = new AreasManager().GetAreaSummary();
            return View(areaSummary);
        }
        public PartialViewResult ReloadAreaConfigDataTable(string AreaJsonStr)
        {
            var am = JsonConvert.DeserializeObject<AreaModel>(AreaJsonStr);
            var areaConfigList = new AreaConfigurationManager().GetAreaConfigurations(am);
            return PartialView("_AreaConfigDataTablePartial", areaConfigList);
        }

        public PartialViewResult ReloadAreaConfig(string AreaJsonStr)
        {
            var am = JsonConvert.DeserializeObject<AreaModel>(AreaJsonStr);
            var areaConfigList = new AreaConfigurationManager().GetAreaConfigurations(am);
            return PartialView("_AreaConfigPartial", areaConfigList);
        }

        public JsonResult SaveAreaConfig(string AreaConfigJsonStr)
        {
            var areaConfig = JsonConvert.DeserializeObject<AreaConfigurationModel>(AreaConfigJsonStr);
            new AreaConfigurationManager().SaveAreaConfig(areaConfig);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteAreaConfig(string AreaConfigJsonStr)
        {
            var areaConfig = JsonConvert.DeserializeObject<AreaConfigurationModel>(AreaConfigJsonStr);
            new AreaConfigurationManager().DeleteAreaConfig(areaConfig);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAreaConfigNextModelNumber()
        {
            var nextModelNumber = new AreaConfigurationManager().GetAreaConfigNextModelNumber();
            return Json(nextModelNumber, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Station Configuration

        public ActionResult StationConfig()
        {
            var sSummary = new StationManager().GetStationSummary();
            return View(sSummary);
        }
        public PartialViewResult ReloadStationConfigDataTable()
        {
            var smList = new StationManager().GetStations();
            return PartialView("_StationConfigDataTablePartial", smList);
        }

        public PartialViewResult ReloadStationConfig(string StationJsonStr)
        {
            var sm = JsonConvert.DeserializeObject<StationModel>(StationJsonStr);
            var stationConfigList = new StationConfigurationManager().GetStationConfigurationsBaseOnStructure(sm);
            return PartialView("_StationConfigPartial", stationConfigList);
        }

        public JsonResult SaveStationConfig(string StationConfigJsonStr)
        {
            var stationConfigs = JsonConvert.DeserializeObject<List<StationConfigurationModel>>(StationConfigJsonStr);
            new StationConfigurationManager().SaveStationConfig(stationConfigs);
            return Json("success", JsonRequestBehavior.AllowGet);
        }


        public PartialViewResult ReloadIntArrayStationConfig(string StationConfigJsonStr, string MemberType)
        {
            var scm = JsonConvert.DeserializeObject<StationConfigurationModel>(StationConfigJsonStr);
            var stationConfigList = new StationConfigurationManager().GetIntArrayStationConfigurations(scm, MemberType);
            return PartialView("_StationConfigIntArrayPartial", stationConfigList);
        }

        public JsonResult DownloadStationConfig(string StationJsonStr)
        {
            var stations = JsonConvert.DeserializeObject<List<StationModel>>(StationJsonStr);
            new StationConfigurationManager().DownloadStationConfig(stations);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public JsonResult UploadStationConfig(string stationJsonStr)
        {
            var stations = JsonConvert.DeserializeObject<List<StationModel>>(stationJsonStr);
            new StationConfigurationManager().UploadStationConfig(stations);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region PLC Configuration

        public ActionResult PlcConfig()
        {
            var plcConfigSummary = new PlcConfigurationManager().GetPlcConfigurationSummary();
            return View(plcConfigSummary);
        }

        public PartialViewResult ReloadPlcConfigDataTable()
        {
            var plcHierList = new PlcConfigurationManager().GetPlcHierList();
            return PartialView("_PlcConfigDataTablePartial", plcHierList);
        }

        public PartialViewResult ReloadPlcConfig(string PlcHierJsonStr)
        {
            var plcHier = JsonConvert.DeserializeObject<PlcHierarchy>(PlcHierJsonStr);
            var plcConfigList = new PlcConfigurationManager().GetPlcConfigurations(plcHier);
            if (plcConfigList.Count() == 0)
            {
                plcConfigList = new PlcConfigurationManager().GetInitialPlcConfigurations(plcHier);
            }
            return PartialView("_PlcConfigPartial", plcConfigList);
        }

        public JsonResult SavePlcConfig(string PlcConfigJsonStr)
        {
            var plcConfigs = JsonConvert.DeserializeObject<List<PlcConfigurationModel>>(PlcConfigJsonStr);
            new PlcConfigurationManager().SavePlcConfig(plcConfigs);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Task Configuration

        public ActionResult TaskConfig()
        {
            var taskSummary = new TaskManager().GetTaskSummary();
            return View(taskSummary);
        }

        public PartialViewResult ReloadTaskInstancesDataTable(string taskJsonStr)
        {
            var tm = JsonConvert.DeserializeObject<TaskModel>(taskJsonStr);
            var taskInstanceList = new TaskManager().GetTaskInstances(tm);
            return PartialView("_TaskInstancesDataTablePartial", taskInstanceList);
        }

        public PartialViewResult ReloadTaskConfig(string taskJsonStr)
        {
            var tm = JsonConvert.DeserializeObject<TaskModel>(taskJsonStr);
            var taskConfigList = new TaskConfigurationManager().GetTaskConfigurationsBaseOnStructure(tm);
            return PartialView("_TaskConfigPartial", taskConfigList);
        }

        public PartialViewResult ReloadIntArrayTaskConfig(string taskConfigJsonStr, string memberType)
        {
            var tcm = JsonConvert.DeserializeObject<TaskConfigurationModel>(taskConfigJsonStr);
            var taskConfigList = new TaskConfigurationManager().GetIntArrayTaskConfigurations(tcm, memberType);
            return PartialView("_TaskConfigIntArrayPartial", taskConfigList);
        }

        public JsonResult SaveTaskConfig(string taskConfigJsonStr)
        {
            var taskConfigs = JsonConvert.DeserializeObject<List<TaskConfigurationModel>>(taskConfigJsonStr);
            new TaskConfigurationManager().SaveTaskConfig(taskConfigs);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ReloadBoolArrayTaskConfig(string taskConfigJsonStr, string memberType)
        {
            var tcm = JsonConvert.DeserializeObject<TaskConfigurationModel>(taskConfigJsonStr);
            var taskConfigList = new TaskConfigurationManager().GetBoolArrayTaskConfigurations(tcm, memberType);
            return PartialView("_TaskConfigBoolArrayPartial", taskConfigList);
        }

        public JsonResult DownloadTaskConfig(string taskJsonStr)
        {
            var tasks = JsonConvert.DeserializeObject<List<TaskModel>>(taskJsonStr);
            new TaskConfigurationManager().DownloadTaskConfig(tasks);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public JsonResult UploadTaskConfig(string taskJsonStr)
        {
            var tasks = JsonConvert.DeserializeObject<List<TaskModel>>(taskJsonStr);
            new TaskConfigurationManager().UploadTaskConfig(tasks);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Model Configuration

        public ActionResult ModelConfig()
        {
            return View();
        }

        #endregion


        public ActionResult PfMgmt()
        {
            var tv = new TreeViewManager().GenerateProjectFileTreeView();
            return View(tv);
        }
    }
}