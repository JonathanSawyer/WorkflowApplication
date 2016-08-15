﻿using BL;
using BL.Workflow;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateIT.Example.DalMappings
{
    public class UserBaseClassMap : ClassMap<EntityWorkflow<BL.User>>
    {
        public UserBaseClassMap()
        {
            Table("UserWorkflow");
            DiscriminateSubClassesOnColumn("Type");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.WorkflowStatus,    "Status");
            Map(x => x.MakerDateTime);
            Map(x => x.ApproverDateTime);
            Map(x => x.WorkflowType,      "Type").ReadOnly();
            References(x => x.Owner).Column("UserId");
        }

        public static void InitializeUserData(ComponentPart<UserData> partUserData)
        {
            partUserData.Map(x => x.Name, "Name");
            partUserData.Map(x => x.Surname, "Surname");
        }
    }
}
