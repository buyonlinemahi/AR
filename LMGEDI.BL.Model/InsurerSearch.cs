﻿
namespace LMGEDI.Core.BL.Model
{
    public class InsurerSearch
    {
        public int InsurerId   { get; set; }
        public string InsurerName  { get; set; }
        public string InsurerAddress { get; set; }
        public string InsurerCity { get; set; }
        public string InsurerStateName { get; set; }
        public string InsurerZip { get; set; }
        public string InsurerPhone { get; set; }
        public string DBNameInsurer { get; set; }
    }
}