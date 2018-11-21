using PPB.Constant.Constants;
using PPB.DBManager.Entities;
using PPB.DBManager.Models;
using PPB.DBManager.Repositories;
using PPB.PythonService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Managers
{
    public class TaskConfigurationManager
    {
        public List<TaskConfigurationModel> GetIntArrayTaskConfigurations(TaskConfigurationModel tcm, string iaMemberType)
        {
            var tceList = new TaskConfigurationRepository().GetTaskConfigurationsLikeMemberName(tcm.AreaName, tcm.SectionName, tcm.StationName, tcm.TaskName, tcm.TaskInstance, tcm.MemberName);
            //first time to operate the int array type configuration, insert the items and retrieve again
            if (tceList.Count == 0)
            {
                AddInitialTaskConfigurations(tcm, iaMemberType);
                tceList = new TaskConfigurationRepository().GetTaskConfigurationsLikeMemberName(tcm.AreaName, tcm.SectionName, tcm.StationName, tcm.TaskName, tcm.TaskInstance, tcm.MemberName);
            }
            var tcmList = ConvertEntityToModel(tceList);
            //generate the display name
            for (var i = 0; i < tceList.Count; i++)
            {
                tcmList[i].DisplayName = tcm.DisplayName + "[" + i.ToString() + "]";
            }
            return tcmList;
        }

        public void AddInitialTaskConfigurations(TaskConfigurationModel tcm, string iaMemberType)
        {
            var iaCount = CommonManager.GetMemberTypeLength(iaMemberType);
            var memberType = CommonManager.GetMemberTypeWithoutLength(iaMemberType);
            for (var i = 0; i < iaCount; i++)
            {
                var config = new TaskConfigurationModel
                {
                    AreaName = tcm.AreaName,
                    SectionName = tcm.SectionName,
                    StationName = tcm.StationName,
                    TaskName = tcm.TaskName,
                    MemberName = tcm.MemberName + "[" + i + "]",
                    MemberValue = "0",
                    MemberType = memberType,
                    BaseTag = "Config",
                    TaskInstance = tcm.TaskInstance
                };
                new TaskConfigurationRepository().AddTaskConfiguration(config);
            }
        }

        public List<TaskConfigurationModel> GetBoolArrayTaskConfigurations(TaskConfigurationModel tcm, string iaMemberType)
        {
            var tceList = new TaskConfigurationRepository().GetTaskConfigurations(tcm.AreaName, tcm.SectionName, tcm.StationName, tcm.TaskName, tcm.TaskInstance, tcm.MemberName);
            //first time to operate the bool array type configuration, insert the items and retrieve again
            if (tceList.Count == 0)
            {
                AddInitialTaskConfigurationsForBoolArray(tcm, iaMemberType);
                tceList = new TaskConfigurationRepository().GetTaskConfigurations(tcm.AreaName, tcm.SectionName, tcm.StationName, tcm.TaskName, tcm.TaskInstance, tcm.MemberName);
            }
            var iaCount = CommonManager.GetBoolArrayMemberTypeLength(iaMemberType);
            var tcmList = new List<TaskConfigurationModel>();
            var tce = tceList.FirstOrDefault();
            var memberValue = Int32.Parse(ConvertEntityToModel(tce).MemberValue);
            var memberValue2BitArray = Convert.ToString(memberValue, 2).ToList(); //big bit --> small bit
            memberValue2BitArray.Reverse(); //reverse it to small bit --> big bit

            //Calcaulte the member value about the bool array
            for (var i = 0; i < iaCount; i++)
            {
                var tcmObj = ConvertEntityToModel(tce);
                if (memberValue != 0 && i < memberValue2BitArray.Count && memberValue2BitArray[i].ToString().Equals("1"))
                {
                    tcmObj.MemberValue = "1";
                }
                else
                {
                    tcmObj.MemberValue = "0";
                }
                tcmObj.DisplayName = tcm.DisplayName + "[" + i.ToString() + "]";
                tcmList.Add(tcmObj);
            }

            return tcmList;
        }

        public void AddInitialTaskConfigurationsForBoolArray(TaskConfigurationModel tcm, string iaMemberType)
        {
            var memberType = CommonManager.GetMemberTypeWithoutLength(iaMemberType);
            var config = new TaskConfigurationModel
            {
                AreaName = tcm.AreaName,
                SectionName = tcm.SectionName,
                StationName = tcm.StationName,
                TaskName = tcm.TaskName,
                MemberName = tcm.MemberName,
                MemberValue = "0",
                MemberType = memberType,
                BaseTag = "Config",
                TaskInstance = tcm.TaskInstance
            };
            new TaskConfigurationRepository().AddTaskConfiguration(config);
        }

        /// <summary>
        /// Int/Bool Array Type just contains one item
        /// </summary>
        /// <param name="tm"></param>
        /// <returns></returns>
        public List<TaskConfigurationModel> GetTaskConfigurationsBaseOnStructure(TaskModel tm)
        {
            var initialTcmList = GetInitialTaskConfigurations(tm);
            var tceList = new TaskConfigurationRepository().GetTaskConfigurations(tm.AreaName, tm.SectionName, tm.StationName, tm.TaskName, tm.TaskInstance);
            foreach (var tcm in initialTcmList)
            {
                if (tcm.IsIntArray)
                    continue;

                var matchedObj = tceList.FirstOrDefault(x => x.MemberName.Equals(tcm.MemberName));
                if (matchedObj == null)
                    continue;


                tcm.Id = matchedObj.Id;
                tcm.MemberValue = matchedObj.MemberValue;
            }

            return initialTcmList;
        }

        public List<TaskConfigurationModel> GetInitialTaskConfigurations(TaskModel tm)
        {
            var tcmList = new List<TaskConfigurationModel>();
            var taskNameOfStructure = string.IsNullOrWhiteSpace(tm.ModelAffiliation) ? tm.TaskName : tm.ModelAffiliation;
            var tsmList = new TaskStructureManager().GetTaskStructures(taskNameOfStructure);
            var msmList = new MemberStructureManager().GetMemberStructureList();
            var ctypeDic = new Dictionary<string, int>();
            foreach (var tsm in tsmList)
            {
                if (CommonManager.IsCommonDataType(CommonManager.GetMemberTypeWithoutLength(tsm.MemberType)))
                {
                    //deal with the common types
                    var scm = new TaskConfigurationModel
                    {
                        Id = -1,
                        AreaName = tm.AreaName,
                        SectionName = tm.SectionName,
                        StationName = tm.StationName,
                        TaskName = tm.TaskName,
                        MemberName = tm.TaskInstance >= 0 ? tsm.TaskName + "[" + tm.TaskInstance.ToString() + "]." + tsm.MemberName : tsm.TaskName + "." + tsm.MemberName,
                        MemberValue = tsm.MemberType.Equals("BOOL") ? "False" : "0",
                        MemberType = tsm.MemberType,
                        BaseTag = "Config",
                        TaskInstance = tm.TaskInstance,
                        DisplayName = string.IsNullOrWhiteSpace(tsm.MemberDescription1) ? tsm.MemberName : tsm.MemberDescription1,
                        IsIntArray = tsm.MemberType.Contains("INT[") && !tsm.MemberType.Contains("INT[BOOL("),
                        IsBoolArray = tsm.MemberType.Contains("INT[BOOL(")
                    };
                    tcmList.Add(scm);
                }
                else
                {
                    //deal with the customize types
                    if (ctypeDic.ContainsKey(tsm.MemberType))
                    {
                        ctypeDic[tsm.MemberType]++;
                    }
                    else
                    {
                        ctypeDic.Add(tsm.MemberType, 0);
                    }
                    var customizeTypeList = msmList.Where(x => x.Member.Equals(tsm.MemberType)).ToList();
                    foreach (var cType in customizeTypeList)
                    {
                        var scm = new TaskConfigurationModel
                        {
                            Id = -1,
                            AreaName = tm.AreaName,
                            SectionName = tm.SectionName,
                            StationName = tm.StationName,
                            TaskName = tm.TaskName,
                            MemberName = GenerateCustomizeMemberName(cType, ctypeDic, tsm, tm),
                            MemberValue = cType.MemberType.Equals("BOOL") ? "False" : "0",
                            MemberType = cType.MemberType,
                            BaseTag = "Config",
                            TaskInstance = tm.TaskInstance,
                            DisplayName = string.IsNullOrWhiteSpace(cType.MemberDescription1) ? cType.MemberName + ctypeDic[tsm.MemberType].ToString() : cType.MemberDescription1 + ctypeDic[tsm.MemberType].ToString(),
                            IsIntArray = cType.MemberType.Contains("INT[") && !cType.MemberType.Contains("INT[BOOL("),
                            IsBoolArray = cType.MemberType.Contains("INT[BOOL(")
                        };
                        tcmList.Add(scm);
                    }
                }
            }
            return tcmList;
        }

        private string GenerateCustomizeMemberName(MemberStructureModel msm, Dictionary<string, int> cTypeDic, TaskStructureModel tsm, TaskModel tm)
        {
            if (tm.TaskInstance >= 0)
            {
                //eg: Multi_Spindle_Task[0].SubTask_Number_Config[4].Low_Half_Byte
                var memberName = tsm.TaskName + "[" + tm.TaskInstance.ToString() + "]." + msm.Member + "[" + cTypeDic[tsm.MemberType].ToString() + "]." + msm.MemberName;
                return memberName;
            }
            else
            {
                //eg: Multi_Spindle_Task.SubTask_Number_Config[4].High_Half_Byte
                var memberName = tsm.TaskName + "." + msm.Member + "[" + cTypeDic[tsm.MemberType].ToString() + "]." + msm.MemberName;
                return memberName;
            }
        }

        public void SaveTaskConfig(List<TaskConfigurationModel> taskConfigs)
        {
            foreach (var tcm in taskConfigs)
            {
                if (tcm.Id > 0)
                {
                    EditStationConfig(tcm);
                }
                else
                {
                    AddStationConfig(tcm);
                }
            }
        }

        public void AddStationConfig(TaskConfigurationModel taskConfig)
        {
            new TaskConfigurationRepository().AddTaskConfiguration(taskConfig);
        }

        public void EditStationConfig(TaskConfigurationModel taskConfig)
        {
            new TaskConfigurationRepository().UpdateTaskConfiguration(taskConfig);
        }

        public void DownloadTaskConfig(List<TaskModel> tasks)
        {
            foreach (var task in tasks)
            {
                var fileObj = new PlcConfigurationManager().GetPythonFileObj(task);
                var taskConfigList = GetTaskConfigurations(task);

                foreach (var taskConfig in taskConfigList)
                {
                    if (!taskConfig.MemberName.Contains("."))
                        continue;
                    if (taskConfig.MemberType.Contains(DbMemberTypeConstant.MtString))
                    {
                        DownloadStringTypeMember(ref fileObj, taskConfig);
                    }
                    else
                    {
                        DownloadNotStringTypeMember(ref fileObj, taskConfig);
                    }
                }
            }
        }

        private List<TaskConfigurationEntity> GetTaskConfigurations(TaskModel task)
        {
            if (task.TaskInstance >= 0)
            {
                //task instance
                return new TaskConfigurationRepository().GetTaskConfigurations(task.AreaName, task.SectionName, task.StationName, task.TaskName, task.TaskInstance);
            }
            else
            {
                //task
                return new TaskConfigurationRepository().GetTaskConfigurations(task.AreaName, task.SectionName, task.StationName, task.TaskName);
            }
        }

        private void DownloadNotStringTypeMember(ref dynamic fileObj, TaskConfigurationEntity taskConfig)
        {
            var memberName = CommonManager.GetDownloadMemberName(taskConfig.MemberName, taskConfig.BaseTag);
            var memberValue = CommonManager.GetDownloadMemberValue(taskConfig.MemberValue);
            LogixService.DownloadTag(ref fileObj, memberName, memberValue);
        }

        private void DownloadStringTypeMember(ref dynamic fileObj, TaskConfigurationEntity taskConfig)
        {
            var memberName = CommonManager.GetDownloadMemberName(taskConfig.MemberName, taskConfig.BaseTag);
            var memberValue = CommonManager.GetDownloadMemberValue(taskConfig.MemberValue);
            var actualMemberValueLength = memberValue.Length;
            var memberLength = CommonManager.GetMemberTypeLength(taskConfig.MemberType);
            memberValue = CommonManager.SupplementMemberValue(memberValue, memberLength);
            CommonManager.DownloadStringTypeMember(ref fileObj, memberName, memberValue);
            CommonManager.DownloadStringTypeLength(ref fileObj, memberName, actualMemberValueLength);
        }

        public void UploadTaskConfig(List<TaskModel> tasks)
        {
            foreach (var task in tasks)
            {
                var fileObj = new PlcConfigurationManager().GetPythonFileObj(task);
                var taskConfigList = GetTaskConfigurations(task);

                foreach (var taskConfig in taskConfigList)
                {
                    if (!taskConfig.MemberName.Contains("."))
                        continue;
                    var memberName = CommonManager.GetDownloadMemberName(taskConfig.MemberName, taskConfig.BaseTag);
                    var memberValue = LogixService.UploadTag(ref fileObj, memberName);
                    if (!memberValue.Equals(taskConfig.MemberValue))
                    {
                        new TaskConfigurationRepository().UpdateTaskConfiguration(taskConfig.Id, memberValue);
                    }
                }
            }
        }



        #region Convertor between Entity(DB) and Model(UI)
        private List<TaskConfigurationModel> ConvertEntityToModel(List<TaskConfigurationEntity> eList)
        {
            var mList = new List<TaskConfigurationModel>();
            foreach (var e in eList)
            {
                var m = ConvertEntityToModel(e);
                mList.Add(m);
            }
            return mList;
        }

        private TaskConfigurationModel ConvertEntityToModel(TaskConfigurationEntity e)
        {
            var m = new TaskConfigurationModel
            {
                Id = e.Id,
                AreaName = e.AreaName,
                SectionName = e.SectionName,
                StationName = e.StationName,
                TaskName = e.TaskName,
                TaskInstance = e.TaskInstance,
                MemberName = e.MemberName,
                MemberValue = e.MemberValue,
                MemberType = e.MemberType,
                BaseTag = e.BaseTag
            };
            return m;
        }

        private List<TaskConfigurationEntity> ConvertModelToEntity(List<TaskConfigurationModel> mList)
        {
            var eList = new List<TaskConfigurationEntity>();
            foreach (var m in mList)
            {
                var e = ConvertModelToEntity(m);
                eList.Add(e);
            }
            return eList;
        }

        private TaskConfigurationEntity ConvertModelToEntity(TaskConfigurationModel m)
        {
            var e = new TaskConfigurationEntity
            {
                Id = m.Id,
                AreaName = m.AreaName,
                SectionName = m.SectionName,
                StationName = m.StationName,
                TaskName = m.TaskName,
                TaskInstance = m.TaskInstance,
                MemberName = m.MemberName,
                MemberValue = m.MemberValue,
                MemberType = m.MemberType,
                BaseTag = m.BaseTag
            };
            return e;
        }

        #endregion
    }
}
