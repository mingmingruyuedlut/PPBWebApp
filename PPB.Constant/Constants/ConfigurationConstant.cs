using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.Constant.Constants
{
    public static class ConfigurationConstant
    {
        public const string DotNumber = ".Number";
        public const string DotTask_Number = ".Task_Number";
        public const string DotTaskNumber = ".TaskNumber";

        public const string StationTaskNumber = "Station_Task_Number.Station_Task_Number";
        public const string StationTaskNumberNew = "StationConfig.Station_Task_Number";
    }

    public static class DbTableNameConstant
    { 
        public const string StationsConfiguration = "Stations_Configuration";
        public const string TasksConfiguration = "Tasks_Configuration";
    }

    public static class PlantColumnConstant
    {
        public const string Id = "ID";
        public const string PlantName = "Plant_Name";
    }

    public static class StationStructureColumnConstant
    {
        public const string Id = "ID";
        public const string Parent = "Parent";
        public const string MemberName = "MemberName";
        public const string MemberType = "MemberType";
        public const string MemberOrder = "MemberOrder";
        public const string MemberDescription_1 = "MemberDescription_1";
        public const string MemberDescription_2 = "MemberDescription_2";
        public const string MemberDescription_3 = "MemberDescription_3";
        public const string MemberValues = "MemberValues";
        public const string TaskXrefName = "TaskXrefName";
        public const string Visible = "Visible";
        public const string Global = "Global";
        public const string BASE = "BASE";
        public const string MaxLength = "MaxLength";
        public const string MinValue = "MinValue";
        public const string MaxValue = "MaxValue";
        public const string ExclusionString = "ExclusionString";
        public const string Version = "Version";
        public const string MemberGroup = "MemberGroup";
    }

    public static class StationsConfigurationColumnConstant
    {
        public const string Id = "ID";
        public const string AreaName = "Area_Name";
        public const string SectionName = "Section_Name";
        public const string StationName = "Station_Name";
        public const string MemberName = "Member_Name";
        public const string MemberValue = "Member_Value";
        public const string MemberType = "Member_type";
        public const string BaseTag = "Base_Tag";
        public const string StationInstance = "Station_Instance";
    }

    public static class StationsColumnConstant
    {
        public const string Id = "ID";
        public const string AreaName = "Area_Name";
        public const string SectionName = "Section_Name";
        public const string StationName = "Station_Name";
        public const string StationType = "Station_Type";
        public const string PlcType = "PLC_Type";
        public const string MasterFileName = "MasterFile_Name";
        public const string MasterFileRevision = "MasterFile_Revision";
        public const string ModelAffiliation = "ModelAffiliation";
        public const string Accept = "Accept";
        public const string MaxNoOfInstances = "MaxNoOfInstances";
    }

    public static class TaskStructureColumnConstant
    {
        public const string Id = "ID";
        public const string TaskName = "TaskName";
        public const string MemberName = "MemberName";
        public const string MemberType = "MemberType";
        public const string MemberOrder = "MemberOrder";
        public const string MemberDescription_1 = "MemberDescription_1";
        public const string MemberDescription_2 = "MemberDescription_2";
        public const string MemberDescription_3 = "MemberDescription_3";
        public const string MemberValues = "MemberValues";
        public const string TaskXrefName = "TaskXrefName";
        public const string Visible = "Visible";
        public const string Global = "Global";
        public const string BASE = "BASE";
        public const string MaxLength = "MaxLength";
        public const string MinValue = "MinValue";
        public const string MaxValue = "MaxValue";
        public const string ExclusionString = "ExclusionString";
        public const string Version = "Version";
    }

    public static class TasksConfigurationColumnConstant
    {
        public const string Id = "ID";
        public const string AreaName = "Area_Name";
        public const string SectionName = "Section_Name";
        public const string StationName = "Station_Name";
        public const string TaskName = "Task_Name";
        public const string TaskInstance = "Task_Instance";
        public const string MemberName = "Member_Name";
        public const string MemberValue = "Member_Value";
        public const string MemberType = "Member_type";
        public const string BaseTag = "Base_Tag";
    }

    public static class TasksColumnConstant
    {
        public const string Id = "ID";
        public const string AreaName = "Area_Name";
        public const string SectionName = "Section_Name";
        public const string StationName = "Station_Name";
        public const string TaskName = "Task_Name";
        public const string MasterFileName = "MasterFile_Name";
        public const string ModelAffiliation = "ModelAffiliation";
        public const string TaskMemory = "Task_Memory";
        public const string TaskMemoryPlus = "Task_MemoryPLUS";
        public const string TaskNodes = "Task_Nodes";
        public const string TaskConnection = "Task_Connection";
        public const string MaxNoOfInstances = "MaxNoOfInstances";
    }

    public static class MemberStructureColumnConstant
    {
        public const string Id = "ID";
        public const string Member = "Member";
        public const string MemberName = "MemberName";
        public const string MemberType = "MemberType";
        public const string MemberOrder = "MemberOrder";
        public const string MemberDescription_1 = "MemberDescription_1";
        public const string MemberDescription_2 = "MemberDescription_2";
        public const string MemberDescription_3 = "MemberDescription_3";
        public const string MemberValues = "MemberValues";
        public const string TaskXrefName = "TaskXrefName";
        public const string Visible = "Visible";
        public const string Global = "Global";
        public const string BASE = "BASE";
        public const string MaxLength = "MaxLength";
        public const string MinValue = "MinValue";
        public const string MaxValue = "MaxValue";
        public const string ExclusionString = "ExclusionString";
        public const string Version = "Version";
    }

    public static class MasterFileColumnConstant
    {
        public const string MasterFileName = "MasterFile_Name";
        public const string MasterFileRevision = "MasterFile_Revision";
        public const string MasterFileApplicationNotes = "MasterFile_Application_Notes";
        public const string OverheadMemoryBase = "Overhead_Memory_Base";
        public const string OverheadMemoryPlus = "Overhead_Memory_PLUS";
    }

    public static class MasterFilesTaskColumnConstant
    {
        public const string MasterFileName = "MasterFile_Name";
        public const string TaskName = "Task_Name";
        public const string MemoryUsed = "Memory_Used";
        public const string Version = "Version";
        public const string MultiStation = "Multi-Station";
        public const string MaxNoOfInstances = "MaxNoOfInstances";
        public const string ModelAffiliation = "ModelAffiliation";
        public const string L5XFileName = "L5XFileName";
    }

    public static class ModelConfigurationColumnConstant
    {
        public const string AreaName = "Area_Name";
        public const string SectionName = "Section_Name";
        public const string StationName = "Station_Name";
        public const string TaskName = "Task_Name";
        public const string TaskInstance = "Task_Instance";
        public const string MemberName = "Member_Name";
        public const string MemberValue = "Member_Value";
        public const string MemberType = "Member_type";
        public const string ModelInstance = "Model_Instance";
    }

    public static class PlcsConfigurationColumnConstant
    {
        public const string Id = "ID";
        public const string AreaName = "Area_Name";
        public const string SectionName = "Section_Name";
        public const string StationName = "Station_Name";
        public const string PlcName = "PLC_Name";
        public const string MemberName = "Member_Name";
        public const string MemberValue = "Member_Value";

        public const string Address1 = "PLC.IP Address[1]";
        public const string Address2 = "PLC.IP Address[2]";
        public const string Address3 = "PLC.IP Address[3]";
        public const string Address4 = "PLC.IP Address[4]";
        public const string Slot = "PLC.PLC Slot";
    }

    public static class PlcInformationColumnConstant
    {
        public const string ProcessorType = "Processor_Type";
        public const string ApplicationNotes = "Application_Notes";
        public const string TotalBytesAvailable = "Total_Bytes_Available";
        public const string MaxNodes = "Max_Nodes";
        public const string MaxConnections = "Max_Connections";
    }

    public static class AreasColumnConstant
    {
        public const string Id = "ID";
        public const string AreaName = "Area_Name";
    }

    public static class AreasConfigurationColumnConstant
    {
        public const string Id = "ID";
        public const string AreaName = "Area_Name";
        public const string ModelNumber = "Model_Number";
        public const string ModelName = "Model_Name";
    }

    public static class MemoryUsageColumnConstant
{
    public const string AreaName = "Area_Name";
        public const string SectionName = "Section_Name";
        public const string StationName = "Station_Name";
        public const string PlcType = "PLCType";
        public const string TotalMem = "Total_Mem";
        public const string TotalMemRsvd = "Total_Mem_Rsvd";
        public const string MemAvailable = "Mem_Available";
        public const string MemUsed = "Mem_Used";
        public const string PercentAvailable = "Percent_Available";
        public const string PercentUsed = "Percent_Used";
    }

    public static class SectionConfigurationColumnConstant
    {
        public const string AreaName = "Area_Name";
        public const string SectionName = "Section_Name";
        public const string MemberName = "Member_Name";
        public const string MemberValue = "Member_Value";
    }

    public static class AreaStructureConstant
    {
        public const string ModelID = "ModelID";
    }

    public static class OrderChangeConstant
    {
        public const string Show = "Save is OK";
        public const string Station = "STATION";
        public const string StationConfig = "StationConfig";
        public const string Space = "-------";
    }

    public static class StructureConstant
    {
        public const string Space = "SPACE";
        public const string Parent = "Parent";
    }

    public static class Form2Constant
    {
        public const string OrderChange = "Order Change";
        public const string AcceptStation = "Accept Station";
        public const string Config = "Config";
        public const string Dot = ".";
        public const string LogixService = @".\Services\LogixService.py";
        public const string True1 = "True";
        public const string False1 = "False";
    }

    public static class TreeNodeTagConstant
    {
        public const string PLANT = "PLANT";
        public const string AREA = "AREA";
        public const string SECTION = "SECTION";
        public const string STATION = "STATION";
        public const string TASK = "TASK";
        public const string TASKInstancePrefix = "TASK|";
        public const string MASTERFILE = "MASTERFILE";
        public const string MASTERFILEATTRIBUTE = "MASTERFILEATTRIBUTE";
        public const string PLC = "PLC";
        public const string PLCAttribute = "PLCAttribute";
    }

    public static class TreeNodeImageIndexConstant
    {
        public const int Plant = 0;
        public const int Area = 1;
        public const int Section = 2;
        public const int Station = 3;
        public const int Task = 5;
        public const int MasterFileSub = 10;
        public const int MasterFile = 12;
        public const int MasterFileAttribute = 16;
        public const int Plc = 15;
        public const int PlcAttribute = 16;

        public const int InvalidNode = 17;
        public const int ValidNode = 19;
        public const int NotFoundNode = 18;
        public const int NotFoundValidationFieldNode = 9;
        public const int ManualAcceptNode = 20;
    }

    public static class StationTypeConstant
    {
        public const string Auto = "Auto";
        public const string Manual = "Manual";
    }

    public static class PlcMemoryGridViewHeaderConstant
    {
        public const string No = "No.";
        public const string AreaName = "Area Name";
        public const string SectionName = "Section Name";
        public const string StationName = "Station Name";
        public const string StationType = "Station Type";
        public const string PlcType = "PLC Type";
        public const string TotalPlcMem = "Total PLC Mem";
        public const string TotalRsvdMem = "Total Rsvd Mem";
        public const string MemAvailable = "Mem Available";
        public const string MemUsed = "Mem Used";
    }

    public static class PlcMemoryGdvColumnNameConstant
    {
        public const string No = "No";
        public const string AreaName = "AreaName";
        public const string SectionName = "SectionName";
        public const string StationName = "StationName";
        public const string StationType = "StationType";
        public const string PlcType = "PLCType";
        public const string TotalPlcMem = "TotalPLCMem";
        public const string TotalRsvdMem = "TotalRsvdMem";
        public const string MemAvailable = "MemAvailable";
        public const string MemUsed = "MemUsed";
    }
}
