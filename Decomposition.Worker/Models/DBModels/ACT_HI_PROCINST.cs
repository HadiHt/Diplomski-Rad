using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decomposition.Worker.Models.DBModels
{
    public class ACT_HI_PROCINST
    {
        public string ID_ { get; set; }
        public string PROC_INST_ID_ { get; set; }
        public string BUSINESS_KEY_ { get; set; }
        public string PROC_DEF_KEY_ { get; set; }
        public string PROC_DEF_ID_ { get; set; }
        public DateTime START_TIME_ { get; set; }
        public DateTime END_TIME_ { get; set; }
        public DateTime REMOVAL_TIME_ { get; set; }
        public double DURATION_ { get; set; }
        public string START_USER_ID_ { get; set; }
        public string START_ACT_ID_ { get; set; }
        public string END_ACT_ID_ { get; set; }
        public string SUPER_PROCESS_INSTANCE_ID_ { get; set; }
        public string ROOT_PROC_INST_ID_ { get; set; }
        public string SUPER_CASE_INSTANCE_ID_ { get; set; }
        public string CASE_INST_ID_ { get; set; }
        public string DELETE_REASON_ { get; set; }
        public string TENANT_ID_ { get; set; }
        public string STATE_ { get; set; }
    }
}
